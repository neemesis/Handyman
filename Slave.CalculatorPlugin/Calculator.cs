using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Slave.Framework.Entities;
using Slave.Framework.Interfaces;

namespace Slave.CalculatorPlugin {
    public class Calculator : IMaster {
        public string Name => "Calculator Plugin";
        public string Description => "Calculate string to expression.";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.CalculatorPlugin/README.MD";
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

        public Calculator() {
            _alias = "c";
            _hotKey = Shortcut.None;
        }

        public void Initialize() {
        }

        public void Execute(string[] args, Action<string, DisplayData> display) {
            display(new DataTable().Compute(string.Join("", args), null).ToString(), DisplayData.Launcher);
        }
    }
}
