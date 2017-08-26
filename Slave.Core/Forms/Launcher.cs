using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Slave.Core.Helpers;
using Slave.Framework.Entities;

namespace Slave.Core.Forms {
    public partial class Launcher : Form {
        private Font MyFont = null;
        private PrivateFontCollection fonts = new PrivateFontCollection();

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private Launcher() {
            InitializeComponent();

            var fontData = Properties.Resources.Ubuntu_L;
            var fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Ubuntu_L.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Ubuntu_L.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            MyFont = new Font(fonts.Families[0], 16.0F);

            uxInputText.Font = MyFont;
        }

        #region Singleton

        private static volatile Launcher _singleton;
        private static readonly object _syncRoot = new Object();

        public static Launcher Current {
            get {
                if (_singleton == null) {
                    lock (_syncRoot) {
                        if (_singleton == null) {
                            _singleton = new Launcher();
                        }
                    }
                }

                return _singleton;
            }
        }
        #endregion

        protected override void OnLoad(EventArgs e) {
            base.OnLoad(e);
            Size = new Size(uxInputText.Size.Width + 20, uxInputText.Size.Height + 20);
            BackColor = uxInputText.BackColor;

            UpdateAutoCompletion();

            // position on bottom right
            StartPosition = FormStartPosition.Manual;
            //Left = Screen.PrimaryScreen.WorkingArea.Width - Width;
            //Top = Screen.PrimaryScreen.WorkingArea.Height - Height;
            Left = ( Screen.PrimaryScreen.WorkingArea.Width - uxInputText.Size.Width ) / 2;
            Top = ( Screen.PrimaryScreen.WorkingArea.Height - uxInputText.Size.Height ) / 2;
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);

            UpdateAutoCompletion();
        }

        public void UpdateAutoCompletion() {
            uxInputText.AutoCompleteMode = AutoCompleteMode.Append;
            uxInputText.AutoCompleteSource = AutoCompleteSource.CustomSource;
            var sr = new AutoCompleteStringCollection();
            sr.AddRange(Context.Current.AutoCompleteSource);

            uxInputText.AutoCompleteCustomSource = sr;
        }


        private void OnInputTextBoxKeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                var alias = uxInputText.Text;
                HideForm();
                Context.Current.Start(alias);
            } else if (e.KeyCode == Keys.Escape) {
                HideForm();
            } else if (e.KeyCode == Keys.Tab) {
                uxInputText.Text = uxInputText.Text + " ";
                uxInputText.SelectionStart = Math.Max(0, uxInputText.Text.Length);
                uxInputText.SelectionLength = 0;
            }
        }

        /// <summary>
        /// Hides the form.
        /// </summary>
        private void HideForm() {
            Hide();

            Properties.Settings.Default.Position = Location;
            Properties.Settings.Default.Size = Size;
            Properties.Settings.Default.Save();
        }

        private void OnExitToolStripMenuItemClick(object sender, EventArgs e) {
            AppForms.Exit();
        }

        private void OnHideToolStripMenuItemClick(object sender, EventArgs e) {
            if (Visible) {
                HideForm();
            } else {
                Show();
            }

        }

        private void OnSetupToolStripMenuItemClick(object sender, EventArgs e) {
            AppForms.Setup();
        }

        private void OnHelpToolStripMenuItemClick(object sender, EventArgs e) {
            AppForms.Help();
        }

        private void OnNewSlaveToolStripMenuItemClick(object sender, EventArgs e) {
            AppForms.ShowNewSlaveForm();
        }

        private void uxInputContextMenuStrip_Opening(object sender, CancelEventArgs e) {
            // update the show/hide input bar
            uxHideToolStripMenuItem.Text = Visible ? "Hide command line" : "Show command line";

            // adding Slaves
            uxSlavesToolStripMenuItem.DropDownItems.Clear();
            Context.Current.Slaves.Sort((w1, w2) => string.Compare(w1.Alias, w2.Alias, StringComparison.Ordinal));
            foreach (var word in Context.Current.Slaves) {
                var item = new ToolStripMenuItem(word.Alias);
                item.Click += delegate {
                    Context.Current.Start(item.Text);
                };
                uxSlavesToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        public void ShowData(string text, DisplayData dd = DisplayData.Launcher) {
            if (dd == DisplayData.Launcher) {
                uxInputText.Text = text;
                uxInputText.SelectionStart = uxInputText.Text.Length;
                uxInputText.SelectionLength = 0;
                Show();
            } else if (dd == DisplayData.PopUp) {
                
            }
        }

        private void OnDeactivate(object sender, EventArgs e) {
            HideForm();
        }

        private void OnInputTextBoxMouseDown(object sender, MouseEventArgs e) {
            //if (uxInputText.Text == "enter command") {
            //    uxInputText.Text = "";
            //}
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e) {
            Context.Current.Slaves = SlavesManager.Load();
        }
    }
}