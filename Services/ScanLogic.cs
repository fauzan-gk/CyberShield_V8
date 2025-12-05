using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;

namespace CyberShield_V3.Services
{
    public class ThreatInfo
    {
        public string FilePath { get; set; } = string.Empty;
        public string ThreatName { get; set; } = string.Empty;
        public string ThreatType { get; set; } = "Generic";
        public string DetectionMethod { get; set; } = "Unknown";
        public DateTime DetectedTime { get; set; } = DateTime.Now;
        public bool IsQuarantined { get; set; }
        public string QuarantinedPath { get; set; } = string.Empty;
        public string QuarantineError { get; set; } = string.Empty;
        public ThreatSeverity Severity { get; set; } = ThreatSeverity.Low;
    }

    public class ScanResult
    {
        public bool IsThreat { get; set; }
        public string ThreatName { get; set; } = string.Empty;
        public string ThreatType { get; set; } = string.Empty;
        public string DetectionMethod { get; set; } = string.Empty;
        public ThreatSeverity Severity { get; set; } = ThreatSeverity.Low;
    }

    public enum ThreatSeverity { Low, Medium, High, Critical }

    public class VirusDatabaseEnhanced
    {
        private Dictionary<string, string> signatures = new Dictionary<string, string>();

        public void AddSignature(string hash, string threatName)
        {
            if (!signatures.ContainsKey(hash.ToLower()))
                signatures[hash.ToLower()] = threatName;
        }

        public ScanResult ScanFile(string filePath)
        {
            ScanResult result = new ScanResult();
            try
            {
                FileInfo fileInfo = new FileInfo(filePath);
                if (fileInfo.Length > 100 * 1024 * 1024 || IsSystemFile(filePath)) return result;

                string md5 = CalculateMD5(filePath);
                if (!string.IsNullOrEmpty(md5) && signatures.TryGetValue(md5, out var name))
                    return CreateResult(name, "MD5");

                string sha256 = CalculateSHA256(filePath);
                if (!string.IsNullOrEmpty(sha256) && signatures.TryGetValue(sha256, out name))
                    return CreateResult(name, "SHA256");
            }
            catch (UnauthorizedAccessException) { }
            catch (IOException) { }
            catch (Exception ex)
            {
                // optional: log ex.Message
            }
            return result;
        }

        private ScanResult CreateResult(string name, string method)
        {
            return new ScanResult { IsThreat = true, ThreatName = name, ThreatType = "Malware", DetectionMethod = method, Severity = ThreatSeverity.Critical };
        }

        private bool IsSystemFile(string filePath)
        {
            string p = filePath.ToLower();
            return p.Contains("\\windows\\") || p.Contains("\\program files") || p.Contains("\\programdata");
        }

        private string CalculateMD5(string path)
        {
            try { using (var md5 = MD5.Create()) using (var s = File.OpenRead(path)) return BitConverter.ToString(md5.ComputeHash(s)).Replace("-", "").ToLower(); } catch { return ""; }
        }
        private string CalculateSHA256(string path)
        {
            try { using (var sha = SHA256.Create()) using (var s = File.OpenRead(path)) return BitConverter.ToString(sha.ComputeHash(s)).Replace("-", "").ToLower(); } catch { return ""; }
        }
    }
}