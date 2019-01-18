using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Windows.Forms;
using Handyman.Framework.Entities;
using Handyman.Framework.Interfaces;

namespace Handyman.BatchPlugin {
    public class BatchPlugin : IMaster {
        public string Name => "Batch Plugin";
        public string Description => "Execute list of programs.";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Handyman/blob/master/Handyman.BatchPlugin/README.MD";
        public Shortcut HotKey { get; set; }
        public string Alias { get; set; }
        public IParse Parser { get; set; }
        public List<string> Suggestions { get; set; }

        private List<Batch> Batches { get; set; }

        public BatchPlugin() {
            Alias = "batch";
            HotKey = Shortcut.None;
            Parser = new BatchParser();
            Suggestions = new List<string> { "batch create", "batch delete", "batch change" };
        }

        private void Save() {
            Framework.Persistence.Persist.Save(Batches, Alias);
        }

        private void Add(string name, List<string> apps) {
            var b = Batches.SingleOrDefault(x => x.Name == name);
            if (b == null) {
                b = new Batch {Name = name, Apps = apps};
            } else
                b.Apps = apps;

            Batches.Add(b);
            Save();
        }

        private void Delete(string name) {
            Batches.Remove(Batches.Single(x => x.Name == name));
            Save();
        }

        public void Initialize() {
            Batches = Framework.Persistence.Persist.Load<List<Batch>>(Alias);
        }

        public void Execute(string[] args, Action<string, DisplayData, List<string>, Action<string>> display) {
            if (args.Length > 2 && args[0] == "create") {
                Add(args[1], args.Skip(2).ToList());
            } else if (args.Length == 2 && args[0] == "delete") {
                Delete(args[1]);
            } else if (args.Length > 2 && args[0] == "change") {
                Add(args[1], args.Skip(2).ToList());
            } else if (args.Length == 1) {
                var batch = Batches.Single(x => x.Name == args[0]);
                foreach (var a in batch.Apps) {
                    Process.Start(a);
                }
            } else {
                display("Command not found!", DisplayData.Launcher, null, null);
                return;
            }
            display("Done", DisplayData.Launcher, null, null);
        }
    }
}
