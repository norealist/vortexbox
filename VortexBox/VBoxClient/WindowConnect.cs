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

namespace VBoxClient
{
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
            string jsonPostData = JsonConvert.SerializeObject(loginData);

            var httpContent = new StringContent(jsonPostData, Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new())
            {
                HttpResponseMessage response = await httpClient.PostAsync(textBox_addr.Text + "/login", httpContent);
                Session responseBody = await response.Content.ReadFromJsonAsync<Session>();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Успех: {responseBody}");
                    File.WriteAllText(".session", responseBody.session_id);
                }
                else
                {
                    MessageBox.Show($"Ошибка: {responseBody}");
                }
            }
        }

        private async void buttonReg_Click(object sender, EventArgs e)
        {
            var regData = new
            {
                login = textBox_login.Text,
                password = genSHA256(textBox_password.Text)
            };
            string jsonPostData = JsonConvert.SerializeObject(regData);

            var httpContent = new StringContent(jsonPostData, Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new())
            {
                HttpResponseMessage response = await httpClient.PostAsync(textBox_addr.Text + "/register", httpContent);
                Session responseBody = await response.Content.ReadFromJsonAsync<Session>();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Успех: {responseBody}");
                    File.WriteAllText(".session", responseBody.session_id);
                }
                else
                {
                    MessageBox.Show($"Ошибка: {responseBody}");
                }
            }
        }

        private void WindowConnect_Load(object sender, EventArgs e)
        {
            if (File.Exists(".session"))
            {
                this.WindowState = FormWindowState.Minimized;
                MainWindow mainWindow = new(File.ReadAllText(".session"));

                mainWindow.Show();
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

    public record Session(string session_id);
}
