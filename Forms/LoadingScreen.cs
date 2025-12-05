using CyberShield_V3.Services;
using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Threading;
using System.Windows.Forms;

namespace CyberShield_V3
{
    public partial class LoadingScreen : Form
    {
        private const string API_KEY = "be38b59f806fcd52c6a74e32142677dedee087645ba915c6";

        // Animation state
        private double rotationAngle = 0;
        private double opacityValue = 0;
        private double progressValue = 0;

        private CancellationTokenSource cts = new CancellationTokenSource();


        // Target duration (make it longer/shorter)
        private const int DISPLAY_DURATION_MS = 3500;

        // Controls
        private Panel logoPanel;
        private Label titleLabel;
        private Label statusLabel;
        private Label versionLabel;
        private Panel loadingProgressPanel;

        // Threads
        private Thread animationThread;
        private Thread loadingThread;

        public LoadingScreen()
        {
            InitializeCustomComponents();
            StartFadeIn();

            this.Shown += (s, e) =>
            {
                StartAnimationLoop(); // now safe: Handle is created
                StartLoadingSimulation();
            };
        }

        private void InitializeCustomComponents()
        {
            this.Size = new Size(600, 400);
            this.StartPosition = FormStartPosition.CenterScreen;
            this.FormBorderStyle = FormBorderStyle.None;
            this.BackColor = Color.FromArgb(20, 25, 45);
            this.DoubleBuffered = true;

            // Rounded corners
            Region = Region.FromHrgn(CreateRoundRectRgn(0, 0, Width, Height, 20, 20));

            // ----- LOGO PANEL -----
            logoPanel = new Panel
            {
                Size = new Size(150, 150),
                Location = new Point((Width - 150) / 2, 60),
                BackColor = Color.Transparent
            };

            typeof(Panel).InvokeMember("DoubleBuffered",
                System.Reflection.BindingFlags.SetProperty |
                System.Reflection.BindingFlags.Instance |
                System.Reflection.BindingFlags.NonPublic,
                null, logoPanel, new object[] { true });

            logoPanel.Paint += LogoPanel_Paint;

            // TITLE
            titleLabel = new Label
            {
                Text = "CyberShield",
                Font = new Font("Segoe UI", 28, FontStyle.Bold),
                ForeColor = Color.White,
                AutoSize = true,
                Location = new Point((Width - 250) / 2, 230)
            };

            // VERSION
            versionLabel = new Label
            {
                Text = "Version 3.0",
                Font = new Font("Segoe UI", 10),
                ForeColor = Color.FromArgb(150, 160, 180),
                AutoSize = true,
                Location = new Point((Width - 100) / 2, 280)
            };

            // STATUS
            statusLabel = new Label
            {
                Text = "Initializing...",
                Font = new Font("Segoe UI", 11),
                ForeColor = Color.FromArgb(100, 150, 255),
                AutoSize = true,
                Location = new Point((Width - 150) / 2, 310)
            };

            // PROGRESS BAR
            loadingProgressPanel = new Panel
            {
                Size = new Size(400, 8),
                Location = new Point((Width - 400) / 2, 350),
                BackColor = Color.FromArgb(40, 50, 70) // Dark background color
            };
            loadingProgressPanel.Paint += LoadingProgressPanel_Paint;
            Controls.Add(loadingProgressPanel);

            // Add all controls
            Controls.Add(logoPanel);
            Controls.Add(titleLabel);
            Controls.Add(versionLabel);
            Controls.Add(statusLabel);
            Controls.Add(loadingProgressPanel);
            
                    }

        private void LoadingProgressPanel_Paint(object sender, PaintEventArgs e)
        {
            int progressWidth = (int)((double)loadingProgressPanel.Width * (progressValue / 100.0));
            Rectangle rect = new Rectangle(0, 0, progressWidth, loadingProgressPanel.Height);

            // Define your desired blue color
            Color progressBarColor = Color.FromArgb(100, 150, 255);

            using (SolidBrush brush = new SolidBrush(progressBarColor))
            {
                e.Graphics.FillRectangle(brush, rect);
            }
        }

        private void LogoPanel_Paint(object sender, PaintEventArgs e)
        {
            Graphics g = e.Graphics;
            g.SmoothingMode = SmoothingMode.AntiAlias;

            int cx = logoPanel.Width / 2;
            int cy = logoPanel.Height / 2;

            // OUTER ARC
            using (Pen p = new Pen(Color.FromArgb(100, 150, 255), 4))
                g.DrawArc(p, new Rectangle(10, 10, 130, 130), (float)rotationAngle, 260);

            // MIDDLE ARC
            using (Pen p = new Pen(Color.FromArgb(80, 120, 200), 3))
                g.DrawArc(p, new Rectangle(25, 25, 100, 100), -(float)rotationAngle, 180);

            DrawShield(g, cx, cy, 35);
        }

