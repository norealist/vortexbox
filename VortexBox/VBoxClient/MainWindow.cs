using Newtonsoft.Json;
using System.Diagnostics;

namespace VBoxClient;

public partial class MainWindow : Form
{
    private string sessionID { get; set; }
    private string addrServer { get; set; }
    //private HttpClient client = new HttpClient();

    public MainWindow(string sessionID, string addrServer)
    {
        this.sessionID = sessionID;
        this.addrServer = addrServer;
        InitializeComponent();
    }

    private async void MainWindow_Load(object sender, EventArgs e)
    {
        if (addrServer.Contains("http://"))
            showNotifyIcon("У этого сервера отсутсвует TLS сертификат. Запросы не зашифрованы", NotifyIconType.Warning);

        getListFiles();
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

    private async void buttonLogout_Click(object sender, EventArgs e)
    {
        var postData = new
        {
            session_id = sessionID
        };
        var (response, responseBody, jsonResponseBody) = await VBoxRequests.sendPostRequest(addrServer, postData, "/logout");
        if (response == null) return;

        showNotifyIcon("Вы вышли из системы", NotifyIconType.Info);

        File.Delete(".session");

        this.Close();
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
                showNotifyIcon("Файл удалён", NotifyIconType.None);
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

    private async void buttonDownload_Click(object sender, EventArgs e)
    {
        DialogResult result = savePathDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            string selectedFile = listFiles.SelectedItem.ToString();

            buttonDownload.Enabled = false;
            buttonDownload.Text = "В процессе...";

            await VboxFS.downloadFile(sessionID, addrServer, selectedFile, savePathDialog.SelectedPath.ToString());

            buttonDownload.Text = "Скачать";
            buttonDownload.Enabled = true;

            showNotifyIcon($"{selectedFile} успешно сохранён в {savePathDialog.SelectedPath.ToString()}", NotifyIconType.Info);

            EventHandler handler = null;
            handler = (s, args) =>
            {
                try
                {
                    using Process proc = new();
                    proc.StartInfo.FileName = "explorer.exe";
                    proc.StartInfo.Arguments = savePathDialog.SelectedPath.ToString();
                    proc.Start();
                }
                finally
                {
                    notifyIcon.BalloonTipClicked -= handler;
                }
            };
            notifyIcon.BalloonTipClicked += handler;

        }
    }

    private async void buttonUpload_Click(object sender, EventArgs e)
    {
        DialogResult result = uploadFileDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            buttonUpload.Enabled = false;
            buttonUpload.Text = "В процессе...";

            string answer = await VboxFS.uploadFile(sessionID, addrServer, uploadFileDialog.FileName);

            switch (answer)
            {
                case "OK":
                    showNotifyIcon($"{Path.GetFileName(uploadFileDialog.FileName)} успешно загружен", NotifyIconType.Info);
                    break;
                case "Invalid filename":
                    showNotifyIcon($"Некорректное имя файла: {Path.GetFileName(uploadFileDialog.FileName)}", NotifyIconType.Error);
                    break;
                case "Access denied":
                    showNotifyIcon($"Ошибка доступа", NotifyIconType.Error);
                    break;
            }

            buttonUpload.Enabled = true;
            buttonUpload.Text = "Загрузить";

            getListFiles();
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

    private void showNotifyIcon(string text, NotifyIconType notifyIconType)
    {
        switch (notifyIconType)
        {
            case NotifyIconType.None:
                notifyIcon.BalloonTipIcon = ToolTipIcon.None;
                notifyIcon.BalloonTipText = text;

                notifyIcon.ShowBalloonTip(5);

                notifyIcon.BalloonTipText = string.Empty;
                break;

            case NotifyIconType.Info:
                notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
                notifyIcon.BalloonTipText = text;

                notifyIcon.ShowBalloonTip(5);

                notifyIcon.BalloonTipText = string.Empty;
                notifyIcon.BalloonTipIcon = ToolTipIcon.None;
                break;

            case NotifyIconType.Warning:
                notifyIcon.BalloonTipIcon = ToolTipIcon.Warning;
                notifyIcon.BalloonTipText = text;

                notifyIcon.ShowBalloonTip(5);

                notifyIcon.BalloonTipText = string.Empty;
                notifyIcon.BalloonTipIcon = ToolTipIcon.None;
                break;

            case NotifyIconType.Error:
                notifyIcon.BalloonTipIcon = ToolTipIcon.Error;
                notifyIcon.BalloonTipText = text;

                notifyIcon.ShowBalloonTip(5);

                notifyIcon.BalloonTipText = string.Empty;
                notifyIcon.BalloonTipIcon = ToolTipIcon.None;
                break;
        }
    }

    enum NotifyIconType
    {
        None,
        Info,
        Warning,
        Error
    }
}