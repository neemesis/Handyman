using System;
using System.Diagnostics;

namespace Handyman.Framework {
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

        /// <summary>
        /// Run CMD command
        /// </summary>
        /// <param name="commandWithArguments">Command with arguments</param>
        /// <param name="path">Path to directory in which we execute</param>
        /// <param name="errors">Out string if there are errors</param>
        /// <returns>Output of the command as string</returns>
        public static string CMD(string commandWithArguments, string path, out string errors) {
            return Application(@"cmd.exe", "/c " + commandWithArguments, path, out errors);
        }

        /// <summary>
        /// Execute application.
        /// </summary>
        /// <param name="application">Name of the application</param>
        /// <param name="commandWithArguments">Arguments</param>
        /// <param name="path">Path for execution</param>
        /// <param name="error">Out errors</param>
        /// <returns>Output</returns>
        public static string Application(string application, string commandWithArguments, string path, out string error) {
            var info = new ProcessStartInfo(application, commandWithArguments) {
                WindowStyle = ProcessWindowStyle.Hidden,
                WorkingDirectory = path,
                ErrorDialog = true,
                RedirectStandardError = true,
                RedirectStandardOutput = true,
                UseShellExecute = false
            };
            var process = new Process { StartInfo = info };
            process.Start();

            error = process.StandardError.ReadToEnd();
            return process.StandardOutput.ReadToEnd();
        }

        /// <summary>
        /// Run PowerShell command
        /// </summary>
        /// <param name="commandWithArguments">Command with arguments</param>
        /// <param name="path">Path to directory in which we execute</param>
        /// <param name="errors">Out string if there are errors</param>
        /// <returns>Output of the command as string</returns>
        public static string PowerShell(string commandWithArguments, string path, out string errors) {
            return Application(@"powershell.exe", commandWithArguments, path, out errors);
        }
    }
}
