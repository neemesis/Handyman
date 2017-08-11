using System.ComponentModel;

namespace Slave.Framework.Interfaces
{
	public interface IMaster
	{
		string Name { get;}
		string Description { get;}
		string Author { get;}
		string Version { get;}

		void Initialize();
		void Execute(string[] args);

		// configurable
		[Editor(@"System.Windows.Forms.Design.ShortcutKeysEditor, System.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(System.Drawing.Design.UITypeEditor))]
		System.Windows.Forms.Shortcut HotKey { get; set;}
		string Alias { get; set;}
	}
}
