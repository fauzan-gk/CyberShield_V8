namespace CyberShield_V3
{
    partial class ScanPanel
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            mainPanel = new Panel();
            cardCloud = new Panel();
            lblCloudStatus = new Label();
            lblCloudHeader = new Label();
            cardProtection = new Panel();
            lblProtectionStatus = new Label();
            lblProtectionHeader = new Label();
            cardFiles = new Panel();
            filesScannedLabel = new Label();
            lblFileHeader = new Label();
            cardThreats = new Panel();
            threatsFoundLabel = new Label();
            lblThreatHeader = new Label();
            currentFileLabel = new Label();
            statusLabel = new Label();
            scanningHeaderLabel = new Label();
            scanCircularBar = new Guna.UI2.WinForms.Guna2CircleProgressBar();
            progressLabel = new Label();
            cancelButton = new Guna.UI2.WinForms.Guna2Button();
            backButton = new Guna.UI2.WinForms.Guna2Button();
            lblThreatIcon = new Label();
            lblFileIcon = new Label();
            mainPanel.SuspendLayout();
            cardCloud.SuspendLayout();
            cardProtection.SuspendLayout();
            cardFiles.SuspendLayout();
            cardThreats.SuspendLayout();
            scanCircularBar.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.BackColor = Color.FromArgb(30, 33, 57);
            mainPanel.Controls.Add(cardCloud);
            mainPanel.Controls.Add(cardProtection);
            mainPanel.Controls.Add(cardFiles);
            mainPanel.Controls.Add(cardThreats);
            mainPanel.Controls.Add(currentFileLabel);
            mainPanel.Controls.Add(statusLabel);
            mainPanel.Controls.Add(scanningHeaderLabel);
            mainPanel.Controls.Add(scanCircularBar);
            mainPanel.Controls.Add(cancelButton);
            mainPanel.Controls.Add(backButton);
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Size = new Size(725, 442);
            mainPanel.TabIndex = 0;
            // 
            // cardCloud
            // 
            cardCloud.BackColor = Color.FromArgb(40, 44, 70);
            cardCloud.Controls.Add(lblCloudStatus);
            cardCloud.Controls.Add(lblCloudHeader);
            cardCloud.Location = new Point(63, 191);
            cardCloud.Name = "cardCloud";
            cardCloud.Size = new Size(180, 70);
            cardCloud.TabIndex = 11;
            // 
            // lblCloudStatus
            // 
            lblCloudStatus.AutoSize = true;
            lblCloudStatus.Font = new Font("Segoe UI", 9F);
            lblCloudStatus.ForeColor = Color.LimeGreen;
            lblCloudStatus.Location = new Point(10, 35);
            lblCloudStatus.Name = "lblCloudStatus";
            lblCloudStatus.Size = new Size(75, 15);
            lblCloudStatus.TabIndex = 0;
            lblCloudStatus.Text = "● Connected";
            // 
            // lblCloudHeader
            // 
            lblCloudHeader.AutoSize = true;
            lblCloudHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCloudHeader.ForeColor = Color.FromArgb(100, 150, 255);
            lblCloudHeader.Location = new Point(10, 10);
            lblCloudHeader.Name = "lblCloudHeader";
            lblCloudHeader.Size = new Size(93, 15);
            lblCloudHeader.TabIndex = 1;
            lblCloudHeader.Text = "CLOUD UPLINK";
            // 
            // cardProtection
            // 
            cardProtection.BackColor = Color.FromArgb(40, 44, 70);
            cardProtection.Controls.Add(lblProtectionStatus);
            cardProtection.Controls.Add(lblProtectionHeader);
            cardProtection.Location = new Point(63, 101);
            cardProtection.Name = "cardProtection";
            cardProtection.Size = new Size(180, 70);
            cardProtection.TabIndex = 10;
            // 
            // lblProtectionStatus
            // 
            lblProtectionStatus.AutoSize = true;
            lblProtectionStatus.Font = new Font("Segoe UI", 9F);
            lblProtectionStatus.ForeColor = Color.LimeGreen;
            lblProtectionStatus.Location = new Point(10, 35);
            lblProtectionStatus.Name = "lblProtectionStatus";
            lblProtectionStatus.Size = new Size(50, 15);
            lblProtectionStatus.TabIndex = 0;
            lblProtectionStatus.Text = "● Active";
            // 
            // lblProtectionHeader
            // 
            lblProtectionHeader.AutoSize = true;
            lblProtectionHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblProtectionHeader.ForeColor = Color.FromArgb(100, 150, 255);
            lblProtectionHeader.Location = new Point(10, 10);
            lblProtectionHeader.Name = "lblProtectionHeader";
            lblProtectionHeader.Size = new Size(114, 15);
            lblProtectionHeader.TabIndex = 1;
            lblProtectionHeader.Text = "REAL-TIME ENGINE";
            // 
            // cardFiles
            // 
            cardFiles.BackColor = Color.FromArgb(40, 44, 70);
            cardFiles.Controls.Add(filesScannedLabel);
            cardFiles.Controls.Add(lblFileHeader);
            cardFiles.Location = new Point(476, 191);
            cardFiles.Name = "cardFiles";
            cardFiles.Size = new Size(180, 70);
            cardFiles.TabIndex = 13;
            // 
            // filesScannedLabel
            // 
            filesScannedLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            filesScannedLabel.ForeColor = Color.LightGray;
            filesScannedLabel.Location = new Point(10, 33);
            filesScannedLabel.Name = "filesScannedLabel";
            filesScannedLabel.Size = new Size(160, 25);
            filesScannedLabel.TabIndex = 0;
            filesScannedLabel.Text = "0";
            filesScannedLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblFileHeader
            // 
            lblFileHeader.AutoSize = true;
            lblFileHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblFileHeader.ForeColor = Color.White;
            lblFileHeader.Location = new Point(10, 10);
            lblFileHeader.Name = "lblFileHeader";
            lblFileHeader.Size = new Size(94, 15);
            lblFileHeader.TabIndex = 1;
            lblFileHeader.Text = "FILES SCANNED";
            // 
            // cardThreats
            // 
            cardThreats.BackColor = Color.FromArgb(40, 44, 70);
            cardThreats.Controls.Add(threatsFoundLabel);
            cardThreats.Controls.Add(lblThreatHeader);
            cardThreats.Location = new Point(476, 101);
            cardThreats.Name = "cardThreats";
            cardThreats.Size = new Size(180, 70);
            cardThreats.TabIndex = 12;
            // 
            // threatsFoundLabel
            // 
            threatsFoundLabel.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            threatsFoundLabel.ForeColor = Color.White;
            threatsFoundLabel.Location = new Point(10, 30);
            threatsFoundLabel.Name = "threatsFoundLabel";
            threatsFoundLabel.Size = new Size(160, 35);
            threatsFoundLabel.TabIndex = 0;
            threatsFoundLabel.Text = "0";
            threatsFoundLabel.TextAlign = ContentAlignment.MiddleRight;
            // 
            // lblThreatHeader
            // 
            lblThreatHeader.AutoSize = true;
            lblThreatHeader.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblThreatHeader.ForeColor = Color.FromArgb(220, 50, 50);
            lblThreatHeader.Location = new Point(10, 10);
            lblThreatHeader.Name = "lblThreatHeader";
            lblThreatHeader.Size = new Size(103, 15);
            lblThreatHeader.TabIndex = 1;
            lblThreatHeader.Text = "THREATS FOUND";
            // 
            // currentFileLabel
            // 
            currentFileLabel.Font = new Font("Segoe UI", 8F);
            currentFileLabel.ForeColor = Color.DimGray;
            currentFileLabel.Location = new Point(238, 296);
            currentFileLabel.Name = "currentFileLabel";
            currentFileLabel.Size = new Size(243, 20);
            currentFileLabel.TabIndex = 14;
            currentFileLabel.Text = "Waiting...";
            currentFileLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // statusLabel
            // 
            statusLabel.Font = new Font("Segoe UI", 10F);
            statusLabel.ForeColor = Color.FromArgb(100, 150, 255);
            statusLabel.Location = new Point(238, 271);
            statusLabel.Name = "statusLabel";
            statusLabel.Size = new Size(243, 25);
            statusLabel.TabIndex = 2;
            statusLabel.Text = "Initializing...";
            statusLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // scanningHeaderLabel
            // 
            scanningHeaderLabel.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold, GraphicsUnit.Point, 0);
            scanningHeaderLabel.ForeColor = Color.Gray;
            scanningHeaderLabel.Location = new Point(279, 52);
            scanningHeaderLabel.Name = "scanningHeaderLabel";
            scanningHeaderLabel.Size = new Size(160, 25);
            scanningHeaderLabel.TabIndex = 15;
            scanningHeaderLabel.Text = "SCANNING...";
            scanningHeaderLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // scanCircularBar
            // 
            scanCircularBar.Animated = true;
            scanCircularBar.BackColor = Color.Transparent;
            scanCircularBar.Controls.Add(progressLabel);
            scanCircularBar.FillColor = Color.FromArgb(40, 44, 70);
            scanCircularBar.FillThickness = 12;
            scanCircularBar.Font = new Font("Segoe UI", 12F);
            scanCircularBar.ForeColor = Color.White;
            scanCircularBar.Location = new Point(279, 101);
            scanCircularBar.Minimum = 0;
            scanCircularBar.Name = "scanCircularBar";
            scanCircularBar.ProgressColor = Color.FromArgb(100, 150, 255);
            scanCircularBar.ProgressColor2 = Color.FromArgb(100, 200, 255);
            scanCircularBar.ProgressThickness = 12;
            scanCircularBar.ShadowDecoration.CustomizableEdges = customizableEdges1;
            scanCircularBar.ShadowDecoration.Mode = Guna.UI2.WinForms.Enums.ShadowMode.Circle;
            scanCircularBar.Size = new Size(160, 160);
            scanCircularBar.TabIndex = 0;
            // 
            // progressLabel
            // 
            progressLabel.BackColor = Color.Transparent;
            progressLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            progressLabel.ForeColor = Color.White;
            progressLabel.Location = new Point(3, 54);
            progressLabel.Name = "progressLabel";
            progressLabel.Size = new Size(160, 50);
            progressLabel.TabIndex = 0;
            progressLabel.Text = "0%";
            progressLabel.TextAlign = ContentAlignment.MiddleCenter;
            // 
            // cancelButton
            // 
            cancelButton.BorderRadius = 5;
            cancelButton.CustomizableEdges = customizableEdges2;
            cancelButton.FillColor = Color.FromArgb(220, 50, 50);
            cancelButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            cancelButton.ForeColor = Color.White;
            cancelButton.Location = new Point(556, 346);
            cancelButton.Name = "cancelButton";
            cancelButton.ShadowDecoration.CustomizableEdges = customizableEdges3;
            cancelButton.Size = new Size(100, 35);
            cancelButton.TabIndex = 16;
            cancelButton.Text = "STOP";
            cancelButton.Click += CancelButton_Click;
            // 
            // backButton
            // 
            backButton.BorderRadius = 5;
            backButton.CustomizableEdges = customizableEdges4;
            backButton.FillColor = Color.FromArgb(100, 150, 255);
            backButton.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            backButton.ForeColor = Color.White;
            backButton.Location = new Point(556, 346);
            backButton.Name = "backButton";
            backButton.ShadowDecoration.CustomizableEdges = customizableEdges5;
            backButton.Size = new Size(100, 35);
            backButton.TabIndex = 17;
            backButton.Text = "FINISH";
            backButton.Visible = false;
            backButton.Click += BackButton_Click;
            // 
            // lblThreatIcon
            // 
            lblThreatIcon.Location = new Point(0, 0);
            lblThreatIcon.Name = "lblThreatIcon";
            lblThreatIcon.Size = new Size(100, 23);
            lblThreatIcon.TabIndex = 0;
            // 
            // lblFileIcon
            // 
            lblFileIcon.Location = new Point(0, 0);
            lblFileIcon.Name = "lblFileIcon";
            lblFileIcon.Size = new Size(100, 23);
            lblFileIcon.TabIndex = 0;
            // 
            // ScanPanel
            // 
            BackColor = Color.FromArgb(30, 33, 57);
            Controls.Add(mainPanel);
            Name = "ScanPanel";
            Size = new Size(725, 442);
            mainPanel.ResumeLayout(false);
            cardCloud.ResumeLayout(false);
            cardCloud.PerformLayout();
            cardProtection.ResumeLayout(false);
            cardProtection.PerformLayout();
            cardFiles.ResumeLayout(false);
            cardFiles.PerformLayout();
            cardThreats.ResumeLayout(false);
            cardThreats.PerformLayout();
            scanCircularBar.ResumeLayout(false);
            ResumeLayout(false);
        }

        #endregion

        // Existing Logic Controls
        private System.Windows.Forms.Panel mainPanel;
        private Guna.UI2.WinForms.Guna2CircleProgressBar scanCircularBar;
        private System.Windows.Forms.Label progressLabel;
        private System.Windows.Forms.Label statusLabel;
        private System.Windows.Forms.Label currentFileLabel;
        private System.Windows.Forms.Label filesScannedLabel;
        private System.Windows.Forms.Label threatsFoundLabel;
        private Guna.UI2.WinForms.Guna2Button cancelButton;
        private Guna.UI2.WinForms.Guna2Button backButton;

        // New Visual Controls
        private System.Windows.Forms.Label scanningHeaderLabel;
        private System.Windows.Forms.Panel cardThreats;
        private System.Windows.Forms.Label lblThreatHeader;
        private System.Windows.Forms.Label lblThreatIcon;
        private System.Windows.Forms.Panel cardFiles;
        private System.Windows.Forms.Label lblFileHeader;
        private System.Windows.Forms.Label lblFileIcon;
        private System.Windows.Forms.Panel cardProtection;
        private System.Windows.Forms.Label lblProtectionHeader;
        private System.Windows.Forms.Label lblProtectionStatus;
        private System.Windows.Forms.Panel cardCloud;
        private System.Windows.Forms.Label lblCloudHeader;
        private System.Windows.Forms.Label lblCloudStatus;
    }
}