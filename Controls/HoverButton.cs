using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.ComponentModel;

namespace CyberShield_V3.Controls
{
    public class HoverButton : Control
    {
        private System.Windows.Forms.Timer _timer;
        private System.Windows.Forms.Timer _fadeTimer;
        private float _angle = 0;
        private float _hoverAngle = 15;
        private int _rotationSpeed = 20;
        private bool _IsHovered = false;
        private float _imageOpacity = 0.0f;
        private float _textOpacity = 1.0f;
        private bool _isFadingToImage = false;

        private Image _hoverImage;
        private string _centerText = "Scan";
        private Color _centerTextColor = Color.White;
        private Color _centerColor1 = Color.Black;
        private Color _centerColor2 = Color.FromArgb(30, 25, 56);
        private Color _surroundColor1 = Color.Green;
        private Color _surroundColor2 = Color.Green;

        private Color _bigStrokeStartColor = Color.FromArgb(30, 35, 70);
        private Color _bigStrokeEndColor = Color.FromArgb(10, 10, 30);
        private int _bigStrokeThickness = 25;
        private Color _rotatingStrokeStartColor = Color.DarkRed;
        private Color _rotatingStrokeEndColor = Color.LimeGreen;
        private int _rotatingStrokeRadius = 10;
        private Color _surroundStrokeColor = Color.White;
        private int _surroundStrokeThickness = 10;

        public HoverButton()
        {
            this.DoubleBuffered = true;
            this.Size = new Size(200, 200);
            this.BackColor = Color.Black;
            this.Cursor = Cursors.Hand;

            InitializeTimers();
        }

