using Handyman.Framework.Interfaces;

namespace Handyman.TranslatorPlugin {
    public class TranslatorParser : IParse {
        public string[] Parse(string str) {
            return str.Split(' ');
        }
    }
}
