using System;

namespace Slave.Framework
{
	/// <summary>
	/// Exposes some usefull helpers. need to be sorted out.
	/// </summary>
	public static class Utilities
	{
		public static void RunOnStart(string appName, string appPath)
		{
			var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
			key.SetValue(appName, appPath);
			key.Close();
			key = null;
		}

		public static void RemoveRunOnStart(string appName)
		{
			try
			{
				var key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
				key.DeleteSubKey(appName);
				key.Close();
				key = null;
			}
			catch (Exception)
			{
				//throw;
			}

		}
	}
}
