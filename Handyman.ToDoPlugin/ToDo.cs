using System;

namespace Handyman.ToDoPlugin {
    public class ToDo {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public bool Finished { get; set; }
        public DateTime TimeFinished { get; set; }
    }
}
