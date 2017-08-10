using System;
using System.Windows.Forms;

namespace Slave.Core
{
	class Program
	{
		/// <summary>
		/// The main entry point for the application.
		/// </summary>
		[STAThread]
		static void Main()
		{
			var applicationContext = new SlaveContext();
			Application.Run(applicationContext);
		}
	}
}
