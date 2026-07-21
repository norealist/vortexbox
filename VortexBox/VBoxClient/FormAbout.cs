using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VBoxClient
{
    public partial class FormAbout : Form
    {
        public FormAbout()
        {
            InitializeComponent();
        }

        private void labelNorealist_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/norealist") { UseShellExecute = true });
        }

        private void labelKM_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/KM-090105") { UseShellExecute = true });
        }

        private void labelClientLink_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/norealist/vortexbox") { UseShellExecute = true });
        }

        private void labelServerLink_Click(object sender, EventArgs e)
        {
            Process.Start(new ProcessStartInfo("https://github.com/norealist/vortexbox-server") { UseShellExecute = true });
        }

        private void label1_Click(object sender, EventArgs e)
        {

        }
    }
}
