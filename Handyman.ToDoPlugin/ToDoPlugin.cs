using Handyman.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Handyman.Framework.Entities;

namespace Handyman.ToDoPlugin {
    public class ToDoPlugin : IMaster {
        private List<ToDo> ToDos { get; set; }

        public ToDoPlugin() {
            _alias = "todo";
            _hotKey = Shortcut.None;
        }

        public string Name => "ToDo Plugin";
        public string Description => "ToDo Lists";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Handyman/blob/master/Handyman.ToDoPlugin/README.MD";
        public IParse Parser { get; set; }
        public List<string> Suggestions { get; set; }

        private void DeleteToDo(string name) {
            if (ToDos == null)
                return;

            ToDos.Remove(ToDos.First(x => x.Name == name));
            SaveToDos();
        }

        public void Initialize() {
            ToDos = Framework.Persistence.Persist.Load<List<ToDo>>(_alias);
            Suggestions = new List<string> { "todo add", "todo delete", "todo done" };
        }

        private void LoadToDos() {
            ToDos = Framework.Persistence.Persist.Load<List<ToDo>>(_alias);
        }

        private void InsertToDo(ToDo td) {
            ToDos.Add(td);
            Framework.Persistence.Persist.Save(ToDos, _alias);
        }


        private void SaveToDos() {
            Framework.Persistence.Persist.Save(ToDos, _alias);
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

        public void Execute(string[] args, Action<string, DisplayData, List<string>, Action<string>> display) {
            if (args.Length > 0 && args[0] == "help") {
                DisplayHelp();
            } else if (args.Length == 0 || args.Length > 0 && args[0] == "list") {
                DisplayToDoList(ToDos);
            } else if (args[0] == "add" && args.Length > 1) {
                var td = new ToDo {
                    Created = DateTime.Now,
                    Finished = false,
                    TimeFinished = DateTime.MinValue,
                    Name = args.Single(x => x.StartsWith("n:") || x.StartsWith("name:")).Split(':')[1],
                    Description = args.SingleOrDefault(x => x.StartsWith("d:") || x.StartsWith("desc:") || x.StartsWith("description:"))?.Split(':')[1],
                };
                InsertToDo(td);
                //SaveToDos();
            } else if (args[0] == "delete") {
                var td = ToDos.SingleOrDefault(x => x.Name == args[1]);
                if (td != null) {
                    ToDos.Remove(td);
                    SaveToDos();
                }
            } else if (args[0] == "done") {
                var td = ToDos.SingleOrDefault(x => x.Name == args[1]);
                if (td != null) {
                    td.Finished = true;
                    td.TimeFinished = DateTime.Now;
                    SaveToDos();
                }
            }
        }

        private void DisplayToDoList(List<ToDo> items) {
            var dlg1 = new Form {
                Text = "ToDo",
                AutoScroll = true,
                Size = new Size(500, 650)
            };
            var tl = new Label {
                AutoSize = true,
                Text = "===============================\r\n"
            };

            foreach (var i in items)
                tl.Text += "- " + i.Name + " - " + i.Description + "\r\n";

            tl.Text += "=================================";

            dlg1.Controls.Add(tl);

            dlg1.ShowDialog();
            return;
        }

        private void DisplayHelp() {
            var dlg1 = new Form {
                Text = "Email Plugin Help",
                AutoScroll = true,
                Size = new Size(900, 650),
                Font = new Font("Arial", 14, FontStyle.Regular)
            };
            var tl = new Label {
                AutoSize = true,
                Text = "Usage\r\n==================\r\n"
                + _alias + " help: display help\r\n"
                + _alias + " add n:<name> d:<description|optional>: add new ToDo item\r\n"
                + _alias + " delete <name>: delete ToDo item\r\n"
                + _alias + " done <name>: set ToDo item Done\r\n"
                + _alias + " list: show ToDo items\r\n"
                + "=================="
            };
            dlg1.Controls.Add(tl);

            dlg1.ShowDialog();
            return;
        }
    }
}
