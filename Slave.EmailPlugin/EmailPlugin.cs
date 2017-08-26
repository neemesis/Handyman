using Slave.EmailPlugin.Properties;
using Slave.Framework.Interfaces;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using Slave.Framework.Entities;

namespace Slave.EmailPlugin {
    public class EmailPlugin : IMaster {
        public EmailPlugin() {
            _alias = "email";
            _hotKey = Shortcut.None;
        }

        public string Name => "Email Plugin";
        public string Description => "Send email fast and easy";
        public string Author => "Mirche Toshevski";
        public string Version => "1.0.0.0";
        public string HelpUrl => "https://github.com/neemesis/Slave/blob/master/Slave.EmailPlugin/README.MD";
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

        public void Execute(string[] args, Action<string, DisplayData> display) {
            if (args.Length < 2 || args[0] == "help") {
                DisplayHelp();
                return;
            }

            if (args[0] == "set" && args.Length == 3) {
                if (args[1] == "email") {
                    Settings.Default.MyEmail = args[2];
                } else if (args[1] == "pass") {
                    Settings.Default.MyPassword = args[2];
                } else if (args[1] == "port") {
                    Settings.Default.Port = int.Parse(args[2]);
                } else if (args[1] == "host") {
                    Settings.Default.Host = args[2];
                }
                return;
            }

            if (string.IsNullOrEmpty(Settings.Default.MyEmail) || string.IsNullOrEmpty(Settings.Default.MyPassword)) {
                DisplayHelp();
                return;
            }


            var client = new SmtpClient();
            client.Port = Settings.Default.Port;
            client.Host = Settings.Default.Host;
            client.EnableSsl = true;
            client.Timeout = 10000;
            client.DeliveryMethod = SmtpDeliveryMethod.Network;
            client.UseDefaultCredentials = false;
            client.Credentials = new System.Net.NetworkCredential(Settings.Default.MyEmail, Settings.Default.MyPassword);

            var mm = new MailMessage();
            mm.From = new MailAddress(Settings.Default.MyEmail);
            mm.Subject = args.SingleOrDefault(x => x.StartsWith("s:") || x.StartsWith("subject:")).Split(':')[1];
            mm.Body = args.SingleOrDefault(x => x.StartsWith("b:") || x.StartsWith("body:")).Split(':')[1];
            foreach (var to in args.Where(x => x.StartsWith("t:") || x.StartsWith("to:")))
                mm.To.Add(new MailAddress(to.Split(':')[1]));
            mm.BodyEncoding = Encoding.UTF8;
            mm.DeliveryNotificationOptions = DeliveryNotificationOptions.OnFailure;

            client.Send(mm);
        }

        public void Initialize() {
            
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
                Text = "\r\nYou need to set Email and Password first!\r\nUsage\r\n==================\r\n"
                + _alias + " help: display help\r\n"
                + _alias + " set email | pass | port | host <value>: set either email or password to some value\r\n"
                + _alias + " s:<subject|optional> b:<body> t:<to>: you can have as many t:<to> as you want\r\n"
                + "=================="
                + "example: " + _alias + " set email someNewEmail@someNewProvider.com\r\n"
                + "example: " + _alias + " set pass newPassValue\r\n"
                + "example: " + _alias + " s:Hello from slaves b:This is body t:email1@am.amam t:email2@am.amam\r\n"
            };

            dlg1.Controls.Add(tl);

            dlg1.ShowDialog();

            return;
        }
    }
}
