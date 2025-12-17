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
        // CHANGED: Removed 'readonly' so we can update it in ReloadRules
        private CompiledRules _rules;
        private readonly YaraContext _ctx;

        // Constructor can now accept a File Path (index.yar) OR a Folder Path
        public YaraScanner(string path)
        {
            try
            {
                // 1. Initialize global context
                _ctx = new YaraContext();

                // 2. Compile rules based on the path provided
                CompileRulesInternal(path);
            }
            catch (Exception ex)
            {
                System.Diagnostics.Debug.WriteLine($"YARA Initialization Error: {ex.Message}");
            }
        }

        // Helper method to handle the logic for both Constructor and Reload
        private void CompileRulesInternal(string path)
        {
            // 1. Save original location
            string originalDirectory = Directory.GetCurrentDirectory();

            try
            {
                using (var compiler = new Compiler())
                {
                    // SCENARIO A: Path is a specific file (e.g., malware_index.yar)
                    if (File.Exists(path))
                    {
                        // CRITICAL FIX: Switch "Current Directory" to the Rules folder
                        // This allows 'include "./malware/..."' to work
                        string ruleFolder = Path.GetDirectoryName(path);
                        if (!string.IsNullOrEmpty(ruleFolder))
                        {
                            Directory.SetCurrentDirectory(ruleFolder);
                        }

                        compiler.AddRuleFile(path);
                        System.Diagnostics.Debug.WriteLine($"YARA: Loading index file: {path}");
                    }
                    // SCENARIO B: Path is a directory
                    else if (Directory.Exists(path))
                    {
                        // Switch focus to that directory
                        Directory.SetCurrentDirectory(path);

                        string potentialIndex = Path.Combine(path, "malware_index.yar");

                        if (File.Exists(potentialIndex))
                        {
                            compiler.AddRuleFile(potentialIndex);
                        }
                        else
                        {
                            // Fallback: Load loose files
                            string[] ruleFiles = Directory.GetFiles(path, "*.yar*");
                            foreach (var file in ruleFiles)
                            {
                                if (!file.EndsWith("index.yar")) compiler.AddRuleFile(file);
                            }
                        }
                    }

                    // Compile the rules
                    // If the rules are broken, THIS line throws an exception automatically.
                    _rules = compiler.Compile();
                }
            }
            catch (Exception ex)
            {
                // This is where YARA syntax errors appear (e.g., "syntax error at line 50")
                System.Diagnostics.Debug.WriteLine($"YARA Compilation Failed: {ex.Message}");
            }
            finally
            {
                // CRITICAL: Always restore the original directory
                Directory.SetCurrentDirectory(originalDirectory);
            }
        }

        public CyberShield_V3.Services.ScanResult ScanFile(string filePath)
        {
            var myResult = new CyberShield_V3.Services.ScanResult { IsThreat = false };

            try
            {
                if (_rules == null) return myResult;

                var scanner = new dnYara.Scanner();

                // Scan the file
                List<dnYara.ScanResult> yaraResults = scanner.ScanFile(filePath, _rules);

                foreach (var scanRes in yaraResults)
                {
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

                            return myResult;
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

        public void ReloadRules(string path)
        {
            // Dispose old rules to free memory
            _rules?.Dispose();

            // Re-run the compilation logic
            CompileRulesInternal(path);
        }

        public void Dispose()
        {
            _rules?.Dispose();
            _ctx?.Dispose();
        }
    }
}