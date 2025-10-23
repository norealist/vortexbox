namespace VBoxClient
{
    public partial class MainWindow : Form
    {
        private string sessionID { get; set; }

        public MainWindow(string sessionID)
        {
            InitializeComponent();
            this.sessionID = sessionID;
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void buttonUpload_Click(object sender, EventArgs e)
        {

        }
    }
}
