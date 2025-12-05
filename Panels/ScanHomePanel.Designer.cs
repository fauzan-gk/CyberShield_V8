using CyberShield_V3.Controls;

namespace CyberShield_V3
{
    partial class ScanHomePanel
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
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            titleLabel = new Label();
            subTitleLabel = new Label();
            hoverButton1 = new HoverButton();
            btnDeepScan = new Guna.UI2.WinForms.Guna2Button();
            panelLastScan = new Panel();
            lblLastScanValue = new Label();
            lblLastScanTitle = new Label();
            panelDatabase = new Panel();
            lblDatabaseValue = new Label();
            lblDatabaseTitle = new Label();
            panelEngine = new Panel();
            lblEngineValue = new Label();
            lblEngineTitle = new Label();
            panelLastScan.SuspendLayout();
            panelDatabase.SuspendLayout();
            panelEngine.SuspendLayout();
            SuspendLayout();
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(30, 15);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(213, 37);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Security Center";
            // 
            // subTitleLabel
            // 
            subTitleLabel.AutoSize = true;
            subTitleLabel.Font = new Font("Segoe UI", 10F);
            subTitleLabel.ForeColor = Color.Gray;
            subTitleLabel.Location = new Point(34, 52);
            subTitleLabel.Name = "subTitleLabel";
            subTitleLabel.Size = new Size(231, 19);
            subTitleLabel.TabIndex = 1;
            subTitleLabel.Text = "System integrity verification module.";
            // 
            // hoverButton1
            // 
            hoverButton1.BackColor = Color.FromArgb(30, 33, 57);
            hoverButton1.BigStrokeEndColor = Color.FromArgb(30, 33, 60);
            hoverButton1.BigStrokeStartColor = Color.FromArgb(40, 44, 70);
            hoverButton1.BigStrokeThickness = 20;
            hoverButton1.CenterColor1 = Color.FromArgb(50, 60, 90);
            hoverButton1.CenterColor2 = Color.FromArgb(20, 22, 40);
            hoverButton1.CenterText = "SCAN";
            hoverButton1.CenterTextColor = Color.White;
            hoverButton1.Font = new Font("Segoe UI", 16F, FontStyle.Bold);
            hoverButton1.HoverImage = null;
            hoverButton1.Location = new Point(283, 81);
            hoverButton1.Name = "hoverButton1";
            hoverButton1.RotatingStrokeEndColor = Color.FromArgb(0, 100, 200);
            hoverButton1.RotatingStrokeRadius = 10;
            hoverButton1.RotatingStrokeStartColor = Color.FromArgb(0, 200, 255);
            hoverButton1.RotationSpeed = 8;
            hoverButton1.Size = new Size(160, 160);
            hoverButton1.SurroundColor1 = Color.FromArgb(30, 33, 57);
            hoverButton1.SurroundColor2 = Color.FromArgb(30, 33, 57);
            hoverButton1.SurroundStrokeColor = Color.FromArgb(30, 33, 57);
            hoverButton1.SurroundStrokeThickness = 2;
            hoverButton1.TabIndex = 0;
            hoverButton1.Text = "hoverButton1";
            hoverButton1.Click += BtnQuickScan_Click;
            // 
            // btnDeepScan
            // 
            btnDeepScan.BorderColor = Color.FromArgb(220, 50, 50);
            btnDeepScan.BorderRadius = 15;
            btnDeepScan.BorderThickness = 1;
            btnDeepScan.Cursor = Cursors.Hand;
            btnDeepScan.CustomizableEdges = customizableEdges3;
            btnDeepScan.FillColor = Color.FromArgb(45, 48, 75);
            btnDeepScan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            btnDeepScan.ForeColor = Color.FromArgb(220, 50, 50);
            btnDeepScan.HoverState.FillColor = Color.FromArgb(220, 50, 50);
            btnDeepScan.HoverState.ForeColor = Color.White;
            btnDeepScan.Location = new Point(263, 261);
            btnDeepScan.Name = "btnDeepScan";
            btnDeepScan.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnDeepScan.Size = new Size(200, 40);
            btnDeepScan.TabIndex = 1;
            btnDeepScan.Text = "DEEP SCAN (ALL DRIVES)";
            btnDeepScan.Click += BtnDeepScan_Click;
            // 
            // panelLastScan
            // 
            panelLastScan.BackColor = Color.FromArgb(40, 44, 70);
            panelLastScan.Controls.Add(lblLastScanValue);
            panelLastScan.Controls.Add(lblLastScanTitle);
            panelLastScan.Location = new Point(72, 331);
            panelLastScan.Name = "panelLastScan";
            panelLastScan.Size = new Size(180, 60);
            panelLastScan.TabIndex = 2;
            // 
            // lblLastScanValue
            // 
            lblLastScanValue.AutoSize = true;
            lblLastScanValue.Font = new Font("Segoe UI", 10F);
            lblLastScanValue.ForeColor = Color.White;
            lblLastScanValue.Location = new Point(15, 30);
            lblLastScanValue.Name = "lblLastScanValue";
            lblLastScanValue.Size = new Size(45, 19);
            lblLastScanValue.TabIndex = 0;
            lblLastScanValue.Text = "Never";
            // 
            // lblLastScanTitle
            // 
            lblLastScanTitle.AutoSize = true;
            lblLastScanTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblLastScanTitle.ForeColor = Color.Gray;
            lblLastScanTitle.Location = new Point(15, 12);
            lblLastScanTitle.Name = "lblLastScanTitle";
            lblLastScanTitle.Size = new Size(66, 13);
            lblLastScanTitle.TabIndex = 1;
            lblLastScanTitle.Text = "LAST SCAN";
            // 
            // panelDatabase
            // 
            panelDatabase.BackColor = Color.FromArgb(40, 44, 70);
            panelDatabase.Controls.Add(lblDatabaseValue);
            panelDatabase.Controls.Add(lblDatabaseTitle);
            panelDatabase.Location = new Point(273, 331);
            panelDatabase.Name = "panelDatabase";
            panelDatabase.Size = new Size(180, 60);
            panelDatabase.TabIndex = 3;
            // 
            // lblDatabaseValue
            // 
            lblDatabaseValue.AutoSize = true;
            lblDatabaseValue.Font = new Font("Segoe UI", 10F);
            lblDatabaseValue.ForeColor = Color.LimeGreen;
            lblDatabaseValue.Location = new Point(15, 30);
            lblDatabaseValue.Name = "lblDatabaseValue";
            lblDatabaseValue.Size = new Size(49, 19);
            lblDatabaseValue.TabIndex = 0;
            lblDatabaseValue.Text = "Online";
            // 
            // lblDatabaseTitle
            // 
            lblDatabaseTitle.AutoSize = true;
            lblDatabaseTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblDatabaseTitle.ForeColor = Color.Gray;
            lblDatabaseTitle.Location = new Point(15, 12);
            lblDatabaseTitle.Name = "lblDatabaseTitle";
            lblDatabaseTitle.Size = new Size(96, 13);
            lblDatabaseTitle.TabIndex = 1;
            lblDatabaseTitle.Text = "VIRUS DATABASE";
            // 
            // panelEngine
            // 
            panelEngine.BackColor = Color.FromArgb(40, 44, 70);
            panelEngine.Controls.Add(lblEngineValue);
            panelEngine.Controls.Add(lblEngineTitle);
            panelEngine.Location = new Point(475, 331);
            panelEngine.Name = "panelEngine";
            panelEngine.Size = new Size(180, 60);
            panelEngine.TabIndex = 4;
            // 
            // lblEngineValue
            // 
            lblEngineValue.AutoSize = true;
            lblEngineValue.Font = new Font("Segoe UI", 10F);
            lblEngineValue.ForeColor = Color.LimeGreen;
            lblEngineValue.Location = new Point(15, 30);
            lblEngineValue.Name = "lblEngineValue";
            lblEngineValue.Size = new Size(46, 19);
            lblEngineValue.TabIndex = 0;
            lblEngineValue.Text = "Active";
            // 
            // lblEngineTitle
            // 
            lblEngineTitle.AutoSize = true;
            lblEngineTitle.Font = new Font("Segoe UI", 8F, FontStyle.Bold);
            lblEngineTitle.ForeColor = Color.Gray;
            lblEngineTitle.Location = new Point(15, 12);
            lblEngineTitle.Name = "lblEngineTitle";
            lblEngineTitle.Size = new Size(64, 13);
            lblEngineTitle.TabIndex = 1;
            lblEngineTitle.Text = "REAL-TIME";
            // 
            // ScanHomePanel
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 33, 57);
            Controls.Add(titleLabel);
            Controls.Add(subTitleLabel);
            Controls.Add(hoverButton1);
            Controls.Add(btnDeepScan);
            Controls.Add(panelLastScan);
            Controls.Add(panelDatabase);
            Controls.Add(panelEngine);
            Name = "ScanHomePanel";
            Size = new Size(725, 442);
            panelLastScan.ResumeLayout(false);
            panelLastScan.PerformLayout();
            panelDatabase.ResumeLayout(false);
            panelDatabase.PerformLayout();
            panelEngine.ResumeLayout(false);
            panelEngine.PerformLayout();
            ResumeLayout(false);
            PerformLayout();
        }

        #endregion

        private System.Windows.Forms.Label titleLabel;
        private System.Windows.Forms.Label subTitleLabel;
        private HoverButton hoverButton1;
        private Guna.UI2.WinForms.Guna2Button btnDeepScan;
        private System.Windows.Forms.Panel panelLastScan;
        private System.Windows.Forms.Label lblLastScanTitle;
        public System.Windows.Forms.Label lblLastScanValue;
        private System.Windows.Forms.Panel panelDatabase;
        private System.Windows.Forms.Label lblDatabaseTitle;
        private System.Windows.Forms.Label lblDatabaseValue;
        private System.Windows.Forms.Panel panelEngine;
        private System.Windows.Forms.Label lblEngineTitle;
        private System.Windows.Forms.Label lblEngineValue;
    }
}