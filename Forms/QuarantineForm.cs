using CyberShield_V3.Services;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace CyberShield_V3
{
    public partial class QuarantineForm : Form
    {
        private Panel mainPanel;
        private Label titleLabel;
        private ListView threatsListView;
        private Button deleteButton;
        private Button restoreButton;
        private Button closeButton;
        private Label summaryLabel;
        private List<ThreatInfo> threats;

        public QuarantineForm(List<ThreatInfo> detectedThreats)
        {
            this.threats = detectedThreats;
            InitializeComponent();
            LoadThreats();
        }

        private void InitializeComponent()
        {
            this.Size = new Size(900, 600);
            this.StartPosition = FormStartPosition.CenterParent;
            this.FormBorderStyle = FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Text = "CyberShield - Quarantine Manager";
            this.BackColor = Color.FromArgb(30, 40, 60);

            // Main Panel
            mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.FromArgb(30, 40, 60);
            mainPanel.Padding = new Padding(20);

            // Title Label
            titleLabel = new Label();
            titleLabel.Text = "Quarantined Threats";
            titleLabel.Font = new Font("Segoe UI", 20, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(20, 20);

            // Summary Label
            summaryLabel = new Label();
            summaryLabel.Font = new Font("Segoe UI", 11);
            summaryLabel.ForeColor = Color.LightGray;
            summaryLabel.AutoSize = true;
            summaryLabel.Location = new Point(20, 60);

            // Threats ListView
            threatsListView = new ListView();
            threatsListView.Location = new Point(20, 100);
            threatsListView.Size = new Size(840, 380);
            threatsListView.View = View.Details;
            threatsListView.FullRowSelect = true;
            threatsListView.GridLines = true;
            threatsListView.BackColor = Color.FromArgb(40, 50, 70);
            threatsListView.ForeColor = Color.White;
            threatsListView.Font = new Font("Segoe UI", 9);

            // Add columns
            threatsListView.Columns.Add("Threat Name", 200);
            threatsListView.Columns.Add("Type", 150);
            threatsListView.Columns.Add("Original Location", 300);
            threatsListView.Columns.Add("Detection Method", 150);
            threatsListView.Columns.Add("Status", 100);

            // Delete Button
            deleteButton = new Button();
            deleteButton.Text = "Delete Selected";
            deleteButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            deleteButton.Size = new Size(150, 40);
            deleteButton.Location = new Point(20, 500);
            deleteButton.BackColor = Color.FromArgb(220, 50, 50);
            deleteButton.ForeColor = Color.White;
            deleteButton.FlatStyle = FlatStyle.Flat;
            deleteButton.FlatAppearance.BorderSize = 0;
            deleteButton.Cursor = Cursors.Hand;
            deleteButton.Click += DeleteButton_Click;

            // Restore Button
            restoreButton = new Button();
            restoreButton.Text = "Restore Selected";
            restoreButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            restoreButton.Size = new Size(150, 40);
            restoreButton.Location = new Point(180, 500);
            restoreButton.BackColor = Color.FromArgb(255, 165, 0);
            restoreButton.ForeColor = Color.White;
            restoreButton.FlatStyle = FlatStyle.Flat;
            restoreButton.FlatAppearance.BorderSize = 0;
            restoreButton.Cursor = Cursors.Hand;
            restoreButton.Click += RestoreButton_Click;

            // Close Button
            closeButton = new Button();
            closeButton.Text = "Close";
            closeButton.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            closeButton.Size = new Size(150, 40);
            closeButton.Location = new Point(710, 500);
            closeButton.BackColor = Color.FromArgb(100, 150, 255);
            closeButton.ForeColor = Color.White;
            closeButton.FlatStyle = FlatStyle.Flat;
            closeButton.FlatAppearance.BorderSize = 0;
            closeButton.Cursor = Cursors.Hand;
            closeButton.Click += CloseButton_Click;

            // Add controls
            mainPanel.Controls.Add(titleLabel);
            mainPanel.Controls.Add(summaryLabel);
            mainPanel.Controls.Add(threatsListView);
            mainPanel.Controls.Add(deleteButton);
            mainPanel.Controls.Add(restoreButton);
            mainPanel.Controls.Add(closeButton);

            this.Controls.Add(mainPanel);
        }

        private void LoadThreats()
        {
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
                        if (threat.IsQuarantined && File.Exists(threat.QuarantinedPath))
                        {
                            File.Delete(threat.QuarantinedPath);
                            itemsToRemove.Add(item);
                            threats.Remove(threat);
                        }
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show($"Failed to delete {threat.ThreatName}: {ex.Message}",
                            "Deletion Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }
                }

                foreach (var item in itemsToRemove)
                {
                    threatsListView.Items.Remove(item);
                }

                LoadThreats();
                MessageBox.Show($"{itemsToRemove.Count} threat(s) permanently deleted.",
                    "Deletion Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
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

                LoadThreats();
                MessageBox.Show($"{restoredCount} threat(s) restored.",
                    "Restoration Complete", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void CloseButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}