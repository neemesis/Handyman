using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Handyman.Framework.Entities;
using Handyman.Framework.Interfaces;

namespace Handyman.WorkPlugin {
    public class WorkPlugin : IMaster {
        public string Name => "Work Plugin";
        public string Description => "Work / rest timer";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Handyman/blob/master/Handyman.WorkPlugin/README.MD";
        public Shortcut HotKey { get; set; }
        public string Alias { get; set; }
        public IParse Parser { get; set; }
        public List<string> Suggestions { get; set; }

        private Work Work { get; set; }
        private Timer Timer { get; set; }
        private DateTime Started { get; set; }
        private int Status { get; set; }

        public WorkPlugin() {
            Alias = "work";
            HotKey = Shortcut.None;
        }

        public void Initialize() {
            Timer = new Timer();
            Status = 0;
            Work = Framework.Persistence.Persist.Load<Work>(Alias);
            if (Work == null) {
                Work = new Work {WorkMin = 57, RestMin = 13};
                Save();
            }
            Suggestions = new List<string> { "work stop", "work status", "work reset"};
        }

        private void Save() => Framework.Persistence.Persist.Save(Work, Alias);

        public void Execute(string[] args, Action<string, DisplayData, List<string>, Action<string>> display) {
            if (args.Length == 0) {
                if (Status == 0)
                    Start(1, display);
                else DisplayStatus(display);
            } else if (args.Length == 3 && args[1] == "rest") {
                int.TryParse(args[0], out var workMin);
                int.TryParse(args[2], out var restMin);

                if (workMin == 0)
                    workMin = 57;
                if (restMin == 0)
                    restMin = 13;

                Work.WorkMin = workMin;
                Work.RestMin = restMin;

                Save();
            } else if (args.Length == 1 && args[0] == "stop") {
                Start(0, display);
            } else if (args.Length == 1 && args[0] == "status") {
                DisplayStatus(display);
                return;
            } else if (args.Length == 1 && args[0] == "reset") {
              Start(0, display);  
            } else {
                display("Invalid command!", DisplayData.Launcher, null, null);
                return;
            }
            display("Done", DisplayData.Launcher, null, null);
        }

        private void DisplayStatus(Action<string, DisplayData, List<string>, Action<string>> display) {
            var tn = DateTime.Now - Started;
            var outMsg = "";
            switch (Status) {
                case 0:
                    outMsg = $"idle || Work: {Work.WorkMin}min || Rest: {Work.RestMin}min";
                    break;
                case 1:
                    outMsg = $"work: {(int)tn.TotalMinutes} of {Work.WorkMin} min.";
                    break;
                case 2:
                    outMsg = $"rest: {(int)tn.TotalMinutes} of {Work.RestMin} min.";
                    break;
            }
            display(outMsg, DisplayData.Launcher, null, null);
        }

        private void Start(int work, Action<string, DisplayData, List<string>, Action<string>> display) {
            if (work == 1) {
                Status = 1;
                Timer.Stop();
                Timer.Interval = Work.WorkMin * 60 * 1000;
                Timer.Tick += (sender, args) => {
                    display("work period over", DisplayData.Launcher, null, null);
                    Start(2, display);
                };
                Started = DateTime.Now;
                Timer.Start();
            } else if (work == 2) {
                Status = 2;
                Timer.Stop();
                Timer.Interval = Work.WorkMin * 60 * 1000;
                Timer.Tick += (sender, args) => {
                    display("rest period over", DisplayData.Launcher, null, null);
                    Start(2, display);
                };
                Started = DateTime.Now;
                Timer.Start();
            } else if (work == 0) {
                Status = 0;
                Timer.Stop();
                display("timer stoped", DisplayData.Launcher, null, null);
            }
        }
    }
}
