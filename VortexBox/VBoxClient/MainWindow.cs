using Microsoft.Toolkit.Uwp.Notifications;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text.Json.Nodes;
using Windows.Media.Protection.PlayReady;

namespace VBoxClient;

public partial class MainWindow : Form
{
    private string sessionID { get; set; }
    private string addrServer { get; set; }
    private readonly string[] pictureExts = { ".png", ".jpg", ".jpeg" };
    //private HttpClient client = new HttpClient();

    public MainWindow(string sessionID, string addrServer)
    {
        this.sessionID = sessionID;
        this.addrServer = addrServer;
        ToastNotificationManagerCompat.OnActivated += OnToastActivated;
        InitializeComponent();
    }

    private async void MainWindow_Load(object sender, EventArgs e)
    {
        if (addrServer.Contains("http://"))
            showNotifyIcon("У этого сервера отсутсвует TLS сертификат. Запросы не зашифрованы", NotifyIconType.Warning);

        if (!File.Exists("map.dat"))
        {
            File.WriteAllText("map.dat", "{}");
        }

        getListFiles();
    }

    private async void listFiles_SelectedIndexChanged(object sender, EventArgs e)
    {
        try
        {
            if (listFiles.SelectedItem.ToString() == "...")
            {
                fileInfo.Text = "Обновить список файлов...";
                getListFiles();
            }
            else
            {
                using HttpClient client = new();
                HttpResponseMessage response = await client.GetAsync(addrServer + $"/fileInfo/{listFiles.SelectedItem.ToString()}?session_id={sessionID}");
                string responseBody = await response.Content.ReadAsStringAsync();
                JsonNode jsonResponseBody = JsonNode.Parse(responseBody);

                fileInfo.Text = $"""
                    * Имя файла: {jsonResponseBody["fileInfo"]!["name"]}
                    
                    * Загружен: {jsonResponseBody["fileInfo"]!["uploaded"]}
                    * Размер: {VboxFS.convertFileSize(long.Parse(jsonResponseBody["fileInfo"]!["size"].ToString()))}
                    """;
                if (jsonResponseBody["fileInfo"]!["name"].ToString().Contains(".enc"))
                    fileInfo.Text += """
                        

                        ### ЗАШИФРОВАН ###
                        """;
            }
        }
        catch (NullReferenceException)
        {
            fileInfo.Text = "";
        }
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

            if ((savePathDialog.SelectedPath.ToString() + "/" + selectedFile).EndsWith(".enc"))
            {
                buttonDownload.Text = "Расшифровка...";
                if (new FileInfo(savePathDialog.SelectedPath.ToString() + "/" + selectedFile).Length < 100 * 1024 * 1024)
                    VboxFS.DecryptFile(savePathDialog.SelectedPath.ToString() + "/" + selectedFile, savePathDialog.SelectedPath.ToString());
                else
                    VboxFS.DecryptBigFile(savePathDialog.SelectedPath.ToString() + "/" + selectedFile, savePathDialog.SelectedPath.ToString());
            }

            buttonDownload.Text = "Скачать";
            buttonDownload.Enabled = true;

            /*showNotifyIcon($"{selectedFile} успешно сохранён в {savePathDialog.SelectedPath.ToString()}", NotifyIconType.Info);

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
            */

            var nIcon = new ToastContentBuilder();
            //nIcon.AddText("VortexBox");
            nIcon.AddText($"{selectedFile} успешно сохранён в {savePathDialog.SelectedPath.ToString()}");
            // Если сохраняем изображение, то добавляем AddHeroImage для красоты
            if (pictureExts.Contains(Path.GetExtension(selectedFile).ToLower()))
                nIcon.AddInlineImage(new Uri(savePathDialog.SelectedPath.ToString() + "/" + selectedFile));
            nIcon.AddButton(new ToastButton()
                .SetContent("Показать в проводнике")
                .AddArgument("action", "open"));
            nIcon.Show();

        }
    }

    private async void buttonUpload_Click(object sender, EventArgs e)
    {
        DialogResult result = uploadFileDialog.ShowDialog();
        if (result == DialogResult.OK)
        {
            buttonUpload.Enabled = false;

            if (encryptFileOrNot.Checked)
            {
                buttonUpload.Text = "Шифрование...";
                VboxFS.EncryptFile(uploadFileDialog.FileName);
                if (new FileInfo(uploadFileDialog.FileName).Length < 100 * 1024 * 1024)
                {
                    if (File.Exists("temp/" + Path.GetFileName(uploadFileDialog.FileName) + ".enc"))
                        uploadFileDialog.FileName = "temp/" + Path.GetFileName(uploadFileDialog.FileName) + ".enc";
                }
                else
                {
                    if (File.Exists("temp/" + Path.GetFileName(uploadFileDialog.FileName) + ".tar.enc"))
                        uploadFileDialog.FileName = "temp/" + Path.GetFileName(uploadFileDialog.FileName) + ".tar.enc";
                }
            }
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



    private void restartExplorer_Click(object sender, EventArgs e)
    {
        var psi = new ProcessStartInfo
        {
            FileName = "cmd.exe",
            Arguments = "/c taskkill /f /im explorer.exe & start explorer.exe", // /c выполняет команду и закрывает CMD
            UseShellExecute = false,
            CreateNoWindow = true
        };

        Process.Start(psi);
    }

    private void checkUpdateToolMenuStrip_Click(object sender, EventArgs e)
    {
        Process.Start(new ProcessStartInfo("https://github.com/norealist/vortexbox/releases") { UseShellExecute = true });
    }

    private void aboutToolMenuStrip_Click(object sender, EventArgs e)
    {
        FormAbout about = new();
        about.Show();
    }

    private void MainWindow_FormClosing(object sender, FormClosingEventArgs e)
    {
        if (Directory.Exists("temp"))
            Directory.Delete("temp", true);
        ToastNotificationManagerCompat.OnActivated -= OnToastActivated;
        Application.Exit();
    }



    private void OnToastActivated(ToastNotificationActivatedEventArgsCompat toastArgs)
    {
        ToastArguments args = ToastArguments.Parse(toastArgs.Argument);

        if (args.TryGetValue("action", out string action))
        {
            if (action == "open")
            {
                using Process proc = new();
                proc.StartInfo.FileName = "explorer.exe";
                proc.StartInfo.Arguments = savePathDialog.SelectedPath.ToString();
                proc.Start();
            }
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

    private void clearCache_Click(object sender, EventArgs e)
    {
        if (Directory.Exists("temp/"))
        {
            Directory.Delete("temp/", recursive: true);
            showNotifyIcon("Временные файлы успешно удалены!", NotifyIconType.Info);
        }
    }

    private void deleteCurrentSession_Click(object sender, EventArgs e)
    {
        if (File.Exists(".session"))
        {
            File.Delete(".session");
            showNotifyIcon("Файл сессии успешно удалён. Перезапустите приложение...", NotifyIconType.Warning);
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