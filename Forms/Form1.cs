using CyberShield_V3.Models;
using CyberShield_V3.Panels;
using CyberShield_V3.Services;
using CyberShield_V4.Panels;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;


namespace CyberShield_V3
{
    public partial class Form1 : Form
    {
        // Panels
        private DashboardHome dashboardHome;
        private ScanPanel scanPanel;
        private QuarantinePanel quarantinePanel;
        private JunkCleanerPanel junkCleanerPanel;
        private ScanHomePanel scanHomePanel;
        private SettingsPanel settingsPanel;
        private SecurityPanel securityPanel;

        private string threatsLogPath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CyberShield", "threats_log.json");

        private YaraScanner _yaraScanner;
        private volatile bool _stopRequested = false;

        private System.Diagnostics.Stopwatch _uiUpdateStopwatch = new System.Diagnostics.Stopwatch();

        private List<FileSystemWatcher> _protectors = new List<FileSystemWatcher>();
        private NotifyIcon _trayIcon;

        // Logic
        private ScanHistory _scanHistory;
        private CancellationTokenSource scanCancellationToken;
        private List<ThreatInfo> detectedThreats;

        public VirusDatabaseEnhanced VirusDatabase { get; private set; }
        private MalwareBazaarClient _cloudScanner;
        private const string API_KEY = "be38b59f806fcd52c6a74e32142677dedee087645ba915c6";

        // NEW: Track the signature count locally to update the UI later
        private int _signatureCount = 0;

        // Define a list of extensions that MUST be checked in the cloud
        private static readonly HashSet<string> CloudTargetExtensions = new HashSet<string>(StringComparer.OrdinalIgnoreCase)
        {
            // Executables
            ".exe", ".dll", ".scr", ".com", ".msi", ".drv", ".sys",
            // Scripts
            ".bat", ".cmd", ".ps1", ".vbs", ".js", ".jse", ".wsf",
            // Documents (Macros & Exploits)
            ".docm", ".xlsm", ".pptm", ".pdf", ".jar",
            // Archives (Optional - good to check if the specific zip is known malware)
            ".zip", ".rar", ".7z", ".iso"
        };

        public Form1(Dictionary<string, string> preloadedHashes)
        {
            try
            {

                string rulesPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Rules");
                Directory.CreateDirectory(rulesPath); // Keep this to ensure the base folder exists

                // CHANGE: Point directly to the master index file
                string indexFilePath = Path.Combine(rulesPath, "malware_index.yar");

                // Initialize the scanner with the specific index file
                _yaraScanner = new YaraScanner(indexFilePath);

                InitializeComponent(); // Loads Form1.Designer.cs
                this.TransparencyKey = Color.Empty;
                this.BackColor = Color.FromArgb(34, 67, 92);

                detectedThreats = new List<ThreatInfo>();
                LoadThreatsFromDisk();
                VirusDatabase = new VirusDatabaseEnhanced();
                _cloudScanner = new MalwareBazaarClient(API_KEY);
                _scanHistory = new ScanHistory();

                this.Resize += Form1_Resize;

                // Load Hashes and Count Them
                if (preloadedHashes != null)
                {
                    foreach (var kvp in preloadedHashes)
                    {
                        VirusDatabase.AddSignature(kvp.Key, kvp.Value);
                    }
                    _signatureCount += preloadedHashes.Count;
                }

                // Add EICAR Test
                VirusDatabase.AddSignature("44d88612fea8a8f36de82e1278abb02f", "EICAR-Test-File (Safe Test)");
                _signatureCount++; // Count the EICAR signature
            }
            catch (Exception ex)
            {
                MessageBox.Show($"Constructor Error:\n{ex.Message}", "Error");
                throw;
            }
        }

        // Required imports for moving the borderless form
        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int ReleaseCapture();

        [System.Runtime.InteropServices.DllImport("user32.dll")]
        private static extern int SendMessage(IntPtr hWnd, int Msg, int wParam, int lParam);

        protected override void OnLoad(EventArgs e)
        {
            try
            {
                base.OnLoad(e);
                InitializePanels();
                InitializeButtonEvents();
                ShowDashboard();

                // NEW: Trigger the cloud connection check once the UI is ready
                CheckCloudStatus();

                InitializeRealTimeProtection();
            }
            catch (Exception ex)
            {
                MessageBox.Show($"OnLoad Error:\n{ex.Message}", "Error");
            }
        }

        // NEW: Background check for API connectivity

