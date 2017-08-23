using Slave.Framework.Interfaces;
using System;

namespace Slave.TranslatorPlugin {
    public class TranslatorPlugin : IMaster {
        public TranslatorPlugin() {
            _alias = "translator";
            _hotKey = System.Windows.Forms.Shortcut.None;
        }

        string IMaster.Name => "Translator plugin";
        string IMaster.Description => "This is a Google Translator wrapper";
        string IMaster.Author => "John Roland";
        string IMaster.Version => "1.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.TranslatorPlugin/README.MD";
        public IParse Parser { get; set; }
        void IMaster.Initialize() {
            //
        }

        void IMaster.Execute(string[] args, Action<string> display) {
            var form = new MainForm {
                StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            };
            form.Select();
            form.Focus();
            form.ShowDialog();
        }

        System.Windows.Forms.Shortcut IMaster.HotKey {
            get => _hotKey;
            set => _hotKey = value;
        }

        private string _alias;
        private System.Windows.Forms.Shortcut _hotKey;

        string IMaster.Alias {
            get => _alias;
            set => _alias = value;
        }
    }
}
