namespace PrintSSaver
{
    partial class PrintSSaver
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
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
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(PrintSSaver));
            this.screenshotsLocation = new System.Windows.Forms.FolderBrowserDialog();
            this.location = new System.Windows.Forms.Button();
            this.locationLabel = new System.Windows.Forms.Label();
            this.titleLabel = new System.Windows.Forms.Label();
            this.saveClipboard = new System.Windows.Forms.Button();
            this.formatLabel = new System.Windows.Forms.Label();
            this.inTray = new System.Windows.Forms.NotifyIcon(this.components);
            this.menuStrip = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.openFolderItem = new System.Windows.Forms.ToolStripMenuItem();
            this.saveClipboardItem = new System.Windows.Forms.ToolStripMenuItem();
            this.deactivate = new System.Windows.Forms.ToolStripMenuItem();
            this.toolTipSeparator = new System.Windows.Forms.ToolStripSeparator();
            this.exit = new System.Windows.Forms.ToolStripMenuItem();
            this.statusLbl = new System.Windows.Forms.Label();
            this.SSSLoc = new System.Windows.Forms.Label();
            this.labelTip = new System.Windows.Forms.ToolTip(this.components);
            this.radioPName = new System.Windows.Forms.RadioButton();
            this.radioWName = new System.Windows.Forms.RadioButton();
            this.titlePanel = new System.Windows.Forms.Panel();
            this.formatPanel = new System.Windows.Forms.Panel();
            this.radioPngFormat = new System.Windows.Forms.RadioButton();
            this.radioJpegFormat = new System.Windows.Forms.RadioButton();
            this.ActivateBtn = new System.Windows.Forms.Button();
            this.openFolder = new System.Windows.Forms.Button();
            this.namePanel = new System.Windows.Forms.Panel();
            this.titleNtimestampRadio = new System.Windows.Forms.RadioButton();
            this.timestampRadio = new System.Windows.Forms.RadioButton();
            this.titleRadio = new System.Windows.Forms.RadioButton();
            this.appendLbl = new System.Windows.Forms.Label();
            this.soundLbl = new System.Windows.Forms.Label();
            this.chooseBtn = new System.Windows.Forms.Button();
            this.sfxChkBox = new System.Windows.Forms.CheckBox();
            this.defaultSFXbtn = new System.Windows.Forms.Button();
            this.aboutLabel = new System.Windows.Forms.LinkLabel();
            this.menuStrip.SuspendLayout();
            this.titlePanel.SuspendLayout();
            this.formatPanel.SuspendLayout();
            this.namePanel.SuspendLayout();
            this.SuspendLayout();
            // 
            // screenshotsLocation
            // 
            this.screenshotsLocation.Description = "Screenshots\' Save Location";
            // 
            // location
            // 
            this.location.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.location.Location = new System.Drawing.Point(357, 46);
            this.location.Name = "location";
            this.location.Size = new System.Drawing.Size(75, 23);
            this.location.TabIndex = 0;
            this.location.Text = "Browse";
            this.location.UseVisualStyleBackColor = true;
            this.location.Click += new System.EventHandler(this.location_Click);
            // 
            // locationLabel
            // 
            this.locationLabel.AutoEllipsis = true;
            this.locationLabel.Location = new System.Drawing.Point(27, 51);
            this.locationLabel.Name = "locationLabel";
            this.locationLabel.Size = new System.Drawing.Size(324, 13);
            this.locationLabel.TabIndex = 16;
            this.locationLabel.Text = "%UserName%\\Pictures\\PSSaver";
            // 
            // titleLabel
            // 
            this.titleLabel.AutoSize = true;
            this.titleLabel.Location = new System.Drawing.Point(12, 112);
            this.titleLabel.Name = "titleLabel";
            this.titleLabel.Size = new System.Drawing.Size(33, 13);
            this.titleLabel.TabIndex = 18;
            this.titleLabel.Text = "Title :";
            // 
            // saveClipboard
            // 
            this.saveClipboard.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.saveClipboard.Location = new System.Drawing.Point(337, 106);
            this.saveClipboard.Name = "saveClipboard";
            this.saveClipboard.Size = new System.Drawing.Size(95, 30);
            this.saveClipboard.TabIndex = 2;
            this.saveClipboard.Text = "Save Clipboard";
            this.saveClipboard.UseVisualStyleBackColor = true;
            this.saveClipboard.Click += new System.EventHandler(this.SaveClipboard_Click);
            // 
            // formatLabel
            // 
            this.formatLabel.AutoSize = true;
            this.formatLabel.Location = new System.Drawing.Point(12, 142);
            this.formatLabel.Name = "formatLabel";
            this.formatLabel.Size = new System.Drawing.Size(45, 13);
            this.formatLabel.TabIndex = 19;
            this.formatLabel.Text = "Format :";
            // 
            // inTray
            // 
            this.inTray.BalloonTipText = "Capturing \'PrintScreen\'";
            this.inTray.BalloonTipTitle = "Working";
            this.inTray.ContextMenuStrip = this.menuStrip;
            this.inTray.Icon = ((System.Drawing.Icon)(resources.GetObject("inTray.Icon")));
            this.inTray.Text = "PrintSSaver (Inactive)";
            this.inTray.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.inTray_Click);
            // 
            // menuStrip
            // 
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.openFolderItem,
            this.saveClipboardItem,
            this.deactivate,
            this.toolTipSeparator,
            this.exit});
            this.menuStrip.Name = "toolTipStrip";
            this.menuStrip.Size = new System.Drawing.Size(154, 98);
            // 
            // openFolderItem
            // 
            this.openFolderItem.Name = "openFolderItem";
            this.openFolderItem.Size = new System.Drawing.Size(153, 22);
            this.openFolderItem.Text = "Open Folder";
            this.openFolderItem.Click += new System.EventHandler(this.openFolder_Click);
            // 
            // saveClipboardItem
            // 
            this.saveClipboardItem.Name = "saveClipboardItem";
            this.saveClipboardItem.Size = new System.Drawing.Size(153, 22);
            this.saveClipboardItem.Text = "Save Clipboard";
            this.saveClipboardItem.Click += new System.EventHandler(this.SaveClipboard_Click);
            // 
            // deactivate
            // 
            this.deactivate.Name = "deactivate";
            this.deactivate.Size = new System.Drawing.Size(153, 22);
            this.deactivate.Text = "Deactivate";
            this.deactivate.Click += new System.EventHandler(this.disActivateBtn_Click);
            // 
            // toolTipSeparator
            // 
            this.toolTipSeparator.Name = "toolTipSeparator";
            this.toolTipSeparator.Size = new System.Drawing.Size(150, 6);
            // 
            // exit
            // 
            this.exit.Name = "exit";
            this.exit.Size = new System.Drawing.Size(153, 22);
            this.exit.Text = "Exit";
            this.exit.Click += new System.EventHandler(this.exit_Click);
            // 
            // statusLbl
            // 
            this.statusLbl.Anchor = System.Windows.Forms.AnchorStyles.Top;
            this.statusLbl.AutoSize = true;
            this.statusLbl.Font = new System.Drawing.Font("Microsoft Sans Serif", 9.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.statusLbl.ForeColor = System.Drawing.Color.ForestGreen;
            this.statusLbl.Location = new System.Drawing.Point(52, 9);
            this.statusLbl.Name = "statusLbl";
            this.statusLbl.Size = new System.Drawing.Size(342, 16);
            this.statusLbl.TabIndex = 14;
            this.statusLbl.Text = "Press \'PrintScreen\' Key Anywhere To Save Screenshots";
            this.labelTip.SetToolTip(this.statusLbl, "\'Alt+PrintScreen\' Works Too");
            // 
            // SSSLoc
            // 
            this.SSSLoc.AutoSize = true;
            this.SSSLoc.Location = new System.Drawing.Point(12, 33);
            this.SSSLoc.Name = "SSSLoc";
            this.SSSLoc.Size = new System.Drawing.Size(141, 13);
            this.SSSLoc.TabIndex = 15;
            this.SSSLoc.Text = "Screenshots Save Location:";
            // 
            // radioPName
            // 
            this.radioPName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radioPName.AutoSize = true;
            this.radioPName.Checked = true;
            this.radioPName.Location = new System.Drawing.Point(4, 7);
            this.radioPName.Name = "radioPName";
            this.radioPName.Size = new System.Drawing.Size(94, 17);
            this.radioPName.TabIndex = 7;
            this.radioPName.TabStop = true;
            this.radioPName.Text = "Process Name";
            this.radioPName.UseVisualStyleBackColor = true;
            this.radioPName.CheckedChanged += new System.EventHandler(this.radioTitle_CheckedChanged);
            // 
            // radioWName
            // 
            this.radioWName.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radioWName.AutoSize = true;
            this.radioWName.Location = new System.Drawing.Point(108, 7);
            this.radioWName.Name = "radioWName";
            this.radioWName.Size = new System.Drawing.Size(87, 17);
            this.radioWName.TabIndex = 8;
            this.radioWName.TabStop = true;
            this.radioWName.Text = "Window Title";
            this.radioWName.UseVisualStyleBackColor = true;
            // 
            // titlePanel
            // 
            this.titlePanel.Controls.Add(this.radioPName);
            this.titlePanel.Controls.Add(this.radioWName);
            this.titlePanel.Location = new System.Drawing.Point(43, 104);
            this.titlePanel.Name = "titlePanel";
            this.titlePanel.Size = new System.Drawing.Size(199, 30);
            this.titlePanel.TabIndex = 9;
            // 
            // formatPanel
            // 
            this.formatPanel.Controls.Add(this.radioPngFormat);
            this.formatPanel.Controls.Add(this.radioJpegFormat);
            this.formatPanel.Location = new System.Drawing.Point(55, 134);
            this.formatPanel.Name = "formatPanel";
            this.formatPanel.Size = new System.Drawing.Size(110, 30);
            this.formatPanel.TabIndex = 10;
            // 
            // radioPngFormat
            // 
            this.radioPngFormat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radioPngFormat.AutoSize = true;
            this.radioPngFormat.Checked = true;
            this.radioPngFormat.Location = new System.Drawing.Point(4, 7);
            this.radioPngFormat.Name = "radioPngFormat";
            this.radioPngFormat.Size = new System.Drawing.Size(48, 17);
            this.radioPngFormat.TabIndex = 9;
            this.radioPngFormat.TabStop = true;
            this.radioPngFormat.Text = "PNG";
            this.radioPngFormat.UseVisualStyleBackColor = true;
            this.radioPngFormat.CheckedChanged += new System.EventHandler(this.formatRadio_CheckedChanged);
            // 
            // radioJpegFormat
            // 
            this.radioJpegFormat.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.radioJpegFormat.AutoSize = true;
            this.radioJpegFormat.Location = new System.Drawing.Point(61, 7);
            this.radioJpegFormat.Name = "radioJpegFormat";
            this.radioJpegFormat.Size = new System.Drawing.Size(45, 17);
            this.radioJpegFormat.TabIndex = 10;
            this.radioJpegFormat.TabStop = true;
            this.radioJpegFormat.Text = "JPG";
            this.radioJpegFormat.UseVisualStyleBackColor = true;
            // 
            // ActivateBtn
            // 
            this.ActivateBtn.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.ActivateBtn.Location = new System.Drawing.Point(337, 138);
            this.ActivateBtn.Name = "ActivateBtn";
            this.ActivateBtn.Size = new System.Drawing.Size(95, 23);
            this.ActivateBtn.TabIndex = 3;
            this.ActivateBtn.Text = "Activate";
            this.ActivateBtn.UseVisualStyleBackColor = true;
            this.ActivateBtn.Click += new System.EventHandler(this.disActivateBtn_Click);
            // 
            // openFolder
            // 
            this.openFolder.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.openFolder.Location = new System.Drawing.Point(357, 73);
            this.openFolder.Name = "openFolder";
            this.openFolder.Size = new System.Drawing.Size(75, 23);
            this.openFolder.TabIndex = 1;
            this.openFolder.Text = "Open Folder";
            this.openFolder.UseVisualStyleBackColor = true;
            this.openFolder.Click += new System.EventHandler(this.openFolder_Click);
            // 
            // namePanel
            // 
            this.namePanel.BackColor = System.Drawing.Color.Transparent;
            this.namePanel.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.namePanel.Controls.Add(this.titleNtimestampRadio);
            this.namePanel.Controls.Add(this.timestampRadio);
            this.namePanel.Controls.Add(this.titleRadio);
            this.namePanel.Location = new System.Drawing.Point(51, 74);
            this.namePanel.Name = "namePanel";
            this.namePanel.Size = new System.Drawing.Size(255, 30);
            this.namePanel.TabIndex = 15;
            // 
            // titleNtimestampRadio
            // 
            this.titleNtimestampRadio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.titleNtimestampRadio.AutoSize = true;
            this.titleNtimestampRadio.Checked = true;
            this.titleNtimestampRadio.Location = new System.Drawing.Point(4, 7);
            this.titleNtimestampRadio.Name = "titleNtimestampRadio";
            this.titleNtimestampRadio.Size = new System.Drawing.Size(102, 17);
            this.titleNtimestampRadio.TabIndex = 4;
            this.titleNtimestampRadio.TabStop = true;
            this.titleNtimestampRadio.Text = "Title+Timestamp";
            this.titleNtimestampRadio.UseVisualStyleBackColor = true;
            this.titleNtimestampRadio.CheckedChanged += new System.EventHandler(this.nameRadio_CheckedChanged);
            // 
            // timestampRadio
            // 
            this.timestampRadio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.timestampRadio.AutoSize = true;
            this.timestampRadio.Location = new System.Drawing.Point(171, 7);
            this.timestampRadio.Name = "timestampRadio";
            this.timestampRadio.Size = new System.Drawing.Size(76, 17);
            this.timestampRadio.TabIndex = 6;
            this.timestampRadio.Text = "Timestamp";
            this.timestampRadio.UseVisualStyleBackColor = true;
            this.timestampRadio.CheckedChanged += new System.EventHandler(this.nameRadio_CheckedChanged);
            // 
            // titleRadio
            // 
            this.titleRadio.Anchor = System.Windows.Forms.AnchorStyles.Left;
            this.titleRadio.AutoSize = true;
            this.titleRadio.Location = new System.Drawing.Point(116, 7);
            this.titleRadio.Name = "titleRadio";
            this.titleRadio.Size = new System.Drawing.Size(45, 17);
            this.titleRadio.TabIndex = 5;
            this.titleRadio.Text = "Title";
            this.titleRadio.UseVisualStyleBackColor = true;
            this.titleRadio.CheckedChanged += new System.EventHandler(this.nameRadio_CheckedChanged);
            // 
            // appendLbl
            // 
            this.appendLbl.AutoSize = true;
            this.appendLbl.Location = new System.Drawing.Point(12, 82);
            this.appendLbl.Name = "appendLbl";
            this.appendLbl.Size = new System.Drawing.Size(41, 13);
            this.appendLbl.TabIndex = 17;
            this.appendLbl.Text = "Name :";
            // 
            // soundLbl
            // 
            this.soundLbl.AutoSize = true;
            this.soundLbl.Location = new System.Drawing.Point(12, 172);
            this.soundLbl.Name = "soundLbl";
            this.soundLbl.Size = new System.Drawing.Size(69, 13);
            this.soundLbl.TabIndex = 20;
            this.soundLbl.Text = "Enable SFX :";
            // 
            // chooseBtn
            // 
            this.chooseBtn.Location = new System.Drawing.Point(193, 169);
            this.chooseBtn.Name = "chooseBtn";
            this.chooseBtn.Size = new System.Drawing.Size(74, 20);
            this.chooseBtn.TabIndex = 12;
            this.chooseBtn.Text = "Choose SFX";
            this.chooseBtn.UseVisualStyleBackColor = true;
            this.chooseBtn.Click += new System.EventHandler(this.chooseBtn_Click);
            // 
            // sfxChkBox
            // 
            this.sfxChkBox.Location = new System.Drawing.Point(83, 171);
            this.sfxChkBox.Name = "sfxChkBox";
            this.sfxChkBox.Size = new System.Drawing.Size(108, 17);
            this.sfxChkBox.TabIndex = 11;
            this.sfxChkBox.Text = "Default.wav";
            this.sfxChkBox.UseVisualStyleBackColor = true;
            this.sfxChkBox.CheckedChanged += new System.EventHandler(this.sfxChkBox_CheckedChanged);
            // 
            // defaultSFXbtn
            // 
            this.defaultSFXbtn.Location = new System.Drawing.Point(269, 169);
            this.defaultSFXbtn.Name = "defaultSFXbtn";
            this.defaultSFXbtn.Size = new System.Drawing.Size(52, 20);
            this.defaultSFXbtn.TabIndex = 13;
            this.defaultSFXbtn.Text = "Default";
            this.defaultSFXbtn.UseVisualStyleBackColor = true;
            this.defaultSFXbtn.Click += new System.EventHandler(this.defaultSFXbtn_Click);
            // 
            // aboutLabel
            // 
            this.aboutLabel.ActiveLinkColor = System.Drawing.Color.Gray;
            this.aboutLabel.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Right)));
            this.aboutLabel.AutoSize = true;
            this.aboutLabel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.aboutLabel.LinkColor = System.Drawing.Color.LightGray;
            this.aboutLabel.Location = new System.Drawing.Point(397, 176);
            this.aboutLabel.Name = "aboutLabel";
            this.aboutLabel.Size = new System.Drawing.Size(35, 13);
            this.aboutLabel.TabIndex = 21;
            this.aboutLabel.TabStop = true;
            this.aboutLabel.Text = "About";
            this.labelTip.SetToolTip(this.aboutLabel, "A.S. (GhostasDead)");
            this.aboutLabel.VisitedLinkColor = System.Drawing.Color.LightGray;
            this.aboutLabel.Click += new System.EventHandler(this.aboutLabel_Click);
            // 
            // PrintSSaver
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(444, 197);
            this.Controls.Add(this.aboutLabel);
            this.Controls.Add(this.defaultSFXbtn);
            this.Controls.Add(this.sfxChkBox);
            this.Controls.Add(this.chooseBtn);
            this.Controls.Add(this.soundLbl);
            this.Controls.Add(this.namePanel);
            this.Controls.Add(this.appendLbl);
            this.Controls.Add(this.openFolder);
            this.Controls.Add(this.ActivateBtn);
            this.Controls.Add(this.formatPanel);
            this.Controls.Add(this.titlePanel);
            this.Controls.Add(this.SSSLoc);
            this.Controls.Add(this.statusLbl);
            this.Controls.Add(this.formatLabel);
            this.Controls.Add(this.saveClipboard);
            this.Controls.Add(this.titleLabel);
            this.Controls.Add(this.locationLabel);
            this.Controls.Add(this.location);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.Name = "PrintSSaver";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "PrintSSaver (Inactive)";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.PrinScrnSaver_FormClosing);
            this.Resize += new System.EventHandler(this.PrintSSaver_Resize);
            this.menuStrip.ResumeLayout(false);
            this.titlePanel.ResumeLayout(false);
            this.titlePanel.PerformLayout();
            this.formatPanel.ResumeLayout(false);
            this.formatPanel.PerformLayout();
            this.namePanel.ResumeLayout(false);
            this.namePanel.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.FolderBrowserDialog screenshotsLocation;
        private System.Windows.Forms.Button location;
        private System.Windows.Forms.Label locationLabel;
        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Button saveClipboard;
        private System.Windows.Forms.Label formatLabel;
        private System.Windows.Forms.NotifyIcon inTray;
        private System.Windows.Forms.Label statusLbl;
        private System.Windows.Forms.Label SSSLoc;
        private System.Windows.Forms.ToolTip labelTip;
        private System.Windows.Forms.RadioButton radioPName;
        private System.Windows.Forms.RadioButton radioWName;
        private System.Windows.Forms.Panel titlePanel;
        private System.Windows.Forms.Panel formatPanel;
        private System.Windows.Forms.RadioButton radioPngFormat;
        private System.Windows.Forms.RadioButton radioJpegFormat;
        private System.Windows.Forms.ContextMenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem deactivate;
        private System.Windows.Forms.ToolStripMenuItem exit;
        private System.Windows.Forms.ToolStripSeparator toolTipSeparator;
        private System.Windows.Forms.Button ActivateBtn;
        private System.Windows.Forms.Button openFolder;
        private System.Windows.Forms.ToolStripMenuItem openFolderItem;
        private System.Windows.Forms.ToolStripMenuItem saveClipboardItem;
        private System.Windows.Forms.Panel namePanel;
        private System.Windows.Forms.RadioButton titleNtimestampRadio;
        private System.Windows.Forms.RadioButton timestampRadio;
        private System.Windows.Forms.RadioButton titleRadio;
        private System.Windows.Forms.Label appendLbl;
        private System.Windows.Forms.Label soundLbl;
        private System.Windows.Forms.Button chooseBtn;
        private System.Windows.Forms.CheckBox sfxChkBox;
        private System.Windows.Forms.Button defaultSFXbtn;
        private System.Windows.Forms.LinkLabel aboutLabel;
    }
}

