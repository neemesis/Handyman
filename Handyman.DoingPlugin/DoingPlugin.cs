using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Handyman.Framework.Entities;
using Handyman.Framework.Interfaces;

namespace Handyman.DoingPlugin {
    public class DoingPlugin : IMaster {
        public string Name => "Doing Plugin";
        public string Description => "Track when you started doing things.";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Handyman/blob/master/Handyman.DoingPlugin/README.MD";
        public Shortcut HotKey { get; set; }
        public string Alias { get; set; }
        public IParse Parser { get; set; }
        public List<string> Suggestions { get; set; }
        private List<DoingModel> Doings { get; set; }

        public DoingPlugin() {
            Alias = "do";
            HotKey = Shortcut.None;
            Initialize();
        }

        public void Initialize() {
            Doings = Framework.Persistence.Persist.Load<List<DoingModel>>(Alias);
            Suggestions = new List<string> { "do list" };
        }

        private void Load() {
            Doings = Framework.Persistence.Persist.Load<List<DoingModel>>(Alias);
        }

        private void Add(string name) {
            Doings.Add(new DoingModel { Name = name, Date = DateTime.Now });
            Framework.Persistence.Persist.Save(Doings, Alias);
        }

        public void Execute(string[] args, Action<string, DisplayData, List<string>, Action<string>> display) {
            if (args[0] == "list") {
                Load();
                if (Doings.Count < 1) {
                    display("it is empty here :(", DisplayData.Launcher, null, null);
                    return;
                }

                List<string> response = Doings.Select(x => x.Name + " - " + x.Date.ToString("d")).ToList();
                if (args.Length >= 2) {
                    if (args[1] == "today") {
                        response = Doings.Where(x => x.Date.Date == DateTime.Now.Date)
                            .Select(x => x.Name + " - " + x.Date.ToString("d")).ToList();
                    } else if (args[1] == "yesterday") {
                        response = Doings.Where(x => x.Date.Date == DateTime.Now.AddDays(-1).Date)
                            .Select(x => x.Name + " - " + x.Date.ToString("d")).ToList();
                    } else if (args[1] == "this" && args[2] == "week") {
                        response = Doings.Where(x => x.Date.Date == DateTime.Now.AddDays(-7).Date)
                            .Select(x => x.Name + " - " + x.Date.ToString("d")).ToList();
                    }
                }
                
                display("what you have done", DisplayData.Question, response, null);
            } else {
                Add(string.Join(" ", args));
                display("Done", DisplayData.Launcher, null, null);
            }
        }
    }
}
