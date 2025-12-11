using System;
using System.Drawing;
using System.Windows.Forms;

namespace CyberShield_V3.Panels
{
    public partial class SettingsPanel : UserControl
    {
        // Event to tell Form1 that protection changed
        public event EventHandler<bool> ProtectionToggled;

        public SettingsPanel()
        {
            InitializeComponent();

            // Wire up the new toggle
            if (toggleProtection != null)
            {
                toggleProtection.CheckedChanged += ToggleProtection_CheckedChanged;
            }
        }

        private void ToggleProtection_CheckedChanged(object sender, EventArgs e)
        {
            bool isOn = toggleProtection.Checked;

            // Update local label
            if (lblProtState != null)
            {
                lblProtState.Text = isOn ? "Active" : "Disabled";
                lblProtState.ForeColor = isOn ? Color.White : Color.LightCoral;
            }

            // Update panel color locally (Visual Feedback)
            if (pnlProtection != null)
            {
                pnlProtection.BorderColor = isOn ? Color.FromArgb(100, 150, 255) : Color.Red;
            }

            // Fire event for Form1
            ProtectionToggled?.Invoke(this, isOn);
        }

        private void label3_Click(object sender, EventArgs e) { }
        private void guna2Panel2_Paint(object sender, PaintEventArgs e) { }
    }
}