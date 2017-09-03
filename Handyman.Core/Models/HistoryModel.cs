using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Handyman.Core.Models {
    public class HistoryModel {
        public string Alias { get; set; }
        public DateTime Date { get; set; }
        public int Hits { get; set; }
    }
}
