using System;
using System.Windows.Forms;

namespace Handyman.Core {
    class Program {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main() {
            var applicationContext = new HandymanContext();
            Application.EnableVisualStyles();
            Application.Run(applicationContext);
        }
    }
}
