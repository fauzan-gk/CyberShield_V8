using System;
using System.Windows.Forms;

namespace CyberShield_V3.root
{
    internal static class Program
    {
        [STAThread]
        static void Main()
        {
            Application.SetUnhandledExceptionMode(UnhandledExceptionMode.CatchException);
            Application.ThreadException += (s, e) => ShowError(e.Exception);
            AppDomain.CurrentDomain.UnhandledException += (s, e) => ShowError(e.ExceptionObject as Exception);

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            try
            {
                Application.Run(new LoadingScreen());
            }
            catch (Exception ex)
            {
                ShowError(ex);
            }
        }

        static void ShowError(Exception ex)
        {
            if (ex == null) return;
            string msg = $"ERROR TYPE: {ex.GetType().Name}\n\nMESSAGE: {ex.Message}\n\nLOCATION:\n{ex.StackTrace}";
            MessageBox.Show(msg, "Crash Report", MessageBoxButtons.OK, MessageBoxIcon.Error);
        }
    }
}