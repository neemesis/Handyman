using Handyman.Framework.Components;
using Handyman.Framework.Entities;
using Handyman.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;

namespace Handyman.ConfigPlugin {
    public class ConfigPlugin : IMaster {
        public ConfigPlugin() {
            _alias = "config";
            _hotKey = Shortcut.None;
        }

        public string Name => "Config Plugin";
        public string Description => "Edit config files of other plugins";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Handyman/blob/master/Handyman.ConfigPlugin/README.MD";
        public IParse Parser { get; set; }
        public List<string> Suggestions { get; set; }
        private Shortcut _hotKey;
        private string _alias;

        Shortcut IMaster.HotKey {
            get => _hotKey;
            set => _hotKey = value;
        }

        string IMaster.Alias {
            get => _alias;
            set => _alias = value;
        }

        public void Execute(string[] args, Action<string, DisplayData, List<string>, Action<string>> display) {
            display(EnumerateOpenedWindows.GetActiveExplorerPath(), DisplayData.Launcher, null, null);

            return;

            if (args.Length != 1) {
                display("only 1 argument allowed", DisplayData.Launcher, null, null);
                return;
            }

            if (args[0] == "handyman")
                Process.Start("Handyman.Core.exe.config");
            else
                Process.Start($"{Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments)}\\{args[0]}.hmcfg");
        }

        public void Initialize() {
            Suggestions = new List<string> { "config handyman" };
        }
    }
}
