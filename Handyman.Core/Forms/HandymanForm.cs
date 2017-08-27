using System;
using System.Windows.Forms;
using Handyman.Framework.Entities;

namespace Handyman.Core.Forms
{
	public partial class HandymanForm : Form
	{
		public HandymanForm()
		{
			InitializeComponent();
		}

	    public Commands Handyman { get; set; }

	    protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);

			uxStartupModeComboBox.DataSource = Enum.GetValues(typeof(System.Diagnostics.ProcessWindowStyle));
			
			uxAliasTextBox.DataBindings.Add("Text", Handyman, "Alias", false, DataSourceUpdateMode.OnPropertyChanged);
			uxFilenameTextBox.DataBindings.Add("Text", Handyman, "FileName", false, DataSourceUpdateMode.OnPropertyChanged);
			uxArgumentsTextBox.DataBindings.Add("Text", Handyman, "Arguments", false, DataSourceUpdateMode.OnPropertyChanged);
			uxNotesTextBox.DataBindings.Add("Text", Handyman, "Notes", false, DataSourceUpdateMode.OnPropertyChanged);
		}
    }
}