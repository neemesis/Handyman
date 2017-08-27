using System;
using System.Windows.Forms;

namespace Handyman.Core.Controls {
    public partial class HelpViewer : UserControl {
        public HelpViewer() {
            InitializeComponent();
        }

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            webBrowser1.Url = new Uri(Properties.Settings.Default.HelpUrl);
        }
    }
}
