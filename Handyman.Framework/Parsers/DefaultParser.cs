using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handyman.Framework.Interfaces;

namespace Handyman.Framework.Parsers {
    public class DefaultParser : IParse {
        public string[] Parse(string str) {
            var args = new List<string>();
            var colSplit = str.Split(':');
            var start = colSplit[0].Split(' ');
            for (var i = 0; i < ( colSplit.Length == 1 ? start.Length : start.Length - 1 ); ++i) {
                args.Add(start[i]);
            }
            for (var i = 0; i < colSplit.Length - 1; ++i) {
                var spl = colSplit[i + 1].Split(' ');
                var count = spl.Length;
                var take = count > 1 ? i + 1 == colSplit.Length - 1 ? count : count - 1 : 1;
                var parts = colSplit[i].Split(' ').Last() + ":" + string.Join(" ", spl.Take(take));
                args.Add(parts);
            }
            return args.Where(x => !string.IsNullOrEmpty(x)).ToArray();
        }
    }
}
