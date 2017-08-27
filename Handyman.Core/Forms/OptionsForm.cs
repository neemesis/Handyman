using System;
using System.Windows.Forms;
using Handyman.Framework;

namespace Handyman.Core.Forms {
    public partial class OptionsForm : Form {
        public OptionsForm() {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            dataGridView1.DataSource = Context.Current.Tools;
            propertyGrid1.SelectedObject = Properties.Settings.Default;
        }

        private void uxCancelButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.Cancel;
        }

        private void uxAcceptButton_Click(object sender, EventArgs e) {
            DialogResult = DialogResult.OK;
        }

        private void OnWebsiteLinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            System.Diagnostics.Process.Start("https://github.com/neemesis/Handyman");
        }

        private void OnImportLinkLabelLinkClicked(object sender, LinkLabelLinkClickedEventArgs e) {
            var dialog = new OpenFileDialog {
                Title = "Import a SlickRun qrs file...",
                Filter = "SlickRun files (*.qrs)|*.qrs",
                CheckFileExists = true,
                CheckPathExists = true,
                Multiselect = false
            };

            if (dialog.ShowDialog() == DialogResult.OK) {
                try {
                    Context.Current.Handymans.AddRange(HandymansHelper.ImportFile(dialog.FileName));
                    MessageBox.Show(dialog.FileName + " imported successfully.");
                } catch (Exception exception) {
                    MessageBox.Show("An error occured: " + exception.Message);
                }

            }
        }
    }
}