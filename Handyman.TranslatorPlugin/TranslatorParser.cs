using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Handyman.Framework.Interfaces;

namespace Handyman.TranslatorPlugin {
    public class TranslatorParser : IParse {
        public string[] Parse(string str) {
            return str.Split(' ');
        }
    }
}
