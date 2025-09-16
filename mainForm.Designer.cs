namespace yt_downloaders
{
    partial class mainForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(mainForm));
            groupBox1 = new GroupBox();
            panelInputURLColorBar = new Panel();
            panelInputURL = new Panel();
            inputURL = new TextBox();
            groupBox2 = new GroupBox();
            panel6 = new Panel();
            chkHighestAvailableSettings = new CheckBox();
            label4 = new Label();
            panel5 = new Panel();
            dropdownCodec = new ComboBox();
            label3 = new Label();
            panel3 = new Panel();
            dropdownQuality = new ComboBox();
            label1 = new Label();
            panel4 = new Panel();
            dropdownFormat = new ComboBox();
            label2 = new Label();
            statusWaiting = new Label();
            btnDownload = new Button();
            statusTitle = new Label();
            titleStrip = new ToolTip(components);
            statusVideoTitle = new Label();
            chkOpenFolderOnFinish = new CheckBox();
            panelDownload = new Panel();
            chkCloseAppOnFinish = new CheckBox();
            groupBox1.SuspendLayout();
            panelInputURLColorBar.SuspendLayout();
            panelInputURL.SuspendLayout();
            groupBox2.SuspendLayout();
            panel6.SuspendLayout();
            panel5.SuspendLayout();
            panel3.SuspendLayout();
            panel4.SuspendLayout();
            panelDownload.SuspendLayout();
            SuspendLayout();
            // 
            // groupBox1
            // 
            groupBox1.Controls.Add(panelInputURLColorBar);
            groupBox1.Location = new Point(25, 37);
            groupBox1.Name = "groupBox1";
            groupBox1.Size = new Size(571, 77);
            groupBox1.TabIndex = 0;
            groupBox1.TabStop = false;
            groupBox1.Text = "Enter URL";
            // 
            // panelInputURLColorBar
            // 
            panelInputURLColorBar.BackColor = SystemColors.ActiveBorder;
            panelInputURLColorBar.Controls.Add(panelInputURL);
            panelInputURLColorBar.Location = new Point(6, 24);
            panelInputURLColorBar.Name = "panelInputURLColorBar";
            panelInputURLColorBar.Size = new Size(559, 40);
            panelInputURLColorBar.TabIndex = 0;
            panelInputURLColorBar.MouseDown += panelInputURLColorBar_MouseDown;
            // 
            // panelInputURL
            // 
            panelInputURL.Anchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;
            panelInputURL.BackColor = SystemColors.Control;
            panelInputURL.Controls.Add(inputURL);
            panelInputURL.Cursor = Cursors.IBeam;
            panelInputURL.Location = new Point(1, 1);
            panelInputURL.Name = "panelInputURL";
            panelInputURL.Size = new Size(557, 38);
            panelInputURL.TabIndex = 3;
            panelInputURL.MouseDown += panelInputURL_MouseDown;
            // 
            // inputURL
            // 
            inputURL.Anchor = AnchorStyles.Top | AnchorStyles.Left | AnchorStyles.Right;
            inputURL.BackColor = SystemColors.Control;
            inputURL.BorderStyle = BorderStyle.None;
            inputURL.Enabled = false;
            inputURL.Font = new Font("Bahnschrift Light", 14F);
            inputURL.ForeColor = Color.Black;
            inputURL.Location = new Point(5, 7);
            inputURL.Multiline = true;
            inputURL.Name = "inputURL";
            inputURL.PlaceholderText = "https://www.youtube.com/...";
            inputURL.Size = new Size(542, 26);
            inputURL.TabIndex = 2;
            inputURL.TextChanged += inputURL_TextChanged;
            inputURL.KeyDown += inputURL_KeyDown;
            // 
            // groupBox2
            // 
            groupBox2.Controls.Add(panel6);
            groupBox2.Controls.Add(panel5);
            groupBox2.Controls.Add(panel3);
            groupBox2.Controls.Add(panel4);
            groupBox2.Location = new Point(25, 120);
            groupBox2.Name = "groupBox2";
            groupBox2.Size = new Size(571, 178);
            groupBox2.TabIndex = 1;
            groupBox2.TabStop = false;
            groupBox2.Text = "Video options";
            // 
            // panel6
            // 
            panel6.Controls.Add(chkHighestAvailableSettings);
            panel6.Controls.Add(label4);
            panel6.Location = new Point(6, 100);
            panel6.Name = "panel6";
            panel6.Size = new Size(273, 70);
            panel6.TabIndex = 4;
            // 
            // chkHighestAvailableSettings
            // 
            chkHighestAvailableSettings.AutoSize = true;
            chkHighestAvailableSettings.Location = new Point(6, 35);
            chkHighestAvailableSettings.Name = "chkHighestAvailableSettings";
            chkHighestAvailableSettings.Size = new Size(200, 22);
            chkHighestAvailableSettings.TabIndex = 2;
            chkHighestAvailableSettings.Text = "Highest available settings";
            chkHighestAvailableSettings.UseVisualStyleBackColor = true;
            chkHighestAvailableSettings.CheckedChanged += chkHighestAvailableSettings_CheckedChanged;
            // 
            // label4
            // 
            label4.Location = new Point(5, 5);
            label4.Name = "label4";
            label4.Size = new Size(265, 21);
            label4.TabIndex = 0;
            label4.Text = "Override";
            label4.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel5
            // 
            panel5.Controls.Add(dropdownCodec);
            panel5.Controls.Add(label3);
            panel5.Location = new Point(292, 100);
            panel5.Name = "panel5";
            panel5.Size = new Size(273, 70);
            panel5.TabIndex = 3;
            // 
            // dropdownCodec
            // 
            dropdownCodec.DropDownStyle = ComboBoxStyle.DropDownList;
            dropdownCodec.Font = new Font("Bahnschrift Light", 14F);
            dropdownCodec.FormattingEnabled = true;
            dropdownCodec.Location = new Point(6, 29);
            dropdownCodec.Name = "dropdownCodec";
            dropdownCodec.Size = new Size(264, 31);
            dropdownCodec.TabIndex = 1;
            // 
            // label3
            // 
            label3.Location = new Point(5, 5);
            label3.Name = "label3";
            label3.Size = new Size(265, 21);
            label3.TabIndex = 0;
            label3.Text = "Codec";
            label3.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel3
            // 
            panel3.Controls.Add(dropdownQuality);
            panel3.Controls.Add(label1);
            panel3.Location = new Point(6, 24);
            panel3.Name = "panel3";
            panel3.Size = new Size(273, 70);
            panel3.TabIndex = 2;
            // 
            // dropdownQuality
            // 
            dropdownQuality.DropDownStyle = ComboBoxStyle.DropDownList;
            dropdownQuality.Font = new Font("Bahnschrift Light", 14F);
            dropdownQuality.FormattingEnabled = true;
            dropdownQuality.Location = new Point(6, 29);
            dropdownQuality.Name = "dropdownQuality";
            dropdownQuality.Size = new Size(264, 31);
            dropdownQuality.TabIndex = 1;
            // 
            // label1
            // 
            label1.Location = new Point(5, 5);
            label1.Name = "label1";
            label1.Size = new Size(265, 21);
            label1.TabIndex = 0;
            label1.Text = "Quality";
            label1.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // panel4
            // 
            panel4.Controls.Add(dropdownFormat);
            panel4.Controls.Add(label2);
            panel4.Location = new Point(292, 24);
            panel4.Name = "panel4";
            panel4.Size = new Size(273, 70);
            panel4.TabIndex = 1;
            // 
            // dropdownFormat
            // 
            dropdownFormat.DropDownStyle = ComboBoxStyle.DropDownList;
            dropdownFormat.Font = new Font("Bahnschrift Light", 14F);
            dropdownFormat.FormattingEnabled = true;
            dropdownFormat.Location = new Point(5, 29);
            dropdownFormat.Name = "dropdownFormat";
            dropdownFormat.Size = new Size(264, 31);
            dropdownFormat.TabIndex = 2;
            // 
            // label2
            // 
            label2.Location = new Point(5, 5);
            label2.Name = "label2";
            label2.Size = new Size(265, 21);
            label2.TabIndex = 1;
            label2.Text = "Format";
            label2.TextAlign = ContentAlignment.MiddleLeft;
            // 
            // statusWaiting
            // 
            statusWaiting.BackColor = Color.Silver;
            statusWaiting.Font = new Font("Bahnschrift SemiLight", 11F);
            statusWaiting.Location = new Point(0, 0);
            statusWaiting.Name = "statusWaiting";
            statusWaiting.Size = new Size(622, 28);
            statusWaiting.TabIndex = 2;
            statusWaiting.Text = "Waiting for input";
            statusWaiting.TextAlign = ContentAlignment.MiddleCenter;
            statusWaiting.MouseDown += statusWaiting_MouseDown;
            // 
            // btnDownload
            // 
            btnDownload.Cursor = Cursors.Hand;
            btnDownload.Font = new Font("Bahnschrift", 11F);
            btnDownload.Location = new Point(402, 47);
            btnDownload.Name = "btnDownload";
            btnDownload.Size = new Size(182, 48);
            btnDownload.TabIndex = 3;
            btnDownload.Text = "📥 Download";
            btnDownload.UseVisualStyleBackColor = true;
            btnDownload.Click += btnDownload_Click;
            // 
            // statusTitle
            // 
            statusTitle.Cursor = Cursors.Hand;
            statusTitle.Font = new Font("Bahnschrift Light", 9F);
            statusTitle.Location = new Point(13, 40);
            statusTitle.Name = "statusTitle";
            statusTitle.Size = new Size(270, 55);
            statusTitle.TabIndex = 4;
            statusTitle.TextAlign = ContentAlignment.MiddleLeft;
            statusTitle.Click += statusTitle_Click;
            statusTitle.MouseEnter += statusTitle_MouseEnter;
            statusTitle.MouseLeave += statusTitle_MouseLeave;
            // 
            // titleStrip
            // 
            titleStrip.ToolTipIcon = ToolTipIcon.Info;
            titleStrip.ToolTipTitle = "Video title";
            // 
            // statusVideoTitle
            // 
            statusVideoTitle.Location = new Point(13, 19);
            statusVideoTitle.Name = "statusVideoTitle";
            statusVideoTitle.Size = new Size(270, 21);
            statusVideoTitle.TabIndex = 5;
            statusVideoTitle.Text = "Fetched video";
            statusVideoTitle.TextAlign = ContentAlignment.MiddleLeft;
            statusVideoTitle.Visible = false;
            // 
            // chkOpenFolderOnFinish
            // 
            chkOpenFolderOnFinish.CheckAlign = ContentAlignment.MiddleRight;
            chkOpenFolderOnFinish.Checked = true;
            chkOpenFolderOnFinish.CheckState = CheckState.Checked;
            chkOpenFolderOnFinish.Cursor = Cursors.Hand;
            chkOpenFolderOnFinish.Font = new Font("Bahnschrift Light", 9F);
            chkOpenFolderOnFinish.Location = new Point(402, 3);
            chkOpenFolderOnFinish.Name = "chkOpenFolderOnFinish";
            chkOpenFolderOnFinish.Size = new Size(182, 41);
            chkOpenFolderOnFinish.TabIndex = 6;
            chkOpenFolderOnFinish.Text = "Open Downloads folder upon completion";
            chkOpenFolderOnFinish.TextAlign = ContentAlignment.MiddleCenter;
            chkOpenFolderOnFinish.UseVisualStyleBackColor = true;
            // 
            // panelDownload
            // 
            panelDownload.Controls.Add(chkCloseAppOnFinish);
            panelDownload.Controls.Add(statusVideoTitle);
            panelDownload.Controls.Add(chkOpenFolderOnFinish);
            panelDownload.Controls.Add(btnDownload);
            panelDownload.Controls.Add(statusTitle);
            panelDownload.Location = new Point(12, 304);
            panelDownload.Name = "panelDownload";
            panelDownload.Size = new Size(598, 98);
            panelDownload.TabIndex = 7;
            panelDownload.Visible = false;
            // 
            // chkCloseAppOnFinish
            // 
            chkCloseAppOnFinish.CheckAlign = ContentAlignment.MiddleRight;
            chkCloseAppOnFinish.Cursor = Cursors.Hand;
            chkCloseAppOnFinish.Font = new Font("Bahnschrift Light", 9F);
            chkCloseAppOnFinish.Location = new Point(295, 3);
            chkCloseAppOnFinish.Name = "chkCloseAppOnFinish";
            chkCloseAppOnFinish.Size = new Size(101, 41);
            chkCloseAppOnFinish.TabIndex = 7;
            chkCloseAppOnFinish.Text = "Close app on finish";
            chkCloseAppOnFinish.TextAlign = ContentAlignment.MiddleCenter;
            chkCloseAppOnFinish.UseVisualStyleBackColor = true;
            // 
            // mainForm
            // 
            AutoScaleDimensions = new SizeF(8F, 18F);
            AutoScaleMode = AutoScaleMode.Font;
            ClientSize = new Size(622, 411);
            Controls.Add(panelDownload);
            Controls.Add(groupBox2);
            Controls.Add(groupBox1);
            Controls.Add(statusWaiting);
            Font = new Font("Bahnschrift Light", 11F);
            FormBorderStyle = FormBorderStyle.FixedSingle;
            Icon = (Icon)resources.GetObject("$this.Icon");
            Margin = new Padding(3, 4, 3, 4);
            MaximizeBox = false;
            Name = "mainForm";
            StartPosition = FormStartPosition.CenterScreen;
            Text = "yt-downloaders";
            Load += mainForm_Load;
            groupBox1.ResumeLayout(false);
            panelInputURLColorBar.ResumeLayout(false);
            panelInputURL.ResumeLayout(false);
            panelInputURL.PerformLayout();
            groupBox2.ResumeLayout(false);
            panel6.ResumeLayout(false);
            panel6.PerformLayout();
            panel5.ResumeLayout(false);
            panel3.ResumeLayout(false);
            panel4.ResumeLayout(false);
            panelDownload.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        private GroupBox groupBox1;
        private Panel panelInputURLColorBar;
        private TextBox inputURL;
        private Panel panelInputURL;
        private GroupBox groupBox2;
        private Panel panel3;
        private Panel panel4;
        private Label label1;
        private Label label2;
        private ComboBox dropdownQuality;
        private ComboBox dropdownFormat;
        private Panel panel5;
        private ComboBox dropdownCodec;
        private Label label3;
        private Panel panel6;
        private CheckBox chkHighestAvailableSettings;
        private Label label4;
        private Label statusWaiting;
        private Button btnDownload;
        private Label statusTitle;
        private ToolTip titleStrip;
        private Label statusVideoTitle;
        private CheckBox chkOpenFolderOnFinish;
        private Panel panelDownload;
        private CheckBox chkCloseAppOnFinish;
    }
}
