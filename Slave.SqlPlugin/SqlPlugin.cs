using Slave.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Serialization;
using Slave.Framework.Entities;

namespace Slave.SqlPlugin {
    public class SqlPlugin : IMaster {
        public SqlPlugin() {
            _alias = "sql";
            _hotKey = Shortcut.ShiftF5;
        }

        private List<ConnectionString> _connections;
        private readonly string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Environment.UserName + ".sqlslave";

        public string Name => "SQL Plugin";
        public string Description => "Execute queries directly from launcher";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.SqlPlugin/README.MD";
        public IParse Parser { get; set; }
        public void Initialize() {
            LoadConnections();
        }

        private void LoadConnections() {
            if (File.Exists(_path)) {
                var serializer = new XmlSerializer(typeof(List<ConnectionString>));
                var reader = File.OpenText(_path);
                _connections = (List<ConnectionString>)serializer.Deserialize(reader);
                reader.Close();
            } else {
                _connections = new List<ConnectionString>();
                SaveConnections();
            }
        }

        private void InsertConnection(ConnectionString cs) {
            if (_connections == null)
                LoadConnections();

            _connections.Add(cs);
            SaveConnections();
        }

        private void DeleteConnection(string name) {
            if (_connections == null)
                return;

            _connections.Remove(_connections.First(x => x.Name == name));
            SaveConnections();
        }


        private void SaveConnections() {
            var ser = new XmlSerializer(typeof(List<ConnectionString>));
            var sw = new StreamWriter(_path);
            ser.Serialize(sw, _connections);
            sw.Close();
        }

        public void Execute(string[] args, Action<string, DisplayData> display) {
            if (args.Length < 1 || args.Any() && args[0] == "help") {
                DisplayHelp();
            } else if (args.Length > 2 && args[0] == "set") {
                var cs = new ConnectionString {
                    Name = args[1],
                    Connection = string.Join(" ", args.Skip(2))
                };
                InsertConnection(cs);
            } else if (args.Length == 2 && args[0] == "remove") {
                DeleteConnection(args[1]);
            } else if (args.Length > 2 && args[0] == "change") {
                var cs = _connections.First(x => x.Name == args[1]);
                cs.Connection = string.Join(" ", args.Skip(2));
                SaveConnections();
            } else if (args.Length > 2 && args[0] == "raw") {
                var conn = _connections.Single(x => x.Name == args[1]).Connection;
                RawQuery(conn, string.Join(" ", args.Skip(2)));
            } else if (args.Length > 2 && args[0] == "insert") {
                Insert(args);
            } else if (args.Length >= 2) {
                Select(args);
            }
        }

        private void Insert(IReadOnlyList<string> args) {
            var conn = _connections.Single(x => x.Name == args[1]).Connection;
            var sb = new StringBuilder();
            var table = args.Single(x => x.StartsWith("t:") || x.StartsWith("table:"));
            var values = args.Single(x => x.StartsWith("v:") || x.StartsWith("values:")).Split(':')[1];
            sb.Append("insert into " + table);
            sb.Append(" values (" + values[0]);
            foreach (var v in values.Skip(1))
                sb.Append(", " + v);
            RawQuery(conn, sb.ToString());
        }

        private void Select(IReadOnlyList<string> args) {
            var conn = _connections.Single(x => x.Name == args[0]).Connection;
            var sb = new StringBuilder();
            sb.Append("select ");
            var top = args.SingleOrDefault(x => x.StartsWith("top:"));
            if (!string.IsNullOrEmpty(top))
                sb.Append("top " + top.Split(':')[1] + " ");
            var select = args.SingleOrDefault(x => x.StartsWith("s:") || x.StartsWith("select:"));
            sb.Append(string.IsNullOrEmpty(select) ? "* " : string.Join(", ", select.Split(',').Where(x => !string.IsNullOrEmpty(x)).Skip(1)));
            var table = args.Single(x => x.StartsWith("t:") || x.StartsWith("table:"));
            sb.Append(" from ");
            sb.Append(table.Split(':')[1]);

            var where = args.SingleOrDefault(x => x.StartsWith("w:") || x.StartsWith("where:"));
            if (!string.IsNullOrEmpty(where)) {
                var whParts = where.Split(',').Where(x => !string.IsNullOrEmpty(x)).Skip(1);
                sb.Append(" where " + whParts.First());
                foreach (var p in whParts.Skip(1))
                    sb.Append(" and " + p);
            }

            var order = args.SingleOrDefault(x => x.StartsWith("o:") || x.StartsWith("order:"));
            if (!string.IsNullOrEmpty(order)) {
                sb.Append(" order by " + order.Split(':')[1].Split(',')[0]);
                if (order.Split(':')[1].Split(',').Length == 2)
                    sb.Append(" " + order.Split(':')[1].Split(',')[1]);
            }

            var final = sb.ToString();
            RawQuery(conn, final);
        }

        private static void RawQuery(string connection, string query) {
            using (var conn = new SqlConnection(connection))
            using (var cmd = new SqlCommand(query, conn)) {
                conn.Open();
                var result = cmd.ExecuteReader();

                var form = new Form { Text = "Result for: " + query, MinimumSize = new Size(800, 300) };
                var fv = new DataGridView { Size = form.Size, ScrollBars = ScrollBars.Both, Dock = DockStyle.Fill };
                var dt = new DataTable();
                dt.Load(result);
                fv.DataSource = dt;
                form.Controls.Add(fv);

                form.SizeChanged += (o, e) => {
                    fv.Size = form.Size;
                };

                form.ShowDialog();
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
                + _alias + " set <name> <connectionString>: add new connection string\r\n"
                + _alias + " remove <name>: delete connection string\r\n"
                + _alias + " change <name> <newConnectionString>: update connection string\r\n"
                + _alias + " <connectionName> s:Col1,Col2,Col3 t:Table w:A<5,B>10 : execute query to \r\n\tselect Col1, Col2, Col3 from table Table with A smaller then 5 and B bigger then 10\r\n"
                + "=================="
            };
            dlg1.Controls.Add(tl);

            dlg1.ShowDialog();
        }

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
    }
}
