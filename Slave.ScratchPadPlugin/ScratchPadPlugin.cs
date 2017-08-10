using System.Windows.Forms;
using Slave.Framework.Interfaces;

namespace Slave.ScratchPadPlugin
{
	public class ScratchPadPlugin : ITool
	{
		public ScratchPadPlugin()
		{
			_mAlias = "scratchpad";
			_mHotKey = Shortcut.ShiftF2;
			//ScratchPad.Current.Size = new System.Drawing.Size(200, 200);
		}

		#region ITool Members

		string ITool.Name => "ScratPad plugin";

	    string ITool.Description => "The ScratchPad is a simple text editor to collect and keep text.";

	    string ITool.Author => "John Roland";

	    string ITool.Version => "1.0";

	    void ITool.Initialize()
		{
			// todo restore settings
		}

		void ITool.Execute(string[] args)
		{
			
			ScratchPad.Current.Show();
			ScratchPad.Current.Left = Screen.PrimaryScreen.WorkingArea.Width - ScratchPad.Current.Width;
			ScratchPad.Current.Top = Screen.PrimaryScreen.WorkingArea.Height - ScratchPad.Current.Height;

			ScratchPad.Current.Select();
			ScratchPad.Current.Focus();
		}
		
		private Shortcut _mHotKey;
		private string _mAlias;

 
		Shortcut ITool.HotKey
		{
			get => _mHotKey;
		    set => _mHotKey = value;
		}

		string ITool.Alias
		{
			get => _mAlias;
		    set => _mAlias = value;
		}

		#endregion
	}
}
