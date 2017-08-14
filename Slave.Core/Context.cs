using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Xml.Serialization;
using Slave.Core.Forms;
using Slave.Framework;
using Slave.Framework.Components;
using Slave.Framework.Entities;
using Slave.Framework.Interfaces;
using System.Net;
using Newtonsoft.Json;
using Slave.Core.Models;

namespace Slave.Core {
    public class Context : IDisposable {
        #region Properties

        private readonly string _mWordsPath = null;


        public List<Commands> Slaves { get; set; }

        public List<IMaster> Tools { get; set; }

        #endregion

        public Context() {
            #region Register app at windows startup
            if (Properties.Settings.Default.RunAtWindowsStart) {
                Utilities.RunOnStart("Slaver", System.Windows.Forms.Application.ExecutablePath);
            } else {
                Utilities.RemoveRunOnStart("Slaver");
            }
            #endregion

            _mWordsPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Environment.UserName + ".slaves";

            Slaves = new List<Commands>();

            if (File.Exists(_mWordsPath)) {
                LoadSlaves();
            } else {
                GetDefaultSlaveList();
                AddGoogleSlaves();
                SaveSlaves();
            }

            LoadPlugins();
        }

        #region public methods

        public void GetDefaultSlaveList() {
            Slaves.Clear();

            if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe")) {
                var word = new Commands {
                    Alias = "firefox",
                    FileName = @"C:\Program Files\Mozilla Firefox\firefox.exe"
                };
                Slaves.Add(word);
            }

            var word1 = new Commands {
                Alias = "paint",
                FileName = @"pbrush.exe"
            };
            Slaves.Add(word1);
        }

        /// <summary>
        /// Saves the magic words.
        /// </summary>
        public void SaveSlaves() {
            var ser = new XmlSerializer(typeof(List<Commands>));
            var sw = new StreamWriter(_mWordsPath);
            ser.Serialize(sw, Slaves);
            sw.Close();
        }

        /// <summary>
        /// Gets the auto complete source.
        /// </summary>
        /// <value>The auto complete source.</value>
        public string[] AutoCompleteSource {
            get {
                var app = new List<string>();
                app.Add("add");
                app.Add("exit");
                app.Add("help");
                app.Add("setup");
                app.Add("google");
                app.Add("youtube");

                foreach (var word in Slaves) {
                    app.Add(word.Alias);
                }

                foreach (var tool in Tools) {
                    app.Add(tool.Alias);
                }

                return app.ToArray();
            }
        }

        public void Start(string alias) {
            switch (alias) {
                case "exit":
                    Exit();
                    return;

                case "setup":
                    Setup();
                    return;

                case "help":
                    Help();
                    return;

                case "add":
                    ShowNewSlaveForm();
                    return;
                default:
                    break;
            }

            #region Google
            if (alias.StartsWith("google")) {
                var otherParts = alias.Replace("google", "");
                if (!string.IsNullOrEmpty(otherParts))
                    Process.Start(@"https://www.google.com/search?q=" + Uri.EscapeDataString(otherParts));
                else Process.Start(@"https://www.google.com/");
                return;
            }
            if (alias.StartsWith("youtube")) {
                var otherParts = alias.Replace("yotube", "");
                if (!string.IsNullOrEmpty(otherParts))
                    Process.Start(@"https://www.youtube.com/results?search_query=" + Uri.EscapeDataString(otherParts));
                else Process.Start(@"https://www.youtube.com/");
                return;
            }
            #endregion

            #region Install Plugins
            var parts = alias.Split(' ');
            if (parts[0] == "install" && parts.Count() == 2) {
                InstallPlugin(parts[1]);
                return;
            }
            if (parts[0] == "packages" && parts.Count() == 1) {
                ListPlugins();
                return;
            }
            #endregion

            #region Check Tools
            foreach (var tool in Tools) {
                if (alias.StartsWith(tool.Alias)) {
                    try {
                        tool.Execute(ParseArguments(alias.Substring(tool.Alias.Length)), Launcher.Current.ChangeLauncherText);
                        return;
                    } catch (Exception e) {
                        SetError(e);
                    }
                }
            }
            #endregion

            #region Check Slaves
            foreach (var word in Slaves) {
                if (word.Alias.Equals(alias)) {
                    try {
                        Execute(word);
                        return;
                    } catch (Exception e) {
                        SetError(e);
                    }
                }
            }
            #endregion

            #region CMD Fallback
            Execute(alias);
            #endregion
        }

