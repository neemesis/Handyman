using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Handyman.Framework.Entities;
using Handyman.Framework.Interfaces;

namespace Handyman.TrackPlugin {
    public class TrackPlugin : IMaster {
        public string Name => "Track Plugin";
        public string Description => "Track work time on projects";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Handyman/blob/master/Handyman.TrackPlugin/README.MD";
        public Shortcut HotKey { get; set; }
        public string Alias { get; set; }
        public IParse Parser { get; set; }
        public List<string> Suggestions { get; set; }

        private List<Project> Projects { get; set; }
        private List<TrackData> Data { get; set; }
        private string ProjSave { get; set; }
        private string DataSave { get; set; }

        public TrackPlugin() {
            Alias = "track";
            HotKey = Shortcut.None;
            ProjSave = Alias + "Projects";
            DataSave = Alias + "Data";
            Suggestions = new List<string> {"track add", "track stop", "track today"};
        }

        private void Load() {
            Projects = Framework.Persistence.Persist.Load<List<Project>>(ProjSave);
            Data = Framework.Persistence.Persist.Load<List<TrackData>>(DataSave);
        }

        private void Save() {
            Framework.Persistence.Persist.Save(Projects, ProjSave);
            Framework.Persistence.Persist.Save(Data, DataSave);
        }

        private void AddP(string p) {
            if (!Projects.Any(x => x.Name == p))
                Projects.Add(new Project { Name = p });
            Save();
        }

        private void AddD(string p) {
            var pr = Projects.SingleOrDefault(x => x.Name == p);
            if (pr == null)
                return;

            foreach (var data in Data.Where(x => !x.EndTime.HasValue)) {
                data.EndTime = DateTime.Now;
            }
            Data.Add(new TrackData {Project = pr, StartTime = DateTime.Now});
            Save();
        }

        private void Finish() {
            var td = Data.Where(x => !x.EndTime.HasValue).OrderByDescending(x => x.StartTime);

            foreach(var td2 in Data)
                td2.EndTime = DateTime.Now;
            Save();
        }

        private List<string> CrunchData() {
            var today = Data
                .Where(x => x.StartTime >= DateTime.Now.Date && x.EndTime < DateTime.Now.AddDays(1).Date)
                .GroupBy(x => x.Project);

            var ls = new List<string>();
            foreach (var g in today) {
                var totalTime = TimeSpan.Zero;
                foreach (var data in g) {
                    var diff = data.EndTime.Value - data.StartTime;
                    totalTime += diff;
                }
                ls.Add($"{g.Key.Name}  -  {totalTime:g}");
            }
            return ls;
        }

        public void Initialize() {
            Load();
        }

        public void Execute(string[] args, Action<string, DisplayData, List<string>, Action<string>> display) {
            if (args[0] == "add") {
                AddP(args[1]);
            } else if (args[0] == "stop") {
                Finish();
            } else if (args[0] == "today") {
                var finalize = CrunchData();
                display("Today times:", DisplayData.Question, finalize, null);
            } else if (args.Length == 1) {
                AddD(args[0]);
            } else {
                display("Command not found!", DisplayData.Launcher, null, null);
            }
        }
    }
}
