using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace CyberShield_V3.Models
{
    public class ScanHistoryData
    {
        public DateTime LastScanDate { get; set; }
        public Dictionary<string, DateTime> History { get; set; } = new Dictionary<string, DateTime>();
    }

    public class ScanHistory
    {
        private ScanHistoryData _data = new ScanHistoryData();
        private readonly string _historyPath;
        private readonly object _lock = new object();

        public DateTime LastScanDate
        {
            get
            {
                lock (_lock) { return _data.LastScanDate; }
            }
            set
            {
                lock (_lock) { _data.LastScanDate = value; }
            }
        }

        public ScanHistory()
        {
            string docPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
            string folder = Path.Combine(docPath, "CyberShield");
            if (!Directory.Exists(folder)) Directory.CreateDirectory(folder);

            _historyPath = Path.Combine(folder, "scan_history.json");
            Load();
        }

        // Normalize paths to full lowercase
        private string NormalizePath(string path)
        {
            return Path.GetFullPath(path).ToLowerInvariant();
        }

        public bool NeedsScanning(string filePath)
        {
            try
            {
                string normalizedPath = NormalizePath(filePath);
                FileInfo fi = new FileInfo(filePath);
                if (!fi.Exists) return false;

                lock (_lock)
                {
                    if (!_data.History.ContainsKey(normalizedPath)) return true;

                    DateTime lastSeen = _data.History[normalizedPath];
                    if (TrimMilliseconds(fi.LastWriteTime) > TrimMilliseconds(lastSeen)) return true;

                    return false;
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"NeedsScanning error: {ex.Message}");
#endif
                return true; // fallback to scanning if any error
            }
        }

        public void MarkAsScanned(string filePath)
        {
            try
            {
                string normalizedPath = NormalizePath(filePath);
                FileInfo fi = new FileInfo(filePath);
                if (!fi.Exists) return;

                lock (_lock)
                {
                    _data.History[normalizedPath] = TrimMilliseconds(fi.LastWriteTime);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"MarkAsScanned error: {ex.Message}");
#endif
            }
        }

        private DateTime TrimMilliseconds(DateTime dt)
        {
            return new DateTime(dt.Year, dt.Month, dt.Day, dt.Hour, dt.Minute, dt.Second);
        }

        public void Load()
        {
            try
            {
                if (File.Exists(_historyPath))
                {
                    string json = File.ReadAllText(_historyPath);
                    var loaded = JsonSerializer.Deserialize<ScanHistoryData>(json);
                    if (loaded != null)
                    {
                        lock (_lock)
                        {
                            _data = loaded;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"ScanHistory Load error: {ex.Message}");
#endif
                lock (_lock)
                {
                    _data = new ScanHistoryData();
                }
            }
        }

        public void Save()
        {
            try
            {
                lock (_lock)
                {
                    var options = new JsonSerializerOptions { WriteIndented = true };
                    string json = JsonSerializer.Serialize(_data, options);
                    File.WriteAllText(_historyPath, json);
                }
            }
            catch (Exception ex)
            {
#if DEBUG
                Console.WriteLine($"ScanHistory Save error: {ex.Message}");
#endif
            }
        }
    }
}
