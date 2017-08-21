using Slave.Framework.Interfaces;
using System;
using System.Diagnostics;
using System.Windows.Forms;

namespace Slave.SysPugin {
    public class SysPlugin : IMaster {
        public SysPlugin() {
            _mAlias = "sys";
            _mHotKey = Shortcut.None;
        }

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
            var cmdArgs = "shutdown ";
            if (args[0] == "shutdown") {
                if (args.Length == 1) {
                    cmdArgs += "/s";
                } else {
                    var mins = int.Parse(args[1]);
                    cmdArgs += "/s /t " + (mins * 60).ToString();
                } 
            } else if (args[0] == "restart") {
                cmdArgs += "/r";
            } else if (args[0] == "hibernate") {
                cmdArgs += "/h";
            } else if (args[0] == "abort") {
                cmdArgs += "/a";
            } else if (args[0] == "logoff") {
                cmdArgs += "/l";
            }

            var startInfo = new ProcessStartInfo {
                FileName = @"cmd.exe",
                Arguments = cmdArgs,
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            };
            var process = new Process { StartInfo = startInfo };
            process.Start();
        }

        public void Initialize() {
            //throw new NotImplementedException();
        }
    }
}
