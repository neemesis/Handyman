using System;

namespace Slave.Framework {
    public static class Utilities {
        public static void RunOnStart(string appName, string appPath) {
            var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (key != null) {
                key.SetValue(appName, appPath);
                key.Close();
            }
        }

        public static void RemoveRunOnStart(string appName) {
            try {
                var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (key != null) {
                    key.DeleteSubKey(appName);
                    key.Close();
                }
            } catch (Exception) {
                //throw;
            }

        }
    }
}
