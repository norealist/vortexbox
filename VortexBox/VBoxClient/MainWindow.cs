using Newtonsoft.Json;
using System.Net.Http;
using System.Net.Http.Json;
using System.Text;

namespace VBoxClient
{
    public partial class MainWindow : Form
    {
        private string sessionID { get; set; }
        private string addrServer { get; set; }

        public MainWindow(string sessionID, string addrServer)
        {
            InitializeComponent();
            this.sessionID = sessionID;
            this.addrServer = addrServer;
        }

        private async void Form1_Load(object sender, EventArgs e)
        {
            var postData = new
            {
                session_id = sessionID,
                path = ""
            };

            string jsonPostData = JsonConvert.SerializeObject(postData);
            var httpContent = new StringContent(jsonPostData, Encoding.UTF8, "application/json");

            using HttpClient client = new HttpClient();

            HttpResponseMessage response = await client.PostAsync(addrServer+"/ls", httpContent);
            ListFiles responseBody = await response.Content.ReadFromJsonAsync<ListFiles>();

            if (response.IsSuccessStatusCode)
            {
                listFiles.Items.Add("...");
                listFiles.Items.AddRange(responseBody.files);
            }
            else
            {
                MessageBox.Show(
                    responseBody.ToString(),
                    "Error",
                    MessageBoxButtons.OK,
                    MessageBoxIcon.Error
                );
            }
        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {

        }
    }
}

public record ListFiles(string[] files);