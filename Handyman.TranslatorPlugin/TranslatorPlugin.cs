using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Handyman.Framework.Entities;
using Handyman.Framework.Interfaces;

namespace Handyman.TranslatorPlugin {
    public class TranslatorPlugin : IMaster {
        public string Name => "Translator Plugin";
        public string Description => "Translate using google translate.";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Handyman/blob/master/Handyman.TranslatorPlugin/README.MD";
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

        public TranslatorPlugin() {
            _alias = "tr";
            _hotKey = Shortcut.None;
            Parser = new TranslatorParser();
        }

        public void Initialize() {
            Suggestions = new List<string> {"tr set from", "tr set to" };
        }

        public async void Execute(string[] args, Action<string, DisplayData> display) {
            if (args[0] == "set") {
                if (args[1] == "from") {
                    Properties.Settings.Default.DefFrom = args[2];
                }
                else if (args[1] == "to") {
                    Properties.Settings.Default.DefTo = args[2];
                }
            }
            else {
                var lang = args[0].Contains(":") ? args[0] : null;
                var fromLang = string.IsNullOrEmpty(lang) ? Properties.Settings.Default.DefFrom : lang.Split(':')[0];
                var toLang = string.IsNullOrEmpty(lang) ? Properties.Settings.Default.DefTo : lang.Split(':')[1];
                var text = string.IsNullOrEmpty(lang)
                    ? string.Join(" ", args.Skip(1))
                    : string.Join(" ", args.Skip(1)).Replace(lang, "");
                var url = "https://translate.google.com/#" + fromLang + "/" + toLang + "/" + Uri.EscapeDataString(text);
                Process.Start(url);
            }
        }
    }
}
