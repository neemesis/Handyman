using System.Windows.Forms;
using Slave.Framework.Interfaces;
using System;

namespace Slave.ScratchPadPlugin {
    public class ScratchPadPlugin : IMaster {
        public ScratchPadPlugin() {
            _alias = "scratchpad";
            _hotKey = Shortcut.ShiftF2;
            //ScratchPad.Current.Size = new System.Drawing.Size(200, 200);
        }

        string IMaster.Name => "ScratPad plugin";
        string IMaster.Description => "The ScratchPad is a simple text editor to collect and keep text.";
        string IMaster.Author => "John Roland";
        string IMaster.Version => "1.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.ScratchPadPlugin/README.MD";
        public IParse Parser { get; set; }
        void IMaster.Initialize() {
            // todo restore settings
        }

        void IMaster.Execute(string[] args, Action<string> display) {

            ScratchPad.Current.Show();
            ScratchPad.Current.Left = Screen.PrimaryScreen.WorkingArea.Width - ScratchPad.Current.Width;
            ScratchPad.Current.Top = Screen.PrimaryScreen.WorkingArea.Height - ScratchPad.Current.Height;

            ScratchPad.Current.Select();
            ScratchPad.Current.Focus();
        }

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
    }
}
