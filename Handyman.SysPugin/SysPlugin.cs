using Handyman.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Windows.Forms;
using Handyman.Framework.Entities;

namespace Handyman.SysPugin {
    public class SysPlugin : IMaster {
        public SysPlugin() {
            _alias = "sys";
            _hotKey = Shortcut.None;
        }

        public string Name => "System Plugin";
        public string Description => "Execute system commands.";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Handyman/blob/master/Handyman.SysPlugin/README.MD";

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

        public IParse Parser { get; set; }
        public List<string> Suggestions { get; set; }

        public void Execute(string[] args, Action<string, DisplayData, List<string>, Action<string>> display = null) {
            var cmdArgs = "shutdown ";
            if (args[0] == "shutdown" || args[0] == "sd") {
                if (args.Length == 1) {
                    cmdArgs += "/s";
                    display("shuting down", DisplayData.Launcher, null, null);
                } else {
                    var mins = int.Parse(args[1]);
                    cmdArgs += "/s /t " + (mins * 60);
                    display($"shuting down in {mins} minutes", DisplayData.Launcher, null, null);
                } 
            } else if (args[0] == "restart" || args[0] == "rs") {
                cmdArgs += "/r";
                if (args.Length == 1) {
                    cmdArgs += "/r";
                    display("restarting", DisplayData.Launcher, null, null);
                } else {
                    var mins = int.Parse(args[1]);
                    cmdArgs += "/r /t " + ( mins * 60 );
                    display($"restarting in {mins} minutes", DisplayData.Launcher, null, null);
                }
            } else if (args[0] == "hibernate" || args[0] == "hn") {
                cmdArgs += "/h";
                if (args.Length == 1) {
                    cmdArgs += "/h";
                    display("going in hibernate", DisplayData.Launcher, null, null);
                } else {
                    var mins = int.Parse(args[1]);
                    cmdArgs += "/h /t " + ( mins * 60 );
                    display($"hibernating in {mins} minutes", DisplayData.Launcher, null, null);
                }
            } else if (args[0] == "abort") {
                cmdArgs += "/a";
                display("aborted", DisplayData.Launcher, null, null);
            } else if (args[0] == "logoff" || args[0] == "lo") {
                cmdArgs += "/l";
                if (args.Length == 1) {
                    cmdArgs += "/l";
                    display("logging off", DisplayData.Launcher, null, null);
                } else {
                    var mins = int.Parse(args[1]);
                    cmdArgs += "/l /t " + ( mins * 60 );
                    display($"logging of in {mins} minutes", DisplayData.Launcher, null, null);
                }
            }

            var res = Framework.Utilities.CMD(cmdArgs, null, out var errors);
        }

        public void Initialize() {
            Suggestions = new List<string> { "sys shutdown", "sys restart", "sys hibernate", "sys logoff", "sys abort"};
            //throw new NotImplementedException();
        }
    }
}
