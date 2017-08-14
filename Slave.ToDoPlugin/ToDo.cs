using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.ToDoPlugin {
    public class ToDo {
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime Created { get; set; }
        public bool Finished { get; set; }
        public DateTime TimeFinished { get; set; }
    }
}
