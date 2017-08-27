using System;
using System.Drawing;
using System.Windows.Forms;

namespace Handyman.ScratchPadPlugin {
    public partial class ScratchPad : Form {
        //private string m_file;

        public ScratchPad() {
            InitializeComponent();

            Size = new Size(300, Screen.PrimaryScreen.WorkingArea.Height);
            FormClosing += ScratchPad_FormClosing;

            richTextBox1.Text = Properties.Settings.Default.ScratchPadText;
        }

        void ScratchPad_FormClosing(object sender, FormClosingEventArgs e) {
            if (e.CloseReason == CloseReason.UserClosing) {
                e.Cancel = true;
                Hide();
            } else {
                Properties.Settings.Default.Save();
            }
        }

        #region Singleton

        private static volatile ScratchPad _singleton;
        private static readonly object SyncRoot = new Object();

        public static ScratchPad Current {
            get {
                if (_singleton == null) {
                    lock (SyncRoot) {
                        if (_singleton == null) {
                            _singleton = new ScratchPad();
                        }
                    }
                }

                return _singleton;
            }
        }
        #endregion

    }
}