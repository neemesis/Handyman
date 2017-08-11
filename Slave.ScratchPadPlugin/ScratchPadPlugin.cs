using System.Windows.Forms;
using Slave.Framework.Interfaces;

namespace Slave.ScratchPadPlugin
{
	public class ScratchPadPlugin : IMaster
	{
		public ScratchPadPlugin()
		{
			_mAlias = "scratchpad";
			_mHotKey = Shortcut.ShiftF2;
			//ScratchPad.Current.Size = new System.Drawing.Size(200, 200);
		}

		#region ITool Members

		string IMaster.Name { get { return "ScratPad plugin"; }}

	    string IMaster.Description { get { return "The ScratchPad is a simple text editor to collect and keep text."; }}

	    string IMaster.Author { get { return "John Roland"; }}

	    string IMaster.Version  { get { return  "1.0"; }}

	    void IMaster.Initialize()
		{
			// todo restore settings
		}

		void IMaster.Execute(string[] args)
		{
			
			ScratchPad.Current.Show();
			ScratchPad.Current.Left = Screen.PrimaryScreen.WorkingArea.Width - ScratchPad.Current.Width;
			ScratchPad.Current.Top = Screen.PrimaryScreen.WorkingArea.Height - ScratchPad.Current.Height;

			ScratchPad.Current.Select();
			ScratchPad.Current.Focus();
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

		#endregion
	}
}
