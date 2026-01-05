using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace VBoxClient;

internal class VboxFS
{
    //private const ushort KEY_LENGTH = 32;

    private static readonly byte[] Magic = Encoding.ASCII.GetBytes("VBOXAES1");
    private static readonly int Version = 1;
    private static readonly int SaltLength = 16;
    private static readonly int NonceLength = 12;
    private static readonly int TagLength = 16;

    public static void EncryptFile(string inputPath)
    {
        if (!File.Exists(inputPath)) throw new FileNotFoundException("Файл не найден", inputPath);

        byte[] plaintext = File.ReadAllBytes(inputPath);

        if (plaintext.Length > 100 * 1024 * 1024)
            return; // ограничение на 100 МБ

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

        string outputPath = "temp/" + Path.GetFileName(inputPath+".enc");
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

    private static byte[] DeriveKey(string password, byte[] salt, int keyBytes = 32, int iter = 200_000)
    {
        // PBKDF2-HMACSHA256
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
