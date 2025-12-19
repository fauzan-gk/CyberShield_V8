# 🛡️ CyberShield

**CyberShield** is a robust, Windows-based cybersecurity utility built with **.NET 8.0/10.0 (Windows Forms)**. It combines local signature scanning, YARA pattern matching, and cloud-based threat intelligence to detect malware, manage threats, and optimize system performance.

## 🚀 Key Features

### 🔍 Advanced Threat Detection
* **Hybrid Scanning Engine**: Utilizes a multi-layered approach to identify threats.
    * **Signature-Based**: Rapidly detects known threats using local MD5 and SHA256 hash matching via the `VirusDatabaseEnhanced` engine.
    * **YARA Integration**: Implements **dnYara** to scan files against a comprehensive database of YARA rules (supporting APTs, Ransomware, RATs, and more).
    * **Cloud Intelligence**: Integrates with the **MalwareBazaar API** to verify suspicious file hashes against a global database of recent malware samples.

### 🧹 System Optimization (Junk Cleaner)
Includes a dedicated panel to reclaim disk space and maintain system hygiene:
* **Temporary Files**: Cleans Windows temp directories.
* **Recycle Bin**: Emptying the system recycle bin.
* **Old Downloads**: Automatically identifies and removes files in the Downloads folder older than 30 days.
* **Browser Cache**: Cleans cache files for Google Chrome and Microsoft Edge.

### 🔒 Threat Management
* **Quarantine System**: Securely isolates detected threats to prevent system execution.
* **Real-Time Dashboard**: Provides an immediate overview of system security status and recent scan history.

### 🎨 Modern UI
* Designed with **Guna.UI2** for a clean, flat, and responsive user experience.
* Modular navigation including Dashboard, Scan, Junk Cleaner, Quarantine, and Settings.

---

## 🛠️ Technology Stack

* **Framework**: .NET 8.0 / .NET 10.0 (Windows Forms)
* **Language**: C#
* **UI Library**: [Guna.UI2](https://gunaui.com/)
* **YARA Wrapper**: [dnYara](https://github.com/dnYara/dnYara)
* **Cloud API**: [MalwareBazaar](https://bazaar.abuse.ch/)

---

## 📂 Project Structure

```text
CyberShield_V9/
├── 📁 Controls/          # Custom UI components (HoverButton, PulseButton)
├── 📁 Forms/             # Main application windows (Form1, QuarantineForm)
├── 📁 Panels/            # UserControls for main tabs
│   ├── DashboardPanel.cs
│   ├── ScanPanel.cs      # Scanning interface
│   ├── JunkCleanerPanel.cs
│   └── QuarantinePanel.cs
├── 📁 Services/          # Core logic
│   ├── ScanLogic.cs      # Hash calculation and local signature db
│   ├── YaraScanner.cs    # dnYara wrapper and rule compilation
│   └── MalwareBazaarClient.cs # API client for cloud lookups
└── 📁 Rules/             # YARA rule definitions (.yar files)
```

## 🤝 Credits & Acknowledgments

* **MalwareBazaar** (abuse.ch) – Cloud malware intelligence API
* **dnYara** – .NET YARA rule engine wrapper
* **Guna UI** – Modern WinForms UI components
* **YARA Rules Contributors** – Open-source security researchers (credited in the Rules folder)
