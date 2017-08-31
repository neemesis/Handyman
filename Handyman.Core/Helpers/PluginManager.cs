using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Newtonsoft.Json;
using Handyman.Core.Forms;
using Handyman.Core.Models;
using Handyman.Framework.Components;
using Handyman.Framework.Interfaces;

namespace Handyman.Core.Helpers {
    public static class PluginManager {
        public static void ListPlugins() {
            using (var wc = new WebClient()) {
                var json = wc.DownloadString(Properties.Settings.Default.DownloadURL + "plugins.json");
                var pckgs = JsonConvert.DeserializeObject<List<Plugin>>(json);
                var form = new Form { Text = "Packages", Size = new Size(400, 600) };
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
            }
        }

        public static void InstallPlugin(string name) {
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
                    Launcher.Current.ShowData("success :)");
                    return;
                }
            } catch (Exception e) {
                Launcher.Current.ShowData("error :(");
            }
        }

        public static List<IMaster> LoadPlugins(out IContainer _components) {
            var tools = new List<IMaster>();
            _components = new Container();

            // we extract all the IAttributeDefinition implementations 
            foreach (var filename in Directory.GetFiles(Application.StartupPath, "*.dll")) {
                var assembly = System.Reflection.Assembly.LoadFrom(filename);
                foreach (var type in assembly.GetTypes()) {
                    var plugin = type.GetInterface("Handyman.Framework.Interfaces.IMaster");
                    if (plugin != null) {
                        var tool = (IMaster)Activator.CreateInstance(type);
                        tool.Initialize();

                        var hotkey = new SystemHotkey(_components) {
                            Shortcut = tool.HotKey
                        };

                        hotkey.Pressed += delegate {
                            tool.Execute(null, Launcher.Current.ShowData);
                        };
                        tools.Add(tool);
                    }
                }
            }
            return tools;
        }

        public static bool Handle(string alias) {
            var parts = alias.Split(' ');

            if (parts[0] == "install" && parts.Length == 2) {
                InstallPlugin(parts[1]);
                return true;
            }
            if (parts[0] == "packages") {
                ListPlugins();
                return true;
            }

            return false;
        }
    }
}
