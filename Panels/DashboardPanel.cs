using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.IO;
using System.Windows.Forms;

namespace CyberShield_V3
{
    public partial class DashboardPanel : UserControl
    {
        private Panel mainPanel;
        private Label titleLabel;
        private Label welcomeLabel;

        // Status Cards
        private AnimatedCard protectionStatusCard;
        private AnimatedCard lastScanCard;
        private AnimatedCard threatsDetectedCard;
        private AnimatedCard systemHealthCard;

        // Quick Action Buttons
        private AnimatedButton quickScanButton;
        private AnimatedButton cleanJunkButton;
        private AnimatedButton viewQuarantineButton;

        // Statistics
        private int totalThreatsDetected = 0;
        private DateTime lastScanTime = DateTime.MinValue;

        // Animation
        private System.Windows.Forms.Timer animationTimer;
        private int pulseValue = 0;
        private bool pulseIncreasing = true;

        public event EventHandler QuickScanClicked;
        public event EventHandler CleanJunkClicked;
        public event EventHandler ViewQuarantineClicked;

        public DashboardPanel()
        {
            animationTimer = new System.Windows.Forms.Timer();
            InitializeCustomComponents();
            LoadStatistics();
            StartAnimations();
        }

        private void InitializeCustomComponents()
        {
            this.SuspendLayout();

            // Main Panel
            mainPanel = new Panel();
            mainPanel.Dock = DockStyle.Fill;
            mainPanel.BackColor = Color.FromArgb(30, 33, 57);
            mainPanel.AutoScroll = true;

            // Title Label with fade-in effect
            titleLabel = new Label();
            titleLabel.Text = "Dashboard";
            titleLabel.Font = new Font("Segoe UI", 24, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(30, 20);

            // Welcome Label
            welcomeLabel = new Label();
            welcomeLabel.Text = $"Welcome back! Your system is being protected.";
            welcomeLabel.Font = new Font("Segoe UI", 11);
            welcomeLabel.ForeColor = Color.FromArgb(180, 190, 210);
            welcomeLabel.AutoSize = true;
            welcomeLabel.Location = new Point(30, 60);

            // Create Animated Status Cards
            CreateProtectionStatusCard();
            CreateLastScanCard();
            CreateThreatsDetectedCard();
            CreateSystemHealthCard();

            // Create Animated Quick Action Buttons
            CreateQuickActionButtons();

            // Add all controls to main panel
            mainPanel.Controls.Add(titleLabel);
            mainPanel.Controls.Add(welcomeLabel);
            mainPanel.Controls.Add(protectionStatusCard);
            mainPanel.Controls.Add(lastScanCard);
            mainPanel.Controls.Add(threatsDetectedCard);
            mainPanel.Controls.Add(systemHealthCard);
            mainPanel.Controls.Add(quickScanButton);
            mainPanel.Controls.Add(cleanJunkButton);
            mainPanel.Controls.Add(viewQuarantineButton);

            this.Controls.Add(mainPanel);
            this.Size = new Size(643, 398);
            this.BackColor = Color.FromArgb(30, 33, 57);

            this.ResumeLayout();
        }

        private void CreateProtectionStatusCard()
        {
            protectionStatusCard = new AnimatedCard();
            protectionStatusCard.Size = new Size(280, 100);
            protectionStatusCard.Location = new Point(30, 100);
            protectionStatusCard.BackColor = Color.FromArgb(45, 85, 130);
            protectionStatusCard.CardTitle = "Protection Status";
            protectionStatusCard.CardValue = "✓ Active";
            protectionStatusCard.ValueColor = Color.LimeGreen;
            protectionStatusCard.AnimationType = CardAnimation.Pulse;
        }

        private void CreateLastScanCard()
        {
            lastScanCard = new AnimatedCard();
            lastScanCard.Size = new Size(280, 100);
            lastScanCard.Location = new Point(330, 100);
            lastScanCard.BackColor = Color.FromArgb(90, 70, 120);
            lastScanCard.CardTitle = "Last Scan";

            if (lastScanTime == DateTime.MinValue)
            {
                lastScanCard.CardValue = "Never";
            }
            else
            {
                TimeSpan timeSince = DateTime.Now - lastScanTime;
                if (timeSince.TotalDays >= 1)
                    lastScanCard.CardValue = $"{(int)timeSince.TotalDays}d ago";
                else if (timeSince.TotalHours >= 1)
                    lastScanCard.CardValue = $"{(int)timeSince.TotalHours}h ago";
                else
                    lastScanCard.CardValue = $"{(int)timeSince.TotalMinutes}m ago";
            }

            lastScanCard.ValueColor = Color.White;
            lastScanCard.AnimationType = CardAnimation.Glow;
        }

        private void CreateThreatsDetectedCard()
        {
            threatsDetectedCard = new AnimatedCard();
            threatsDetectedCard.Size = new Size(280, 100);
            threatsDetectedCard.Location = new Point(30, 220);
            threatsDetectedCard.BackColor = Color.FromArgb(180, 60, 60);
            threatsDetectedCard.CardTitle = "Threats Detected";
            threatsDetectedCard.CardValue = totalThreatsDetected.ToString();
            threatsDetectedCard.ValueColor = Color.White;
            threatsDetectedCard.ValueFontSize = 24;
            threatsDetectedCard.AnimationType = CardAnimation.Shake;
        }

        private void CreateSystemHealthCard()
        {
            systemHealthCard = new AnimatedCard();
            systemHealthCard.Size = new Size(280, 100);
            systemHealthCard.Location = new Point(330, 220);
            systemHealthCard.BackColor = Color.FromArgb(60, 150, 100);
            systemHealthCard.CardTitle = "System Health";
            systemHealthCard.CardValue = "Good";
            systemHealthCard.ValueColor = Color.White;
            systemHealthCard.AnimationType = CardAnimation.Glow;
        }

        private void CreateQuickActionButtons()
        {
            // Quick Scan Button
            quickScanButton = new AnimatedButton();
            quickScanButton.Text = "🛡️ Quick Scan";
            quickScanButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            quickScanButton.Size = new Size(180, 45);
            quickScanButton.Location = new Point(30, 340);
            quickScanButton.BaseColor = Color.FromArgb(90, 141, 184);
            quickScanButton.HoverColor = Color.FromArgb(110, 161, 204);
            quickScanButton.Click += (s, e) => QuickScanClicked?.Invoke(this, EventArgs.Empty);

            // Clean Junk Button
            cleanJunkButton = new AnimatedButton();
            cleanJunkButton.Text = "🧹 Clean Junk";
            cleanJunkButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            cleanJunkButton.Size = new Size(180, 45);
            cleanJunkButton.Location = new Point(220, 340);
            cleanJunkButton.BaseColor = Color.FromArgb(255, 165, 0);
            cleanJunkButton.HoverColor = Color.FromArgb(255, 185, 30);
            cleanJunkButton.Click += (s, e) => CleanJunkClicked?.Invoke(this, EventArgs.Empty);

            // View Quarantine Button
            viewQuarantineButton = new AnimatedButton();
            viewQuarantineButton.Text = "🔒 Quarantine";
            viewQuarantineButton.Font = new Font("Segoe UI", 11, FontStyle.Bold);
            viewQuarantineButton.Size = new Size(180, 45);
            viewQuarantineButton.Location = new Point(410, 340);
            viewQuarantineButton.BaseColor = Color.FromArgb(220, 50, 50);
            viewQuarantineButton.HoverColor = Color.FromArgb(240, 70, 70);
            viewQuarantineButton.Click += (s, e) => ViewQuarantineClicked?.Invoke(this, EventArgs.Empty);
        }

        private void StartAnimations()
        {
            animationTimer.Interval = 50;
            animationTimer.Tick += AnimationTimer_Tick;
            animationTimer.Start();
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            if (pulseIncreasing)
            {
                pulseValue += 2;
                if (pulseValue >= 20) pulseIncreasing = false;
            }
            else
            {
                pulseValue -= 2;
                if (pulseValue <= 0) pulseIncreasing = true;
            }

            // Update card animations
            protectionStatusCard?.UpdateAnimation(pulseValue);
            lastScanCard?.UpdateAnimation(pulseValue);
            threatsDetectedCard?.UpdateAnimation(pulseValue);
            systemHealthCard?.UpdateAnimation(pulseValue);
        }

        private void LoadStatistics()
        {
            try
            {
                string quarantineFolder = Path.Combine(
                    Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData),
                    "CyberShield",
                    "Quarantine"
                );

                if (Directory.Exists(quarantineFolder))
                {
                    string[] files = Directory.GetFiles(quarantineFolder, "*.quarantine");
                    totalThreatsDetected = files.Length;

                    if (files.Length > 0)
                    {
                        DateTime mostRecent = DateTime.MinValue;
                        foreach (string file in files)
                        {
                            FileInfo fi = new FileInfo(file);
                            if (fi.CreationTime > mostRecent)
                                mostRecent = fi.CreationTime;
                        }
                        lastScanTime = mostRecent;
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error loading statistics: {ex.Message}");
            }
        }

        public void RefreshStatistics()
        {
            LoadStatistics();
            mainPanel.Controls.Clear();
            InitializeCustomComponents();
            StartAnimations();
        }

        public void UpdateLastScanTime(DateTime scanTime)
        {
            lastScanTime = scanTime;
            RefreshStatistics();
        }

        public void UpdateThreatsDetected(int count)
        {
            totalThreatsDetected = count;
            RefreshStatistics();
        }

        public void CleanupAnimations()
        {
            if (animationTimer != null)
            {
                animationTimer.Stop();
                animationTimer.Dispose();
            }
        }
    }

    // Animated Card Control
    public enum CardAnimation
    {
        None,
        Pulse,
        Glow,
        Shake
    }

    public class AnimatedCard : Panel
    {
        private Label titleLabel;
        private Label valueLabel;
        private int animationValue = 0;
        private int shakeOffset = 0;
        private Point originalLocation;
        private string cardTitle = "";
        private string cardValue = "";
        private Color valueColor = Color.White;
        private int valueFontSize = 18;
        private CardAnimation animationType = CardAnimation.None;


        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string CardTitle
        {
            get { return cardTitle; }
            set { cardTitle = value; }
        }


        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string CardValue
        {
            get { return cardValue; }
            set { cardValue = value; }
        }


        [Browsable(true)]
        [Category("Custom Properties")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color ValueColor
        {
            get { return valueColor; }
            set { valueColor = value; }
        }

        [DefaultValue(12)]
        public int ValueFontSize
        {
            get { return valueFontSize; }
            set { valueFontSize = value; }
        }

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public CardAnimation AnimationType
        {
            get { return animationType; }
            set { animationType = value; }
        }

        public AnimatedCard()
        {
            this.DoubleBuffered = true;
            this.Cursor = Cursors.Hand;

            titleLabel = new Label();
            titleLabel.Font = new Font("Segoe UI", 10, FontStyle.Bold);
            titleLabel.ForeColor = Color.White;
            titleLabel.AutoSize = true;
            titleLabel.Location = new Point(15, 15);
            titleLabel.BackColor = Color.Transparent;

            valueLabel = new Label();
            valueLabel.Font = new Font("Segoe UI", ValueFontSize, FontStyle.Bold);
            valueLabel.ForeColor = ValueColor;
            valueLabel.AutoSize = true;
            valueLabel.Location = new Point(15, 45);
            valueLabel.BackColor = Color.Transparent;

            this.Controls.Add(titleLabel);
            this.Controls.Add(valueLabel);

            this.MouseEnter += AnimatedCard_MouseEnter;
            this.MouseLeave += AnimatedCard_MouseLeave;
            this.Paint += AnimatedCard_Paint;
        }

        protected override void OnLocationChanged(EventArgs e)
        {
            base.OnLocationChanged(e);
            if (originalLocation == Point.Empty)
                originalLocation = this.Location;
        }

        private void AnimatedCard_Paint(object sender, PaintEventArgs e)
        {
            titleLabel.Text = CardTitle;
            valueLabel.Text = CardValue;
            valueLabel.ForeColor = ValueColor;
            valueLabel.Font = new Font("Segoe UI", ValueFontSize, FontStyle.Bold);

            // Draw subtle border with animation
            if (AnimationType == CardAnimation.Glow)
            {
                int glowAlpha = 100 + animationValue * 3;
                using (Pen glowPen = new Pen(Color.FromArgb(glowAlpha, 255, 255, 255), 2))
                {
                    e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
                    e.Graphics.DrawRectangle(glowPen, 1, 1, this.Width - 3, this.Height - 3);
                }
            }
        }

        public void UpdateAnimation(int value)
        {
            animationValue = value;

            if (AnimationType == CardAnimation.Pulse)
            {
                int scale = 5 + value / 4;
                valueLabel.Font = new Font("Segoe UI", ValueFontSize + scale / 10, FontStyle.Bold);
            }
            else if (AnimationType == CardAnimation.Shake && value == 20)
            {
                shakeOffset = (shakeOffset == 0) ? 2 : 0;
                if (originalLocation != Point.Empty)
                {
                    this.Location = new Point(originalLocation.X + shakeOffset, originalLocation.Y);
                }
            }

            this.Invalidate();
        }

        private void AnimatedCard_MouseEnter(object sender, EventArgs e)
        {
            Color currentColor = this.BackColor;
            this.BackColor = Color.FromArgb(
                Math.Min(currentColor.R + 10, 255),
                Math.Min(currentColor.G + 10, 255),
                Math.Min(currentColor.B + 10, 255)
            );
        }

        private void AnimatedCard_MouseLeave(object sender, EventArgs e)
        {
            Color currentColor = this.BackColor;
            this.BackColor = Color.FromArgb(
                Math.Max(currentColor.R - 10, 0),
                Math.Max(currentColor.G - 10, 0),
                Math.Max(currentColor.B - 10, 0)
            );
        }
    }

    // Animated Button Control
    public class AnimatedButton : Button
    {
        private bool isHovered = false;
        private int hoverProgress = 0;
        private System.Windows.Forms.Timer hoverTimer;
        private Color baseColor = Color.FromArgb(90, 141, 184);
        private Color hoverColor = Color.FromArgb(110, 161, 204);


        [Browsable(true)]
        [Category("Custom Properties")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BaseColor
        {
            get { return baseColor; }
            set { baseColor = value; }
        }

        [Browsable(true)]
        [Category("Custom Properties")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color HoverColor
        {
            get { return hoverColor; }
            set { hoverColor = value; }
        }

        public AnimatedButton()
        {
            this.FlatStyle = FlatStyle.Flat;
            this.FlatAppearance.BorderSize = 0;
            this.ForeColor = Color.White;
            this.BackColor = BaseColor;
            this.Cursor = Cursors.Hand;
            this.DoubleBuffered = true;

            hoverTimer = new System.Windows.Forms.Timer();
            hoverTimer.Interval = 20;
            hoverTimer.Tick += HoverTimer_Tick;

            this.MouseEnter += AnimatedButton_MouseEnter;
            this.MouseLeave += AnimatedButton_MouseLeave;
            this.Paint += AnimatedButton_Paint;
        }

        private void AnimatedButton_Paint(object sender, PaintEventArgs e)
        {
            // Draw gradient background
            using (LinearGradientBrush brush = new LinearGradientBrush(
                this.ClientRectangle,
                this.BackColor,
                Color.FromArgb(
                    Math.Max(this.BackColor.R - 20, 0),
                    Math.Max(this.BackColor.G - 20, 0),
                    Math.Max(this.BackColor.B - 20, 0)
                ),
                LinearGradientMode.Vertical))
            {
                e.Graphics.FillRectangle(brush, this.ClientRectangle);
            }

            // Draw text
            SizeF textSize = e.Graphics.MeasureString(this.Text, this.Font);
            PointF textLocation = new PointF(
                (this.Width - textSize.Width) / 2,
                (this.Height - textSize.Height) / 2
            );
            using (SolidBrush textBrush = new SolidBrush(this.ForeColor))
            {
                e.Graphics.DrawString(this.Text, this.Font, textBrush, textLocation);
            }

            // Draw hover effect
            if (isHovered && hoverProgress > 0)
            {
                int alpha = (int)(hoverProgress * 2.55);
                using (SolidBrush hoverBrush = new SolidBrush(Color.FromArgb(alpha, 255, 255, 255)))
                {
                    e.Graphics.FillRectangle(hoverBrush, this.ClientRectangle);
                }
            }
        }

        private void HoverTimer_Tick(object sender, EventArgs e)
        {
            if (isHovered && hoverProgress < 30)
            {
                hoverProgress += 3;
                this.Invalidate();
            }
            else if (!isHovered && hoverProgress > 0)
            {
                hoverProgress -= 3;
                this.Invalidate();
            }
            else
            {
                hoverTimer.Stop();
            }
        }

        private void AnimatedButton_MouseEnter(object sender, EventArgs e)
        {
            isHovered = true;
            this.BackColor = HoverColor;
            hoverTimer.Start();
        }

        private void AnimatedButton_MouseLeave(object sender, EventArgs e)
        {
            isHovered = false;
            this.BackColor = BaseColor;
            hoverTimer.Start();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                hoverTimer?.Stop();
                hoverTimer?.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}