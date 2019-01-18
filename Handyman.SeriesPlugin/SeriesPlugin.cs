using Handyman.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Windows.Forms;
using System.Xml.Serialization;
using Handyman.Framework.Entities;

namespace Handyman.SeriesPlugin {
    public class SeriesPlugin : IMaster {
        private List<Series> SeriesList { get; set; }
        private readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Environment.UserName + ".seriesHandyman";

        static readonly string[] MediaExtensions = {
            ".AVI", ".MP4", ".DIVX", ".WMV", ".MKV", ".FLV", ".WEBM", ".MOV", ".AMV", ".MPG", ".M4V", ".3GP"
        };

        public SeriesPlugin() {
            _alias = "tv";
            _hotKey = Shortcut.None;
        }

        public string Name => "Series Plugin";
        public string Description => "Track and play TV Series";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Handyman/blob/master/Handyman.SeriesPlugin/README.MD";
        public IParse Parser { get; set; }
        public List<string> Suggestions { get; set; }
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

            var res = SeriesList.SingleOrDefault(x => x.Name == td.Name);
            if (res == null)
                SeriesList.Add(td);
            else {
                res.Episode = td.Episode;
                res.Season = td.Season;
            }
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
            return MediaExtensions.Contains(Path.GetExtension(filename), StringComparer.OrdinalIgnoreCase);
        }

        private static string Search(string sDir, string name, string season, string episode, Action<string, DisplayData, List<string>, Action<string>> display) {
            try {
                foreach (var f in Directory.GetFiles(sDir)) {
                    display(f, DisplayData.Launcher, null, null);
                    if (IsVideo(f) && CheckFile(f.ToLower(), name, season, episode))
                        return f;
                }
                foreach (var d in Directory.GetDirectories(sDir)) {
                    foreach (var f in Directory.GetFiles(d)) {
                        if (IsVideo(f) && CheckFile(f.ToLower(), name, season, episode))
                            return f;
                    }
                    var s = Search(d, name, season, episode, display);
                    if (s != null)
                        return s;
                }
                return null;
            } catch (System.Exception excpt) {
                Console.WriteLine(excpt.Message);
                return null;
            }
        }

        public void Initialize() {
            LoadSeriesList();
            Suggestions = new List<string> {"tv play", "tv next", "tv prev", "tv set" };
        }

        private static Tuple<int, int, string> SearchBySeasonEpisode(Series s, string ses, string ep, Action<string, DisplayData, List<string>, Action<string>> display, bool forward = true) {
            if (forward) {
                for (var i = int.Parse(ses.Substring(1)); i < 30; ++i) {
                    for (var j = int.Parse(ep.Substring(1)) + 1; j < 30; ++j) {
                        var path = Search(Properties.Settings.Default.Location, s.Name, "s" + i.ToString("00"), "e" + j.ToString("00"), display);
                        if (!string.IsNullOrEmpty(path))
                            return new Tuple<int, int, string>(i, j, path);
                    }
                }
            } else {
                for (var i = int.Parse(ses.Substring(1)); i >= 0; --i) {
                    for (var j = int.Parse(ep.Substring(1)) - 1; j >= 0; --j) {
                        var path = Search(Properties.Settings.Default.Location, s.Name, "s" + i.ToString("00"), "e" + j.ToString("00"), display);
                        if (!string.IsNullOrEmpty(path))
                            return new Tuple<int, int, string>(i, j, path);
                    }
                }
            }
            return null;
        }

        public void Execute(string[] args, Action<string, DisplayData, List<string>, Action<string>> display) {
            if (args.Length < 1 || args.Length > 0 && args[0] == "help") {
                DisplayHelp();
            } else if (args[0] == "play" && args.Length == 3) {
                var path = Search(Properties.Settings.Default.Location, args[1].ToLower(), args[2].Substring(0, 3).ToLower(), args[2].Substring(3, 3).ToLower(), display);
                if (string.IsNullOrEmpty(path))
                    display("No results! " + Properties.Settings.Default.Location, DisplayData.Launcher, null, null);
                else
                    display("Found!", DisplayData.Launcher, null, null);
                if (!string.IsNullOrEmpty(path)) {
                    var s = new Series {
                        Name = args[1].ToLower(),
                        Season = args[2].Substring(0, 3).ToLower(),
                        Episode = args[2].Substring(3, 3).ToLower()
                    };
                    InsertSeries(s);
                    SaveSeriesList();
                    Process.Start(path);
                }
            } else if (args[0] == "next" && args.Length == 2) {
                var s = SeriesList.SingleOrDefault(x => x.Name == args[1]);
                var res = SearchBySeasonEpisode(s, s.Season, s.Episode, display);
                if (res != null) {
                    s.Season = "s" + res.Item1.ToString("00");
                    s.Episode = "e" + res.Item2.ToString("00");
                    SaveSeriesList();
                    var path = res.Item3;
                    Process.Start(path);
                }
            } else if (args[0] == "prev" && args.Length == 2) {
                var s = SeriesList.SingleOrDefault(x => x.Name == args[1]);
                var res = SearchBySeasonEpisode(s, s.Season, s.Episode, display, false);
                if (res != null) {
                    s.Season = "s" + res.Item1.ToString("00");
                    s.Episode = "e" + res.Item2.ToString("00");
                    SaveSeriesList();
                    var path = res.Item3;
                    Process.Start(path);
                }
            } else if (args[0] == "set" && args.Length == 3) {
                var s = SeriesList.SingleOrDefault(x => x.Name == args[1]);
                if (s == null) {
                    s = new Series { Name = args[1] };
                    InsertSeries(s);
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
                + _alias + " help: display help\r\n"
                + _alias + " set <name> <sXXeYY>: set current watched episode for tv show\r\n"
                + _alias + " play <name> <sXXeYY>: play specific episode\r\n"
                + _alias + " next <name>: play next episode\r\n"
                + _alias + " prev <name>: play previous episode\r\n"
                + "=================="
            };
            dlg1.Controls.Add(tl);

            dlg1.ShowDialog();
        }

    }
}
