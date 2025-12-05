using System;
using System.Windows.Forms;

namespace CyberShield_V3
{
    public partial class ScanHomePanel : UserControl
    {
        // Events Form1 listens to
        public event EventHandler? QuickScanClicked;
        public event EventHandler? DeepScanClicked;

        public ScanHomePanel()
        {
            InitializeComponent();

            // --- FORCE EVENT WIRING ---
            // Fix: Changed 'btnQuickScan' to 'hoverButton1'
            if (this.hoverButton1 != null)
            {
                this.hoverButton1.Click += BtnQuickScan_Click;
            }

            if (this.btnDeepScan != null)
            {
                this.btnDeepScan.Click += BtnDeepScan_Click;
            }
        }

        public void UpdateLastScan(DateTime date)
        {
            if (lblLastScanValue != null)
                lblLastScanValue.Text = date.ToShortDateString() + " " + date.ToShortTimeString();
        }

        public void UpdateProtectionStatus(bool isActive)
        {
            if (lblEngineValue != null)
            {
                if (isActive)
                {
                    lblEngineValue.Text = "Active";
                    lblEngineValue.ForeColor = System.Drawing.Color.LimeGreen;
                }
                else
                {
                    lblEngineValue.Text = "Disabled";
                    lblEngineValue.ForeColor = System.Drawing.Color.FromArgb(220, 50, 50);
                }
            }
        }

        // Internal Handler for Quick Scan (HoverButton)
        private void BtnQuickScan_Click(object? sender, EventArgs e)
        {
            QuickScanClicked?.Invoke(this, EventArgs.Empty);
        }

        // Internal Handler for Deep Scan
        private void BtnDeepScan_Click(object? sender, EventArgs e)
        {
            DeepScanClicked?.Invoke(this, EventArgs.Empty);
        }
    }
}