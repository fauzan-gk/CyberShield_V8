namespace CyberShield_V4.Panels
{
    partial class SecurityPanel
    {
        private Label label2;

        private System.ComponentModel.IContainer components = null;

        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null)) components.Dispose();
            base.Dispose(disposing);
        }

        private void InitializeComponent()
        {
            DataGridViewCellStyle dataGridViewCellStyle4 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle5 = new DataGridViewCellStyle();
            DataGridViewCellStyle dataGridViewCellStyle6 = new DataGridViewCellStyle();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges3 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            Guna.UI2.WinForms.Suite.CustomizableEdges customizableEdges4 = new Guna.UI2.WinForms.Suite.CustomizableEdges();
            dgvThreats = new Guna.UI2.WinForms.Guna2DataGridView();
            colDate = new DataGridViewTextBoxColumn();
            colName = new DataGridViewTextBoxColumn();
            colSeverity = new DataGridViewTextBoxColumn();
            colAction = new DataGridViewTextBoxColumn();
            label1 = new Label();
            toggleRealTime = new Guna.UI2.WinForms.Guna2ToggleSwitch();
            label2 = new Label();
            lblThreatCount = new Label();
            label5 = new Label();
            titleLabel = new Label();
            ((System.ComponentModel.ISupportInitialize)dgvThreats).BeginInit();
            SuspendLayout();
            // 
            // dgvThreats
            // 
            dataGridViewCellStyle4.BackColor = Color.FromArgb(44, 48, 52);
            dgvThreats.AlternatingRowsDefaultCellStyle = dataGridViewCellStyle4;
            dataGridViewCellStyle5.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle5.BackColor = Color.FromArgb(15, 16, 18);
            dataGridViewCellStyle5.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle5.ForeColor = Color.White;
            dataGridViewCellStyle5.SelectionBackColor = SystemColors.Highlight;
            dataGridViewCellStyle5.SelectionForeColor = SystemColors.HighlightText;
            dataGridViewCellStyle5.WrapMode = DataGridViewTriState.True;
            dgvThreats.ColumnHeadersDefaultCellStyle = dataGridViewCellStyle5;
            dgvThreats.Columns.AddRange(new DataGridViewColumn[] { colDate, colName, colSeverity, colAction });
            dataGridViewCellStyle6.Alignment = DataGridViewContentAlignment.MiddleLeft;
            dataGridViewCellStyle6.BackColor = Color.FromArgb(33, 37, 41);
            dataGridViewCellStyle6.Font = new Font("Segoe UI", 9F);
            dataGridViewCellStyle6.ForeColor = Color.White;
            dataGridViewCellStyle6.SelectionBackColor = Color.FromArgb(114, 117, 119);
            dataGridViewCellStyle6.SelectionForeColor = Color.White;
            dataGridViewCellStyle6.WrapMode = DataGridViewTriState.False;
            dgvThreats.DefaultCellStyle = dataGridViewCellStyle6;
            dgvThreats.GridColor = Color.FromArgb(50, 56, 62);
            dgvThreats.Location = new Point(28, 101);
            dgvThreats.Name = "dgvThreats";
            dgvThreats.ReadOnly = true;
            dgvThreats.RowHeadersVisible = false;
            dgvThreats.Size = new Size(675, 250);
            dgvThreats.TabIndex = 1;
            dgvThreats.Theme = Guna.UI2.WinForms.Enums.DataGridViewPresetThemes.Dark;
            dgvThreats.ThemeStyle.AlternatingRowsStyle.BackColor = Color.FromArgb(44, 48, 52);
            dgvThreats.ThemeStyle.AlternatingRowsStyle.Font = null;
            dgvThreats.ThemeStyle.AlternatingRowsStyle.ForeColor = Color.Empty;
            dgvThreats.ThemeStyle.AlternatingRowsStyle.SelectionBackColor = Color.Empty;
            dgvThreats.ThemeStyle.AlternatingRowsStyle.SelectionForeColor = Color.Empty;
            dgvThreats.ThemeStyle.BackColor = Color.White;
            dgvThreats.ThemeStyle.GridColor = Color.FromArgb(50, 56, 62);
            dgvThreats.ThemeStyle.HeaderStyle.BackColor = Color.FromArgb(15, 16, 18);
            dgvThreats.ThemeStyle.HeaderStyle.BorderStyle = DataGridViewHeaderBorderStyle.None;
            dgvThreats.ThemeStyle.HeaderStyle.Font = new Font("Segoe UI", 9F);
            dgvThreats.ThemeStyle.HeaderStyle.ForeColor = Color.White;
            dgvThreats.ThemeStyle.HeaderStyle.HeaightSizeMode = DataGridViewColumnHeadersHeightSizeMode.DisableResizing;
            dgvThreats.ThemeStyle.HeaderStyle.Height = 23;
            dgvThreats.ThemeStyle.ReadOnly = true;
            dgvThreats.ThemeStyle.RowsStyle.BackColor = Color.FromArgb(33, 37, 41);
            dgvThreats.ThemeStyle.RowsStyle.BorderStyle = DataGridViewCellBorderStyle.SingleHorizontal;
            dgvThreats.ThemeStyle.RowsStyle.Font = new Font("Segoe UI", 9F);
            dgvThreats.ThemeStyle.RowsStyle.ForeColor = Color.White;
            dgvThreats.ThemeStyle.RowsStyle.Height = 25;
            dgvThreats.ThemeStyle.RowsStyle.SelectionBackColor = Color.FromArgb(114, 117, 119);
            dgvThreats.ThemeStyle.RowsStyle.SelectionForeColor = Color.White;
            // 
            // colDate
            // 
            colDate.HeaderText = "Date";
            colDate.Name = "colDate";
            colDate.ReadOnly = true;
            // 
            // colName
            // 
            colName.HeaderText = "Threat Name";
            colName.Name = "colName";
            colName.ReadOnly = true;
            // 
            // colSeverity
            // 
            colSeverity.HeaderText = "Severity";
            colSeverity.Name = "colSeverity";
            colSeverity.ReadOnly = true;
            // 
            // colAction
            // 
            colAction.HeaderText = "Status";
            colAction.Name = "colAction";
            colAction.ReadOnly = true;
            // 
            // label1
            // 
            label1.Font = new Font("Segoe UI", 12F, FontStyle.Bold, GraphicsUnit.Point, 0);
            label1.ForeColor = Color.White;
            label1.Location = new Point(45, 63);
            label1.Name = "label1";
            label1.Size = new Size(100, 23);
            label1.TabIndex = 2;
            label1.Text = "Real-Time Protection";
            // 
            // toggleRealTime
            // 
            toggleRealTime.CheckedState.FillColor = Color.Lime;
            toggleRealTime.CustomizableEdges = customizableEdges3;
            toggleRealTime.Location = new Point(162, 63);
            toggleRealTime.Name = "toggleRealTime";
            toggleRealTime.ShadowDecoration.CustomizableEdges = customizableEdges4;
            toggleRealTime.Size = new Size(35, 20);
            toggleRealTime.TabIndex = 3;
            toggleRealTime.UncheckedState.FillColor = Color.Transparent;
            toggleRealTime.UncheckedState.InnerBorderColor = Color.White;
            toggleRealTime.CheckedChanged += toggleRealTime_CheckedChanged;
            // 
            // label2
            // 
            label2.Location = new Point(0, 0);
            label2.Name = "label2";
            label2.Size = new Size(100, 23);
            label2.TabIndex = 0;
            // 
            // lblThreatCount
            // 
            lblThreatCount.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            lblThreatCount.ForeColor = Color.FromArgb(16, 185, 129);
            lblThreatCount.Location = new Point(625, 50);
            lblThreatCount.Name = "lblThreatCount";
            lblThreatCount.Size = new Size(100, 37);
            lblThreatCount.TabIndex = 4;
            lblThreatCount.Text = "0";
            // 
            // label5
            // 
            label5.ForeColor = Color.Gray;
            label5.Location = new Point(534, 63);
            label5.Name = "label5";
            label5.Size = new Size(100, 23);
            label5.TabIndex = 5;
            label5.Text = "Threats Blocked";
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(28, 13);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(237, 37);
            titleLabel.TabIndex = 6;
            titleLabel.Text = "Detected Threats";
            // 
            // SecurityPanel
            // 
            BackColor = Color.FromArgb(30, 33, 57);
            Controls.Add(titleLabel);
            Controls.Add(dgvThreats);
            Controls.Add(label1);
            Controls.Add(toggleRealTime);
            Controls.Add(lblThreatCount);
            Controls.Add(label5);
            Name = "SecurityPanel";
            Size = new Size(725, 442);
            ((System.ComponentModel.ISupportInitialize)dgvThreats).EndInit();
            ResumeLayout(false);
            PerformLayout();
        }
        private Guna.UI2.WinForms.Guna2DataGridView dgvThreats;
        private DataGridViewTextBoxColumn colDate;
        private DataGridViewTextBoxColumn colName;
        private DataGridViewTextBoxColumn colSeverity;
        private DataGridViewTextBoxColumn colAction;
        private Guna.UI2.WinForms.Guna2ToggleSwitch toggleRealTime;
        private Label label1;
        private Label lblThreatCount;
        private Label label5;
        private Label titleLabel;
    }
}