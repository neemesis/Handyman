using System;
using System.Windows.Forms;

namespace Slave.Core.Forms
{
	public partial class DynamicInput : Form
	{
		public DynamicInput()
		{
			InitializeComponent();
		}

		public string Input { get { return comboBox1.Text; } set { comboBox1.Text = value; }}

        public string EncodedInput { get { return System.Web.HttpUtility.UrlEncode(comboBox1.Text); } }

        private void KeyUpSubmit(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter)
                DialogResult = DialogResult.OK;
        }
    }
}