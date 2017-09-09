using System;
using System.Collections.Generic;
using System.Linq;
using System.Management.Instrumentation;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Handyman.Framework.Entities;
using Handyman.Framework.Interfaces;

namespace Handyman.GitPlugin {
    public class GitPlugin : IMaster {
        public string Name => "Git Plugin";
        public string Description => "Execute git commands.";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Handyman/blob/master/Handyman.GitPlugin/README.MD";
        private Shortcut _hotKey;
        private string _alias;
        public IParse Parser { get; set; }
        public List<string> Suggestions { get; set; }

        Shortcut IMaster.HotKey {
            get => _hotKey;
            set => _hotKey = value;
        }

        string IMaster.Alias {
            get => _alias;
            set => _alias = value;
        }

        private List<GitRepo> _namePaths;

        public GitPlugin() {
            _alias = "git";
            _hotKey = Shortcut.None;
        }

        public void Initialize() {
            _namePaths = Framework.Persistence.Persist.Load<List<GitRepo>>(_alias);
            Suggestions = new List<string> {"git add", "git set", "git delete", "git pull", "git clone", "git commit" };
        }

        private void Add(string name, string path) {
            _namePaths.Add(new GitRepo {Name = name, Path = path});
            Framework.Persistence.Persist.Save(_namePaths, _alias);
        }

        private void Change(string name, string path) {
            _namePaths.Remove(_namePaths.Single(x => x.Name == name));
            Add(name, path);
        }
        private void Delete(string name) {
            _namePaths.Remove(_namePaths.Single(x => x.Name == name));
            Framework.Persistence.Persist.Save(_namePaths, _alias);
        }

        private void Pull(string repo) {
            Framework.Utilities.CMD("git pull", _namePaths.Single(x => x.Name == repo).Path, out _);
        }

        private void Clone(string name, string repo, string path) {
            Framework.Utilities.CMD("git clone " + repo, path, out _);
            Add(name, path);
        }

        private void Commit(string name, string msg) {
            var path = _namePaths.Single(x => x.Name == name).Path;
            Framework.Utilities.CMD("git add .", path, out _);
            Framework.Utilities.CMD("git commit -m " + (string.IsNullOrEmpty(msg) ? " " : msg), path, out _);
            Framework.Utilities.CMD("git push -u origin master", path, out _);
        }

        public void Execute(string[] args, Action<string, DisplayData, List<string>, Action<string>> display) {
            if (args[0] == "add") {
                var name = args.SingleOrDefault(x => x.StartsWith("n:"));
                var path = args.SingleOrDefault(x => x.StartsWith("p:"));
                Add(string.IsNullOrEmpty(name) ? args[1] : name.Split(':')[1], string.IsNullOrEmpty(path) ? args[2] : path.Split(':')[1]);
            } else if (args[0] == "set") {
                var name = args.SingleOrDefault(x => x.StartsWith("n:"));
                var path = args.SingleOrDefault(x => x.StartsWith("p:"));
                Change(string.IsNullOrEmpty(name) ? args[1] : name.Split(':')[1], string.IsNullOrEmpty(path) ? args[2] : path.Split(':')[1]);
            } else if (args[0] == "delete") {
                var name = args.SingleOrDefault(x => x.StartsWith("n:"));
                Delete(string.IsNullOrEmpty(name) ? args[1] : name.Split(':')[1]);
            } else if (args[0] == "pull") {
                Pull(args[1]);
            } else if (args[0] == "clone") {
                Clone(args[1], args[2], args[3]);
            } else if (args[0] == "commit") {
                var name = args.SingleOrDefault(x => x.StartsWith("n:"));
                var msg = args.SingleOrDefault(x => x.StartsWith("m:"));
                Commit(string.IsNullOrEmpty(name) ? args[1] : name.Split(':')[1], string.IsNullOrEmpty(msg) ? args[2] : msg.Split(':')[1]);
            }

            display("Done", DisplayData.Launcher, null, null);
        }
    }
}
