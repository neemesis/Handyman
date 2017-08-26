using Slave.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Slave.Framework.Entities;

namespace Slave.ToDoPlugin {
    public class ToDoPlugin : IMaster {
        private List<ToDo> ToDos { get; set; }
        private readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Environment.UserName + ".todoslave";

        public ToDoPlugin() {
            _alias = "todo";
            _hotKey = Shortcut.None;
        }

        public string Name => "ToDo Plugin";
        public string Description => "ToDo Lists";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.ToDoPlugin/README.MD";
        public IParse Parser { get; set; }
        private void DeleteToDo(string name) {
            if (ToDos == null)
                return;

            ToDos.Remove(ToDos.First(x => x.Name == name));
            SaveToDos();
        }

        private void LoadToDos() {
            if (File.Exists(_path)) {
                var serializer = new XmlSerializer(typeof(List<ToDo>));
                var reader = File.OpenText(_path);
                ToDos = (List<ToDo>)serializer.Deserialize(reader);
                reader.Close();
            } else {
                ToDos = new List<ToDo>();
                SaveToDos();
            }
        }

        private void InsertToDo(ToDo td) {
            if (ToDos == null)
                LoadToDos();

            ToDos.Add(td);
            SaveToDos();
        }


        private void SaveToDos() {
            var ser = new XmlSerializer(typeof(List<ToDo>));
            var sw = new StreamWriter(_path);
            ser.Serialize(sw, ToDos);
            sw.Close();
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

        public void Initialize() {
            LoadToDos();
        }

        public void Execute(string[] args, Action<string, DisplayData> display) {
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
                ToDos.Add(td);
                SaveToDos();
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
                tl.Text += "- " + i.Name + "\r\n";

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
