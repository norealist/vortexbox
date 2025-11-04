namespace VBoxClient
{
    partial class MainWindow
    {
        /// <summary>
        ///  Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        ///  Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        ///  Required method for Designer support - do not modify
        ///  the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainWindow));
            listFiles = new ListBox();
            buttonDownload = new Button();
            buttonUpload = new Button();
            fileInfo = new TextBox();
            encryptFileOrNot = new CheckBox();
            buttonDelete = new Button();
            buttonLogout = new Button();
            notifyIcon = new NotifyIcon(components);
            savePathDialog = new FolderBrowserDialog();
            uploadFileDialog = new OpenFileDialog();
            menuStrip = new MenuStrip();
            fileToolStripItem = new ToolStripMenuItem();
            restartExplorer = new ToolStripMenuItem();
            aboutToolStripItem = new ToolStripMenuItem();
            checkUpdateToolMenuStrip = new ToolStripMenuItem();
            aboutToolMenuStrip = new ToolStripMenuItem();
            menuStrip.SuspendLayout();
            SuspendLayout();
            // 
            // listFiles
            // 
            listFiles.BackColor = SystemColors.InactiveBorder;
            listFiles.BorderStyle = BorderStyle.FixedSingle;
            listFiles.Cursor = Cursors.Hand;
            listFiles.Font = new Font("YouTube Sans Light 48pt", 11.9999981F, FontStyle.Regular, GraphicsUnit.Point, 0);
            listFiles.FormattingEnabled = true;
            listFiles.HorizontalScrollbar = true;
            listFiles.Location = new Point(12, 37);
            listFiles.Name = "listFiles";
            listFiles.Size = new Size(623, 386);
            listFiles.TabIndex = 0;
            listFiles.SelectedIndexChanged += listFiles_SelectedIndexChanged;
            // 
            // buttonDownload
            // 
            buttonDownload.Font = new Font("Segoe UI", 11F);
            buttonDownload.Location = new Point(654, 251);
            buttonDownload.Name = "buttonDownload";
            buttonDownload.Size = new Size(129, 74);
            buttonDownload.TabIndex = 1;
            buttonDownload.Text = "Скачать";
            buttonDownload.UseVisualStyleBackColor = true;
            buttonDownload.Click += buttonDownload_Click;
            // 
            // buttonUpload
            // 
            buttonUpload.Font = new Font("Segoe UI", 11F);
            buttonUpload.Location = new Point(789, 251);
            buttonUpload.Name = "buttonUpload";
            buttonUpload.Size = new Size(129, 74);
            buttonUpload.TabIndex = 2;
            buttonUpload.Text = "Загрузить";
            buttonUpload.UseVisualStyleBackColor = true;
            buttonUpload.Click += buttonUpload_Click;
            // 
            // fileInfo
            // 
            fileInfo.BorderStyle = BorderStyle.FixedSingle;
            fileInfo.Font = new Font("Cascadia Mono SemiLight", 12F, FontStyle.Regular, GraphicsUnit.Point, 204);
            fileInfo.Location = new Point(654, 37);
            fileInfo.Multiline = true;
            fileInfo.Name = "fileInfo";
            fileInfo.ReadOnly = true;
            fileInfo.Size = new Size(262, 165);
            fileInfo.TabIndex = 3;
            // 
            // encryptFileOrNot
            // 
            encryptFileOrNot.AutoSize = true;
            encryptFileOrNot.Location = new Point(654, 217);
            encryptFileOrNot.Name = "encryptFileOrNot";
            encryptFileOrNot.Size = new Size(182, 19);
            encryptFileOrNot.TabIndex = 4;
            encryptFileOrNot.Text = "Шифровать перед загрузкой";
            encryptFileOrNot.UseVisualStyleBackColor = true;
            // 
            // buttonDelete
            // 
            buttonDelete.Location = new Point(654, 331);
            buttonDelete.Name = "buttonDelete";
            buttonDelete.Size = new Size(264, 35);
            buttonDelete.TabIndex = 5;
            buttonDelete.Text = "Удалить";
            buttonDelete.UseVisualStyleBackColor = true;
            buttonDelete.Click += buttonDelete_Click;
            // 
            // buttonLogout
            // 
            buttonLogout.Location = new Point(654, 388);
            buttonLogout.Name = "buttonLogout";
            buttonLogout.Size = new Size(262, 35);
            buttonLogout.TabIndex = 6;
            buttonLogout.Text = "Выйти";
            buttonLogout.UseVisualStyleBackColor = true;
            buttonLogout.Click += buttonLogout_Click;
            // 
            // notifyIcon
            // 
            notifyIcon.Icon = (Icon)resources.GetObject("notifyIcon.Icon");
            notifyIcon.Visible = true;
            // 
            // menuStrip
            // 
            menuStrip.Items.AddRange(new ToolStripItem[] { fileToolStripItem, aboutToolStripItem });
            menuStrip.Location = new Point(0, 0);
            menuStrip.Name = "menuStrip";
            menuStrip.Size = new Size(928, 24);
            menuStrip.TabIndex = 7;
            menuStrip.Text = "menuStrip";
            // 
            // fileToolStripItem
            // 
            fileToolStripItem.DropDownItems.AddRange(new ToolStripItem[] { restartExplorer });
            fileToolStripItem.Name = "fileToolStripItem";
            fileToolStripItem.Size = new Size(48, 20);
            fileToolStripItem.Text = "Файл";
            // 
            // restartExplorer
            // 
            restartExplorer.Name = "restartExplorer";
            restartExplorer.Size = new Size(218, 22);
            restartExplorer.Text = "Перезапустить проводник";
            restartExplorer.Click += restartExplorer_Click;
            // 
            // aboutToolStripItem
            // 
            aboutToolStripItem.DropDownItems.AddRange(new ToolStripItem[] { checkUpdateToolMenuStrip, aboutToolMenuStrip });
            aboutToolStripItem.Name = "aboutToolStripItem";
            aboutToolStripItem.Size = new Size(94, 20);
            aboutToolStripItem.Text = "О программе";
            // 
            // checkUpdateToolMenuStrip
            // 
            checkUpdateToolMenuStrip.Name = "checkUpdateToolMenuStrip";
            checkUpdateToolMenuStrip.Size = new Size(211, 22);
            checkUpdateToolMenuStrip.Text = "Посмотреть обновления";
            checkUpdateToolMenuStrip.Click += checkUpdateToolMenuStrip_Click;
            // 
            // aboutToolMenuStrip
            // 
            aboutToolMenuStrip.Name = "aboutToolMenuStrip";
            aboutToolMenuStrip.Size = new Size(211, 22);
            aboutToolMenuStrip.Text = "О программе";
            aboutToolMenuStrip.Click += aboutToolMenuStrip_Click;
            // 
            // MainWindow
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            AutoSize = true;
            AutoSizeMode = AutoSizeMode.GrowAndShrink;
            ClientSize = new Size(928, 439);
            Controls.Add(buttonLogout);
            Controls.Add(buttonDelete);
            Controls.Add(encryptFileOrNot);
            Controls.Add(fileInfo);
            Controls.Add(buttonUpload);
            Controls.Add(buttonDownload);
            Controls.Add(listFiles);
            Controls.Add(menuStrip);
            Icon = (Icon)resources.GetObject("$this.Icon");
            MaximizeBox = false;
            Name = "MainWindow";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "VortexBox";
            FormClosing += MainWindow_FormClosing;
            Load += MainWindow_Load;
            menuStrip.ResumeLayout(false);
            menuStrip.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private ListBox listFiles;
        private Button buttonDownload;
        private Button buttonUpload;
        private TextBox fileInfo;
        private CheckBox encryptFileOrNot;
        private Button buttonDelete;
        private Button buttonLogout;
        private NotifyIcon notifyIcon;
        private FolderBrowserDialog savePathDialog;
        private OpenFileDialog uploadFileDialog;
        private MenuStrip menuStrip;
        private ToolStripMenuItem fileToolStripItem;
        private ToolStripMenuItem restartExplorer;
        private ToolStripMenuItem aboutToolStripItem;
        private ToolStripMenuItem checkUpdateToolMenuStrip;
        private ToolStripMenuItem aboutToolMenuStrip;
    }
}
