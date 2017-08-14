using Slave.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace Slave.SeriesPlugin {
    public class SeriesPlugin : IMaster {
        private List<Series> SeriesList { get; set; }
        private string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Environment.UserName + ".seriesslave";

        static string[] mediaExtensions = {
            ".AVI", ".MP4", ".DIVX", ".WMV", ".MKV", ".FLV", ".WEBM", ".MOV", ".AMV", ".MPG", ".M4V", ".3GP"
        };

        public SeriesPlugin() {
            _mAlias = "tv";
            _mHotKey = Shortcut.None;
        }

        public string Name => "Series Plugin";

        public string Description => "Track and play TV Series";

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

        private void LoadSeriesList() {
            if (File.Exists(_path)) {
                var serializer = new XmlSerializer(typeof(List<Series>));
                var reader = File.OpenText(_path);
                SeriesList = (List<Series>)serializer.Deserialize(reader);
                reader.Close();
            } else {
                SeriesList = new List<Series>();
                SaveSeriesList();
            }
        }

        private void InsertSeries(Series td) {
            if (SeriesList == null)
                LoadSeriesList();

            SeriesList.Add(td);
            SaveSeriesList();
        }


        private void SaveSeriesList() {
            var ser = new XmlSerializer(typeof(List<Series>));
            var sw = new StreamWriter(_path);
            ser.Serialize(sw, SeriesList);
            sw.Close();
        }

        private static bool CheckFile(string fileName, string name, string season, string episode) {
            if (fileName.Contains(name) && fileName.Contains(season) && fileName.Contains(episode))
                return true;
            return false;
        }

        private static bool IsVideo(string filename) {
            return mediaExtensions.Contains(Path.GetExtension(filename), StringComparer.OrdinalIgnoreCase);
        }

        private static string Search(string sDir, string name, string season, string episode) {
            try {
                foreach (string d in Directory.GetDirectories(sDir)) {
                    foreach (string f in Directory.GetFiles(d)) {
                        if (IsVideo(f) && CheckFile(f.ToLower(), name, season, episode))
                            return f;
                    }
                    Search(d, name, season, episode);
                }
                foreach (string f in Directory.GetFiles(sDir)) {
                    if (IsVideo(f) && CheckFile(f.ToLower(), name, season, episode))
                        return f;
                }
                return null;
            } catch (System.Exception excpt) {
                Console.WriteLine(excpt.Message);
                return null;
            }
        }

        public void Initialize() {
            LoadSeriesList();
        }

        private static Tuple<int, int, string> SearchBySeasonEpisode(Series s, string ses, string ep, bool forward = true) {
            if (forward) {
                for (int i = int.Parse(ses.Substring(1)); i < 30; ++i) {
                    for (int j = int.Parse(ep.Substring(1)) + 1; j < 30; ++j) {
                        var path = Search(Properties.Settings.Default.Location, s.Name, "s" + i.ToString("00"), "e" + j.ToString("00"));
                        if (!string.IsNullOrEmpty(path))
                            return new Tuple<int, int, string>(i, j, path);
                    }
                }
            } else {
                for (int i = int.Parse(ses.Substring(1)); i >= 0; --i) {
                    for (int j = int.Parse(ep.Substring(1)) - 1; j >= 0; --j) {
                        var path = Search(Properties.Settings.Default.Location, s.Name, "s" + i.ToString("00"), "e" + j.ToString("00"));
                        if (!string.IsNullOrEmpty(path))
                            return new Tuple<int, int, string>(i, j, path);
                    }
                }
            }
            return null;
        }

        public void Execute(string[] args, Action<string> display) {
            if (args.Count() < 1 || args.Count() > 0 && args[0] == "help") {
                DisplayHelp();
            } else if (args[0] == "play" && args.Count() == 3) {
                var path = Search(Properties.Settings.Default.Location, args[1].ToLower(), args[2].Substring(0, 3).ToLower(), args[2].Substring(3, 3).ToLower());
                if (!string.IsNullOrEmpty(path)) {
                    var s = new Series {
                        Name = args[1].ToLower(),
                        Season = args[2].Substring(0, 3).ToLower(),
                        Episode = args[2].Substring(3, 3).ToLower()
                    };
                    SeriesList.Add(s);
                    SaveSeriesList();
                    Process.Start(path);
                }
            } else if (args[0] == "next" && args.Count() == 2) {
                var s = SeriesList.SingleOrDefault(x => x.Name == args[1]);
                var res = SearchBySeasonEpisode(s, s.Season, s.Episode);
                if (res != null) {
                    s.Season = "s" + res.Item1.ToString("00");
                    s.Episode = "e" + res.Item2.ToString("00");
                    SaveSeriesList();
                    string path = res.Item3;
                    Process.Start(path);
                }
            } else if (args[0] == "prev" && args.Count() == 2) {
                var s = SeriesList.SingleOrDefault(x => x.Name == args[1]);
                var res = SearchBySeasonEpisode(s, s.Season, s.Episode, false);
                if (res != null) {
                    s.Season = "s" + res.Item1.ToString("00");
                    s.Episode = "e" + res.Item2.ToString("00");
                    SaveSeriesList();
                    string path = res.Item3;
                    Process.Start(path);
                }
            } else if (args[0] == "set" && args.Count() == 3) {
                var s = SeriesList.SingleOrDefault(x => x.Name == args[1]);
                if (s == null) {
                    s = new Series { Name = args[1] };
                    SeriesList.Add(s);
                }
                s.Season = args[2].Substring(0, 3).ToLower();
                s.Episode = args[2].Substring(3, 3).ToLower();
                SaveSeriesList();
            }
        }

        private void DisplayHelp() {
            var dlg1 = new Form {
                Text = "Email Plugin Help",
                AutoScroll = true,
                Size = new Size(900, 650),
                Font = new Font("Arial", 14, FontStyle.Regular)
            };
            var tl = new Label {
                AutoSize = true,
                Text = "Usage\r\n==================\r\n"
                + _mAlias + " help: display help\r\n"
                + _mAlias + " set <name> <sXXeYY>: set current watched episode for tv show\r\n"
                + _mAlias + " play <name> <sXXeYY>: play specific episode\r\n"
                + _mAlias + " next <name>: play next episode\r\n"
                + _mAlias + " prev <name>: play previous episode\r\n"
                + "=================="
            };
            dlg1.Controls.Add(tl);

            dlg1.ShowDialog();
            return;
        }

    }
}