        [Category("Custom Settings"), Description("Center Image Shown When Hovered")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Image HoverImage
        {
            get { return _hoverImage; }
            set { _hoverImage = value; Invalidate(); }

        }

        [Category("Custom Settings"), Description("Center Text")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public string CenterText
        {
            get { return _centerText; }
            set { _centerText = value; Invalidate(); }

        }

        [Category("Custom Settings"), Description("Center Text Color")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color CenterTextColor
        {
            get { return _centerTextColor; }
            set { _centerTextColor = value; Invalidate(); }
        }

        [Category("Custom Settings"), Description("Center Color 1")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color CenterColor1
        {
            get { return _centerColor1; }
            set { _centerColor1 = value; Invalidate(); }


        }

        [Category("Custom Settings"), Description("Center Color 2")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color CenterColor2
        {
            get { return _centerColor2; }
            set { _centerColor2 = value; Invalidate(); }
        }

        [Category("Custom Settings"), Description("Surround Color 1")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color SurroundColor1
        {
            get { return _surroundColor1; }
            set { _surroundColor1 = value; Invalidate(); }
        }

        [Category("Custom Settings"), Description("Surround Color 2")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color SurroundColor2
        {
            get { return _surroundColor2; }
            set { _surroundColor2 = value; Invalidate(); }
        }

        [Category("Custom Settings"), Description("Big Stroke Start Color")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BigStrokeStartColor
        {
            get { return _bigStrokeStartColor; }
            set { _bigStrokeStartColor = value; Invalidate(); }
        }

        [Category("Custom Settings"), Description("Big Stroke End Color")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color BigStrokeEndColor
        {
            get { return _bigStrokeEndColor; }
            set { _bigStrokeEndColor = value; Invalidate(); }
        }

        [Category("Custom Settings"), Description("Big Stroke Thickness")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int BigStrokeThickness
        {
            get { return _bigStrokeThickness; }
            set { _bigStrokeThickness = value; Invalidate(); }
        }

        [Category("Custom Settings"), Description("Rotating Stroke Start Color")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color RotatingStrokeStartColor
        {
            get { return _rotatingStrokeStartColor; }
            set { _rotatingStrokeStartColor = value; Invalidate(); }
        }

        [Category("Custom Settings"), Description("Rotating Stroke End Color")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color RotatingStrokeEndColor
        {
            get { return _rotatingStrokeEndColor; }
            set { _rotatingStrokeEndColor = value; Invalidate(); }

        }

        [Category("Custom Settings"), Description("Rotating Stroke Width")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int RotatingStrokeRadius
        {
            get { return _rotatingStrokeRadius; }
            set { _rotatingStrokeRadius = value; Invalidate(); }
        }

        [Category("Custom Settings"), Description("Surround Stroke Color")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public Color SurroundStrokeColor
        {
            get { return _surroundStrokeColor; }
            set { _surroundStrokeColor = value; Invalidate(); }
        }

        [Category("Custom Settings"), Description("Surround Stroke Width")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int SurroundStrokeThickness
        {
            get { return _surroundStrokeThickness; }
            set { _surroundStrokeThickness = value; Invalidate(); }
        }

        [Category("Custom Settings"), Description("Rotation Speed")]
        [DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
        public int RotationSpeed
        {
            get { return _rotationSpeed; }
            set { _rotationSpeed = value; if (_timer != null) _timer.Interval = _rotationSpeed; Invalidate(); }
        }

        private void InitializeTimers()
        {
            _timer = new System.Windows.Forms.Timer();
            _timer.Interval = _rotationSpeed;
            _timer.Tick += Timer_Tick;
            _timer.Start();

            _fadeTimer = new System.Windows.Forms.Timer();
            _fadeTimer.Interval = 30;
            _fadeTimer.Tick += Fade_Tick;

        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            if (_IsHovered)
            {
                if (_angle < _hoverAngle)
                {
                    _angle += 1;
                }
            }
            else
            {
                if (_angle > 0)
                {
                    _angle -= 1;
                }

            }
            this.Invalidate();
        }

        private void Fade_Tick(object sender, EventArgs e)
        {
            if (_isFadingToImage)
            {
                if (_imageOpacity < 1)
                {
                    _imageOpacity += 0.05f;
                    _textOpacity -= 0.05f;

                }
                else
                {
                    _imageOpacity = 1;
                    _fadeTimer.Stop();
                    Invalidate();

                }
            }
            else
            {
                if (_imageOpacity > 0)
                {
                    _imageOpacity -= 0.05f;
                    _textOpacity += 0.05f;
                }
                else
                {
                    _imageOpacity = 0;
                    _fadeTimer.Stop();
                    Invalidate();

                }

            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);
            _IsHovered = true;
            _isFadingToImage = true;
            _fadeTimer.Start();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);
            _IsHovered = false;
            _isFadingToImage = false;
            _fadeTimer.Start();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;
            DrawRadialGradiantBackground(g);
            DrawBigOuterStroke(g);
            DrawSurroundStroke(g);
            DrawRotatingSegments(g);

            if (_IsHovered && _hoverImage != null)
            {
                DrawCenterImage(g, _imageOpacity);
            }
            else
            {
                DrawCenterText(g, _textOpacity);
            }
        }

        private Rectangle GetCenteredSquare(int size)
        {
            int controlSize = Math.Min(this.Width, this.Height);
            int s = Math.Min(size, controlSize);
            int cx = this.Width / 2;
            int cy = this.Height / 2;
            return new Rectangle(cx - s / 2, cy - s / 2, s, s);
        }

        private Rectangle GetInnerCircleRect(int padding)
        {
            int controlSize = Math.Min(this.Width, this.Height);
            int diameter = Math.Max(0, controlSize - padding);
            int cx = this.Width / 2;
            int cy = this.Height / 2;
            return new Rectangle(cx - diameter / 2, cy - diameter / 2, diameter, diameter);
        }

        private void DrawRadialGradiantBackground(Graphics g)
        {
            // inner circle sits inside the big stroke with some margin
            int padding = _bigStrokeThickness + 20;
            Rectangle rect = GetInnerCircleRect(padding);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            using (GraphicsPath path = new GraphicsPath())
            {
                path.AddEllipse(rect);
                using (PathGradientBrush pgb = new PathGradientBrush(path))
                {
                    pgb.CenterColor = _centerColor1;
                    pgb.SurroundColors = new Color[] { _centerColor2 };
                    g.FillEllipse(pgb, rect);
                }
            }
        }

        private void DrawBigOuterStroke(Graphics g)
        {
            int diameter = Math.Min(this.Width, this.Height) - 0;
            diameter = Math.Max(0, diameter - 0);
            // big stroke occupies nearly full control but inset by half thickness
            Rectangle rect = new Rectangle(this.Width/2 - diameter/2 + (_bigStrokeThickness/2), this.Height/2 - diameter/2 + (_bigStrokeThickness/2), diameter - _bigStrokeThickness, diameter - _bigStrokeThickness);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            using (LinearGradientBrush lgb = new LinearGradientBrush(rect, _bigStrokeStartColor, _bigStrokeEndColor, LinearGradientMode.ForwardDiagonal))
            {
                using (Pen pen = new Pen(lgb, _bigStrokeThickness))
                {
                    g.DrawEllipse(pen, rect);
                }
            }
        }

        private void DrawSurroundStroke(Graphics g)
        {
            // surround stroke sits inside big stroke
            int padding = _bigStrokeThickness + 10 + (_surroundStrokeThickness / 2);
            Rectangle rect = GetInnerCircleRect(padding);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            using (Pen pen = new Pen(_surroundStrokeColor, _surroundStrokeThickness))
            {
                g.DrawEllipse(pen, rect);
            }
        }

        private void DrawRotatingSegments(Graphics g)
        {
            // rotating segments sit inside surround stroke
            int padding = _bigStrokeThickness + _surroundStrokeThickness + 20 + (_rotatingStrokeRadius / 2);
            Rectangle rect = GetInnerCircleRect(padding);
            if (rect.Width <= 0 || rect.Height <= 0) return;

            using (LinearGradientBrush lgb = new LinearGradientBrush(rect, _rotatingStrokeStartColor, _rotatingStrokeEndColor, LinearGradientMode.ForwardDiagonal))
            {
                using (Pen pen = new Pen(lgb, _rotatingStrokeRadius))
                {
                    // rotate about control center
                    g.TranslateTransform(this.Width / 2f, this.Height / 2f);
                    g.RotateTransform(_angle);
                    g.TranslateTransform(-this.Width / 2f, -this.Height / 2f);

                    for (int i = 0; i < 12; i++)
                    {
                        g.DrawArc(pen, rect, i * 30, 15);
                    }

                    g.ResetTransform();
                }
            }
        }

        private void DrawCenterText(Graphics g, float opacity)
        {
            float clamped = Math.Max(0, Math.Min(1, opacity));
            using (SolidBrush brush = new SolidBrush(Color.FromArgb((int)(clamped * 255), _centerTextColor)))
            {
                SizeF textSize = g.MeasureString(_centerText, this.Font);
                float cx = this.Width / 2f;
                float cy = this.Height / 2f;
                PointF location = new PointF(cx - textSize.Width / 2f, cy - textSize.Height / 2f);
                g.DrawString(_centerText, this.Font, brush, location);
            }
        }

        private void DrawCenterImage(Graphics g, float opacity)
        {
            if (_hoverImage == null)
                return;

            float clamped = Math.Max(0, Math.Min(1, opacity));
            int controlSize = Math.Min(this.Width, this.Height);
            int imageSize = controlSize / 3;
            Rectangle rect = new Rectangle(this.Width/2 - imageSize/2, this.Height/2 - imageSize/2, imageSize, imageSize);

            using (ImageAttributes imgAttr = new ImageAttributes())
            {
                ColorMatrix colorMatrix = new ColorMatrix();
                colorMatrix.Matrix33 = clamped;
                imgAttr.SetColorMatrix(colorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
                g.DrawImage(_hoverImage, rect, 0, 0, _hoverImage.Width, _hoverImage.Height, GraphicsUnit.Pixel, imgAttr);
            }
        }

    }
}




