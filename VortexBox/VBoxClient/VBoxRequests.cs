using Newtonsoft.Json;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json.Nodes;

namespace VBoxClient;

internal class VBoxRequests
{
    public static async Task<(HttpResponseMessage, string, JsonNode)> sendPostRequest(string addrServer, dynamic postData, string endPoint)
    {
        try
        {
            using HttpClient client = new();
            string jsonPostData = JsonConvert.SerializeObject(postData);
            StringContent httpContent = new StringContent(jsonPostData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(addrServer + endPoint, httpContent);
            string responseBody = await response.Content.ReadAsStringAsync();
            JsonNode jsonResponseBody = JsonNode.Parse(responseBody);

            return (response, responseBody, jsonResponseBody);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return (null, null, null);
        }
    }

    public static async Task<(HttpResponseMessage, string, JsonNode)> sendMultipartPostRequest(string addrServer, string sessionID, string pathToFile)
    {
        try
        {
            if (string.IsNullOrEmpty(sessionID)) throw new ArgumentException("sessionID is required", nameof(sessionID));
            if (string.IsNullOrEmpty(addrServer)) throw new ArgumentException("addrServer is required", nameof(addrServer));
            if (string.IsNullOrEmpty(pathToFile) || !File.Exists(pathToFile)) throw new FileNotFoundException("File not found", pathToFile);

            using HttpClient client = new();
            using MultipartFormDataContent multipart = new();

            multipart.Add(new StringContent(sessionID), "session_id");

            var fileName = Path.GetFileName(pathToFile) ?? "unnamed";
            using var fs = File.OpenRead(pathToFile);
            var streamContent = new StreamContent(fs);
            streamContent.Headers.ContentType = new MediaTypeHeaderValue("application/octet-stream");
            multipart.Add(streamContent, "file", fileName);

            HttpResponseMessage response = await client.PostAsync(addrServer.TrimEnd('/') + "/upload", multipart);
            string responseBody = await response.Content.ReadAsStringAsync();

            JsonNode jsonResponseBody = null;
            try
            {
                jsonResponseBody = JsonNode.Parse(responseBody);
            }
            catch
            {
                // не критично — вернём null если не JSON
            }

            return (response, responseBody, jsonResponseBody);
        }
        catch (Exception ex)
        {
            MessageBox.Show(ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return (null, null, null);
        }
    }
}