        // Add this inside the Form1 class in Form1.cs
        private void SetNavigationState(bool isEnabled)
        {
            // Disable/Enable the main navigation buttons
            if (homeButton != null) homeButton.Enabled = isEnabled;
            if (scanButton != null) scanButton.Enabled = isEnabled;
            if (cleanButton != null) cleanButton.Enabled = isEnabled;
            if (settingsButton != null) settingsButton.Enabled = isEnabled;
            if (securityButton != null) securityButton.Enabled = isEnabled;

            // Optional: If you want to disable dragging the form during scan
            // if (guna2GradientPanel1 != null) guna2GradientPanel1.Enabled = isEnabled;
        }

        private void ShowSettings()
        {

            SetActivePanel(settingsPanel);

            // Update Button States (Visual Feedback)
            if (homeButton != null) homeButton.Checked = false;
            if (scanButton != null) scanButton.Checked = false;
            if (cleanButton != null) cleanButton.Checked = false;
            if (settingsButton != null) settingsButton.Checked = true;
        }

        private void InitializeRealTimeProtection()
        {
            // 1. Setup Tray Icon for Notifications
            _trayIcon = new NotifyIcon();
            _trayIcon.Icon = this.Icon; // Or use your app icon: this.Icon
            _trayIcon.Visible = true;
            _trayIcon.Text = "CyberShield Active";

            _trayIcon.MouseDoubleClick += (s, e) =>
            {
                RestoreWindow();
            };

            // 2. Define folders to watch
            string[] folders = new string[]
            {
                Environment.GetFolderPath(Environment.SpecialFolder.UserProfile) + "\\Downloads",
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)
            };

            // 3. Create a watcher for each folder
            foreach (var path in folders)
            {
                if (Directory.Exists(path))
                {
                    FileSystemWatcher watcher = new FileSystemWatcher();
                    watcher.Path = path;

                    // Watch for changes to FileName, Size, etc.
                    watcher.NotifyFilter = NotifyFilters.FileName | NotifyFilters.Size | NotifyFilters.LastWrite;

                    // Watch for all files
                    watcher.Filter = "*.*";

                    // Hook up events
                    watcher.Created += OnFileChanged;
                    watcher.Changed += OnFileChanged;
                    watcher.Renamed += OnFileRenamed;

                    // Start watching
                    watcher.EnableRaisingEvents = true;

                    _protectors.Add(watcher);
                }
            }

            // Update Dashboard to show Active
            if (dashboardHome != null) dashboardHome.UpdateProtectionStatus(true);
        }

        // Inside Form1.cs

        private void RestoreWindow()
        {
            // 1. Make it visible again
            this.Show();

            // 2. Restore the state from Minimized to Normal
            this.WindowState = FormWindowState.Normal;

            // 3. Force it to the front and give it focus
            this.BringToFront();
            this.Activate();
        }
        private void Form1_Resize(object sender, EventArgs e)
        {
            // If the user clicked the Minimize button
            if (this.WindowState == FormWindowState.Minimized)
            {
                this.Hide();             // Hide from Taskbar
                _trayIcon.Visible = true; // Ensure Tray Icon is visible
            }
        }

        // Event Handler for File Creation/Change
        private async void OnFileChanged(object sender, FileSystemEventArgs e)
        {
            await Task.Run(() => ScanBackgroundFile(e.FullPath));
        }

        // Event Handler for Renaming
        private async void OnFileRenamed(object sender, RenamedEventArgs e)
        {
            await Task.Run(() => ScanBackgroundFile(e.FullPath));
        }

