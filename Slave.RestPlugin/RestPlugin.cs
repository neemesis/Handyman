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
            _mAlias = "rest";
            _mHotKey = Shortcut.None;
        }

        public string Name => "REST Plugin";

        public string Description => "Send requests to REST endpoints";

        public string Author => "Mirche Toshevski";

        public string Version => "1.0.0.0";

        private Shortcut _mHotKey;
        private string _mAlias;


        Shortcut IMaster.HotKey {
            get { return _mHotKey; }
            set { _mHotKey = value; }
        }

        string IMaster.Alias {
            get { return _mAlias; }
            set { _mAlias = value; }
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
                    HttpResponseMessage response = client.GetAsync(url + sb.ToString()).Result;
                    response.EnsureSuccessStatusCode();
                    string result = response.Content.ReadAsStringAsync().Result;
                    Console.WriteLine("Result: " + result);
                }

            } else if (args[0] == "post") {

            }
        }
    }
}
