using System;
using System.Windows.Forms;
using Slave.Framework.Entities;

namespace Slave.Core.Forms
{
	public partial class SlaveForm : Form
	{
		public SlaveForm()
		{
			InitializeComponent();
		}

	    public Commands Slave { get; set; }

	    protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			uxStartupModeComboBox.DataSource = Enum.GetValues(typeof(System.Diagnostics.ProcessWindowStyle));
			
			uxAliasTextBox.DataBindings.Add("Text", Slave, "Alias", false, DataSourceUpdateMode.OnPropertyChanged);
			uxFilenameTextBox.DataBindings.Add("Text", Slave, "FileName", false, DataSourceUpdateMode.OnPropertyChanged);
			uxArgumentsTextBox.DataBindings.Add("Text", Slave, "Arguments", false, DataSourceUpdateMode.OnPropertyChanged);
			uxNotesTextBox.DataBindings.Add("Text", Slave, "Notes", false, DataSourceUpdateMode.OnPropertyChanged);
		}
    }
}