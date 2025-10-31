using System.Text.Json.Nodes;

namespace VBoxClient;

internal class VboxFS
{
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
}
