using Slave.Framework.Interfaces;
using System;

namespace Slave.TranslatorPlugin
{
	public class TranslatorPlugin : IMaster
	{
		public TranslatorPlugin()
		{
			_mAlias = "translator";
			_mHotkey = System.Windows.Forms.Shortcut.ShiftF3;
		}

		#region ITool Members

		string IMaster.Name  { get { return  "Translator plugin"; }}

	    string IMaster.Description  { get { return  "This is a Google Translator wrapper"; }}

	    string IMaster.Author  { get { return  "John Roland"; }}

	    string IMaster.Version  { get { return  "1.0"; }}

	    void IMaster.Initialize()
		{
			//
		}

		void IMaster.Execute(string[] args, Action<string> display)
		{
            var form = new MainForm {
                StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen
            };
            form.Select();
			form.Focus();
			form.ShowDialog();
		}

		System.Windows.Forms.Shortcut IMaster.HotKey
		{
			get { return _mHotkey; }
		    set { _mHotkey = value; }
		}

		private string _mAlias;
		private System.Windows.Forms.Shortcut _mHotkey;

		string IMaster.Alias
		{
			get { return _mAlias; }
		    set { _mAlias = value; }
		}

		#endregion
	}
}
