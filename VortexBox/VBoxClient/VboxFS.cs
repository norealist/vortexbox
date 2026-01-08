using System.Formats.Tar;
using System.IO;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;
using Windows.UI.ViewManagement;

namespace VBoxClient;

internal class VboxFS
{
    //private const ushort KEY_LENGTH = 32;

    private static readonly byte[] Magic = Encoding.ASCII.GetBytes("VBOXAES1");
    private static readonly int Version = 1;
    private static readonly int SaltLength = 16;
    private static readonly int NonceLength = 12;
    private static readonly int TagLength = 16;

    private static readonly int PACKET_SIZE = 1024 * 1024 * 32;
    private static readonly string DICT = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
    private static readonly byte[] MagicChunk = Encoding.UTF8.GetBytes("VBOXCHNK");


    public static void EncryptFile(string inputPath)
    {
        if (!File.Exists(inputPath)) throw new FileNotFoundException("Файл не найден", inputPath);

        byte[] plaintext = File.ReadAllBytes(inputPath);

        if (plaintext.Length > 100 * 1024 * 1024)
            EncryptBigFile(inputPath);
        else
        {

            byte[] salt = RandomNumberGenerator.GetBytes(16);
            byte[] nonce = RandomNumberGenerator.GetBytes(12);
            byte[] key = DeriveKey(Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)), salt);

            byte[] ciphertext = new byte[plaintext.Length];
            byte[] tag = new byte[16];

            using (var aesGcm = new AesGcm(key))
            {
                aesGcm.Encrypt(nonce, plaintext, ciphertext, tag, associatedData: null);
            }

            if (!Directory.Exists("temp"))
                Directory.CreateDirectory("temp");

            string outputPath = "temp/" + Path.GetFileName(inputPath + ".enc");
            using (var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fs.Write(Magic, 0, Magic.Length);
                fs.WriteByte(1); // версия формата
                fs.Write(salt, 0, salt.Length);
                fs.Write(nonce, 0, nonce.Length);
                fs.Write(tag, 0, tag.Length);
                fs.Write(ciphertext, 0, ciphertext.Length);
            }

            writeToMap(Path.GetFileName(outputPath), Convert.ToBase64String(key));

