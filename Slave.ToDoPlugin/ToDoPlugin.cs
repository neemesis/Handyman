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

namespace Slave.ToDoPlugin {
    public class ToDoPlugin : IMaster {
        private List<ToDo> ToDos { get; set; }
        private readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Environment.UserName + ".todoslave";

        public ToDoPlugin() {
            _mAlias = "todo";
            _mHotKey = Shortcut.None;
        }

        public string Name => "ToDo Plugin";
        public string Description => "ToDo Lists";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.ToDoPlugin/README.MD";

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

        public void Initialize() {
            LoadToDos();
        }

        public void Execute(string[] args, Action<string> display) {
            if (args.Count() > 0 && args[0] == "help") {
                DisplayHelp();
            } else if (args.Count() == 0 || args.Count() > 0 && args[0] == "list") {
                DisplayToDoList(ToDos);
            } else if (args[0] == "add" && args.Count() > 1) {
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
                + _mAlias + " help: display help\r\n"
                + _mAlias + " add n:<name> d:<description|optional>: add new ToDo item\r\n"
                + _mAlias + " delete <name>: delete ToDo item\r\n"
                + _mAlias + " done <name>: set ToDo item Done\r\n"
                + _mAlias + " list: show ToDo items\r\n"
                + "=================="
            };
            dlg1.Controls.Add(tl);

            dlg1.ShowDialog();
            return;
        }
    }
}
