using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using dnYara;
using CyberShield_V3.Services;

namespace CyberShield_V3.Services
{
    public class YaraScanner : IDisposable
    {
        private readonly CompiledRules _rules;
        private readonly YaraContext _ctx; // Persistent context to prevent memory errors

        public YaraScanner(string rulesDirectory)
        {
            try
            {
                // 1. Initialize the global YARA context first
                _ctx = new YaraContext();

                using (var compiler = new Compiler())
                {
                    string[] ruleFiles = Array.Empty<string>();

                    if (Directory.Exists(rulesDirectory))
                    {
                        ruleFiles = Directory.GetFiles(rulesDirectory, "*.yar*");
                        foreach (var file in ruleFiles)
                        {
                            compiler.AddRuleFile(file);
                        }
                    }
                    _rules = compiler.Compile();
                    System.Diagnostics.Debug.WriteLine($"YARA: Loaded {ruleFiles.Length} files. Rules compiled successfully.");
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"YARA Compilation Error: {ex.Message}");
            }
        }

        public CyberShield_V3.Services.ScanResult ScanFile(string filePath)
        {
            var myResult = new CyberShield_V3.Services.ScanResult { IsThreat = false };

            try
            {
                if (_rules == null) return myResult;

                var scanner = new dnYara.Scanner();

                // 2. Capture the list of results
                List<dnYara.ScanResult> yaraResults = scanner.ScanFile(filePath, _rules);

                // 3. FIX: Iterate through the results to find the first match
                foreach (var scanRes in yaraResults)
                {
                    // scanRes.Matches is a Dictionary<string, List<Match>>
                    // The KEY of the dictionary is the actual Rule Name
                    if (scanRes.Matches != null && scanRes.Matches.Count > 0)
                    {
                        string ruleName = scanRes.Matches.Keys.FirstOrDefault();

                        if (!string.IsNullOrEmpty(ruleName))
                        {
                            myResult.IsThreat = true;
                            myResult.ThreatName = $"YARA: {ruleName}";
                            myResult.Severity = CyberShield_V3.Services.ThreatSeverity.High;
                            myResult.DetectionMethod = "YARA Pattern";
                            myResult.ThreatType = "Malware";

                            return myResult; // Exit immediately when threat is found
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"YARA Scan Error: {ex.Message}");
            }

            return myResult;
        }

        public void Dispose()
        {
            _rules?.Dispose();
            _ctx?.Dispose(); // Properly shut down the engine
        }
    }
}