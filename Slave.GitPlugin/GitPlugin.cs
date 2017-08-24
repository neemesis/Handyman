using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Slave.Framework.Interfaces;

namespace Slave.GitPlugin {
    public class GitPlugin : IMaster {
        public string Name => "Git Plugin";
        public string Description => "Execute git commands.";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.GitPlugin/README.MD";
        private Shortcut _hotKey;
        private string _alias;
        public IParse Parser { get; set; }

        Shortcut IMaster.HotKey {
            get => _hotKey;
            set => _hotKey = value;
        }

        string IMaster.Alias {
            get => _alias;
            set => _alias = value;
        }

        public GitPlugin() {
            _alias = "git";
            _hotKey = Shortcut.None;
        }

        public void Initialize() {
        }

        public void Execute(string[] args, Action<string> display = null) {
            //
        }
    }
}
