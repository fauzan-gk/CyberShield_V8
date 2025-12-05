using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace CyberShield_V3.Controls
{
    public class PulseButton : Control
    {
        // --- FIX 1: Add the missing components container ---
        private System.ComponentModel.IContainer components = null;

        // --- FIX 2: Explicitly specify System.Windows.Forms.Timer ---
        private System.Windows.Forms.Timer animationTimer;

        private float pulseSize;
        private int pulseAlpha;
        private bool isHovered = false;

        // --- PROPERTIES ---

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("The background color behind the button (should match parent container).")]
        public Color BaseColor { get; set; } = Color.FromArgb(30, 33, 57);

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("The color of the main center circle.")]
        public Color ButtonColor { get; set; } = Color.FromArgb(100, 150, 255);

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("The color of the animating pulse ring.")]
        public Color PulseColor { get; set; } = Color.FromArgb(100, 150, 255);

        [Browsable(true)]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        [Category("Appearance")]
        [Description("The color of the text inside the button.")]
        public Color TextColor { get; set; } = Color.White;

        // ----------------------------------------

        public PulseButton()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(200, 200);
            this.Cursor = Cursors.Hand;
            this.Text = "SCAN";
            this.Font = new Font("Segoe UI", 24, FontStyle.Bold);

            // Initialize components container
            this.components = new System.ComponentModel.Container();

            // Setup Animation Timer (Explicitly using Windows Forms Timer)
            animationTimer = new System.Windows.Forms.Timer(this.components);
            animationTimer.Interval = 30;
            animationTimer.Tick += AnimationTimer_Tick;

            // Initial state
            pulseSize = 0;
            pulseAlpha = 255;
        }

        private void AnimationTimer_Tick(object sender, EventArgs e)
        {
            pulseSize += 2f;
            pulseAlpha -= 5;

            if (pulseAlpha <= 0 || pulseSize >= (this.Width / 2))
            {
                pulseSize = 0;
                pulseAlpha = 255;
            }

            this.Invalidate();
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            isHovered = true;
            animationTimer.Start();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            isHovered = false;
            animationTimer.Stop();

            pulseSize = 0;
            pulseAlpha = 255;
            this.Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;

            int cx = Width / 2;
            int cy = Height / 2;
            int buttonRadius = (Math.Min(Width, Height) / 2) - 20;

            // 1. Draw Background
            using (SolidBrush bgBrush = new SolidBrush(Parent?.BackColor ?? BaseColor))
            {
                e.Graphics.FillRectangle(bgBrush, ClientRectangle);
            }

            // 2. Draw Pulse Ring
            if (isHovered)
            {
                using (Pen pulsePen = new Pen(Color.FromArgb(pulseAlpha, PulseColor), 4))
                {
                    float ringRadius = buttonRadius + pulseSize;
                    float diameter = ringRadius * 2;
                    e.Graphics.DrawEllipse(pulsePen, cx - ringRadius, cy - ringRadius, diameter, diameter);
                }
            }

            // 3. Draw Main Button Circle
            using (SolidBrush btnBrush = new SolidBrush(ButtonColor))
            {
                int diameter = buttonRadius * 2;
                e.Graphics.FillEllipse(btnBrush, cx - buttonRadius, cy - buttonRadius, diameter, diameter);
            }

            // 4. Draw Glow/Shine
            using (LinearGradientBrush shineBrush = new LinearGradientBrush(
                new Rectangle(cx - buttonRadius, cy - buttonRadius, buttonRadius * 2, buttonRadius),
                Color.FromArgb(100, 255, 255, 255),
                Color.Transparent,
                LinearGradientMode.Vertical))
            {
                int diameter = buttonRadius * 2;
                e.Graphics.FillEllipse(shineBrush, cx - buttonRadius, cy - buttonRadius, diameter, diameter);
            }

            // 5. Draw Text
            SizeF textSize = e.Graphics.MeasureString(Text, Font);
            using (SolidBrush textBrush = new SolidBrush(TextColor))
            {
                e.Graphics.DrawString(Text, Font, textBrush,
                    cx - (textSize.Width / 2),
                    cy - (textSize.Height / 2));
            }
        }

        // Clean up resources
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}