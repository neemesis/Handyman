using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using Handyman.Core.Forms;
using Handyman.Framework.Entities;
using Handyman.Framework.Interfaces;

namespace Handyman.Core.Helpers {
    public static class Executor {

        public static bool ExecuteTool(IEnumerable<IMaster> tools, string alias, IParse parser, Action<Exception> setError) {
            foreach (var tool in tools) {
                if (alias.StartsWith(tool.Alias)) {
                    try {
                        var a = alias.Replace(tool.Alias, "").Trim();
                        var args = tool.Parser?.Parse(a) ?? parser.Parse(a);
                        if (args.Any() && args[0].Split(' ')[0].StartsWith("dev")) {
                            HandleDev(alias, tool.Parser ?? parser);
                            Console.WriteLine("Dev. Command handled!");
                            return true;
                        }
                        tool.Execute(args, Launcher.Current.ShowData);
                        return true;
                    } catch (Exception e) {
                        setError(e);
                        return false;
                    }
                }
            }
            return false;
        }

        public static bool ExecuteHandyman(IEnumerable<Commands> handymans, string alias, Action<Exception> setError) {
            foreach (var word in handymans) {
                if (alias.StartsWith(word.Alias)) {
                    try {
                        ExecuteHandymanInternal(word, alias.Split(' ').Skip(1).ToArray());
                        return true;
                    } catch (Exception e) {
                        setError(e);
                        return false;
                    }
                }
            }
            return false;
        }

        private static void ExecuteHandymanInternal(Commands word, string[] commands) {
            var fileName = ParseInputText(word.FileName, word.Notes, commands);
            var arguments = ParseInputText(word.Arguments, word.Notes, commands);

            var info = new ProcessStartInfo(fileName, arguments) {
                WindowStyle = word.StartUpMode,
                WorkingDirectory = word.WorkingDirectory,
                ErrorDialog = true
            };

            Process.Start(info);
        }

        public static bool ExecuteHelp(List<IMaster> tools, string name) {
            var parts = name.Split(' ');
            if (parts.Length == 2 && parts[0] == "help")
                foreach (var t in tools) {
                    if (t.Alias == parts[1]) {
                        Process.Start(t.HelpUrl);
                        return true;
                    }
                }
            return false;
        }

        public static void ExecuteFallback(string word) {
            try {
                var info = new ProcessStartInfo(word) {
                    WindowStyle = ProcessWindowStyle.Normal,
                    UseShellExecute = true
                };
                Process.Start(info);
            } catch (Exception e) {
                System.Media.SystemSounds.Exclamation.Play();
            }
        }

        #region Helpers
        private static string ParseInputText(string inputText, string notes, string[] args) {
            if (!string.IsNullOrEmpty(inputText)) {
                inputText = inputText.Replace("{F}", string.Join(" ", args));
                inputText = inputText.Replace("{FU}", System.Web.HttpUtility.UrlEncode(string.Join(" ", args)));
                inputText = inputText.Replace("{DTN}", System.Web.HttpUtility.UrlEncode(DateTime.Now.ToString(CultureInfo.CurrentCulture)));
                inputText = inputText.Replace("{U}", Environment.UserName);
                inputText = inputText.Replace("{UD}", Environment.UserDomainName);
                inputText = inputText.Replace("{DD}", DateTime.Now.DayOfWeek.ToString());
                inputText = inputText.Replace("{MM}", DateTime.Now.Month.ToString());
                inputText = inputText.Replace("{YY}", DateTime.Now.Year.ToString());
                inputText = inputText.Replace("{HH}", DateTime.Now.Hour.ToString());
                inputText = inputText.Replace("{MIN}", DateTime.Now.Minute.ToString());
                inputText = inputText.Replace("{SS}", DateTime.Now.Second.ToString());
                inputText = inputText.Replace("{RD}", Path.GetPathRoot(Environment.SystemDirectory));

                for (var i = 0; i < args.Length; ++i) {
                    inputText = inputText.Replace("{" + i + "U}", System.Web.HttpUtility.UrlEncode(args[i]));
                    inputText = inputText.Replace("{" + i + "}", args[i]);
                }
            }

            return inputText;
        }

        private static void HandleDev(string args, IParse parser) {
            var res = parser.Parse(args);
            Launcher.Current.ShowData(string.Join(" || ", res.Skip(2)));
        }
        #endregion
    }
}
