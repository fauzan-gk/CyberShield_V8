using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;

namespace CyberShield_V3.Panels
{
    public partial class SettingsPanel : UserControl
    {
        // Event to tell Form1 that protection changed
        public event EventHandler<bool> ProtectionToggled;

        public SettingsPanel()
        {
            InitializeComponent();

            // Wire up the toggle
            if (toggleProtection != null)
            {
                toggleProtection.CheckedChanged += ToggleProtection_CheckedChanged;
            }
        }

        private void ToggleProtection_CheckedChanged(object sender, EventArgs e)
        {
            bool isOn = toggleProtection.Checked;

            if (lblProtState != null)
            {
                lblProtState.Text = isOn ? "Active" : "Disabled";
                lblProtState.ForeColor = isOn ? Color.White : Color.LightCoral;
            }

            if (pnlProtection != null)
            {
                pnlProtection.BorderColor = isOn ? Color.FromArgb(100, 150, 255) : Color.Red;
            }

            ProtectionToggled?.Invoke(this, isOn);
        }

        private async void btnUpdateRules_Click(object sender, EventArgs e)
        {
            btnUpdateRules.Enabled = false;
            btnUpdateRules.Text = "Updating...";
            lblUpdateStatus.Text = "Connecting...";

            var ruleUrls = new Dictionary<string, string>
    {
        { "malware_rules.yar", "https://raw.githubusercontent.com/Yara-Rules/rules/master/malware/MALW_Misc.yar" }
    };

            try
            {
                // 1. Ensure modern security protocols are used
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;

                using (HttpClient client = new HttpClient())
                {
                    // 2. CRITICAL: Add a User-Agent header (GitHub rejects requests without this)
                    client.DefaultRequestHeaders.Add("User-Agent", "CyberShield-Antivirus-Project");

                    string rulesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rules");
                    if (!Directory.Exists(rulesPath)) Directory.CreateDirectory(rulesPath);

                    foreach (var rule in ruleUrls)
                    {
                        // This line usually throws the HttpRequestException if User-Agent is missing
                        string content = await client.GetStringAsync(rule.Value);
                        await File.WriteAllTextAsync(Path.Combine(rulesPath, rule.Key), content);
                    }

                    lblUpdateStatus.Text = $"Last updated: {DateTime.Now.ToShortTimeString()}";
                    MessageBox.Show("Rules updated successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);
                }
            }
            catch (HttpRequestException httpEx)
            {
                lblUpdateStatus.Text = "Network Error!";
                MessageBox.Show($"Could not connect to GitHub. Check your internet.\n\nDetails: {httpEx.Message}", "Connection Error");
            }
            catch (Exception ex)
            {
                lblUpdateStatus.Text = "Update failed!";
                MessageBox.Show($"Update Error: {ex.Message}", "Error");
            }
            finally
            {
                btnUpdateRules.Enabled = true;
                btnUpdateRules.Text = "Update YARA Rules";
            }
        }

        private void label3_Click(object sender, EventArgs e) { }
        private void guna2Panel2_Paint(object sender, PaintEventArgs e) { }
    }
}