        // CHANGED: "async Task" allows us to use 'await' correctly
        private async Task ScanBackgroundFile(string filePath)
        {
            try
            {
                if (!File.Exists(filePath)) return;

                // 1. ROBUST WAIT: Retry loop for file locks 
                // (Browsers lock files while downloading; we must wait until they finish)
                int retries = 0;
                while (IsFileLocked(filePath) && retries < 5)
                {
                    await Task.Delay(500); // Wait 0.5s
                    retries++;
                }

                // If still locked after 2.5s, skip it (it might be a system file)
                if (IsFileLocked(filePath)) return;

                FileInfo fi = new FileInfo(filePath);
                if (fi.Length > 50 * 1024 * 1024) return; // Skip files > 50MB

                // 2. Scan: Local Database (Fast)
                ScanResult result = VirusDatabase.ScanFile(filePath);

                // 3. Scan: YARA (Heuristic)
                // Only run if local DB is clean
                if (!result.IsThreat && _yaraScanner != null)
                {
                    ScanResult yaraResult = _yaraScanner.ScanFile(filePath);
                    if (yaraResult.IsThreat) result = yaraResult;
                }

                // 4. Scan: Cloud (Intelligence)
                // CHANGED: Added 'await' to prevent deadlocks
                string ext = Path.GetExtension(filePath);

                if (!result.IsThreat && CloudTargetExtensions.Contains(ext))
                {
                    try
                    {
                        string hash = CalculateSHA256(filePath);

                        var cloud = await _cloudScanner.QueryByHashAsync(hash);

                        if (cloud.Success && cloud.Samples?.Count > 0)
                        {
                            result.IsThreat = true;
                            result.ThreatName = cloud.Samples[0].MalwareName ?? "Cloud Threat";
                            result.Severity = ThreatSeverity.High;
                        }
                    }
                    catch { /* Ignore cloud errors */ }
                }

                // 5. Act
                if (result.IsThreat)
                {
                    var info = new ThreatInfo
                    {
                        FilePath = filePath,
                        ThreatName = result.ThreatName,
                        Severity = result.Severity,
                        DetectedTime = DateTime.Now
                    };

                    // Attempt Quarantine
                    QuarantineFile(info);

                    // Update UI (Main Thread)
                    this.Invoke((MethodInvoker)(() =>
                    {
                        detectedThreats.Add(info);

                        if (securityPanel != null) securityPanel.AddThreatRealTime(info);

                        System.Media.SystemSounds.Hand.Play();

                        if (_trayIcon != null)
                        {
                            _trayIcon.Visible = true;
                            _trayIcon.ShowBalloonTip(3000, "THREAT DETECTED",
                                $"Removed: {result.ThreatName}", ToolTipIcon.Warning);
                        }

                        if (dashboardHome != null) dashboardHome.UpdateThreatsDetected(detectedThreats.Count);
                        if (scanPanel != null) scanPanel.UpdateThreatsFound(detectedThreats.Count);
                    }));
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Scan Error: " + ex.Message);
            }
        }

        // Helper helper to check for locks without crashing
        private bool IsFileLocked(string filePath)
        {
            try
            {
                using (FileStream stream = File.Open(filePath, FileMode.Open, FileAccess.Read, FileShare.None))
                {
                    stream.Close();
                }
            }
            catch (IOException)
            {
                return true; // File is locked
            }
            return false; // File is free
        }

        private async void CheckCloudStatus()
        {
            if (dashboardHome == null) return;

            try
            {
                // Ping the API with a known hash (EICAR) to see if we get a response
                var result = await _cloudScanner.QueryByHashAsync("44d88612fea8a8f36de82e1278abb02f");

                // If query succeeded (even if hash not found, as long as API replied), we are Online
                // MalwareBazaarClient returns Success=true if "query_status" is ok/no_results
                bool isOnline = result.Success || result.Error == "no_results" || result.Error == "hash_not_found";

                // Update UI safely
                this.Invoke((MethodInvoker)(() =>
                {
                    dashboardHome.UpdateCloudUplinkStatus(isOnline);
                }));
            }
            catch
            {
                this.Invoke((MethodInvoker)(() =>
                {
                    dashboardHome.UpdateCloudUplinkStatus(false);
                }));
            }
        }

        private void guna2GradientPanel1_MouseCaptureChanged(object sender, EventArgs e)
        {
            if (!this.IsHandleCreated || this.Disposing || this.IsDisposed) return;
            try { if (this.InvokeRequired) this.BeginInvoke(new Action(() => this.Opacity = 1.0)); else this.Opacity = 1.0; } catch { }
        }

        private void MoveForm(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                ReleaseCapture();
                SendMessage(Handle, 0xA1, 0x2, 0);
            }
        }

