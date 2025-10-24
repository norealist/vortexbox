using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using System.Security.Cryptography;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Nodes;

namespace VBoxClient;

public partial class WindowConnect : Form
{
    public WindowConnect()
    {
        InitializeComponent();
    }

    private async void buttonConnect_Click(object sender, EventArgs e)
    {
        var loginData = new
        {
            login = textBox_login.Text,
            password = genSHA256(textBox_password.Text)
        };

        var (response, responseBody, jsonResponseBody) = await VBoxRequests.sendPostRequest(textBox_addr.Text, loginData, "/login");
        if (response == null) return;

        if (response.IsSuccessStatusCode)
        {
            notifyIcon.BalloonTipText = "Вы успешно вошли в систему!";
            notifyIcon.ShowBalloonTip(5);

            var data = new
            {
                addr = textBox_addr.Text,
                session_id = jsonResponseBody["session_id"].ToString()
            };
            File.WriteAllText(".session", JsonConvert.SerializeObject(data));

            CreateMainWindow();
        }
        else
        {
            MessageBox.Show(jsonResponseBody["detail"].ToString(), response.StatusCode.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
    }

    private async void buttonReg_Click(object sender, EventArgs e)
    {
        var regData = new
        {
            login = textBox_login.Text,
            password = genSHA256(textBox_password.Text)
        };

        var (response, responseBody, jsonResponseBody) = await VBoxRequests.sendPostRequest(textBox_addr.Text, regData, "/login");
        if (response == null) return;

        if (response.IsSuccessStatusCode)
        {
            var data = new
            {
                addr = textBox_addr.Text,
                session_id = jsonResponseBody["session_id"].ToString()
            };
            File.WriteAllText(".session", JsonConvert.SerializeObject(data));

            notifyIcon.BalloonTipText = "Вы успешно зарегистрированы!";
            notifyIcon.ShowBalloonTip(5);

            CreateMainWindow();
        }
        else
        {
            MessageBox.Show(jsonResponseBody["detail"].ToString(), response.StatusCode.ToString(), MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
        
    }

    private void WindowConnect_Load(object sender, EventArgs e)
    {
        /*if (File.Exists(".session"))
        {
            this.WindowState = FormWindowState.Minimized;

            ReadFileSession readFileSession = JsonConvert.DeserializeObject<ReadFileSession>(File.ReadAllText(".session"));
            MainWindow mainWindow = new(readFileSession.session_id, readFileSession.addr);

            mainWindow.Show();
            mainWindow.FormClosed += (s, args) =>
            {
                mainWindow.Dispose();
                this.WindowState = FormWindowState.Normal;
            };
        }*/
        CreateMainWindow();
    }

    private void CreateMainWindow()
    {
        if (File.Exists(".session"))
        {
            ReadFileSession readFileSession = JsonConvert.DeserializeObject<ReadFileSession>(File.ReadAllText(".session"));
            MainWindow mainWindow = new(readFileSession.session_id, readFileSession.addr);

            mainWindow.Show();
            this.WindowState = FormWindowState.Minimized;

            mainWindow.FormClosed += (s, args) =>
            {
                mainWindow.Dispose();
                this.WindowState = FormWindowState.Normal;
            };
        }
    }

    private string genSHA256(string s)
    {
        using SHA256 sha256 = SHA256.Create();
        byte[] hashBytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(s));
        string hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

        return hash.ToLower();
    }
}

//public record Session(string session_id, string details = "none");

public record ReadFileSession(string addr, string session_id);
