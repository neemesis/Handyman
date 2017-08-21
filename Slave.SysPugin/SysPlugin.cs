using Slave.Framework.Interfaces;
using System;
using System.Windows.Forms;

namespace Slave.SysPugin {
    public class SysPlugin : IMaster {
        public string Name => "System Plugin";
        public string Description => "Execute system commands.";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.SysPlugin/README.MD";

        private Shortcut _mHotKey;
        private string _mAlias;


        Shortcut IMaster.HotKey {
            get => _mHotKey;
            set => _mHotKey = value;
        }

        string IMaster.Alias {
            get => _mAlias;
            set => _mAlias = value;
        }

        public IParse Parser { get; set; }

        public void Execute(string[] args, Action<string> display = null) {
            if (args[0] == "shutdown") {

            } else if (args[0] == "restart") {

            } else if (args[0] == "sleep") {

            } else if (args[0] == "hibernate") {

            }
        }

        public void Initialize() {
            //throw new NotImplementedException();
        }
    }
}
