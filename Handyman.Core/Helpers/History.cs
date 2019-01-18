using System;
using System.Collections.Generic;
using System.Linq;
using Handyman.Core.Models;

namespace Handyman.Core.Helpers {
    public static class History {
        private static List<HistoryModel> _history { get; set; }
        private static readonly string _alias = "handymanHistory";

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
            alias = alias.Trim();
            LoadHistory();

            var ent = _history.SingleOrDefault(x => x.Alias == alias);

            if (ent == null) {
                ent = new HistoryModel {Alias = alias, Hits = 0};
                _history.Add(ent);
            }
            ent.Date = DateTime.Now;
            ++ent.Hits;

            SaveHistory();
        }

        public static List<string> Get(string text) {
            LoadHistory();
            return _history.Where(x => x.Alias.Contains(text))
                .OrderByDescending(x => x.Hits)
                .ThenByDescending(x => x.Date)
                .Select(x => x.Alias)
                .ToList();
        }

        public static List<string> GetAll() {
            LoadHistory();
            return _history.OrderByDescending(x => x.Hits)
                .ThenByDescending(x => x.Date)
                .Select(x => x.Alias)
                .ToList();
        }
    }
}
