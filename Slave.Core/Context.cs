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

            // TODO load plugins
            Tools = new List<IMaster>();
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

            var word2 = new Commands {
                Alias = "torrentspy",
                FileName = @"http://www.torrentspy.com/search?query=$W$&submit.x=0&submit.y=0",
                Arguments = ""
            };
            Slaves.Add(word2);



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
                var test = new List<string>();
                test.Add("add");
                test.Add("exit");
                test.Add("help");
                test.Add("setup");

                foreach (var word in Slaves) {
                    test.Add(word.Alias);
                }

                foreach (var tool in Tools) {
                    test.Add(tool.Alias);
                }

                return test.ToArray();
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

            foreach (var tool in Tools) {
                if (alias.StartsWith(tool.Alias)) {
                    try {
                        tool.Execute(ParseArguments(alias.Substring(tool.Alias.Length)));
                        return;
                    } catch (Exception e) {
                        SetError(e);
                    }
                }
            }

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

            //Slave word = m_Slaves.Find(delegate(Slave w) { return w.Alias.Equals(alias); });
            //if (word != null)
            //{
            //    Execute(word);
            //}
            //else
            {
                Execute(alias);
            }
        }

        private void SetError(Exception e = null) {
            Launcher.Current.ChangeLauncherText("error :(");
        }

        private static string[] ParseArguments(string str) {
            return str.Split(' ').Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }

        #endregion

        #region Private methods
        private System.ComponentModel.IContainer m_Components;

        private void LoadPlugins() {
            m_Components = new System.ComponentModel.Container();

            var pluginPath = string.Empty;

            // we extract all the IAttributeDefinition implementations 
            foreach (var filename in Directory.GetFiles(System.Windows.Forms.Application.StartupPath /* + "\\Plugins" */, "*.dll")) {
                var assembly = System.Reflection.Assembly.LoadFrom(filename);
                foreach (var type in assembly.GetTypes()) {
                    var plugin = type.GetInterface("Slave.Framework.Interfaces.IMaster");
                    if (plugin != null) {
                        var tool = (IMaster)Activator.CreateInstance(type);
                        tool.Initialize();

                        var hotkey = new SystemHotkey(m_Components) {
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

            var googleWord = new Commands {
                Alias = "google",
                FileName = @"iexplore",
                Arguments = "http://www.google.com/search?hl=en&q=$W$"
            };
            Slaves.Add(googleWord);

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
            if (inputText != null && ( inputText.Contains("$W$") || inputText.Contains("$w$") )) {
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
            var txt = new Label {AutoSize = true};
            var sb = new StringBuilder();
            foreach (var s in Slaves) {
                sb.AppendLine("Command: " + s.Alias);
                sb.AppendLine("Description: " + s.Notes);
                sb.AppendLine("Filename: " + s.FileName);
                sb.AppendLine("========================");
            }
            foreach (var s in Tools)
            {
                sb.AppendLine("Command: " + s.Alias);
                sb.AppendLine("Description: " + s.Description);
                sb.AppendLine("Author: " + s.Author);
                sb.AppendLine("========================");
            }
            txt.Text = sb.ToString();
            dlg1.Controls.Add(txt);
            dlg1.ShowDialog();
            //Process.Start("http://code.google.com/p/Slaves");
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
            if (m_Components != null) {
                m_Components.Dispose();
            }
        }

        #endregion
    }
}