            // очистка ключа из памяти
            CryptographicOperations.ZeroMemory(key);
        }
    }

    public static void DecryptFile(string inputPath, string outputPath)
    {
        if (!File.Exists(inputPath)) throw new FileNotFoundException("Файл не найден", inputPath);


        byte[] file = File.ReadAllBytes(inputPath);
        int offset = 0;

        // проверка magic
        if (file.Length < Magic.Length + 1 + 16 + 12 + 16)
            throw new InvalidDataException("File too short or not a valid encrypted file.");

        for (int i = 0; i < Magic.Length; i++)
        {
            if (file[offset + i] != Magic[i])
                return;
        }
        offset += Magic.Length;

        byte version = file[offset++];
        if (version != 1)
            throw new NotSupportedException($"Не поддерживаемая версия: {version}");

        byte[] salt = new byte[16];
        Array.Copy(file, offset, salt, 0, salt.Length);
        offset += salt.Length;

        byte[] nonce = new byte[12];
        Array.Copy(file, offset, nonce, 0, nonce.Length);
        offset += nonce.Length;

        byte[] tag = new byte[16];
        Array.Copy(file, offset, tag, 0, tag.Length);
        offset += tag.Length;

        int cipherLen = file.Length - offset;
        if (cipherLen < 0) throw new InvalidDataException("Недопустимая длина зашифрованного текста");

        byte[] ciphertext = new byte[cipherLen];
        Array.Copy(file, offset, ciphertext, 0, cipherLen);

        Dictionary<string, string> map = readMap();

        string filename = Path.GetFileName(inputPath);
        if (!map.TryGetValue(filename, out string storedBase64Key))
            throw new KeyNotFoundException($"Ключ для файла '{filename}' не найден в map.dat");

        // ключ в map.dat уже хранится как base64 от реального AES-ключа — декодируем и используем напрямую
        byte[] key = Convert.FromBase64String(storedBase64Key);
        byte[] plaintext = new byte[ciphertext.Length];

        try
        {
            using (var aesGcm = new AesGcm(key))
            {
                aesGcm.Decrypt(nonce, ciphertext, tag, plaintext, associatedData: null);
            }
        }
        catch (CryptographicException)
        {
            throw new CryptographicException("Не удалось расшифровать файл. Не верный ключ или повреждён файл");
        }

        File.WriteAllBytes(outputPath+"/"+Path.GetFileName(inputPath)[..^4], plaintext);

        CryptographicOperations.ZeroMemory(key);
        CryptographicOperations.ZeroMemory(plaintext);

        File.Delete(inputPath);
    }

    public static void EncryptBigFile(string inputPath)
    {
        using BinaryReader br = new(File.Open(inputPath, FileMode.Open));
        long fLength = br.BaseStream.Length;
        long packetCount = fLength / PACKET_SIZE;
        int remainderBytes = (int)(fLength % PACKET_SIZE);

        byte[] magic = Encoding.UTF8.GetBytes("VBOXCHNK");

        if (!Directory.Exists("temp"))
            Directory.CreateDirectory("temp");
            Directory.CreateDirectory("temp/chunks");
            //Directory.CreateDirectory("temp/encChunks");

        long b = 0;
        for (long i = 0; i < packetCount; i++)
        {
            using (var fs = new BinaryWriter(File.Open($"temp/chunks/{randomFileName(32)}", FileMode.Create)))
            {
                fs.Write(magic);
                fs.Write((uint)i);
                fs.Write(br.ReadBytes(PACKET_SIZE));
                b += 1;
                //Console.WriteLine($"CHUNK {i} done");
            }

        }
        using (var fs = new BinaryWriter(File.Open($"temp/chunks/{randomFileName(32)}", FileMode.Create)))
        {
            fs.Write(magic);
            fs.Write((uint)b);
            fs.Write(br.ReadBytes(PACKET_SIZE));

            //Console.WriteLine($"REMS CHUNK done");
        }


        byte[] salt = RandomNumberGenerator.GetBytes(16);
        byte[] nonce = RandomNumberGenerator.GetBytes(12);
        byte[] key = DeriveKey(Convert.ToBase64String(RandomNumberGenerator.GetBytes(32)), salt);
        byte[] tag = new byte[16];

        foreach (string chunkFile in Directory.GetFiles("temp/chunks/"))
        {
            byte[] plaintext = File.ReadAllBytes($"{chunkFile}");
            byte[] ciphertext = new byte[plaintext.Length];

            using (var aesGcm = new AesGcm(key))
            {
                aesGcm.Encrypt(nonce, plaintext, ciphertext, tag, associatedData: null);
            }

            string outputPath = "temp/chunks/" + Path.GetFileName(chunkFile + ".enc");
            using (var fs = new FileStream(outputPath, FileMode.Create, FileAccess.Write, FileShare.None))
            {
                fs.Write(Magic, 0, Magic.Length);
                fs.WriteByte(1); // версия формата
                fs.Write(salt, 0, salt.Length);
                fs.Write(nonce, 0, nonce.Length);
                fs.Write(tag, 0, tag.Length);
                fs.Write(ciphertext, 0, ciphertext.Length);
            }
            File.Delete("temp/chunks/" + Path.GetFileName(chunkFile));
        }
        TarFile.CreateFromDirectory("temp/chunks/", "temp/"+Path.GetFileName(inputPath)+".tar.enc", includeBaseDirectory: false);
        writeToMap(Path.GetFileName(inputPath+".tar.enc"), Convert.ToBase64String(key));

        Directory.Delete("temp/chunks/", recursive: true);

        // очистка ключа из памяти
        CryptographicOperations.ZeroMemory(key);
    }

    public static void DecryptBigFile(string inputTarFile, string outFile)
    {
        var inputFullPath = Path.GetFullPath(inputTarFile);
        var inputDir = Path.GetDirectoryName(inputFullPath) ?? Directory.GetCurrentDirectory();
        string chunksPath = Path.Combine(inputDir, Path.GetFileName(inputTarFile) + ".chunks");
        Directory.CreateDirectory(chunksPath);
        TarFile.ExtractToDirectory(inputTarFile, chunksPath, overwriteFiles: false);

        //================ расшифровка ================

        foreach (string chunk in Directory.GetFiles(chunksPath))
        {
            // Directory.GetFiles returns full paths, so use it directly
            string chunkName = chunk;

            byte[] file = File.ReadAllBytes(chunkName);
            int offset = 0;

            // проверка magic
            if (file.Length < Magic.Length + 1 + 16 + 12 + 16)
                throw new InvalidDataException("File too short or not a valid encrypted file.");

            for (int i = 0; i < Magic.Length; i++)
            {
                if (file[offset + i] != Magic[i])
                    return;
            }
            offset += Magic.Length;

            byte version = file[offset++];
            if (version != 1)
                throw new NotSupportedException($"Не поддерживаемая версия: {version}");

            byte[] salt = new byte[16];
            Array.Copy(file, offset, salt, 0, salt.Length);
            offset += salt.Length;

            byte[] nonce = new byte[12];
            Array.Copy(file, offset, nonce, 0, nonce.Length);
            offset += nonce.Length;

            byte[] tag = new byte[16];
            Array.Copy(file, offset, tag, 0, tag.Length);
            offset += tag.Length;

            int cipherLen = file.Length - offset;
            if (cipherLen < 0) throw new InvalidDataException("Недопустимая длина зашифрованного текста");

            byte[] ciphertext = new byte[cipherLen];
            Array.Copy(file, offset, ciphertext, 0, cipherLen);

            Dictionary<string, string> map = readMap();

            string filename = Path.GetFileName(inputTarFile);
            if (!map.TryGetValue(filename, out string storedBase64Key))
                throw new KeyNotFoundException($"Ключ для файла '{filename}' не найден в map.dat");

            // ключ в map.dat уже хранится как base64 от реального AES-ключа — декодируем и используем напрямую
            byte[] key = Convert.FromBase64String(storedBase64Key);
            byte[] plaintext = new byte[ciphertext.Length];

            try
            {
                using (var aesGcm = new AesGcm(key))
                {
                    aesGcm.Decrypt(nonce, ciphertext, tag, plaintext, associatedData: null);
                }
            }
            catch (CryptographicException)
            {
                throw new CryptographicException("Не удалось расшифровать файл. Не верный ключ или повреждён файл");
            }

            File.WriteAllBytes(chunkName[..^4], plaintext);
            File.Delete(chunkName);

            CryptographicOperations.ZeroMemory(key);
            CryptographicOperations.ZeroMemory(plaintext);
        }
        //=============================================

        var files = Directory.GetFiles(chunksPath);
        if (files.Length == 0) throw new InvalidOperationException("Нет файлов в каталоге " + chunksPath);

        if (Directory.Exists(outFile))
        {
            var baseName = Path.GetFileName(inputTarFile);
            if (baseName != null && baseName.EndsWith(".tar.enc", StringComparison.OrdinalIgnoreCase))
            {
                baseName = baseName[..^".tar.enc".Length];
            }
            outFile = Path.Combine(outFile, baseName);
        }

        var outDir = Path.GetDirectoryName(outFile) ?? Directory.GetCurrentDirectory();
        Directory.CreateDirectory(outDir);

        var entries = new List<(string Path, long? Index, long DataOffset)>();

        foreach (var path in files)
        {
            using var fs = File.OpenRead(path);
            using var br = new BinaryReader(fs, Encoding.UTF8, leaveOpen: true);

            var hdr = br.ReadBytes(Magic.Length);
            if (hdr.Length != MagicChunk.Length || !hdr.SequenceEqual(MagicChunk))
                throw new InvalidDataException($"Magic mismatch в файле {Path.GetFileName(path)}");

            long remaining = fs.Length - fs.Position;
            long? index = null;
            if (remaining >= 4)
            {
                index = br.ReadUInt32();
            }

            long dataOffset = fs.Position;
            entries.Add((path, index, dataOffset));
        }

        var indexed = entries.Where(e => e.Index != null).ToList();

        long maxIndex = indexed.Any() ? indexed.Max(e => e.Index.Value) : -1;

        // Проверка на дубликаты и пропуски: ожидаем индексы 0..N-1
        var ordered = entries.OrderBy(e => e.Index).ToList();
        for (long i = 0; i < ordered.Count; i++)
        {
            if (ordered[(int)i].Index != i)
                throw new InvalidDataException($"Непрерывность индексов нарушена: ожидался {i}, найден {ordered[(int)i].Index} (файл {Path.GetFileName(ordered[(int)i].Path)})");
        }

        // Объединяем данные (копируем из позиции DataOffset до конца файла)
        using var outFs = File.Create(outFile);
        foreach (var e in ordered)
        {
            using var inFs = File.OpenRead(e.Path);
            inFs.Position = e.DataOffset;
            inFs.CopyTo(outFs);
        }

        File.Delete(inputTarFile);
        Directory.Delete(chunksPath, recursive: true);
    }

    private static byte[] DeriveKey(string password, byte[] salt, int keyBytes = 32, int iter = 200_000)
    {
        using var pbkdf2 = new Rfc2898DeriveBytes(password, salt, iter, HashAlgorithmName.SHA256);
        return pbkdf2.GetBytes(keyBytes);
    }

    private static void writeToMap(string file, string key)
    {
        var json = File.ReadAllText("map.dat");
        var map = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();

        map[file] = key;

        string updatedJson = JsonSerializer.Serialize(map, new JsonSerializerOptions { WriteIndented = true });
        File.WriteAllText("map.dat", updatedJson);
    }

    private static Dictionary<string, string> readMap()
    {
        var json = File.ReadAllText("map.dat");
        var map = JsonSerializer.Deserialize<Dictionary<string, string>>(json) ?? new Dictionary<string, string>();
        
        return map;
    }

    private static string randomFileName(int length)
    {
        string fName = "";
        for (int j = 0; j < length; j++)
        {
            Random rand = new();
            fName += DICT[rand.Next(0, DICT.Length)];
        }

        return fName;
    }

    #region download/upload file
    public async static Task downloadFile(string sessionID, string addrServer, string file, string pathToSave)
    {
        using HttpClient client = new();

        using Stream stream = await client.GetStreamAsync($"{addrServer}/download/{file}?session_id={sessionID}");
        using FileStream fs = File.Create(pathToSave + "/" + file);

        await stream.CopyToAsync(fs);
    }

    public async static Task<string> uploadFile(string sessionID, string addrServer, string pathToFile)
    {
        if (string.IsNullOrEmpty(pathToFile) || !File.Exists(pathToFile)) return "File not found";

        var (response, responseBody, jsonNode) = await VBoxRequests.sendMultipartPostRequest(addrServer, sessionID, pathToFile);

        if (response == null) return "Request failed";

        // Попытка извлечь поле "status" из JSON-ответа
        if (jsonNode != null && jsonNode is JsonObject obj && obj.TryGetPropertyValue("status", out JsonNode? statusNode))
        {
            return statusNode?.ToString() ?? responseBody;
        }

        // Если не удалось распарсить JSON — вернуть текст ответа или код ошибки
        if (!response.IsSuccessStatusCode)
        {
            return $"HTTP {(int)response.StatusCode}: {responseBody}";
        }

        return responseBody;
    }
    #endregion
}
