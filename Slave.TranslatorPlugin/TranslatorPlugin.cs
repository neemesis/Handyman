using Slave.Framework.Interfaces;

namespace Slave.TranslatorPlugin
{
	public class TranslatorPlugin : ITool
	{
		public TranslatorPlugin()
		{
			_mAlias = "translator";
			_mHotkey = System.Windows.Forms.Shortcut.ShiftF3;
		}

		#region ITool Members

		string ITool.Name => "Translator plugin";

	    string ITool.Description => "This is a Google Translator wrapper";

	    string ITool.Author => "John Roland";

	    string ITool.Version => "1.0";

	    void ITool.Initialize()
		{
			//
		}

		void ITool.Execute(string[] args)
		{
            var form = new MainForm {
                StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            };
            form.Select();
			form.Focus();
			form.ShowDialog();
		}

		System.Windows.Forms.Shortcut ITool.HotKey
		{
			get => _mHotkey;
		    set => _mHotkey = value;
		}

		private string _mAlias;
		private System.Windows.Forms.Shortcut _mHotkey;

		string ITool.Alias
		{
			get => _mAlias;
		    set => _mAlias = value;
		}

		#endregion
	}
}
