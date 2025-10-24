using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

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
}
