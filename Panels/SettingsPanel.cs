using System;
using System.Drawing;
using System.Windows.Forms;
using System.Net.Http;
using System.IO;
using System.Collections.Generic;
using System.Text.RegularExpressions; // Add this at the top

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

        // Helper function to download an index and its dependencies
        private async Task DownloadRulesFromIndex(HttpClient client, string baseUrl, string indexFileName, string localSavePath)
        {
            try
            {
                // 1. Construct the full URL for the index file
                string indexUrl = $"{baseUrl}/{indexFileName}";

                // 2. Download the Index File content
                // (This gets the text list of all other files we need)
                string indexContent = await client.GetStringAsync(indexUrl);

                // 3. Save the Index File locally
                string localIndexDetails = Path.Combine(localSavePath, indexFileName);
                await File.WriteAllTextAsync(localIndexDetails, indexContent);

                // 4. Parse the content for "include" directives
                // This Regex finds lines like: include "./malware/APT_Backspace.yar"
                // It handles both forward slashes (/) and backslashes (\)
                var matches = Regex.Matches(indexContent, "include\\s+\"\\./?([^\"]+)\"");

                foreach (Match match in matches)
                {
                    // Extract the filename (e.g., "malware/APT_Backspace.yar")
                    string childFileName = match.Groups[1].Value;

                    // Construct the full local path where we want to save it
                    string localChildPath = Path.Combine(localSavePath, childFileName);

                    // Construct the web URL to download it from
                    // We use Replace to ensure web URLs always use forward slashes
                    string childUrl = $"{baseUrl}/{childFileName}".Replace("\\", "/");

                    try
                    {
                        // === THE FIX: Create the subdirectory if it doesn't exist ===
                        string directoryPath = Path.GetDirectoryName(localChildPath);
                        if (!string.IsNullOrEmpty(directoryPath) && !Directory.Exists(directoryPath))
                        {
                            Directory.CreateDirectory(directoryPath);
                        }

                        // Download the child file
                        string childContent = await client.GetStringAsync(childUrl);

                        // Save it
                        await File.WriteAllTextAsync(localChildPath, childContent);
                    }
                    catch (Exception ex)
                    {
                        // Log error but continue downloading other files
                        // This prevents one bad link from stopping the whole update
                        System.Diagnostics.Debug.WriteLine($"Failed to download {childFileName}: {ex.Message}");
                    }
                }
            }
            catch (Exception ex)
            {
                // This catches errors with the main index file itself
                MessageBox.Show($"Failed to download index file: {ex.Message}");
                throw; // Re-throw so the button click knows something went wrong
            }
        }

        private async void btnUpdateRules_Click(object sender, EventArgs e)
        {
            btnUpdateRules.Enabled = false;
            lblUpdateStatus.Text = "Downloading definitions...";

            // 1. Define the Base URL (The folder where the files live)
            // We remove the filename so we can append other filenames to it later.
            string baseUrl = "https://raw.githubusercontent.com/Yara-Rules/rules/master";
            // 2. The main file that lists all other files
            string indexFile = "malware_index.yar";
            try
            {
                System.Net.ServicePointManager.SecurityProtocol = System.Net.SecurityProtocolType.Tls12 | System.Net.SecurityProtocolType.Tls13;

                using (HttpClient client = new HttpClient())
                {
                    client.DefaultRequestHeaders.Add("User-Agent", "CyberShield-Antivirus-Project");

                    string rulesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rules");
                    if (!Directory.Exists(rulesPath)) Directory.CreateDirectory(rulesPath);

                    // CALL THE HELPER FUNCTION
                    await DownloadRulesFromIndex(client, baseUrl, indexFile, rulesPath);

                    lblUpdateStatus.Text = $"Updated: {DateTime.Now.ToShortTimeString()}";
                    MessageBox.Show("All malware rules and dependencies downloaded!", "Update Complete");
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Update Error: {ex.Message}");
            }
            finally
            {
                btnUpdateRules.Enabled = true;
            }
        }

        private void label3_Click(object sender, EventArgs e) { }
        private void guna2Panel2_Paint(object sender, PaintEventArgs e) { }
    }
}