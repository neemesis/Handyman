using Slave.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Slave.RestPlugin {
    public class RestPlugin : IMaster {
        public RestPlugin() {
            _alias = "rest";
            _hotKey = Shortcut.None;
        }

        public string Name => "REST Plugin";
        public string Description => "Send requests to REST endpoints";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.RestPlugin/README.MD";
        public IParse Parser { get; set; }
        private Shortcut _hotKey;
        private string _alias;


        Shortcut IMaster.HotKey {
            get => _hotKey;
            set => _hotKey = value;
        }

        string IMaster.Alias {
            get => _alias;
            set => _alias = value;
        }

        public void Initialize() {
        }

        public void Execute(string[] args, Action<string> display = null) {
            if (args.Count() < 2 || args[0] == "help") {
                return;
            } else if (args[0] == "get") {
                var url = args[1];
                var sb = new StringBuilder();
                sb.Append("/?");
                foreach (var at in args.Where(x => !x.StartsWith("u:") && !x.StartsWith("url:"))) {
                    sb.Append("&" + at.Split(':')[0] + "=" + at.Split(':')[1]);
                }

                using (var client = new HttpClient(new HttpClientHandler { AutomaticDecompression = DecompressionMethods.GZip | DecompressionMethods.Deflate })) {
                    var response = client.GetAsync(url + sb).Result;
                    response.EnsureSuccessStatusCode();
                    var result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Result: " + result);
                }

            } else if (args[0] == "post") {

            }
        }
    }
}
