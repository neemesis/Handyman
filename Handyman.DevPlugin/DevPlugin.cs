using System;
using System.Collections.Generic;
using System.Windows.Forms;
using Handyman.Framework.Entities;
using Handyman.Framework.Interfaces;

namespace Handyman.DevPlugin {
    public class DevPlugin : IMaster {
        public string Name => "Dev Plugin";
        public string Description => "Dev Toolkit";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Handyman/blob/master/Handyman.DoingPlugin/README.MD";
        public Shortcut HotKey { get; set; }
        public string Alias { get; set; }
        public IParse Parser { get; set; }
        public List<string> Suggestions { get; set; }

        public DevPlugin() {
            Alias = "dev";
            HotKey = Shortcut.None;
            Initialize();
        }

        public void Initialize() {
            Suggestions = new List<string> { "dev levenshtein", "dev base64", "dev install" };
        }

        public void Execute(string[] args, Action<string, DisplayData, List<string>, Action<string>> display) {
            display("question", DisplayData.Question, new List<string> { "a", "b", "c" }, null);
            return;

            if (args.Length == 3) {
                if (args[0] == "levenshtein")
                    DisplayText(Levenshtein.Compute(args[1], args[2]).ToString(), display);
            }

            if (args.Length == 2) {
                if (args[0] == "base64" || args[0] == "b64") {
                    Base64.FromFile(args[1]);
                    DisplayText("done", display);
                } else if (args[0] == "install" || args[0] == "choco") {
                    var res = Choco.Install(args[1]);
                    DisplayText(res, display);
                }
            }
        }

        private void DisplayText(string text, Action<string, DisplayData, List<string>, Action<string>> display) {
            display(text, DisplayData.Launcher, null, null);
        }
    }
}
