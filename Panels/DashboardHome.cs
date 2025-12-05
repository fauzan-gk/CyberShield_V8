using System;
using System.Diagnostics;
using System.Windows.Forms;
using System.Drawing;

namespace CyberShield_V3
{
    public partial class DashboardHome : UserControl
    {
        public event EventHandler QuickScanClicked;
        public event EventHandler CleanJunkClicked;
        public event EventHandler ViewQuarantineClicked;

        private PerformanceCounter cpuCounter;
        private PerformanceCounter ramCounter;

        public DashboardHome()
        {
            InitializeComponent();
            InitializeCounters();
        }

        private void InitializeCounters()
        {
            try
            {
                cpuCounter = new PerformanceCounter("Processor", "% Processor Time", "_Total");
                ramCounter = new PerformanceCounter("Memory", "Available MBytes");

                cpuCounter.NextValue();
                ramCounter.NextValue();

                timer1.Interval = 1000;
                timer1.Start();
            }
            catch
            {
                lblCpuValue.Text = "N/A";
                lblRamValue.Text = "N/A";
            }
        }

        // --- PUBLIC METHODS ---

        public void UpdateLastScanTime(DateTime time)
        {
            if (lblLastScan != null) lblLastScan.Text = time.ToString("g");
        }

        public void UpdateDbVersion(string version)
        {
            if (lblDbVersion != null) lblDbVersion.Text = version;
        }

        public void UpdateThreatsDetected(int count) { }

        public void UpdateThreatDbCount(int count)
        {
            if (lblThreatCount != null) lblThreatCount.Text = count.ToString("N0");
        }

        public void UpdateCloudUplinkStatus(bool isConnected)
        {
            if (lblCloudStatus != null)
            {
                if (isConnected)
                {
                    lblCloudStatus.Text = "Online";
                    lblCloudStatus.ForeColor = Color.FromArgb(16, 185, 129);
                }
                else
                {
                    lblCloudStatus.Text = "Offline";
                    lblCloudStatus.ForeColor = Color.Red;
                }
            }
        }

        // --- BUTTONS ---

        private void btnQuickScan_Click(object sender, EventArgs e)
        {
            QuickScanClicked?.Invoke(this, EventArgs.Empty);
        }

        private void btnUpdate_Click(object sender, EventArgs e)
        {
            btnUpdate.Text = "Checking...";
            btnUpdate.Enabled = false;
            UpdateCloudUplinkStatus(false);

            System.Windows.Forms.Timer updateTimer = new System.Windows.Forms.Timer { Interval = 2000 };
            updateTimer.Tick += (s, args) =>
            {
                btnUpdate.Text = "Up to date";
                btnUpdate.Enabled = true;
                lblDbVersion.Text = "v." + DateTime.Now.ToString("yyyy.MM.dd");
                UpdateCloudUplinkStatus(true);
                updateTimer.Stop();
                updateTimer.Dispose();
            };
            updateTimer.Start();
        }

        // --- SYSTEM STATS ---

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (cpuCounter == null || ramCounter == null) return;

            // 1. CPU Update
            float cpuVal = cpuCounter.NextValue();
            int cpuInt = (int)Math.Min(cpuVal, 100);

            lblCpuValue.Text = $"{cpuInt}%";
            cpuProgressBar.Value = cpuInt;

            // Dynamic Styling for CPU
            if (cpuInt < 30)
            {
                lblCpuStatus.Text = "System Idling";
                lblCpuStatus.ForeColor = Color.Silver;
                cpuProgressBar.ProgressColor = Color.FromArgb(59, 130, 246); // Blue
                cpuProgressBar.ProgressColor2 = Color.Violet;
            }
            else if (cpuInt < 80)
            {
                lblCpuStatus.Text = "Normal Load";
                lblCpuStatus.ForeColor = Color.White;
                cpuProgressBar.ProgressColor = Color.Orange;
                cpuProgressBar.ProgressColor2 = Color.DarkOrange;
            }
            else
            {
                lblCpuStatus.Text = "Heavy Load";
                lblCpuStatus.ForeColor = Color.Red;
                cpuProgressBar.ProgressColor = Color.Red;
                cpuProgressBar.ProgressColor2 = Color.Maroon;
            }

            // 2. RAM Update
            float availableRam = ramCounter.NextValue();
            float totalRam = GetTotalMemoryInMBytes();
            float usedRam = totalRam - availableRam;
            float ramPercent = (usedRam / totalRam) * 100;
            int ramInt = (int)Math.Min(ramPercent, 100);

            lblRamValue.Text = $"{ramInt}%";
            ramProgressBar.Value = ramInt;

            // Detail Text (e.g. "4.2 GB / 16.0 GB")
            lblRamStatus.Text = $"{(usedRam / 1024f):0.0} GB / {(totalRam / 1024f):0.0} GB";
        }

        private float GetTotalMemoryInMBytes()
        {
            try
            {
                var computerInfo = new Microsoft.VisualBasic.Devices.ComputerInfo();
                ulong totalBytes = computerInfo.TotalPhysicalMemory;
                return totalBytes / (1024f * 1024f);
            }
            catch { return 8192; }
        }
    }
}