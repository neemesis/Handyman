using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slave.Framework.Interfaces;

namespace Slave.Framework.Parsers {
    public class DefaultParser : IParse {
        public string[] Parse(string str) {
            var args = new List<string>();
            var colSplit = str.Split(':');
            var start = colSplit[0].Split(' ');
            for (var i = 1; i < ( colSplit.Length == 1 ? start.Length : start.Length - 1 ); ++i) {
                args.Add(start[i]);
            }
            for (var i = 0; i < colSplit.Length - 1; ++i) {
                var spl = colSplit[i + 1].Split(' ');
                var count = spl.Length;
                var parts = colSplit[i].Split(' ').Last() + ":" + string.Join(" ", spl.Take(count > 1 ? count - 1 : 1));
                args.Add(parts);
            }
            return args.ToArray();
        }
    }
}
