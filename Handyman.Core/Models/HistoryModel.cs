using System;

namespace Handyman.Core.Models {
    public class HistoryModel {
        public string Alias { get; set; }
        public DateTime Date { get; set; }
        public int Hits { get; set; }
    }
}
