using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices.Marshalling;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;

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
                password = textBox_password.Text
            };
            string jsonPostData = JsonConvert.SerializeObject(loginData);

            var httpContent = new StringContent(jsonPostData, Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new())
            {
                HttpResponseMessage response = await httpClient.PostAsync(textBox_addr.Text + "/login", httpContent);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Успех: {responseBody}");
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
                password = textBox_password.Text
            };
            string jsonPostData = JsonConvert.SerializeObject(regData);

            var httpContent = new StringContent(jsonPostData, Encoding.UTF8, "application/json");

            using (HttpClient httpClient = new())
            {
                HttpResponseMessage response = await httpClient.PostAsync(textBox_addr.Text+"/register", httpContent);
                string responseBody = await response.Content.ReadAsStringAsync();

                if (response.IsSuccessStatusCode)
                {
                    MessageBox.Show($"Успех: {responseBody}");
                }
                else
                {
                    MessageBox.Show($"Ошибка: {responseBody}");
                }
            }
        }
    }
}
