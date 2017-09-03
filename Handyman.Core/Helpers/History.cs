using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handyman.Core.Models;

namespace Handyman.Core.Helpers {
    public static class History {
        private static List<HistoryModel> _history { get; set; }
        private static string _alias = "handymanHistory";

        private static void LoadHistory() {
            _history = Framework.Persistence.Persist.Load<List<HistoryModel>>(_alias);
            if (_history == null) {
                _history = new List<HistoryModel>();
                SaveHistory();
            }
        }

        private static void SaveHistory() {
            Framework.Persistence.Persist.Save(_history, _alias);
        }

        public static void Add(string alias) {
            LoadHistory();

            var ent = _history.SingleOrDefault(x => x.Alias == alias);

            if (ent == null) {
                ent = new HistoryModel {Alias = alias, Hits = 0};
            }
            ent.Date = DateTime.Now;
            ++ent.Hits;

            SaveHistory();
        }
    }
}
