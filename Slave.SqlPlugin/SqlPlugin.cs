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

namespace Slave.SqlPlugin
{
    public class SqlPlugin : IMaster
    {
        public SqlPlugin() {
            _mAlias = "sql";
            _mHotKey = Shortcut.ShiftF5;
        }

        private List<ConnectionString> Connections;
        private string _path = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\" + Environment.UserName + ".sqlslave";

        public string Name
        {
            get { return "SQL Plugin"; }
        }

        public string Description
        {
            get { return "Execute queries directly from launcher"; }
        }

        public string Author
        {
            get { return "Mirche Toshevski"; }
        }

        public string Version
        {
            get { return "1.0.0.0"; }
        }

        public void Initialize()
        {
            LoadConnections();
        }

        private void LoadConnections()
        {
            if (File.Exists(_path))
            {
                var serializer = new XmlSerializer(typeof(List<ConnectionString>));
                var reader = File.OpenText(_path);
                Connections = (List<ConnectionString>)serializer.Deserialize(reader);
                reader.Close();
            }
            else
            {
                Connections = new List<ConnectionString>();
                SaveConnections();
            }
        }

        private void InsertConnection(ConnectionString cs)
        {
            if (Connections == null)
                LoadConnections();

            Connections.Add(cs);
            SaveConnections();
        }

        private void DeleteConnection(string name)
        {
            if (Connections == null)
                return;

            Connections.Remove(Connections.First(x => x.Name == name));
            SaveConnections();
        }


        private void SaveConnections()
        {
            var ser = new XmlSerializer(typeof(List<ConnectionString>));
            var sw = new StreamWriter(_path);
            ser.Serialize(sw, Connections);
            sw.Close();
        }

        public void Execute(string[] args, Action<string> display)
        {
            if (args.Count() < 1 || args.Count() > 0 && args[0] == "help")
            {
                DisplayHelp();
            }
            else if (args.Count() > 2 && args[0] == "set")
            {
                var cs = new ConnectionString
                {
                    Name = args[1],
                    Connection = string.Join(" ", args.Skip(2))
                };
                InsertConnection(cs);
            }
            else if (args.Count() == 2 && args[0] == "delete")
            {
                DeleteConnection(args[1]);
            }
            else if (args.Count() > 2 && args[0] == "update")
            {
                var cs = Connections.First(x => x.Name == args[1]);
                cs.Connection = string.Join(" ", args.Skip(2));
                SaveConnections();
            }
            else if (args.Count() > 2 && args[0] == "raw") {
                var conn = Connections.Single(x => x.Name == args[1]).Connection;
                RawQuery(conn, string.Join(" ", args.Skip(2)));
            }
            else if (args.Count() >= 2)
            {
                var conn = Connections.Single(x => x.Name == args[0]).Connection;
                var sb = new StringBuilder();
                sb.Append("select ");
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

                var final = sb.ToString();
                RawQuery(conn, final);
            }
        }

        private void RawQuery(string connection, string query) {
            using (SqlConnection conn = new SqlConnection(connection))
            using (SqlCommand cmd = new SqlCommand(query, conn)) {
                conn.Open();
                var result = cmd.ExecuteReader();

                var form = new Form { Text = "Result for: " + query, MinimumSize = new Size(800, 300) };
                var fv = new DataGridView { Size = form.Size, ScrollBars = ScrollBars.Both, Dock = DockStyle.Fill };
                DataTable dt = new DataTable();
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
            var tl = new Label
            {
                AutoSize = true,
                Text = "Usage\r\n==================\r\n"
                + _mAlias + " help: display help\r\n"
                + _mAlias + " set <name> <connectionString>: add new connection string\r\n"
                + _mAlias + " delete <name>: delete connection string\r\n"
                + _mAlias + " update <name> <newConnectionString>: update connection string\r\n"
                + _mAlias + " <connectionName> s:Col1,Col2,Col3 t:Table w:A<5,B>10 : execute query to \r\n\tselect Col1, Col2, Col3 from table Table with A smaller then 5 and B bigger then 10\r\n"
                //+ _mAlias + " script1:arg1:arg2 script2:arg3:arg4\r\n"
                + "=================="
            };
            dlg1.Controls.Add(tl);

            dlg1.ShowDialog();
            return;
        }

        private Shortcut _mHotKey;
        private string _mAlias;


        Shortcut IMaster.HotKey
        {
            get { return _mHotKey; }
            set { _mHotKey = value; }
        }

        string IMaster.Alias
        {
            get { return _mAlias; }
            set { _mAlias = value; }
        }
    }
}