        public void UpdateStatus(string text)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateStatus(text)));
                return;
            }

            statusLabel.Text = text;
            statusLabel.Location = new Point((Width - statusLabel.Width) / 2, 310);
        }


        public void UpdateProgress(int value)
        {
            if (InvokeRequired)
            {
                Invoke(new Action(() => UpdateProgress(value)));
                return;
            }

            progressValue = value;
            loadingProgressPanel.Invalidate();
        }


        private void DrawShield(Graphics g, int cx, int cy, int size)
        {
            Point[] pts =
            {
                new Point(cx, cy - size),
                new Point(cx + size, cy - size/2),
                new Point(cx + size, cy + size/2),
                new Point(cx, cy + size),
                new Point(cx - size, cy + size/2),
                new Point(cx - size, cy - size/2)
            };

            using GraphicsPath path = new GraphicsPath();
            path.AddPolygon(pts);

            using LinearGradientBrush brush = new LinearGradientBrush(
                new Rectangle(cx - size, cy - size, size * 2, size * 2),
                Color.FromArgb(100, 150, 255),
                Color.FromArgb(60, 100, 200),
                LinearGradientMode.Vertical);

            g.FillPath(brush, path);
            g.DrawPath(new Pen(Color.White, 2), path);

            using Pen check = new Pen(Color.White, 3)
            {
                StartCap = LineCap.Round,
                EndCap = LineCap.Round
            };

            g.DrawLines(check, new[]
            {
                new Point(cx - 15, cy),
                new Point(cx - 5, cy + 10),
                new Point(cx + 15, cy - 10)
            });
        }

        // ============================================================
        //                    ANIMATION THREADS
        // ============================================================

        private void StartFadeIn()
        {
            opacityValue = 0;
            this.Opacity = 0;
        }

        private void StartAnimationLoop()
        {
            animationThread = new Thread(() =>
            {
                const int FPS = 60;
                const int FRAME_TIME = 1000 / FPS;

                var sw = new System.Diagnostics.Stopwatch();
                sw.Start();

                while (true)
                {
                    // FADE IN
                    if (opacityValue < 1)
                        opacityValue += 0.02;

                    // ROTATION
                    rotationAngle += 3;
                    if (rotationAngle >= 360) rotationAngle = 0;

                    // Update UI thread safely
                    if (!IsDisposed && IsHandleCreated && loadingProgressPanel != null && !loadingProgressPanel.IsDisposed)
                    {
                        try
                        {
                            this.BeginInvoke(new Action(() =>
                            {
                                if (!IsDisposed)
                                {
                                    // 🔑 Add/verify this line to update the form's opacity
                                    this.Opacity = opacityValue;

                                    logoPanel.Invalidate();
                                }
                            }));
                        }
                        catch { /* ignore if form disposed */ }
                    }


                    Thread.Sleep(FRAME_TIME);
                }
            });

            animationThread.IsBackground = true;
            animationThread.Start();
        }


        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            cts.Cancel();
            base.OnFormClosing(e);
        }


        private void StartLoadingSimulation()
        {
            // Run this in the background so the spinner keeps spinning
            Task.Run(async () =>
            {
                try
                {
                    // --- PHASE 1: INITIALIZATION ---
                    UpdateStatus("Initializing Security Core...");
                    UpdateProgress(10);
                    await Task.Delay(500);

                    // --- PHASE 2: DOWNLOAD DATABASE ---
                    UpdateStatus("Contacting MalwareBazaar Cloud...");
                    UpdateProgress(30);

                    MalwareHashLoader loader = new MalwareHashLoader(API_KEY);
                    Dictionary<string, string> hashes = new Dictionary<string, string>();

                    try
                    {
                        hashes = await loader.LoadHashesAsync();
                        UpdateProgress(80);
                        UpdateStatus($"Database Updated: {hashes.Count} Signatures");
                        await Task.Delay(800);
                    }
                    catch (Exception ex)
                    {
                        System.Diagnostics.Debug.WriteLine($"API Load Failed: {ex.Message}");
                        UpdateStatus("Offline Mode: API Unavailable");
                        await Task.Delay(1000);
                    }

                    // --- PHASE 3: LAUNCH MAIN FORM ---
                    UpdateStatus("Starting CyberShield V3...");
                    UpdateProgress(100);
                    await Task.Delay(400);

                    if (!IsDisposed && IsHandleCreated)
                    {
                        this.Invoke(new Action(() =>
                        {
                            try
                            {
                                // Add this delay to ensure loading screen is visible
                                System.Threading.Thread.Sleep(200);

                                Form1 mainForm = new Form1(hashes);

                                // Show first, THEN hide loading screen
                                mainForm.Show();
                                mainForm.BringToFront();

                                this.Hide();
                            }
                            catch (Exception ex)
                            {
                                MessageBox.Show($"Startup Error:\n\n{ex.Message}\n\n{ex.StackTrace}",
                                    "Critical Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                                Application.Exit();
                            }
                        }));
                    }
                }
                catch (Exception ex)
                {
                    // Catch background thread errors
                    this.Invoke(new Action(() =>
                    {
                        MessageBox.Show($"Loading Error:\n{ex.Message}", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    }));
                }
            });
        }

        // Rounded corner API
        [System.Runtime.InteropServices.DllImport("Gdi32.dll", EntryPoint = "CreateRoundRectRgn")]
        private static extern IntPtr CreateRoundRectRgn(
            int left, int top, int right, int bottom, int width, int height);
    }
}
