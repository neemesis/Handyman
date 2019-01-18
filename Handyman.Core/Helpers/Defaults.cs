﻿using System.Collections.Generic;
using Handyman.Framework.Entities;

namespace Handyman.Core.Helpers {
    public static class Defaults {
        public static List<Commands> Default() {
            var list = new List<Commands>();

            var word1 = new Commands {
                Alias = "paint",
                FileName = @"pbrush.exe"
            };
            list.Add(word1);

            var google = new Commands {
                Alias = "google",
                FileName = @"https://www.google.com/search?q={FU}",
                Arguments = ""
            };
            list.Add(google);

            var yt = new Commands {
                Alias = "youtube",
                FileName = @"https://www.youtube.com/results?search_query={FU}",
                Arguments = ""
            };
            list.Add(yt);

            var lin = new Commands {
                Alias = "lin",
                FileName = @"https://www.linkedin.com/feed/",
                Arguments = ""
            };
            list.Add(lin);

            var fb = new Commands {
                Alias = "fb",
                FileName = @"https://www.facebook.com/",
                Arguments = ""
            };
            list.Add(fb);

            var tmb = new Commands {
                Alias = "tmb",
                FileName = @"http://themorningbrew.net/",
                Arguments = ""
            };
            list.Add(tmb);

            var reddit = new Commands {
                Alias = "reddit",
                FileName = @"https://www.reddit.com/",
                Arguments = ""
            };
            list.Add(reddit);

            var dw = new Commands {
                Alias = "downloads",
                FileName = @"C:\Users\{U}\Downloads",
                Arguments = ""
            };
            list.Add(dw);

            return list;
        }
    }
}