        private void ListPlugins() {
            using (var wc = new WebClient()) {
                var json = wc.DownloadString(Properties.Settings.Default.DownloadURL + "plugins.json");
                var pckgs = JsonConvert.DeserializeObject<List<Plugin>>(json);
                var form = new Form { Text = "Packages", Size = new Size(400, 600)};
                var lb = new TextBox {
                    AutoSize = true, ScrollBars = ScrollBars.Vertical, WordWrap = true, ReadOnly = true,
                    Multiline = true, SelectedText = ""
                };

                lb.Text = "To install package write \"install <packageName>\"\r\n\r\n";
                lb.Text += "Packages\r\n===============================================\r\n";
                foreach (var p in pckgs) {
                    lb.Text += "Name: " + p.Name + "\r\n";
                    lb.Text += "Author: " + p.Author + "\r\n";
                    lb.Text += "Description: " + p.Description + "\r\n===============================================\r\n\r\n";
                }

                lb.Text += "\r\n\r\n\r\n";

                lb.Size = new Size(form.Size.Width - 10, form.Size.Height);
                lb.Select(0, 0);
                form.Controls.Add(lb);

                form.SizeChanged += (o, e) => { lb.Size = new Size(form.Size.Width - 10, form.Size.Height); };

                form.ShowDialog();

                return;
            }
        }

        private void InstallPlugin(string name) {
            try {
                using (var wc = new WebClient()) {
                    var json = wc.DownloadString(Properties.Settings.Default.DownloadURL + "plugins.json");
                    var pckgs = JsonConvert.DeserializeObject<List<Plugin>>(json);
                    var pckgForInstall = pckgs.SingleOrDefault(x => x.Name == name);
                    if (pckgForInstall != null) {
                        wc.DownloadFile(pckgForInstall.URL, pckgForInstall.Name + ".dll");
                        if (pckgForInstall.HasConfig)
                            wc.DownloadFile(pckgForInstall + ".config", pckgForInstall.Name + ".dll.config");
                    }
                    LoadPlugins();
                    Launcher.Current.ChangeLauncherText("success :)");
                    return;
                }
            } catch (Exception e) {
                SetError(e);
            }
        }

        private void SetError(Exception e = null) {
            Launcher.Current.ChangeLauncherText("error :(");
        }

