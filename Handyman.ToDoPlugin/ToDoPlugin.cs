using Handyman.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using Handyman.Framework.Entities;

namespace Handyman.ToDoPlugin {
    public class ToDoPlugin : IMaster {
        private List<ToDo> ToDos { get; set; }
        private Action<string, DisplayData, List<string>, Action<string>> _display;

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
            _display = display;
            if (args.Length > 0 && args[0] == "help") {
                DisplayHelp(display);
            } else if (args.Length == 0 || args.Length > 0 && args[0] == "list") {
                DisplayToDoList(display);
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
                display("done :)", DisplayData.Launcher, null, null);
            } else if (args[0] == "delete") {
                var td = ToDos.SingleOrDefault(x => x.Name == args[1]);
                if (td != null) {
                    ToDos.Remove(td);
                    SaveToDos();
                    display("done :)", DisplayData.Launcher, null, null);
                }
            } else if (args[0] == "done") {
                var td = ToDos.SingleOrDefault(x => x.Name == args[1]);
                if (td != null) {
                    td.Finished = true;
                    td.TimeFinished = DateTime.Now;
                    SaveToDos();
                    display("done :)", DisplayData.Launcher, null, null);
                }
            }
        }

        private void DisplayToDoList(Action<string, DisplayData, List<string>, Action<string>> display) {
            var list = ToDos.Where(x => !x.Finished).Select(x => x.Name + ": " + x.Description).ToList();
            display("todos", DisplayData.Question, list, OnTodoClick);
        }

        private void OnTodoClick(string s) {
            foreach (var td in ToDos) {
                if (s.Contains(td.Name)) {
                    td.Finished = true;
                    SaveToDos();
                    DisplayToDoList(_display);
                    return;
                }
            }
        }

        private void DisplayHelp(Action<string, DisplayData, List<string>, Action<string>> display) {
            var list = new[] { _alias + " add n:<name> d:<desc>: add new",
                _alias + " delete <name>: delete ToDo item",
                _alias + " done <name>: set ToDo item Done",
                _alias + " list: show ToDo items"
            };

            display("usage", DisplayData.Question, list.ToList(), null);
        }
    }
}
