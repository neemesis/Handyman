using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Drawing;
using System.Threading.Tasks;
using Slave.Framework.Interfaces;
using System.Windows.Forms;
using System.Diagnostics;

namespace Slave.CommandPromptPlugin {
    public class CommandPromptPlugin : IMaster {
        public CommandPromptPlugin() {
            _alias = "cmd";
            _hotKey = Shortcut.ShiftF4;
        }

        public string Name => "Command Prompt Plugin";
        public string Description => "Execute bat scripts";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.CommandPromptPlugin/README.MD";
        public IParse Parser { get; set; }

        public void Initialize() {
            //throw new NotImplementedException();
        }

        public void Execute(string[] args, Action<string> display) {

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
                    + _alias + " set <path>: path should be whitout whitespaces\r\n"
                    + _alias + " <script1> <script2> ...: you can execute as many as you want scripts\r\n"
                    + "to send argument to the script use this syntax:\r\n"
                    + _alias + " script1:arg1,arg2 script2:arg3,arg4\r\n"
                    + "=================="
                };
                dlg1.Controls.Add(tl);

                dlg1.ShowDialog();
                return;
            }

            if (args.Length > 0 && args[0] == "set") {
                Properties.Settings.Default.BatScriptLocation = string.Join("", args.Skip(1));
                return;
            }

            var path = Properties.Settings.Default.BatScriptLocation;
            foreach (var s in args) {
                var sSplit = s.Split(':');
                var name = path + ( sSplit[0].EndsWith(".bat") ? sSplit[0] : sSplit[0] + ".bat" );
                var arguments = " /c \"" + name + "\" ";
                foreach (var sIn in sSplit[1].Split(',')) {
                    arguments += "\"" + sIn + "\"";
                }
                var startInfo = new ProcessStartInfo {
                    FileName = @"cmd.exe",
                    Arguments = arguments,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    UseShellExecute = false,
                    CreateNoWindow = true
                };
                var process = new Process { StartInfo = startInfo };
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
                           "\r\n==================\r\n" + ( !string.IsNullOrEmpty(errors) ? "Errors\r\n==================\r\n" + errors + "\r\n====================" : "" )
                };
                dlg1.Controls.Add(tl);

                dlg1.ShowDialog();
            }
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
