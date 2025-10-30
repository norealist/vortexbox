using Newtonsoft.Json;
using System.Threading.Tasks;

namespace VBoxClient;

public partial class MainWindow : Form
{
    private string sessionID { get; set; }
    private string addrServer { get; set; }
    //private HttpClient client = new HttpClient();

    public MainWindow(string sessionID, string addrServer)
    {
        InitializeComponent();
        this.sessionID = sessionID;
        this.addrServer = addrServer;
    }

    private async void Form1_Load(object sender, EventArgs e)
    {
        if (addrServer.Contains("http://"))
        {

            notifyIcon.BalloonTipText = "У этого сервера отсутсвует TLS сертификат. Запросы не зашифрованы";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Warning;
            notifyIcon.ShowBalloonTip(5);
            notifyIcon.BalloonTipIcon = ToolTipIcon.None;
        }

        getListFiles();
    }

    private async void buttonLogout_Click(object sender, EventArgs e)
    {
        var postData = new
        {
            session_id = sessionID
        };
        var (response, responseBody, jsonResponseBody) = await VBoxRequests.sendPostRequest(addrServer, postData, "/logout");
        if (response == null) return;

        notifyIcon.BalloonTipText = "Вы вышли из системы";
        notifyIcon.ShowBalloonTip(5);

        File.Delete(".session");

        this.Close();
    }

    private async void listFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (listFiles.SelectedItem.ToString() == "...")
            {
                getListFiles();
            }
        }
        catch (NullReferenceException) { }
    }

    private async void buttonDelete_Click(object sender, EventArgs e)
    {
        DialogResult result = MessageBox.Show(
            "Вы уверены?",
            "o_0",
            MessageBoxButtons.YesNo,
            MessageBoxIcon.Question
        );

        if (result == DialogResult.Yes)
        {
            var postData = new
            {
                session_id = sessionID,
                path = listFiles.SelectedItem.ToString()
            };

            var (response, responseBody, jsonResponseBody) = await VBoxRequests.sendPostRequest(addrServer, postData, "/del");
            if (response == null) return;

            if (response.IsSuccessStatusCode)
            {
                notifyIcon.BalloonTipText = "Файл удалён";
                notifyIcon.ShowBalloonTip(5);

                getListFiles();
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

    private async void getListFiles()
    {
        listFiles.Items.Clear();

        var postData = new
        {
            session_id = sessionID,
            path = ""
        };

        var (response, responseBody, jsonResponseBody) = await VBoxRequests.sendPostRequest(addrServer, postData, "/ls");
        if (response == null) return;

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

    private async void buttonDownload_Click(object sender, EventArgs e)
    {
        DialogResult result = savePathDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            await VboxFS.downloadFile(sessionID, addrServer, listFiles.SelectedItem.ToString(), savePathDialog.SelectedPath.ToString());

            notifyIcon.BalloonTipText = $"{listFiles.SelectedItem.ToString()} успешно сохранён в {savePathDialog.SelectedPath.ToString()}";
            notifyIcon.ShowBalloonTip(5);
        }
    }
}