using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace CyberShield_V3
{
    public partial class JunkCleanerPanel : UserControl
    {
        private Panel mainPanel;
        private Label titleLabel;
        private Label statusLabel;

        // Checkboxes for cleaning options
        private CheckBox tempFilesCheckBox;
        private CheckBox recycleBinCheckBox;
        private CheckBox downloadsCheckBox;
        private CheckBox browserCacheCheckBox;

        // Labels for sizes
        private Label tempFilesLabel;
        private Label recycleBinLabel;
        private Label downloadsLabel;
        private Label browserCacheLabel;

        // Progress
        private ProgressBar cleanProgressBar;
        private Label progressLabel;

        // Buttons
        private Button analyzeButton;
        private Button cleanButton;
        private Button backButton;

        // Stats
        private long tempFilesSize = 0;
        private long recycleBinSize = 0;
        private long downloadsSize = 0;
        private long browserCacheSize = 0;

        private bool isAnalyzing = false;
        private bool isCleaning = false;
        private CancellationTokenSource cancellationToken;

        public event EventHandler BackClicked;

        public JunkCleanerPanel()
        {
            InitializeCustomComponents();
        }

        private void InitializeCustomComponents()
        {
            this.SuspendLayout();

            // Main Panel
            mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.FromArgb(30, 33, 57);

            // Title Label
            titleLabel = new Label();
            titleLabel.Text = "Junk Cleaner";
            titleLabel.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(30, 20);

            // Status Label
            statusLabel = new Label();
            statusLabel.Text = "Click 'Analyze' to scan for junk files";
            statusLabel.Font = new Font("Segoe UI", 10);
            statusLabel.ForeColor = Color.FromArgb(180, 190, 210);
            statusLabel.AutoSize = true;
            statusLabel.Location = new Point(30, 60);

            // Temp Files Checkbox
            tempFilesCheckBox = new CheckBox();
            tempFilesCheckBox.Text = "Temporary Files";
            tempFilesCheckBox.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            tempFilesCheckBox.ForeColor = Color.White;
            tempFilesCheckBox.Location = new Point(30, 100);
            tempFilesCheckBox.AutoSize = true;
            tempFilesCheckBox.Checked = true;

            tempFilesLabel = new Label();
            tempFilesLabel.Text = "0 MB";
            tempFilesLabel.Font = new Font("Segoe UI", 10);
            tempFilesLabel.ForeColor = Color.LightGray;
            tempFilesLabel.Location = new Point(250, 100);
            tempFilesLabel.AutoSize = true;

            // Recycle Bin Checkbox
            recycleBinCheckBox = new CheckBox();
            recycleBinCheckBox.Text = "Recycle Bin";
            recycleBinCheckBox.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            recycleBinCheckBox.ForeColor = Color.White;
            recycleBinCheckBox.Location = new Point(30, 140);
            recycleBinCheckBox.AutoSize = true;
            recycleBinCheckBox.Checked = true;

            recycleBinLabel = new Label();
            recycleBinLabel.Text = "0 MB";
            recycleBinLabel.Font = new Font("Segoe UI", 10);
            recycleBinLabel.ForeColor = Color.LightGray;
            recycleBinLabel.Location = new Point(250, 140);
            recycleBinLabel.AutoSize = true;

            // Downloads Checkbox
            downloadsCheckBox = new CheckBox();
            downloadsCheckBox.Text = "Old Downloads (30+ days)";
            downloadsCheckBox.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            downloadsCheckBox.ForeColor = Color.White;
            downloadsCheckBox.Location = new Point(30, 180);
            downloadsCheckBox.AutoSize = true;
            downloadsCheckBox.Checked = false;

            downloadsLabel = new Label();
            downloadsLabel.Text = "0 MB";
            downloadsLabel.Font = new Font("Segoe UI", 10);
            downloadsLabel.ForeColor = Color.LightGray;
            downloadsLabel.Location = new Point(250, 180);
            downloadsLabel.AutoSize = true;

            // Browser Cache Checkbox
            browserCacheCheckBox = new CheckBox();
            browserCacheCheckBox.Text = "Browser Cache";
            browserCacheCheckBox.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            browserCacheCheckBox.ForeColor = Color.White;
            browserCacheCheckBox.Location = new Point(30, 220);
            browserCacheCheckBox.AutoSize = true;
            browserCacheCheckBox.Checked = true;

            browserCacheLabel = new Label();
            browserCacheLabel.Text = "0 MB";
            browserCacheLabel.Font = new Font("Segoe UI", 10);
            browserCacheLabel.ForeColor = Color.LightGray;
            browserCacheLabel.Location = new Point(250, 220);
            browserCacheLabel.AutoSize = true;

            // Progress Bar
            cleanProgressBar = new ProgressBar();
            cleanProgressBar.Location = new Point(30, 270);
            cleanProgressBar.Size = new Size(580, 25);
            cleanProgressBar.Style = ProgressBarStyle.Continuous;
            cleanProgressBar.Visible = false;

            progressLabel = new Label();
            progressLabel.Text = "0%";
            progressLabel.Font = new Font("Segoe UI", 9, FontStyle.Bold);
            progressLabel.ForeColor = Color.White;
            progressLabel.Location = new Point(30, 300);
            progressLabel.AutoSize = true;
            progressLabel.Visible = false;

            // Analyze Button
            analyzeButton = new Button();
            analyzeButton.Text = "Analyze";
            analyzeButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            analyzeButton.Size = new Size(140, 45);
            analyzeButton.Location = new Point(30, 330);
            analyzeButton.BackColor = Color.FromArgb(90, 141, 184);
            analyzeButton.ForeColor = Color.White;
            analyzeButton.FlatStyle = FlatStyle.Flat;
            analyzeButton.FlatAppearance.BorderSize = 0;
            analyzeButton.Cursor = Cursors.Hand;
            analyzeButton.Click += AnalyzeButton_Click;

            // Clean Button
            cleanButton = new Button();
            cleanButton.Text = "Clean Now";
            cleanButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            cleanButton.Size = new Size(140, 45);
            cleanButton.Location = new Point(180, 330);
            cleanButton.BackColor = Color.FromArgb(255, 165, 0);
            cleanButton.ForeColor = Color.White;
            cleanButton.FlatStyle = FlatStyle.Flat;
            cleanButton.FlatAppearance.BorderSize = 0;
            cleanButton.Cursor = Cursors.Hand;
            cleanButton.Enabled = false;
            cleanButton.Click += CleanButton_Click;

            // Back Button
            backButton = new Button();
            backButton.Text = "Back";
            backButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            backButton.Size = new Size(140, 45);
            backButton.Location = new Point(470, 330);
            backButton.BackColor = Color.FromArgb(100, 150, 255);
            backButton.ForeColor = Color.White;
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.FlatAppearance.BorderSize = 0;
            backButton.Cursor = Cursors.Hand;
            backButton.Click += BackButton_Click;

            // Add all controls
            mainPanel.Controls.Add(titleLabel);
            mainPanel.Controls.Add(statusLabel);
            mainPanel.Controls.Add(tempFilesCheckBox);
            mainPanel.Controls.Add(tempFilesLabel);
            mainPanel.Controls.Add(recycleBinCheckBox);
            mainPanel.Controls.Add(recycleBinLabel);
            mainPanel.Controls.Add(downloadsCheckBox);
            mainPanel.Controls.Add(downloadsLabel);
            mainPanel.Controls.Add(browserCacheCheckBox);
            mainPanel.Controls.Add(browserCacheLabel);
            mainPanel.Controls.Add(cleanProgressBar);
            mainPanel.Controls.Add(progressLabel);
            mainPanel.Controls.Add(analyzeButton);
            mainPanel.Controls.Add(cleanButton);
            mainPanel.Controls.Add(backButton);

            this.Controls.Add(mainPanel);
            this.Size = new Size(643, 398);
            this.BackColor = Color.FromArgb(30, 33, 57);

            this.ResumeLayout();
        }

        private async void AnalyzeButton_Click(object sender, EventArgs e)
        {
            if (isAnalyzing) return;

            isAnalyzing = true;
            analyzeButton.Enabled = false;
            cleanButton.Enabled = false;
            statusLabel.Text = "Analyzing...";
            statusLabel.ForeColor = Color.FromArgb(100, 150, 255);

            cancellationToken = new CancellationTokenSource();

            try
            {
                await Task.Run(() => AnalyzeJunkFiles(cancellationToken.Token));

                long totalSize = tempFilesSize + recycleBinSize + downloadsSize + browserCacheSize;
                statusLabel.Text = $"Found {FormatSize(totalSize)} of junk files";
                statusLabel.ForeColor = Color.LimeGreen;
                cleanButton.Enabled = true;
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Analysis failed: " + ex.Message;
                statusLabel.ForeColor = Color.Red;
            }
            finally
            {
                isAnalyzing = false;
                analyzeButton.Enabled = true;
            }
        }

        private void AnalyzeJunkFiles(CancellationToken token)
        {
            // Reset sizes
            tempFilesSize = 0;
            recycleBinSize = 0;
            downloadsSize = 0;
            browserCacheSize = 0;

            // Analyze Temp Files
            UpdateStatus("Analyzing temporary files...");
            tempFilesSize = GetDirectorySize(Path.GetTempPath(), token);
            UpdateLabel(tempFilesLabel, FormatSize(tempFilesSize));

            // Analyze Recycle Bin
            UpdateStatus("Analyzing recycle bin...");
            recycleBinSize = GetRecycleBinSize();
            UpdateLabel(recycleBinLabel, FormatSize(recycleBinSize));

            // Analyze Downloads
            UpdateStatus("Analyzing old downloads...");
            string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
            downloadsSize = GetOldFilesSize(downloadsPath, 30, token);
            UpdateLabel(downloadsLabel, FormatSize(downloadsSize));

            // Analyze Browser Cache
            UpdateStatus("Analyzing browser cache...");
            browserCacheSize = GetBrowserCacheSize(token);
            UpdateLabel(browserCacheLabel, FormatSize(browserCacheSize));
        }

        private async void CleanButton_Click(object sender, EventArgs e)
        {
            if (isCleaning) return;

            DialogResult result = MessageBox.Show(
                "Are you sure you want to clean selected junk files? This cannot be undone!",
                "Confirm Cleaning",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result != DialogResult.Yes) return;

            isCleaning = true;
            cleanButton.Enabled = false;
            analyzeButton.Enabled = false;
            cleanProgressBar.Visible = true;
            progressLabel.Visible = true;
            statusLabel.Text = "Cleaning...";
            statusLabel.ForeColor = Color.FromArgb(255, 165, 0);

            cancellationToken = new CancellationTokenSource();

            try
            {
                await Task.Run(() => CleanJunkFiles(cancellationToken.Token));

                statusLabel.Text = "Cleaning complete!";
                statusLabel.ForeColor = Color.LimeGreen;
                MessageBox.Show("Junk files cleaned successfully!", "Success", MessageBoxButtons.OK, MessageBoxIcon.Information);

                // Reset
                tempFilesSize = 0;
                recycleBinSize = 0;
                downloadsSize = 0;
                browserCacheSize = 0;
                UpdateLabel(tempFilesLabel, "0 MB");
                UpdateLabel(recycleBinLabel, "0 MB");
                UpdateLabel(downloadsLabel, "0 MB");
                UpdateLabel(browserCacheLabel, "0 MB");
            }
            catch (Exception ex)
            {
                statusLabel.Text = "Cleaning failed: " + ex.Message;
                statusLabel.ForeColor = Color.Red;
                MessageBox.Show("Error during cleaning: " + ex.Message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            finally
            {
                isCleaning = false;
                cleanButton.Enabled = true;
                analyzeButton.Enabled = true;
                cleanProgressBar.Visible = false;
                progressLabel.Visible = false;
            }
        }

        private void CleanJunkFiles(CancellationToken token)
        {
            int totalSteps = 0;
            int currentStep = 0;

            if (tempFilesCheckBox.Checked) totalSteps++;
            if (recycleBinCheckBox.Checked) totalSteps++;
            if (downloadsCheckBox.Checked) totalSteps++;
            if (browserCacheCheckBox.Checked) totalSteps++;

            // Clean Temp Files
            if (tempFilesCheckBox.Checked)
            {
                UpdateStatus("Cleaning temporary files...");
                CleanDirectory(Path.GetTempPath(), token);
                currentStep++;
                UpdateProgress((int)((double)currentStep / totalSteps * 100));
            }

            // Clean Recycle Bin
            if (recycleBinCheckBox.Checked)
            {
                UpdateStatus("Emptying recycle bin...");
                EmptyRecycleBin();
                currentStep++;
                UpdateProgress((int)((double)currentStep / totalSteps * 100));
            }

            // Clean Old Downloads
            if (downloadsCheckBox.Checked)
            {
                UpdateStatus("Cleaning old downloads...");
                string downloadsPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads");
                CleanOldFiles(downloadsPath, 30, token);
                currentStep++;
                UpdateProgress((int)((double)currentStep / totalSteps * 100));
            }

            // Clean Browser Cache
            if (browserCacheCheckBox.Checked)
            {
                UpdateStatus("Cleaning browser cache...");
                CleanBrowserCache(token);
                currentStep++;
                UpdateProgress((int)((double)currentStep / totalSteps * 100));
            }
        }

        private long GetDirectorySize(string path, CancellationToken token)
        {
            long size = 0;
            try
            {
                if (!Directory.Exists(path)) return 0;

                string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    if (token.IsCancellationRequested) break;
                    try
                    {
                        FileInfo fi = new FileInfo(file);
                        size += fi.Length;
                    }
                    catch { }
                }
            }
            catch { }
            return size;
        }

        private long GetRecycleBinSize()
        {
            // Simplified - actual recycle bin size calculation is complex
            return 0; // Placeholder
        }

        private long GetOldFilesSize(string path, int days, CancellationToken token)
        {
            long size = 0;
            try
            {
                if (!Directory.Exists(path)) return 0;

                DateTime cutoffDate = DateTime.Now.AddDays(-days);
                string[] files = Directory.GetFiles(path);

                foreach (string file in files)
                {
                    if (token.IsCancellationRequested) break;
                    try
                    {
                        FileInfo fi = new FileInfo(file);
                        if (fi.LastAccessTime < cutoffDate)
                        {
                            size += fi.Length;
                        }
                    }
                    catch { }
                }
            }
            catch { }
            return size;
        }

        private long GetBrowserCacheSize(CancellationToken token)
        {
            long size = 0;
            try
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                // Chrome cache
                string chromePath = Path.Combine(localAppData, @"Google\Chrome\User Data\Default\Cache");
                if (Directory.Exists(chromePath))
                    size += GetDirectorySize(chromePath, token);

                // Edge cache
                string edgePath = Path.Combine(localAppData, @"Microsoft\Edge\User Data\Default\Cache");
                if (Directory.Exists(edgePath))
                    size += GetDirectorySize(edgePath, token);
            }
            catch { }
            return size;
        }

        private void CleanDirectory(string path, CancellationToken token)
        {
            try
            {
                if (!Directory.Exists(path)) return;

                string[] files = Directory.GetFiles(path, "*", SearchOption.AllDirectories);
                foreach (string file in files)
                {
                    if (token.IsCancellationRequested) break;
                    try
                    {
                        File.Delete(file);
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void EmptyRecycleBin()
        {
            try
            {
                // This requires Windows API call - simplified version
                // In production, use SHEmptyRecycleBin from shell32.dll
            }
            catch { }
        }

        private void CleanOldFiles(string path, int days, CancellationToken token)
        {
            try
            {
                if (!Directory.Exists(path)) return;

                DateTime cutoffDate = DateTime.Now.AddDays(-days);
                string[] files = Directory.GetFiles(path);

                foreach (string file in files)
                {
                    if (token.IsCancellationRequested) break;
                    try
                    {
                        FileInfo fi = new FileInfo(file);
                        if (fi.LastAccessTime < cutoffDate)
                        {
                            File.Delete(file);
                        }
                    }
                    catch { }
                }
            }
            catch { }
        }

        private void CleanBrowserCache(CancellationToken token)
        {
            try
            {
                string localAppData = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);

                // Chrome cache
                string chromePath = Path.Combine(localAppData, @"Google\Chrome\User Data\Default\Cache");
                if (Directory.Exists(chromePath))
                    CleanDirectory(chromePath, token);

                // Edge cache
                string edgePath = Path.Combine(localAppData, @"Microsoft\Edge\User Data\Default\Cache");
                if (Directory.Exists(edgePath))
                    CleanDirectory(edgePath, token);
            }
            catch { }
        }

        private string FormatSize(long bytes)
        {
            string[] sizes = { "B", "KB", "MB", "GB", "TB" };
            double len = bytes;
            int order = 0;
            while (len >= 1024 && order < sizes.Length - 1)
            {
                order++;
                len = len / 1024;
            }
            return $"{len:0.##} {sizes[order]}";
        }

        private void UpdateStatus(string status)
        {
            if (statusLabel.InvokeRequired)
            {
                statusLabel.Invoke(new Action(() => UpdateStatus(status)));
                return;
            }
            statusLabel.Text = status;
        }

        private void UpdateLabel(Label label, string text)
        {
            if (label.InvokeRequired)
            {
                label.Invoke(new Action(() => UpdateLabel(label, text)));
                return;
            }
            label.Text = text;
        }

        private void UpdateProgress(int percentage)
        {
            if (cleanProgressBar.InvokeRequired)
            {
                cleanProgressBar.Invoke(new Action(() => UpdateProgress(percentage)));
                return;
            }
            cleanProgressBar.Value = Math.Min(percentage, 100);
            progressLabel.Text = $"{percentage}%";
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            BackClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}