using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Windows.Forms;
using Handyman.Core.Helpers;
using Handyman.Framework.Entities;

namespace Handyman.Core.Forms {
    public partial class Launcher : Form {
        private Font MyFont = null;
        private PrivateFontCollection fonts = new PrivateFontCollection();
        private bool _updateList = true;
        private string _originalAlias { get; set; }

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

            uxListBox.DrawMode = DrawMode.OwnerDrawFixed;
            uxListBox.BackColor = uxInputText.BackColor;
            uxListBox.Font = MyFont;
            uxListBox.ItemHeight = uxInputText.Size.Height + 10;
            uxListBox.Height = uxListBox.Items.Count * uxListBox.ItemHeight;
            uxListBox.ScrollAlwaysVisible = false;
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
            uxListBox.Width = Width + 18;
            BackColor = uxInputText.BackColor;

            UpdateAutoCompletion();

            // position on bottom right
            StartPosition = FormStartPosition.Manual;
            //Left = Screen.PrimaryScreen.WorkingArea.Width - Width;
            //Top = Screen.PrimaryScreen.WorkingArea.Height - Height;
            Left = (Screen.PrimaryScreen.WorkingArea.Width - uxInputText.Size.Width) / 2;
            Top = (Screen.PrimaryScreen.WorkingArea.Height - uxInputText.Size.Height) / 2 - 160;
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

        private void ClearList() {
            uxListBox.Items.Clear();
            uxListBox.Height = 0;
            Height = uxInputText.Size.Height + 19;
        }


        private void OnInputTextBoxKeyUp(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Enter) {
                ClearList();
                var alias = uxInputText.Text;
                HideForm();
                Context.Current.Start(alias);
            } else if (e.KeyCode == Keys.Escape) {
                if (!string.IsNullOrEmpty(uxInputText.Text)) {
                    uxInputText.Text = "";
                } else
                    HideForm();
                ClearList();
            } else if (e.KeyCode == Keys.Tab) {
                uxInputText.Text = uxInputText.Text + " ";
                uxInputText.SelectionStart = Math.Max(0, uxInputText.Text.Length);
                uxInputText.SelectionLength = 0;
                UpdateList();
            } else if (e.KeyCode == Keys.Down) {
                _updateList = false;
                uxListBox.SelectedIndex = Math.Min(uxListBox.SelectedIndex + 1, uxListBox.Items.Count - 1);
            } else if (e.KeyCode == Keys.Up) {
                _updateList = false;
                if (uxListBox.SelectedIndex - 1 < 0) {
                    uxInputText.Text = _originalAlias;
                    uxInputText.SelectionStart = Math.Max(0, uxInputText.Text.Length);
                    uxInputText.SelectionLength = 0;
                    uxListBox.SelectedItem = null;
                    return;
                }
                uxListBox.SelectedIndex = Math.Max(uxListBox.SelectedIndex - 1, 0);
            } else {
                _originalAlias = uxInputText.Text;
                if (_updateList) {
                    UpdateList();
                } else {
                    _updateList = true;
                }
                if (string.IsNullOrEmpty(uxInputText.Text))
                    ClearList();
            }
        }

        private void UpdateList() {
            if (!string.IsNullOrEmpty(uxInputText.Text)) {
                var suggestions = Context.Current.Suggestions.Where(x => x.Contains(uxInputText.Text)).ToList();
                uxListBox.Items.Clear();
                foreach (var a in suggestions)
                    uxListBox.Items.Add(a);
                uxListBox.Height = Math.Min(suggestions.Count, 8) * ( uxInputText.Size.Height + 10 );
                Height = uxInputText.Size.Height + 19 + uxListBox.Height;
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

        private void OnNewHandymanToolStripMenuItemClick(object sender, EventArgs e) {
            AppForms.ShowNewHandymanForm();
        }

        private void uxInputContextMenuStrip_Opening(object sender, CancelEventArgs e) {
            // update the show/hide input bar
            uxHideToolStripMenuItem.Text = Visible ? "Hide command line" : "Show command line";

            // adding Handymans
            uxHandymansToolStripMenuItem.DropDownItems.Clear();
            Context.Current.Handymans.Sort((w1, w2) => string.Compare(w1.Alias, w2.Alias, StringComparison.Ordinal));
            foreach (var word in Context.Current.Handymans) {
                var item = new ToolStripMenuItem(word.Alias);
                item.Click += delegate {
                    Context.Current.Start(item.Text);
                };
                uxHandymansToolStripMenuItem.DropDownItems.Add(item);
            }
        }

        public void ShowData(string text, DisplayData dd = DisplayData.Launcher) {
            // TODO: create popup
            if (dd == DisplayData.Launcher) {
                
            } else if (dd == DisplayData.PopUp) {
                
            }
            uxInputText.Text = text;
            uxInputText.SelectionStart = uxInputText.Text.Length;
            uxInputText.SelectionLength = 0;
            Show();
        }

        private void OnDeactivate(object sender, EventArgs e) {
            HideForm();
        }

        private void OnInputTextBoxMouseDown(object sender, MouseEventArgs e) {
            if (uxInputText.Text == "error :(") {
                uxInputText.Text = "";
            }
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e) {
            Context.Current.Handymans = HandymansManager.Load();
        }

        private void uxListBox_DrawItem(object sender, DrawItemEventArgs e) {
            e.DrawBackground();

            var isItemSelected = ( e.State & DrawItemState.Selected ) == DrawItemState.Selected;
            var itemIndex = e.Index;
            if (itemIndex >= 0 && itemIndex < uxListBox.Items.Count) {
                var g = e.Graphics;

                // Background Color

                var backgroundColorBrush = new SolidBrush(isItemSelected
                    ? Properties.Settings.Default.ListSelectedBackColor
                    : Properties.Settings.Default.ListUnselectedBackColor);
                g.FillRectangle(backgroundColorBrush, e.Bounds);

                // Set text color
                var itemText = uxListBox.Items[itemIndex].ToString();
                var itemTextColorBrush = isItemSelected 
                    ? new SolidBrush(Properties.Settings.Default.ListSelectedForeColor) 
                    : new SolidBrush(Properties.Settings.Default.ListUnselectedForeColor);
                var location = uxListBox.GetItemRectangle(itemIndex).Location;
                var stringSize = e.Graphics.MeasureString(itemText, e.Font);
                location.X = (uxListBox.Width - 18 - (int)stringSize.Width) / 2;
                location.Y += 5;
                
                g.DrawString(itemText, e.Font, itemTextColorBrush, location);

                // Clean up
                backgroundColorBrush.Dispose();
                itemTextColorBrush.Dispose();
            }

            e.DrawFocusRectangle();
        }

        private void uxListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (uxListBox.SelectedIndex >= 0 && uxListBox.SelectedIndex < uxListBox.Items.Count) {
                var itemText = uxListBox.Items[uxListBox.SelectedIndex].ToString();
                uxInputText.Text = itemText + " ";
                uxInputText.SelectionStart = Math.Max(0, uxInputText.Text.Length);
                uxInputText.SelectionLength = 0;
                uxInputText.Focus();
            }
        }
    }
}