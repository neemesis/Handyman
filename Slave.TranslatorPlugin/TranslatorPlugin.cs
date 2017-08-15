using Slave.Framework.Interfaces;
using System;

namespace Slave.TranslatorPlugin {
    public class TranslatorPlugin : IMaster {
        public TranslatorPlugin() {
            _mAlias = "translator";
            _mHotkey = System.Windows.Forms.Shortcut.None;
        }

        #region ITool Members

        string IMaster.Name => "Translator plugin";
        string IMaster.Description => "This is a Google Translator wrapper";
        string IMaster.Author => "John Roland";
        string IMaster.Version => "1.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.TranslatorPlugin/README.MD";

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
            get => _mHotkey;
            set => _mHotkey = value;
        }

        private string _mAlias;
        private System.Windows.Forms.Shortcut _mHotkey;

        string IMaster.Alias {
            get => _mAlias;
            set => _mAlias = value;
        }

        #endregion
    }
}
