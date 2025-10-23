using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json.Nodes;

namespace VBoxClient
{
    public partial class MainWindow : Form
    {
        private string sessionID { get; set; }
        private string addrServer { get; set; }
        private HttpClient client = new HttpClient();

        public MainWindow(string sessionID, string addrServer)
        {
            InitializeComponent();
            this.sessionID = sessionID;
            this.addrServer = addrServer;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            listFiles.Items.Clear();

            var postData = new
            {
                session_id = sessionID,
                path = ""
            };

            var (response, responseBody, jsonResponseBody) = await sendPostRequest(postData, "/ls");

            if (response.IsSuccessStatusCode)
            {
                List<string> files = JsonConvert.DeserializeObject<List<string>>(jsonResponseBody["files"].ToString());
                listFiles.Items.Add("...");
                listFiles.Items.AddRange(files.ToArray());
            }
            else
            {
                MessageBox.Show(
                    jsonResponseBody["detail"].ToString(),
                    response.StatusCode.ToString(),
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private async void buttonLogout_Click(object sender, EventArgs e)
        {
            var postData = new
            {
                session_id = sessionID
            };
            var (response, responseBody, jsonResponseBody) = await sendPostRequest(postData, "/logout");

            MessageBox.Show(jsonResponseBody["status"].ToString(), response.StatusCode.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Information);
            File.Delete(".session");

            this.Close();
        }

        private async void listFiles_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                if (listFiles.SelectedItem.ToString() == "...")
                {
                    listFiles.Items.Clear();

                    var postData = new
                    {
                        session_id = sessionID,
                        path = ""
                    };

                    var (response, responseBody, jsonResponseBody) = await sendPostRequest(postData, "/ls");

                    if (response.IsSuccessStatusCode)
                    {
                        List<string> files = JsonConvert.DeserializeObject<List<string>>(jsonResponseBody["files"].ToString());
                        listFiles.Items.Add("...");
                        listFiles.Items.AddRange(files.ToArray());
                    }
                    else
                    {
                        MessageBox.Show(
                            jsonResponseBody["detail"].ToString(),
                            response.StatusCode.ToString(),
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                    }
                }
            }
            catch (NullReferenceException) { }
        }

        private async Task<(HttpResponseMessage, string, JsonNode)> sendPostRequest(dynamic postData, string endPoint)
        {
            string jsonPostData = JsonConvert.SerializeObject(postData);
            var httpContent = new StringContent(jsonPostData, Encoding.UTF8, "application/json");

            HttpResponseMessage response = await client.PostAsync(addrServer + endPoint, httpContent);
            string responseBody = await response.Content.ReadAsStringAsync();
            var jsonResponseBody = JsonNode.Parse(responseBody);

            return (response, responseBody, jsonResponseBody);
        }
    }
}