namespace CyberShield_V3
{
    partial class DashboardHome
    {
        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Component Designer generated code

        private void InitializeComponent()
        {
            components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DashboardHome));
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges1 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges2 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges5 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges6 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges7 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges8 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges9 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges10 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges11 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges12 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges13 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges14 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            timer1 = new System.Windows.Forms.Timer(components);
            guna2Panel1 = new Guna.UI2.WinForms.Guna2Panel();
            pictureBox1 = new PictureBox();
            label4 = new Label();
            label3 = new Label();
            btnQuickScan = new Guna.UI2.WinForms.Guna2Button();
            btnUpdate = new Guna.UI2.WinForms.Guna2Button();
            panelStatus = new Guna.UI2.WinForms.Guna2Panel();
            lblCloudStatus = new Label();
            label8 = new Label();
            lblThreatCount = new Label();
            label7 = new Label();
            lblDbVersion = new Label();
            label6 = new Label();
            lblLastScan = new Label();
            label5 = new Label();
            panelCpu = new Guna.UI2.WinForms.Guna2Panel();
            lblCpuStatus = new Label();
            cpuProgressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            lblCpuValue = new Label();
            label1 = new Label();
            panelRam = new Guna.UI2.WinForms.Guna2Panel();
            lblRamStatus = new Label();
            ramProgressBar = new Guna.UI2.WinForms.Guna2ProgressBar();
            lblRamValue = new Label();
            label9 = new Label();
            guna2Panel1.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).BeginInit();
            panelStatus.SuspendLayout();
            panelCpu.SuspendLayout();
            panelRam.SuspendLayout();
            SuspendLayout();
            // 
            // timer1
            // 
            timer1.Enabled = true;
            timer1.Interval = 1000;
            timer1.Tick += timer1_Tick;
            // 
            // guna2Panel1
            // 
            guna2Panel1.BackColor = Color.Transparent;
            guna2Panel1.BorderColor = Color.FromArgb(16, 185, 129);
            guna2Panel1.BorderRadius = 6;
            guna2Panel1.BorderThickness = 1;
            guna2Panel1.Controls.Add(pictureBox1);
            guna2Panel1.Controls.Add(label4);
            guna2Panel1.Controls.Add(label3);
            guna2Panel1.CustomizableEdges = customizableEdges1;
            guna2Panel1.FillColor = Color.FromArgb(40, 16, 185, 129);
            guna2Panel1.ForeColor = Color.Transparent;
            guna2Panel1.Location = new Point(34, 25);
            guna2Panel1.Name = "guna2Panel1";
            guna2Panel1.ShadowDecoration.CustomizableEdges = customizableEdges2;
            guna2Panel1.Size = new Size(656, 100);
            guna2Panel1.TabIndex = 4;
            // 
            // pictureBox1
            // 
            pictureBox1.Image = (Image)resources.GetObject("pictureBox1.Image");
            pictureBox1.Location = new Point(16, 23);
            pictureBox1.Name = "pictureBox1";
            pictureBox1.Size = new Size(50, 50);
            pictureBox1.SizeMode = PictureBoxSizeMode.Zoom;
            pictureBox1.TabIndex = 7;
            pictureBox1.TabStop = false;
            // 
            // label4
            // 
            label4.AutoSize = true;
            label4.Font = new Font("Segoe UI", 9F);
            label4.ForeColor = Color.WhiteSmoke;
            label4.Location = new Point(78, 55);
            label4.Name = "label4";
            label4.Size = new Size(257, 15);
            label4.TabIndex = 6;
            label4.Text = "Real-time shield active. Last check: Just now.";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Font = new Font("Segoe UI", 15.75F, FontStyle.Bold);
            label3.ForeColor = Color.White;
            label3.Location = new Point(75, 23);
            label3.Name = "label3";
            label3.Size = new Size(307, 30);
            label3.TabIndex = 5;
            label3.Text = "System Protected  Optimized";
            // 
            // btnQuickScan
            // 
            btnQuickScan.Animated = true;
            btnQuickScan.BorderRadius = 6;
            btnQuickScan.CustomizableEdges = customizableEdges3;
            btnQuickScan.DisabledState.BorderColor = Color.DarkGray;
            btnQuickScan.DisabledState.CustomBorderColor = Color.DarkGray;
            btnQuickScan.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnQuickScan.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnQuickScan.FillColor = Color.FromArgb(59, 130, 246);
            btnQuickScan.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnQuickScan.ForeColor = Color.White;
            btnQuickScan.Location = new Point(34, 150);
            btnQuickScan.Name = "btnQuickScan";
            btnQuickScan.ShadowDecoration.CustomizableEdges = customizableEdges4;
            btnQuickScan.Size = new Size(160, 45);
            btnQuickScan.TabIndex = 5;
            btnQuickScan.Text = "Quick Scan";
            btnQuickScan.Click += btnQuickScan_Click;
            // 
            // btnUpdate
            // 
            btnUpdate.Animated = true;
            btnUpdate.BorderColor = Color.FromArgb(59, 130, 246);
            btnUpdate.BorderRadius = 6;
            btnUpdate.BorderThickness = 1;
            btnUpdate.CustomizableEdges = customizableEdges5;
            btnUpdate.DisabledState.BorderColor = Color.DarkGray;
            btnUpdate.DisabledState.CustomBorderColor = Color.DarkGray;
            btnUpdate.DisabledState.FillColor = Color.FromArgb(169, 169, 169);
            btnUpdate.DisabledState.ForeColor = Color.FromArgb(141, 141, 141);
            btnUpdate.FillColor = Color.Transparent;
            btnUpdate.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            btnUpdate.ForeColor = Color.White;
            btnUpdate.Location = new Point(210, 150);
            btnUpdate.Name = "btnUpdate";
            btnUpdate.ShadowDecoration.CustomizableEdges = customizableEdges6;
            btnUpdate.Size = new Size(160, 45);
            btnUpdate.TabIndex = 6;
            btnUpdate.Text = "Check Updates";
            btnUpdate.Click += btnUpdate_Click;
            // 
            // panelStatus
            // 
            panelStatus.BackColor = Color.Transparent;
            panelStatus.BorderColor = Color.FromArgb(60, 255, 255, 255);
            panelStatus.BorderRadius = 6;
            panelStatus.BorderThickness = 1;
            panelStatus.Controls.Add(lblCloudStatus);
            panelStatus.Controls.Add(label8);
            panelStatus.Controls.Add(lblThreatCount);
            panelStatus.Controls.Add(label7);
            panelStatus.Controls.Add(lblDbVersion);
            panelStatus.Controls.Add(label6);
            panelStatus.Controls.Add(lblLastScan);
            panelStatus.Controls.Add(label5);
            panelStatus.CustomizableEdges = customizableEdges7;
            panelStatus.FillColor = Color.FromArgb(40, 0, 0, 0);
            panelStatus.Location = new Point(400, 150);
            panelStatus.Name = "panelStatus";
            panelStatus.ShadowDecoration.CustomizableEdges = customizableEdges8;
            panelStatus.Size = new Size(290, 110);
            panelStatus.TabIndex = 7;
            // 
            // lblCloudStatus
            // 
            lblCloudStatus.AutoSize = true;
            lblCloudStatus.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblCloudStatus.ForeColor = Color.Orange;
            lblCloudStatus.Location = new Point(130, 80);
            lblCloudStatus.Name = "lblCloudStatus";
            lblCloudStatus.Size = new Size(69, 15);
            lblCloudStatus.TabIndex = 7;
            lblCloudStatus.Text = "Initializing...";
            // 
            // label8
            // 
            label8.AutoSize = true;
            label8.Font = new Font("Segoe UI", 9F);
            label8.ForeColor = Color.LightGray;
            label8.Location = new Point(15, 80);
            label8.Name = "label8";
            label8.Size = new Size(79, 15);
            label8.TabIndex = 6;
            label8.Text = "Cloud Uplink:";
            // 
            // lblThreatCount
            // 
            lblThreatCount.AutoSize = true;
            lblThreatCount.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblThreatCount.ForeColor = Color.White;
            lblThreatCount.Location = new Point(130, 58);
            lblThreatCount.Name = "lblThreatCount";
            lblThreatCount.Size = new Size(14, 15);
            lblThreatCount.TabIndex = 5;
            lblThreatCount.Text = "0";
            // 
            // label7
            // 
            label7.AutoSize = true;
            label7.Font = new Font("Segoe UI", 9F);
            label7.ForeColor = Color.LightGray;
            label7.Location = new Point(15, 58);
            label7.Name = "label7";
            label7.Size = new Size(103, 15);
            label7.TabIndex = 4;
            label7.Text = "Global Signatures:";
            // 
            // lblDbVersion
            // 
            lblDbVersion.AutoSize = true;
            lblDbVersion.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblDbVersion.ForeColor = Color.FromArgb(16, 185, 129);
            lblDbVersion.Location = new Point(130, 36);
            lblDbVersion.Name = "lblDbVersion";
            lblDbVersion.Size = new Size(52, 15);
            lblDbVersion.TabIndex = 3;
            lblDbVersion.Text = "v.2025.4";
            // 
            // label6
            // 
            label6.AutoSize = true;
            label6.Font = new Font("Segoe UI", 9F);
            label6.ForeColor = Color.LightGray;
            label6.Location = new Point(15, 36);
            label6.Name = "label6";
            label6.Size = new Size(66, 15);
            label6.TabIndex = 2;
            label6.Text = "DB Version:";
            // 
            // lblLastScan
            // 
            lblLastScan.AutoSize = true;
            lblLastScan.Font = new Font("Segoe UI", 9F, FontStyle.Bold);
            lblLastScan.ForeColor = Color.White;
            lblLastScan.Location = new Point(130, 14);
            lblLastScan.Name = "lblLastScan";
            lblLastScan.Size = new Size(39, 15);
            lblLastScan.TabIndex = 1;
            lblLastScan.Text = "Never";
            // 
            // label5
            // 
            label5.AutoSize = true;
            label5.Font = new Font("Segoe UI", 9F);
            label5.ForeColor = Color.LightGray;
            label5.Location = new Point(15, 14);
            label5.Name = "label5";
            label5.Size = new Size(59, 15);
            label5.TabIndex = 0;
            label5.Text = "Last Scan:";
            // 
            // panelCpu
            // 
            panelCpu.BackColor = Color.Transparent;
            panelCpu.BorderColor = Color.FromArgb(60, 255, 255, 255);
            panelCpu.BorderRadius = 10;
            panelCpu.BorderThickness = 1;
            panelCpu.Controls.Add(lblCpuStatus);
            panelCpu.Controls.Add(cpuProgressBar);
            panelCpu.Controls.Add(lblCpuValue);
            panelCpu.Controls.Add(label1);
            panelCpu.CustomizableEdges = customizableEdges9;
            panelCpu.FillColor = Color.FromArgb(50, 30, 41, 59);
            panelCpu.Location = new Point(34, 280);
            panelCpu.Name = "panelCpu";
            panelCpu.ShadowDecoration.CustomizableEdges = customizableEdges10;
            panelCpu.Size = new Size(310, 120);
            panelCpu.TabIndex = 8;
            // 
            // lblCpuStatus
            // 
            lblCpuStatus.AutoSize = true;
            lblCpuStatus.Font = new Font("Segoe UI", 9F);
            lblCpuStatus.ForeColor = Color.Silver;
            lblCpuStatus.Location = new Point(20, 85);
            lblCpuStatus.Name = "lblCpuStatus";
            lblCpuStatus.Size = new Size(50, 15);
            lblCpuStatus.TabIndex = 3;
            lblCpuStatus.Text = "Optimal";
            // 
            // cpuProgressBar
            // 
            cpuProgressBar.BorderRadius = 5;
            cpuProgressBar.CustomizableEdges = customizableEdges11;
            cpuProgressBar.FillColor = Color.FromArgb(30, 33, 57);
            cpuProgressBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            cpuProgressBar.Location = new Point(20, 55);
            cpuProgressBar.Name = "cpuProgressBar";
            cpuProgressBar.ProgressColor = Color.FromArgb(59, 130, 246);
            cpuProgressBar.ProgressColor2 = Color.Violet;
            cpuProgressBar.ShadowDecoration.CustomizableEdges = customizableEdges12;
            cpuProgressBar.Size = new Size(270, 15);
            cpuProgressBar.TabIndex = 2;
            cpuProgressBar.Text = "guna2ProgressBar1";
            cpuProgressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // lblCpuValue
            // 
            lblCpuValue.AutoSize = true;
            lblCpuValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblCpuValue.ForeColor = Color.White;
            lblCpuValue.Location = new Point(15, 5);
            lblCpuValue.Name = "lblCpuValue";
            lblCpuValue.Size = new Size(65, 45);
            lblCpuValue.TabIndex = 1;
            lblCpuValue.Text = "0%";
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label1.ForeColor = Color.FromArgb(59, 130, 246);
            label1.Location = new Point(255, 15);
            label1.Name = "label1";
            label1.Size = new Size(37, 19);
            label1.TabIndex = 0;
            label1.Text = "CPU";
            // 
            // panelRam
            // 
            panelRam.BackColor = Color.Transparent;
            panelRam.BorderColor = Color.FromArgb(60, 255, 255, 255);
            panelRam.BorderRadius = 10;
            panelRam.BorderThickness = 1;
            panelRam.Controls.Add(lblRamStatus);
            panelRam.Controls.Add(ramProgressBar);
            panelRam.Controls.Add(lblRamValue);
            panelRam.Controls.Add(label9);
            panelRam.CustomizableEdges = customizableEdges13;
            panelRam.FillColor = Color.FromArgb(50, 30, 41, 59);
            panelRam.Location = new Point(380, 280);
            panelRam.Name = "panelRam";
            panelRam.ShadowDecoration.CustomizableEdges = customizableEdges14;
            panelRam.Size = new Size(310, 120);
            panelRam.TabIndex = 9;
            // 
            // lblRamStatus
            // 
            lblRamStatus.AutoSize = true;
            lblRamStatus.Font = new Font("Segoe UI", 9F);
            lblRamStatus.ForeColor = Color.Silver;
            lblRamStatus.Location = new Point(20, 85);
            lblRamStatus.Name = "lblRamStatus";
            lblRamStatus.Size = new Size(69, 15);
            lblRamStatus.TabIndex = 3;
            lblRamStatus.Text = "4 GB / 16 GB";
            // 
            // ramProgressBar
            // 
            ramProgressBar.BorderRadius = 5;
            ramProgressBar.CustomizableEdges = customizableEdges11;
            ramProgressBar.FillColor = Color.FromArgb(30, 33, 57);
            ramProgressBar.GradientMode = System.Drawing.Drawing2D.LinearGradientMode.Horizontal;
            ramProgressBar.Location = new Point(20, 55);
            ramProgressBar.Name = "ramProgressBar";
            ramProgressBar.ProgressColor = Color.Cyan;
            ramProgressBar.ProgressColor2 = Color.Teal;
            ramProgressBar.ShadowDecoration.CustomizableEdges = customizableEdges12;
            ramProgressBar.Size = new Size(270, 15);
            ramProgressBar.TabIndex = 2;
            ramProgressBar.Text = "guna2ProgressBar2";
            ramProgressBar.TextRenderingHint = System.Drawing.Text.TextRenderingHint.SystemDefault;
            // 
            // lblRamValue
            // 
            lblRamValue.AutoSize = true;
            lblRamValue.Font = new Font("Segoe UI", 24F, FontStyle.Bold);
            lblRamValue.ForeColor = Color.White;
            lblRamValue.Location = new Point(15, 5);
            lblRamValue.Name = "lblRamValue";
            lblRamValue.Size = new Size(65, 45);
            lblRamValue.TabIndex = 1;
            lblRamValue.Text = "0%";
            // 
            // label9
            // 
            label9.AutoSize = true;
            label9.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            label9.ForeColor = Color.Cyan;
            label9.Location = new Point(255, 15);
            label9.Name = "label9";
            label9.Size = new Size(42, 19);
            label9.TabIndex = 0;
            label9.Text = "RAM";
            // 
            // DashboardHome
            // 
            AutoScaleDimensions = new SizeF(7F, 15F);
            AutoScaleMode = AutoScaleMode.Font;
            BackColor = Color.FromArgb(30, 33, 57);
            Controls.Add(panelRam);
            Controls.Add(panelCpu);
            Controls.Add(panelStatus);
            Controls.Add(btnUpdate);
            Controls.Add(btnQuickScan);
            Controls.Add(guna2Panel1);
            Name = "DashboardHome";
            Size = new Size(725, 442);
            guna2Panel1.ResumeLayout(false);
            guna2Panel1.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)pictureBox1).EndInit();
            panelStatus.ResumeLayout(false);
            panelStatus.PerformLayout();
            panelCpu.ResumeLayout(false);
            panelCpu.PerformLayout();
            panelRam.ResumeLayout(false);
            panelRam.PerformLayout();
            ResumeLayout(false);
        }

        #endregion

        private System.Windows.Forms.Timer timer1;
        private Guna.UI2.WinForms.Guna2Panel guna2Panel1;
        private Label label3;
        private Label label4;
        private PictureBox pictureBox1;
        private Guna.UI2.WinForms.Guna2Button btnQuickScan;
        private Guna.UI2.WinForms.Guna2Button btnUpdate;
        private Guna.UI2.WinForms.Guna2Panel panelStatus;
        private Label label5;
        private Label lblLastScan;
        private Label lblDbVersion;
        private Label label6;
        private Label lblCloudStatus;
        private Label label8;
        private Label lblThreatCount;
        private Label label7;
        private Guna.UI2.WinForms.Guna2Panel panelCpu;
        private Guna.UI2.WinForms.Guna2ProgressBar cpuProgressBar;
        private Label lblCpuValue;
        private Label label1;
        private Label lblCpuStatus;
        private Guna.UI2.WinForms.Guna2Panel panelRam;
        private Label lblRamStatus;
        private Guna.UI2.WinForms.Guna2ProgressBar ramProgressBar;
        private Label lblRamValue;
        private Label label9;
    }
}