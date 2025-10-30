namespace VBoxClient;

internal class VboxFS
{
    public async static Task downloadFile(string sessionID, string addrServer, string file, string pathToSave)
    {
        using HttpClient client = new();

        using Stream stream = await client.GetStreamAsync($"{addrServer}/download/{file}?session_id={sessionID}");
        using FileStream fs = File.Create(pathToSave+"/"+file);

        await stream.CopyToAsync(fs);
    }

    public async static void uploadFile(string addrServer, string file)
    {

    }
}
