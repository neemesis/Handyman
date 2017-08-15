using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Slave.Framework.Interfaces;

namespace Slave.PowerShellPlugin {
    public class PowerShellPlugin : IMaster {

        public PowerShellPlugin() {
            _mAlias = "ps";
            _mHotKey = Shortcut.ShiftF3;
            //ScratchPad.Current.Size = new System.Drawing.Size(200, 200);
        }

        public string Name => "PowerShell Plugin";
        public string Description => "Execute PowerShell Scripts";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.PowerShellPlugin/README.MD";

        void IMaster.Initialize() {
            // todo restore settings
        }

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

        public void Execute(string[] args, Action<string> display) {
            if (args.Length > 0 && args[0] == "set") {
                Properties.Settings.Default.PowerShellScriptLocation = string.Join("", args.Skip(1));
                return;
            }
            if (args.Length == 0 || args.Length > 0 && args[0] == "help") {
                var dlg1 = new Form {
                    Text = "Email Plugin Help",
                    AutoScroll = true,
                    Size = new Size(900, 650),
                    Font = new Font("Arial", 14, FontStyle.Regular)
                };
                var tl = new Label {
                    AutoSize = true,
                    Text = "Usage\r\n==================\r\n" 
                    + "ps set <path>: path should be whitout whitespaces\r\n" 
                    + "ps <script1> <script2> ...: you can execute as many as you want scripts\r\n" 
                    + "to send argument to the script use this syntax:\r\n" 
                    + "ps script1:arg1,arg2 script2:arg3,arg4\r\n"
                    + "=================="
                };
                dlg1.Controls.Add(tl);

                dlg1.ShowDialog();
                return;
            }
            var path = Properties.Settings.Default.PowerShellScriptLocation;
            foreach (var s in args) {
                var sSplit = s.Split(':');
                var name = path + (sSplit[0].EndsWith(".ps1") ? sSplit[0] : sSplit[0] + ".ps1");
                var arguments = " -file \"" + name + "\" ";
                foreach (var sIn in sSplit[1].Split(',')) {
                    arguments += "\"" + sIn + "\"";
                }
                var startInfo = new ProcessStartInfo {
                    FileName = @"powershell.exe",
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                var process = new Process {StartInfo = startInfo};
                process.Start();

                var output = process.StandardOutput.ReadToEnd();
                var errors = process.StandardError.ReadToEnd();

                var dlg1 = new Form {
                    Text = "Result from: " + name,
                    AutoScroll = true,
                    Size = new Size(500, 650)
                };
                var tl = new Label {
                    AutoSize = true,
                    Text = "Result\r\n==================\r\n" + output +
                           "\r\n==================\r\n" + (!string.IsNullOrEmpty(errors) ? "Errors\r\n==================\r\n" + errors + "\r\n====================" : "")
                };
                dlg1.Controls.Add(tl);

                dlg1.ShowDialog();
            }
        }
    }
}
