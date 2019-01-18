using System.Collections.Generic;
using System.Linq;
using Handyman.Framework.Interfaces;

namespace Handyman.BatchPlugin {
    public class BatchParser : IParse {
        public string[] Parse(string str) {
            var list = new List<string>();
            var firstSplit = str.Split('#');
            list.AddRange(firstSplit[0].Split(' '));
            list.AddRange(firstSplit.Skip(1));
            return list.Where(x => !string.IsNullOrEmpty(x)).Select(x => x.Trim()).ToArray();
        }
    }
}
