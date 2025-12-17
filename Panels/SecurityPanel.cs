using CyberShield_V3.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;

namespace CyberShield_V4.Panels
{
    public partial class SecurityPanel : UserControl
    {
        private List<ThreatInfo> _threatHistory;

        // Event to notify Form1 when protection settings change
        public event EventHandler<bool> ProtectionToggled;

        public SecurityPanel()
        {
            InitializeComponent();
            SetupDataGridView();
        }

        private void SetupDataGridView()
        {
            // Styling the Guna2DataGridView for a dark theme
            dgvThreats.AlternatingRowsDefaultCellStyle.BackColor = Color.FromArgb(33, 37, 41);
            dgvThreats.BackgroundColor = Color.FromArgb(30, 33, 57);
            dgvThreats.ColumnHeadersDefaultCellStyle.BackColor = Color.FromArgb(16, 185, 129);
            dgvThreats.ColumnHeadersDefaultCellStyle.ForeColor = Color.White;
            dgvThreats.DefaultCellStyle.BackColor = Color.FromArgb(30, 33, 57);
            dgvThreats.DefaultCellStyle.ForeColor = Color.White;
        }

        public void RefreshSecurityData(List<ThreatInfo> threats, bool isProtectionActive)
        {
            _threatHistory = threats;

            // Update UI Stats
            lblThreatCount.Text = threats.Count.ToString();

            toggleRealTime.Checked = isProtectionActive;

            // Update DataGrid
            dgvThreats.Rows.Clear();
            foreach (var threat in threats)
            {
                dgvThreats.Rows.Add(
                    threat.DetectedTime.ToShortDateString(),
                    threat.ThreatName,
                    threat.Severity.ToString(),
                    threat.IsQuarantined ? "Quarantined" : "Deleted"
                );
            }
        }
        public void AddThreatRealTime(ThreatInfo threat)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action(() => AddThreatRealTime(threat)));
                return;
            }

            // Add to the top of the grid (index 0) so the user sees it immediately
            dgvThreats.Rows.Insert(0,
                threat.DetectedTime.ToShortTimeString(),
                threat.ThreatName,
                threat.Severity.ToString(),
                threat.IsQuarantined ? "Quarantined" : "Action Required"
            );

            // Update the counter label
            if (int.TryParse(lblThreatCount.Text, out int currentCount))
            {
                lblThreatCount.Text = (currentCount + 1).ToString();
            }
        }
        private void toggleRealTime_CheckedChanged(object sender, EventArgs e)
        {
            ProtectionToggled?.Invoke(this, toggleRealTime.Checked);
        }
    }
}