using System;
using System.Windows.Forms;

namespace Slave.Core.Controls {
    public partial class ListSlaves : UserControl {
        public ListSlaves() {
            InitializeComponent();
            uxDataGridView.AutoGenerateColumns = false;
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);

            uxModesColumn.DataSource = Enum.GetValues(typeof(System.Diagnostics.ProcessWindowStyle));
            bindingSource1.DataSource = Context.Current.Slaves;
        }

        private void OnDataGridViewCellParsing(object sender, DataGridViewCellParsingEventArgs e) {
            if (e.ColumnIndex == uxModesColumn.Index && e.Value is string) {
                e.Value = Enum.Parse(typeof(System.Diagnostics.ProcessWindowStyle), e.Value.ToString(), true);
                e.ParsingApplied = true;
            }
        }


        private void OnDataGridViewCellFormatting(object sender, DataGridViewCellFormattingEventArgs e) {
            if (e.ColumnIndex == uxModesColumn.Index && e.Value is System.Diagnostics.ProcessWindowStyle) {
                e.Value = e.Value.ToString();
                e.FormattingApplied = true;
            }
        }

        private void OnDataGridViewDataError(object sender, DataGridViewDataErrorEventArgs e) {
            Console.WriteLine(e.Exception);
            Console.WriteLine(e.RowIndex);
        }
    }
}
