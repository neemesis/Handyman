using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Slave.Core.Models {
    public class Plugin {
        public string Name { get; set; }
        public string Url { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string HasConfig { get; set; }
    }
}