        private static string[] ParseArguments(string str) {
            return Parse(str);
            //return str.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }

        private static string[] Parse(string str) {
            var args = new List<string>();
            var colSplit = str.Split(':');
            var start = colSplit[0].Split(' ');
            for (int i = 1; i < (colSplit.Count() == 1 ? start.Count() : start.Count() - 1); ++i) {
                args.Add(start[i]);
            }
            for (int i = 0; i < colSplit.Count() - 1; ++i) {
                var spl = colSplit[i + 1].Split(' ');
                var count = spl.Count();
                var parts = colSplit[i].Split(' ').Last() + ":" + string.Join(" ", spl.Take(count > 1 ? count - 1 : 1));
                args.Add(parts);
            }
            return args.ToArray();
        }
        #endregion

        #region Private methods
        private System.ComponentModel.IContainer _components;

        private void LoadPlugins() {
            Tools = new List<IMaster>();
            _components = new System.ComponentModel.Container();

            var pluginPath = string.Empty;

            // we extract all the IAttributeDefinition implementations 
            foreach (var filename in Directory.GetFiles(System.Windows.Forms.Application.StartupPath /* + "\\Plugins" */, "*.dll")) {
                var assembly = System.Reflection.Assembly.LoadFrom(filename);
                foreach (var type in assembly.GetTypes()) {
                    var plugin = type.GetInterface("Slave.Framework.Interfaces.IMaster");
                    if (plugin != null) {
                        var tool = (IMaster)Activator.CreateInstance(type);
                        tool.Initialize();

                        var hotkey = new SystemHotkey(_components) {
                            Shortcut = tool.HotKey
                        };
                        hotkey.Pressed += new EventHandler(delegate (object sender, EventArgs e) {
                            tool.Execute(null);
                        });
                        Tools.Add(tool);
                    }
                }
            }

        }

        private void AddGoogleSlaves() {
            #region google words

            var mailWord = new Commands {
                Alias = "mail",
                FileName = @"http://mail.google.com",
                Arguments = ""
            };
            Slaves.Add(mailWord);

            var calendarWord = new Commands {
                Alias = "calendar",
                FileName = @"http://calendar.google.com",
                Arguments = ""
            };
            Slaves.Add(calendarWord);

            var docsWord = new Commands {
                Alias = "docs",
                FileName = @"http://www.google.com/docs",
                Arguments = ""
            };
            Slaves.Add(docsWord);

            var baseWord = new Commands {
                Alias = "base",
                FileName = @"http://base.google.com",
                Arguments = ""
            };
            Slaves.Add(baseWord);

            var analyticsWord = new Commands {
                Alias = "analytics",
                FileName = @"http://www.google.com/analytics",
                Arguments = ""
            };
            Slaves.Add(analyticsWord);

            var adsenseWord = new Commands {
                Alias = "adsense",
                FileName = @"http://www.google.com/adsense",
                Arguments = ""
            };
            Slaves.Add(adsenseWord);

            var readerWord = new Commands {
                Alias = "reader",
                FileName = @"http://www.google.com/reader",
                Arguments = ""
            };
            Slaves.Add(readerWord);

            var photoWord = new Commands {
                Alias = "photos",
                FileName = @"http://picasaweb.google.com",
                Arguments = ""
            };
            Slaves.Add(photoWord);

            #endregion
        }


        /// <summary>
        /// Loads the magic words.
        /// </summary>
        private void LoadSlaves() {
            var serializer = new XmlSerializer(typeof(List<Commands>));
            var reader = File.OpenText(_mWordsPath);
            Slaves = (List<Commands>)serializer.Deserialize(reader);
            reader.Close();
        }



        #endregion

        #region Singleton

        private static volatile Context _singleton;
        private static readonly object _syncRoot = new Object();

        public static Context Current {
            get {
                if (_singleton == null) {
                    lock (_syncRoot) {
                        if (_singleton == null) {
                            _singleton = new Context();
                        }
                    }
                }

                return _singleton;
            }
        }
        #endregion

        private string ParseInputText(string inputText, string notes) {
            // TODO preprocess infos
            if (inputText != null && (inputText.Contains("$W$") || inputText.Contains("$w$"))) {
                var form = new DynamicInput();
                switch (form.ShowDialog()) {
                    case System.Windows.Forms.DialogResult.OK:
                        return inputText.Replace("$W$", form.EncodedInput).Replace("$w$", form.Input);
                    default:
                        return inputText;
                }
            }

            return inputText;
        }

        private void Execute(Commands word) {
            var fileName = ParseInputText(word.FileName, word.Notes);
            var arguments = ParseInputText(word.Arguments, word.Notes);

            var info = new ProcessStartInfo(fileName, arguments) {
                WindowStyle = word.StartUpMode,
                WorkingDirectory = word.WorkingDirectory,
                ErrorDialog = true
            };

            var process = Process.Start(info);
        }

        private void Execute(string word) {
            try {
                var info = new ProcessStartInfo(word) {
                    WindowStyle = ProcessWindowStyle.Normal,
                    UseShellExecute = true
                };
                Process.Start(info);
            } catch (Exception) {
                System.Media.SystemSounds.Exclamation.Play();
            }

        }

        #region BuildIn Slaves launcher

        public void Help() {
            var dlg1 = new Form();
            dlg1.Text = "Help";
            dlg1.AutoScroll = true;
            dlg1.Size = new Size(500, 650);
            var txt = new Label { AutoSize = true };
            var sb = new StringBuilder();

            foreach (var s in Tools) {
                sb.AppendLine("Command: " + s.Alias);
                sb.AppendLine("Description: " + s.Description);
                sb.AppendLine("Author: " + s.Author);
                sb.AppendLine("for help type: " + s.Alias + " help");
                sb.AppendLine("========================");
            }

            foreach (var s in Slaves) {
                sb.AppendLine("Command: " + s.Alias);
                sb.AppendLine("Description: " + s.Notes);
                sb.AppendLine("Filename: " + s.FileName);
                sb.AppendLine("========================");
            }
            
            txt.Text = sb.ToString();
            dlg1.Controls.Add(txt);
            dlg1.ShowDialog();
        }

        public void Setup() {
            var form = new OptionsForm();

            switch (form.ShowDialog()) {
                case System.Windows.Forms.DialogResult.OK:
                    Current.SaveSlaves();
                    Properties.Settings.Default.Save();
                    break;

                case System.Windows.Forms.DialogResult.Cancel:
                    Properties.Settings.Default.Reload();
                    break;

                default:
                    break;
            }
        }

        public void Exit() {
            System.Windows.Forms.Application.Exit();
        }

        #endregion

        public void AddActiveApplicationSlave(string appExeName, string appExePath) {
            var word = new Commands {
                Alias = appExeName,
                FileName = appExePath
            };

            ShowSlaveForm(word);
        }

        public void ShowNewSlaveForm() {
            ShowSlaveForm(new Commands());
        }

        private void ShowSlaveForm(Commands word) {
            var form = new SlaveForm {
                Slave = word
            };

            if (form.ShowDialog() == System.Windows.Forms.DialogResult.OK && Slaves.Contains(word) == false) {
                Slaves.Add(word);
            }
        }

        #region IDisposable Members

        void IDisposable.Dispose() {
            if (_components != null) {
                _components.Dispose();
            }
        }

        #endregion
    }
}