        private void InitializePanels()
        {
            try
            {
                // 1. DASHBOARD
                dashboardHome = new DashboardHome();
                dashboardHome.Dock = DockStyle.Fill;

                // NEW: Immediately update the stats on the dashboard
                dashboardHome.UpdateThreatDbCount(_signatureCount);
                if (_scanHistory != null && _scanHistory.LastScanDate != DateTime.MinValue)
                    dashboardHome.UpdateLastScanTime(_scanHistory.LastScanDate);

                dashboardHome.QuickScanClicked += (s, e) => { ShowScanPanel(); StartScanAsync(false); };
                dashboardHome.CleanJunkClicked += (s, e) => ShowJunkCleaner();
                dashboardHome.ViewQuarantineClicked += (s, e) => ShowQuarantinePanel();

                // 2. SCAN HOME
                scanHomePanel = new ScanHomePanel();
                scanHomePanel.Dock = DockStyle.Fill;
                ((ScanHomePanel)scanHomePanel).QuickScanClicked += (s, e) => { ShowScanPanel(); StartScanAsync(false); };
                ((ScanHomePanel)scanHomePanel).DeepScanClicked += (s, e) => { ShowScanPanel(); StartScanAsync(true); };

                // 3. SCANNER
                scanPanel = new ScanPanel();
                scanPanel.Dock = DockStyle.Fill;
                scanPanel.ScanCancelled += (s, e) =>
                {
                    // This is the Kill Switch
                    _stopRequested = true;
                    scanCancellationToken?.Cancel();
                };

                scanPanel.BackClicked += (s, e) => { if (detectedThreats.Count > 0) ShowQuarantinePanel(); else ShowDashboard(); };

                // 4. QUARANTINE
                quarantinePanel = new QuarantinePanel();
                quarantinePanel.Dock = DockStyle.Fill;
                quarantinePanel.BackClicked += (s, e) => ShowDashboard();

                quarantinePanel.ThreatsChanged += (s, e) => SaveThreatsToDisk();

                // 5. JUNK CLEANER
                junkCleanerPanel = new JunkCleanerPanel();
                junkCleanerPanel.Dock = DockStyle.Fill;
                junkCleanerPanel.BackClicked += (s, e) => ShowDashboard();

                // 7. SECURITY PANEL
                securityPanel = new SecurityPanel();
                securityPanel.Dock = DockStyle.Fill;
                securityPanel.ProtectionToggled += (s, active) => {
                    // Logic to enable/disable FileSystemWatchers
                    foreach (var p in _protectors) p.EnableRaisingEvents = active;
                };

                // 6. SETTINGS
                settingsPanel = new SettingsPanel();
                settingsPanel.Dock = DockStyle.Fill;

                // --- ADD THIS MISSING CONNECTION ---
                settingsPanel.ProtectionToggled += (s, isActive) =>
                {
                    // 1. Enable/Disable the Watchers
                    if (_protectors != null)
                    {
                        foreach (var watcher in _protectors)
                        {
                            watcher.EnableRaisingEvents = isActive;
                        }
                    }

                    // 2. Update the Dashboard UI (Turns Red/Green)
                    if (dashboardHome != null)
                    {
                        dashboardHome.UpdateProtectionStatus(isActive);
                    }
                };

                if (mainPanel == null) throw new Exception("mainPanel is null - Designer may have failed");

                mainPanel.Controls.Add(dashboardHome);
                mainPanel.Controls.Add(scanHomePanel);
                mainPanel.Controls.Add(scanPanel);
                mainPanel.Controls.Add(quarantinePanel);
                mainPanel.Controls.Add(junkCleanerPanel);
                mainPanel.Controls.Add(settingsPanel);
                mainPanel.Controls.Add(securityPanel);

            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"InitializePanels FAILED: {ex.Message}");
                throw;
            }
        }

        private void InitializeButtonEvents()
        {
            if (homeButton != null) homeButton.Click += (s, e) => ShowDashboard();
            if (scanButton != null) scanButton.Click += (s, e) => ShowScanHome();
            if (cleanButton != null) cleanButton.Click += (s, e) => ShowJunkCleaner();
            if (settingsButton != null) settingsButton.Click += (s, e) => ShowSettings();
            if (securityButton != null) securityButton.Click += (s, e) => ShowSecurityPanel();

            if (guna2GradientPanel1 != null)
            {
                guna2GradientPanel1.MouseDown += MoveForm;
                guna2GradientPanel1.MouseCaptureChanged += guna2GradientPanel1_MouseCaptureChanged;
            }
        }

        // --- NAVIGATION ---
        private void SetActivePanel(Control panel)
        {
            // 1. Hide the currently visible panel (if any)
            foreach (Control c in mainPanel.Controls)
            {
                if (c.Visible && c != panel)
                {
                    guna2Transition1.HideSync(c); // Smoothly hide the old panel
                }
            }

            // 2. Prepare the new panel (ensure it's on top but hidden)
            panel.Visible = false;
            panel.BringToFront();

            // 3. Smoothly show the new panel
            guna2Transition1.ShowSync(panel);
        }

        private void ShowDashboard()
        {
            SetActivePanel(dashboardHome);
            if (homeButton != null) homeButton.Checked = true;
            if (scanButton != null) scanButton.Checked = false;
            if (cleanButton != null) cleanButton.Checked = false;
        }

        private void ShowScanHome()
        {
            SetActivePanel(scanHomePanel);
            if (scanHomePanel is ScanHomePanel panel && _scanHistory != null && _scanHistory.LastScanDate != DateTime.MinValue)
                panel.UpdateLastScan(_scanHistory.LastScanDate);

            if (homeButton != null) homeButton.Checked = false;
            if (scanButton != null) scanButton.Checked = true;
            if (cleanButton != null) cleanButton.Checked = false;
        }

        private void ShowSecurityPanel()
        {
            securityPanel.RefreshSecurityData(detectedThreats, true); // Replace true with your actual protection state variable
            SetActivePanel(securityPanel);
            // Set button checked states...
        }

        private void ShowScanPanel()
        {
            SetActivePanel(scanPanel);
            if (scanButton != null) scanButton.Checked = true;
        }

        private void ShowQuarantinePanel()
        {
            quarantinePanel.LoadThreats(detectedThreats);
            SetActivePanel(quarantinePanel);
            if (homeButton != null) homeButton.Checked = false;
            if (scanButton != null) scanButton.Checked = false;
            if (cleanButton != null) cleanButton.Checked = false;
        }

        private void ShowJunkCleaner()
        {
            SetActivePanel(junkCleanerPanel);
            if (homeButton != null) homeButton.Checked = false;
            if (scanButton != null) scanButton.Checked = false;
            if (cleanButton != null) cleanButton.Checked = true;
        }

        private void hoverButton1_Click(object sender, EventArgs e)
        {
            ShowScanPanel();
            StartScanAsync(false);
        }

        // --- SCANNING LOGIC ---
        private async void StartScanAsync(bool isDeepScan)
        {

            SetNavigationState(false);

            scanCancellationToken = new CancellationTokenSource();
            scanPanel.StartScan();
            detectedThreats.Clear();

            try
            {
                await Task.Run(() => PerformScan(scanCancellationToken.Token, isDeepScan));

                DateTime now = DateTime.Now;

                // Update UI with results
                if (dashboardHome != null)
                {
                    dashboardHome.UpdateLastScanTime(now);
                    dashboardHome.UpdateThreatsDetected(detectedThreats.Count);
                }

                if (scanHomePanel is ScanHomePanel panel) panel.UpdateLastScan(now);

                if (_scanHistory != null)
                {
                    _scanHistory.LastScanDate = now;
                    _scanHistory.Save();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show("Error: " + ex.Message);
                scanPanel.ScanComplete(true);
            }
            finally
            {
                // 2. RE-ENABLE BUTTONS END
                // This runs whether the scan finishes successfully, is cancelled, or crashes
                SetNavigationState(true);
                // -------------------------
            }

        }

        private void PerformScan(CancellationToken token, bool isDeepScan)
        {
            // RESET Kill Switch
            _stopRequested = false;

            try
            {
                string[] directories;

                // 1. SAFE UI UPDATE: Use Invoke
                this.Invoke((MethodInvoker)(() =>
                {
                    if (isDeepScan)
                    {
                        scanPanel?.UpdateStatus("Initializing Deep Scan...");
                    }
                    else
                    {
                        scanPanel?.UpdateStatus("Initializing Quick Scan...");
                    }
                }));

                if (isDeepScan)
                {
                    directories = Directory.GetLogicalDrives();
                }
                else
                {
                    directories = new string[] {
                Environment.GetFolderPath(Environment.SpecialFolder.Desktop),
                Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments),
                Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.UserProfile), "Downloads"),
                Environment.GetFolderPath(Environment.SpecialFolder.Startup)
            };
                }

                // 2. SAFE UI UPDATE
                this.Invoke((MethodInvoker)(() =>
                {
                    if (scanPanel != null) scanPanel.UpdateStatus("Counting files...");
                }));

                // 3. Count Files (Heavy work - Keep on background thread)
                int totalFiles = 0;
                foreach (var dir in directories)
                {
                    if (_stopRequested) break;
                    if (isDeepScan || Directory.Exists(dir)) totalFiles += CountFiles(dir, token, isDeepScan);
                }
                if (totalFiles == 0) totalFiles = 1;

                // 4. SAFE UI UPDATE
                this.Invoke((MethodInvoker)(() =>
                {
                    if (scanPanel != null) scanPanel.UpdateStatus($"Scanning {totalFiles} files...");
                }));

                int scannedFiles = 0;
                _uiUpdateStopwatch.Restart();

                // 5. Start Scanning
                foreach (var dir in directories)
                {
                    if (_stopRequested) break;
                    if (isDeepScan || Directory.Exists(dir)) ScanDirectory(dir, ref scannedFiles, totalFiles, token, isDeepScan);
                }

                // --- FINAL STOP CHECK ---
                if (_stopRequested || token.IsCancellationRequested)
                {
                    // SAFE UI UPDATE
                    this.Invoke((MethodInvoker)(() =>
                    {
                        if (scanPanel != null)
                        {
                            scanPanel.UpdateStatus("Scan Cancelled");
                            scanPanel.ScanComplete(true);
                        }
                    }));
                    return;
                }

                if (_scanHistory != null) _scanHistory.Save();

                // SAFE UI UPDATE
                this.Invoke((MethodInvoker)(() =>
                {
                    if (scanPanel != null)
                    {
                        scanPanel.UpdateProgress(100);
                        scanPanel.UpdateStatus("Scan Complete");
                    }
                }));

                Thread.Sleep(500); // Small pause so user sees "100%"

                // SAFE UI UPDATE
                this.Invoke((MethodInvoker)(() =>
                {
                    if (scanPanel != null) scanPanel.ScanComplete(false);
                }));
            }
            catch (Exception ex)
            {
                // SAFE UI UPDATE
                this.Invoke((MethodInvoker)(() =>
                {
                    if (scanPanel != null) scanPanel.ScanComplete(true);
                    // Optional: Show error for debugging
                    // MessageBox.Show(ex.Message); 
                }));
            }
        }

        private int CountFiles(string path, CancellationToken token, bool deep)
        {
            // 1. Safety Checks
            if (token.IsCancellationRequested) return 0;

            int count = 0;

            // 2. Count Files (Isolated Try/Catch)
            // If this fails (Access Denied), we still want to try checking subfolders!
            try
            {
                count += Directory.GetFiles(path).Length;
            }
            catch
            {
                // Ignore file errors, count remains 0 for this specific folder 
            }

            // 3. Check Subdirectories (Isolated Try/Catch)
            try
            {
                foreach (var sub in Directory.GetDirectories(path))
                {
                    if (token.IsCancellationRequested) break;

                    string name = Path.GetFileName(sub).ToLower();

                    // Skip protected system folders to prevent loops/errors
                    if (name.StartsWith("$") || name == "system volume information") continue;

                    // Quick Scan Optimization
                    if (!deep && (name == "windows" || name == "program files" || name == "program files (x86)" || name == "appdata")) continue;

                    // Recursive Call
                    count += CountFiles(sub, token, deep);
                }
            }
            catch
            {
                // Ignore directory access errors (e.g., cannot open folder)
            }

            return count;
        }

        private void ScanDirectory(string path, ref int scanned, int total, CancellationToken token, bool deep)
        {
            if (_stopRequested || token.IsCancellationRequested) return;

            try
            {
                var files = Directory.EnumerateFiles(path);

                foreach (var file in files)
                {
                    if (_stopRequested || token.IsCancellationRequested) return;

                    try
                    {
                        FileInfo fi = new FileInfo(file);
                        if (fi.Length > 50 * 1024 * 1024) continue;

                        // --- SCANNING LOGIC ---
                        ScanResult result = VirusDatabase.ScanFile(file);

                        if (!result.IsThreat && _yaraScanner != null)
                        {
                            if (IsScannableExtension(file))
                            {
                                var yaraResult = _yaraScanner.ScanFile(file);
                                if (yaraResult.IsThreat) result = yaraResult;
                            }
                        }

                        if (result.IsThreat)
                        {
                            HandleThreatFound(file, result);
                        }
                        // -----------------------

                        scanned++;

                        // Update every 10 files 
                        if (scanned < 10 || scanned % 10 == 0)
                        {
                            // FIX: Copy 'ref' value to a local variable for the lambda
                            int currentCount = scanned;
                            int progress = (int)((double)scanned / total * 100);

                            this.Invoke((MethodInvoker)(() =>
                            {
                                if (scanPanel != null)
                                {
                                    scanPanel.UpdateProgress(progress);
                                    scanPanel.UpdateStatus($"Scanning: {Path.GetFileName(file)}");

                                    // Use the LOCAL copy, not the 'ref' variable
                                    scanPanel.UpdateFilesScanned(currentCount);
                                }
                            }));
                        }
                    }
                    catch { }
                }

                var dirs = Directory.EnumerateDirectories(path);
                foreach (var sub in dirs)
                {
                    if (_stopRequested || token.IsCancellationRequested) return;

                    string name = Path.GetFileName(sub).ToLower();
                    if (name.StartsWith("$") || name == "system volume information") continue;
                    if (!deep && (name == "windows" || name == "program files" || name == "program files (x86)" || name == "appdata")) continue;

                    ScanDirectory(sub, ref scanned, total, token, deep);
                }
            }
            catch { }
        }

        private void HandleThreatFound(string filePath, ScanResult result)
        {
            // 1. Create the Threat Info object
            var info = new ThreatInfo
            {
                FilePath = filePath,
                ThreatName = result.ThreatName,
                Severity = result.Severity,
                DetectedTime = DateTime.Now
            };

            // 2. Quarantine / Delete the file (Disk Operation)
            QuarantineFile(info);

            // 3. Update the UI (Must be done on the UI Thread)
            this.Invoke((MethodInvoker)(() =>
            {
                // Add to local list
                detectedThreats.Add(info);

                // Update the Security Panel (if visible)
                if (securityPanel != null)
                    securityPanel.AddThreatRealTime(info);

                // Play Alert Sound
                try { System.Media.SystemSounds.Hand.Play(); } catch { }

                // Show Tray Notification
                if (_trayIcon != null)
                {
                    _trayIcon.Visible = true;
                    _trayIcon.ShowBalloonTip(3000, "THREAT DETECTED",
                        $"Removed: {result.ThreatName}", ToolTipIcon.Warning);
                }

                // Update Dashboard Counters
                if (dashboardHome != null)
                    dashboardHome.UpdateThreatsDetected(detectedThreats.Count);

                // Update Scan Panel Counters (Specific to the manual scan page)
                if (scanPanel != null)
                    scanPanel.UpdateThreatsFound(detectedThreats.Count);
            }));
        }

        private bool IsScannableExtension(string filePath)
        {
            string ext = Path.GetExtension(filePath).ToLower();

            // List of files that CAN contain code or scripts
            return ext == ".exe" || ext == ".dll" || ext == ".bat" ||
                   ext == ".ps1" || ext == ".vbs" || ext == ".js" ||
                   ext == ".msi" || ext == ".com" || ext == ".scr" ||
                   ext == ".docm" || ext == ".xlsm" || ext == ".jar";
        }

        private void ScanFile(string file, ref int scanned, int total, CancellationToken token)
        {
            // 1. Stop immediately if user cancelled
            if (_stopRequested) return;

            scanned++;
            try
            {
                // UI Updates (Counters and Progress bar)
                if (_uiUpdateStopwatch.ElapsedMilliseconds > 100 || scanned == total)
                {
                    if (_stopRequested) return;

                    if (scanPanel != null)
                    {
                        scanPanel.UpdateFilesScanned(scanned);
                        scanPanel.UpdateCurrentFile(Path.GetFileName(file));
                        scanPanel.UpdateProgress((int)((double)scanned / total * 100));
                    }
                    _uiUpdateStopwatch.Restart();
                }

                // Optimization: Skip if file hasn't changed since last scan
                if (_scanHistory != null && !_scanHistory.NeedsScanning(file)) return;

                // --- LAYER 1: LOCAL SIGNATURE DATABASE SCAN ---
                if (_stopRequested) return;
                ScanResult result = VirusDatabase.ScanFile(file);

                // --- LAYER 2: YARA PATTERN SCAN (NEW) ---
                // We only run YARA if the local database didn't already find something
                if (!result.IsThreat && _yaraScanner != null)
                {
                    if (_stopRequested) return;

                    // This looks inside the file for malicious code patterns defined in your .yar files
                    ScanResult yaraResult = _yaraScanner.ScanFile(file);
                    if (yaraResult.IsThreat)
                    {
                        result = yaraResult;
                    }
                }

                // --- LAYER 3: CLOUD SCAN (MALWAREBAZAAR) ---
                if (!result.IsThreat && IsExecutable(file))
                {
                    try
                    {
                        if (_stopRequested) return;
                        string hash = CalculateSHA256(file);

                        if (_stopRequested) return;

                        // Pass token to Cloud Scanner
                        var cloudTask = _cloudScanner.QueryByHashAsync(hash, token);
                        var cloud = cloudTask.GetAwaiter().GetResult();

                        if (cloud.Success && cloud.Samples?.Count > 0)
                        {
                            result.IsThreat = true;
                            result.ThreatName = cloud.Samples[0].MalwareName ?? "Cloud Threat";
                            result.Severity = ThreatSeverity.High;
                        }
                    }
                    catch { /* Network error or timeout, skip cloud for this file */ }
                }

                // FINAL ACTION: If any layer found a threat, handle it
                if (result.IsThreat)
                {
                    var info = new ThreatInfo
                    {
                        FilePath = file,
                        ThreatName = result.ThreatName,
                        Severity = result.Severity,
                        DetectedTime = DateTime.Now
                    };

                    detectedThreats.Add(info);

                    // Update the Security Panel in real-time
                    this.Invoke((MethodInvoker)(() => {
                        if (securityPanel != null) securityPanel.AddThreatRealTime(info);
                    }));

                    if (scanPanel != null) scanPanel.UpdateThreatsFound(detectedThreats.Count);

                    // Move file to quarantine
                    QuarantineFile(info);
                }
                else if (_scanHistory != null)
                {
                    // If clean, mark it so we don't scan it again until it changes
                    _scanHistory.MarkAsScanned(file);
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"Error scanning {file}: {ex.Message}");
            }
        }

        private string CalculateSHA256(string file)
        {
            try { using (var s = SHA256.Create()) using (var f = File.OpenRead(file)) return BitConverter.ToString(s.ComputeHash(f)).Replace("-", "").ToLower(); } catch { return ""; }
        }

        private void QuarantineFile(ThreatInfo threat)
        {
            // Retry up to 3 times (helps if file is still being downloaded/written)
            for (int i = 0; i < 3; i++)
            {
                try
                {
                    if (!File.Exists(threat.FilePath)) return; // Already gone

                    string folder = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CyberShield", "Quarantine");
                    Directory.CreateDirectory(folder);

                    // FIX 1: Generate a UNIQUE name (OriginalName_Timestamp.quarantine)
                    // This prevents "File already exists" errors if you scan the same threat twice.
                    string uniqueName = $"{Path.GetFileNameWithoutExtension(threat.FilePath)}_{DateTime.Now.Ticks}{Path.GetExtension(threat.FilePath)}.quarantine";
                    string dest = Path.Combine(folder, uniqueName);

                    // FIX 2: Move the file
                    File.Move(threat.FilePath, dest);

                    // Success
                    threat.QuarantinedPath = dest;
                    threat.IsQuarantined = true;
                    SaveThreatsToDisk();
                    return; // Done
                }
                catch (IOException)
                {
                    // File is locked (e.g. browser still downloading). Wait 1s and retry.
                    Thread.Sleep(1000);
                }
                catch (Exception ex)
                {
                    // Other errors (permission, etc)
                    threat.QuarantineError = ex.Message;
                    System.Diagnostics.Debug.WriteLine($"Quarantine Failed: {ex.Message}");
                    break;
                }
            }
        }

        private bool IsExecutable(string f) { string x = Path.GetExtension(f).ToLower(); return x == ".exe" || x == ".dll" || x == ".bat"; }

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            // 1. Clean up the Tray Icon so it doesn't get stuck in the taskbar
            if (_trayIcon != null)
            {
                _trayIcon.Visible = false;
                _trayIcon.Dispose();
            }

            // 2. Stop the scanner if it's running
            if (scanCancellationToken != null)
            {
                scanCancellationToken.Cancel();
            }

            SaveThreatsToDisk();
            base.OnFormClosing(e);
        }
        private void mainPanel_Paint(object sender, PaintEventArgs e) { }
        private void guna2PictureBox1_Click(object sender, EventArgs e) { }

        private void sidePanel_Paint(object sender, PaintEventArgs e)
        {

        }

        private void SaveThreatsToDisk()
        {
            try
            {
                // Ensure the directory exists before writing
                string directory = Path.GetDirectoryName(threatsLogPath);
                if (!Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                string json = JsonSerializer.Serialize(detectedThreats, new JsonSerializerOptions { WriteIndented = true });
                File.WriteAllText(threatsLogPath, json);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine("Failed to save threats: " + ex.Message);
            }
        }

        private void LoadThreatsFromDisk()
        {
            try
            {
                if (File.Exists(threatsLogPath))
                {
                    string json = File.ReadAllText(threatsLogPath);
                    var loadedThreats = JsonSerializer.Deserialize<List<ThreatInfo>>(json);
                    if (loadedThreats != null)
                    {
                        detectedThreats = loadedThreats;
                        // Verify files still exist in quarantine (optional cleanup)
                        detectedThreats.RemoveAll(t => t.IsQuarantined && !File.Exists(t.QuarantinedPath));
                    }
                }
            }
            catch { /* File might be corrupted, start fresh */ }
        }
    }
}