using CyberShield_V3.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CyberShield_V3.Panels
{
    public partial class QuarantinePanel : UserControl
    {
        private Panel mainPanel;
        private Label titleLabel;
        private ListView threatsListView;
        private Button deleteButton;
        private Button deleteAllButton;
        private Button restoreButton;
        private Button backButton;
        private Label summaryLabel;
        private List<ThreatInfo> threats;

        public event EventHandler BackClicked;
        public event EventHandler ThreatsChanged;

        public QuarantinePanel()
        {
            InitializeComponent();
        }

        private void InitializeComponent()
        {
            mainPanel = new Panel();
            titleLabel = new Label();
            summaryLabel = new Label();
            threatsListView = new ListView();
            deleteButton = new Button();
            deleteAllButton = new Button();
            restoreButton = new Button();
            backButton = new Button();
            mainPanel.SuspendLayout();
            SuspendLayout();
            // 
            // mainPanel
            // 
            mainPanel.BackColor = Color.FromArgb(30, 40, 60);
            mainPanel.Controls.Add(titleLabel);
            mainPanel.Controls.Add(summaryLabel);
            mainPanel.Controls.Add(threatsListView);
            mainPanel.Controls.Add(deleteButton);
            mainPanel.Controls.Add(deleteAllButton);
            mainPanel.Controls.Add(restoreButton);
            mainPanel.Controls.Add(backButton);
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.Location = new Point(0, 0);
            mainPanel.Name = "mainPanel";
            mainPanel.Padding = new Padding(20);
            mainPanel.Size = new Size(725, 442);
            mainPanel.TabIndex = 0;
            // 
            // titleLabel
            // 
            titleLabel.AutoSize = true;
            titleLabel.Font = new Font("Segoe UI", 20F, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.Location = new Point(20, 20);
            titleLabel.Name = "titleLabel";
            titleLabel.Size = new Size(280, 37);
            titleLabel.TabIndex = 0;
            titleLabel.Text = "Quarantined Threats";
            // 
            // summaryLabel
            // 
            summaryLabel.AutoSize = true;
            summaryLabel.Font = new Font("Segoe UI", 11F);
            summaryLabel.ForeColor = Color.LightGray;
            summaryLabel.Location = new Point(20, 60);
            summaryLabel.Name = "summaryLabel";
            summaryLabel.Size = new Size(0, 20);
            summaryLabel.TabIndex = 1;
            // 
            // threatsListView
            // 
            threatsListView.BackColor = Color.FromArgb(40, 50, 70);
            threatsListView.Font = new Font("Segoe UI", 9F);
            threatsListView.ForeColor = Color.White;
            threatsListView.FullRowSelect = true;
            threatsListView.GridLines = true;
            threatsListView.Location = new Point(40, 99);
            threatsListView.Name = "threatsListView";
            threatsListView.Size = new Size(634, 240);
            threatsListView.TabIndex = 2;
            threatsListView.UseCompatibleStateImageBehavior = false;
            threatsListView.View = View.Details;
            // 
            // deleteButton
            // 
            deleteButton.BackColor = Color.FromArgb(220, 50, 50);
            deleteButton.Cursor = Cursors.Hand;
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            deleteButton.ForeColor = Color.White;
            deleteButton.Location = new Point(40, 360);
            deleteButton.Name = "deleteButton";
            deleteButton.Size = new Size(120, 35);
            deleteButton.TabIndex = 3;
            deleteButton.Text = "Delete Selected";
            deleteButton.UseVisualStyleBackColor = false;
            deleteButton.Click += DeleteButton_Click;
            // 
            // deleteAllButton
            // 
            deleteAllButton.BackColor = Color.FromArgb(180, 30, 30);
            deleteAllButton.Cursor = Cursors.Hand;
            deleteAllButton.FlatAppearance.BorderSize = 0;
            deleteAllButton.FlatStyle = FlatStyle.Flat;
            deleteAllButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            deleteAllButton.ForeColor = Color.White;
            deleteAllButton.Location = new Point(177, 360);
            deleteAllButton.Name = "deleteAllButton";
            deleteAllButton.Size = new Size(100, 35);
            deleteAllButton.TabIndex = 4;
            deleteAllButton.Text = "Delete All";
            deleteAllButton.UseVisualStyleBackColor = false;
            deleteAllButton.Click += DeleteAllButton_Click;
            // 
            // restoreButton
            // 
            restoreButton.BackColor = Color.FromArgb(255, 165, 0);
            restoreButton.Cursor = Cursors.Hand;
            restoreButton.FlatAppearance.BorderSize = 0;
            restoreButton.FlatStyle = FlatStyle.Flat;
            restoreButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            restoreButton.ForeColor = Color.White;
            restoreButton.Location = new Point(293, 360);
            restoreButton.Name = "restoreButton";
            restoreButton.Size = new Size(120, 35);
            restoreButton.TabIndex = 5;
            restoreButton.Text = "Restore Selected";
            restoreButton.UseVisualStyleBackColor = false;
            restoreButton.Click += RestoreButton_Click;
            // 
            // backButton
            // 
            backButton.BackColor = Color.FromArgb(100, 150, 255);
            backButton.Cursor = Cursors.Hand;
            backButton.FlatAppearance.BorderSize = 0;
            backButton.FlatStyle = FlatStyle.Flat;
            backButton.Font = new Font("Segoe UI", 10F, FontStyle.Bold);
            backButton.ForeColor = Color.White;
            backButton.Location = new Point(554, 360);
            backButton.Name = "backButton";
            backButton.Size = new Size(120, 35);
            backButton.TabIndex = 6;
            backButton.Text = "Back to Home";
            backButton.UseVisualStyleBackColor = false;
            backButton.Click += BackButton_Click;
            // 
            // QuarantinePanel
            // 
            BackColor = Color.FromArgb(30, 40, 60);
            Controls.Add(mainPanel);
            Name = "QuarantinePanel";
            Size = new Size(725, 442);
            mainPanel.ResumeLayout(false);
            mainPanel.PerformLayout();
            ResumeLayout(false);

            // threatListView setup
            threatsListView.View = View.Details;
            threatsListView.GridLines = true;
            threatsListView.FullRowSelect = true;

            // --- ADD THESE LINES ---
            threatsListView.Columns.Add("Threat Name", 150);
            threatsListView.Columns.Add("Type", 100);
            threatsListView.Columns.Add("Original Path", 250);
            threatsListView.Columns.Add("Detection", 100);
            threatsListView.Columns.Add("Status", 100);
            // -----------------------
        }

        public void LoadThreats(List<ThreatInfo> detectedThreats)
        {
            this.threats = detectedThreats;
            threatsListView.Items.Clear();

            int quarantinedCount = threats.Count(t => t.IsQuarantined);
            summaryLabel.Text = $"Total threats detected: {threats.Count} | Successfully quarantined: {quarantinedCount}";

            foreach (var threat in threats)
            {
                ListViewItem item = new ListViewItem(threat.ThreatName);
                item.SubItems.Add(threat.ThreatType);
                item.SubItems.Add(threat.FilePath);
                item.SubItems.Add(threat.DetectionMethod);
                item.SubItems.Add(threat.IsQuarantined ? "Quarantined" : "Failed");
                item.Tag = threat;

                if (threat.IsQuarantined)
                {
                    item.ForeColor = Color.LightGreen;
                }
                else
                {
                    item.ForeColor = Color.LightCoral;
                }

                threatsListView.Items.Add(item);
            }
        }

        private void DeleteButton_Click(object sender, EventArgs e)
        {
            if (threatsListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a threat to delete.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to permanently delete {threatsListView.SelectedItems.Count} threat(s)?",
                "Confirm Deletion",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                List<ListViewItem> itemsToRemove = new List<ListViewItem>();

                foreach (ListViewItem item in threatsListView.SelectedItems)
                {
                    ThreatInfo threat = (ThreatInfo)item.Tag;

                    try
                    {
                        // 1. Try to delete the Quarantined Backup
                        if (!string.IsNullOrEmpty(threat.QuarantinedPath) && File.Exists(threat.QuarantinedPath))
                        {
                            File.Delete(threat.QuarantinedPath);
                        }

                        // 2. NEW: Try to delete the Original File (if it's still there!)
                        // This handles cases where Quarantine failed and the file was left behind.
                        if (!string.IsNullOrEmpty(threat.FilePath) && File.Exists(threat.FilePath))
                        {
                            File.Delete(threat.FilePath);
                        }

                        // Remove from list
                        itemsToRemove.Add(item);
                        threats.Remove(threat);
                    }
                    catch (Exception ex)
                    {
                        // If file is open in another app, this might fail.
                        MessageBox.Show($"Failed to delete {threat.ThreatName} from disk.\nIt might be open in another program.\n\nError: {ex.Message}",
                            "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                // Clean up UI
                foreach (var item in itemsToRemove)
                {
                    threatsListView.Items.Remove(item);
                }

                // Save changes
                ThreatsChanged?.Invoke(this, EventArgs.Empty);

                LoadThreats(threats);
                MessageBox.Show($"{itemsToRemove.Count} threat(s) permanently deleted.",
                    "Deletion Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void DeleteAllButton_Click(object sender, EventArgs e)
        {
            if (threats.Count == 0) return;

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to permanently delete ALL {threats.Count} threat(s)? This cannot be undone!",
                "Confirm Delete All",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int deletedCount = 0;
                List<ThreatInfo> threatsToRemove = new List<ThreatInfo>();

                foreach (var threat in threats)
                {
                    try
                    {
                        // 1. Delete Backup
                        if (!string.IsNullOrEmpty(threat.QuarantinedPath) && File.Exists(threat.QuarantinedPath))
                        {
                            File.Delete(threat.QuarantinedPath);
                        }

                        // 2. Delete Original
                        if (!string.IsNullOrEmpty(threat.FilePath) && File.Exists(threat.FilePath))
                        {
                            File.Delete(threat.FilePath);
                        }

                        threatsToRemove.Add(threat);
                        deletedCount++;
                    }
                    catch (Exception ex)
                    {
                        // Log error or continue trying others
                        System.Diagnostics.Debug.WriteLine($"Failed to delete {threat.FilePath}: {ex.Message}");
                    }
                }

                foreach (var threat in threatsToRemove)
                {
                    threats.Remove(threat);
                }

                ThreatsChanged?.Invoke(this, EventArgs.Empty);
                LoadThreats(threats);

                MessageBox.Show($"All {deletedCount} threat(s) permanently deleted.",
                    "Delete All Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void RestoreButton_Click(object sender, EventArgs e)
        {
            if (threatsListView.SelectedItems.Count == 0)
            {
                MessageBox.Show("Please select a threat to restore.", "No Selection",
                    MessageBoxButtons.OK, MessageBoxIcon.Information);
                return;
            }

            DialogResult result = MessageBox.Show(
                $"Are you sure you want to restore {threatsListView.SelectedItems.Count} threat(s)? " +
                "This may expose your system to danger!",
                "Confirm Restoration",
                MessageBoxButtons.YesNo,
                MessageBoxIcon.Warning);

            if (result == DialogResult.Yes)
            {
                int restoredCount = 0;

                foreach (ListViewItem item in threatsListView.SelectedItems)
                {
                    ThreatInfo threat = (ThreatInfo)item.Tag;

                    try
                    {
                        if (threat.IsQuarantined && File.Exists(threat.QuarantinedPath))
                        {
                            // Restore to original location
                            string originalDir = Path.GetDirectoryName(threat.FilePath);

                            if (!Directory.Exists(originalDir))
                            {
                                Directory.CreateDirectory(originalDir);
                            }

                            File.Move(threat.QuarantinedPath, threat.FilePath);
                            threat.IsQuarantined = false;
                            restoredCount++;
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to restore {threat.ThreatName}: {ex.Message}",
                            "Restoration Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                LoadThreats(threats);
                MessageBox.Show($"{restoredCount} threat(s) restored.",
                    "Restoration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void BackButton_Click(object sender, EventArgs e)
        {
            BackClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}
