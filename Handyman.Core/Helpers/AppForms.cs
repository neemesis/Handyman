using System.Collections.Generic;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using Handyman.Core.Forms;
using Handyman.Framework.Entities;

namespace Handyman.Core.Helpers {
    public static class AppForms {

        public static List<string> GetAppForms() {
            return new List<string> {"add", "exit", "help", "setup"};
        }

        public static bool HandleForm(string alias) {
            switch (alias) {
                case "exit":
                    Exit();
                    return true;

                case "setup":
                    Setup();
                    return true;

                case "help":
                    Help();
                    return true;

                case "add":
                    ShowNewHandymanForm();
                    return true;
            }
            return false;
        }

        public static void Exit() {
            Application.Exit();
        }

        public static void Setup() {
            var form = new OptionsForm();

            switch (form.ShowDialog()) {
                case DialogResult.OK:
                    HandymansManager.Save(Context.Current.Handymans);
                    Properties.Settings.Default.Save();
                    break;

                case DialogResult.Cancel:
                    Properties.Settings.Default.Reload();
                    break;
            }
        }

        public static void Help() {
            var dlg1 = new Form();
            dlg1.Text = "Help";
            dlg1.AutoScroll = true;
            dlg1.Size = new Size(500, 650);
            var txt = new Label { AutoSize = true };
            var sb = new StringBuilder();

            foreach (var s in Context.Current.Tools) {
                sb.AppendLine("Command: " + s.Alias);
                sb.AppendLine("Description: " + s.Description);
                sb.AppendLine("Author: " + s.Author);
                sb.AppendLine("for help type: " + s.Alias + " help");
                sb.AppendLine("========================");
            }

            foreach (var s in Context.Current.Handymans) {
                sb.AppendLine("Command: " + s.Alias);
                sb.AppendLine("Description: " + s.Notes);
                sb.AppendLine("Filename: " + s.FileName);
                sb.AppendLine("========================");
            }

            txt.Text = sb.ToString();
            dlg1.Controls.Add(txt);
            dlg1.ShowDialog();
        }

        public static void ShowNewHandymanForm() {
            ShowHandymanForm(new Commands());
        }

        public static void ShowHandymanForm(Commands word) {
            var form = new HandymanForm {
                Handyman = word
            };

            if (form.ShowDialog() == DialogResult.OK && Context.Current.Handymans.Contains(word) == false) {
                Context.Current.Handymans.Add(word);
                HandymansManager.Save(Context.Current.Handymans);
            }
        }
    }
}
