using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Slave.Framework.Entities;

namespace Slave.Core.Helpers {
    public static class Defaults {
        public static List<Commands> Default() {
            var list = new List<Commands>();

            if (File.Exists(@"C:\Program Files\Mozilla Firefox\firefox.exe")) {
                var word = new Commands {
                    Alias = "firefox",
                    FileName = @"C:\Program Files\Mozilla Firefox\firefox.exe"
                };
                list.Add(word);
            }

            var word1 = new Commands {
                Alias = "paint",
                FileName = @"pbrush.exe"
            };
            list.Add(word1);

            var mailWord = new Commands {
                Alias = "mail",
                FileName = @"http://mail.google.com",
                Arguments = ""
            };
            list.Add(mailWord);

            var calendarWord = new Commands {
                Alias = "calendar",
                FileName = @"http://calendar.google.com",
                Arguments = ""
            };
            list.Add(calendarWord);

            var docsWord = new Commands {
                Alias = "docs",
                FileName = @"http://www.google.com/docs",
                Arguments = ""
            };
            list.Add(docsWord);

            return list;
        }
    }
}
