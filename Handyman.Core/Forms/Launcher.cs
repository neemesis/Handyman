using System;
using System.Collections.Generic;
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

        private DisplayData Mode { get; set; }
        private Action<string> PluginCallback { get; set; }
        private List<string> PluginChoices { get; set; }

        [System.Runtime.InteropServices.DllImport("gdi32.dll")]
        private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
            IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private Launcher() {
            InitializeComponent();

            // set Ubuntu font as default font
            var fontData = Properties.Resources.Ubuntu_L;
            var fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
            System.Runtime.InteropServices.Marshal.Copy(fontData, 0, fontPtr, fontData.Length);
            uint dummy = 0;
            fonts.AddMemoryFont(fontPtr, Properties.Resources.Ubuntu_L.Length);
            AddFontMemResourceEx(fontPtr, (uint)Properties.Resources.Ubuntu_L.Length, IntPtr.Zero, ref dummy);
            System.Runtime.InteropServices.Marshal.FreeCoTaskMem(fontPtr);

            MyFont = new Font(fonts.Families[0], 16.0F);

            uxInputText.Font = MyFont;

            // set default mode to DisplayData.Default
            Mode = DisplayData.Default;

            // setup listbox items
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
            Left = ( Screen.PrimaryScreen.WorkingArea.Width - uxInputText.Size.Width ) / 2;
            Top = ( Screen.PrimaryScreen.WorkingArea.Height - uxInputText.Size.Height ) / 2 - 160;
        }

        protected override void OnShown(EventArgs e) {
            base.OnShown(e);
            uxInputText.Focus();

            UpdateAutoCompletion();
        }

        public void FocusOnInput() {
            uxInputText.Focus();
            UpdateAutoCompletion();
        }

        /// <summary>
        /// Update autocomplete appender
        /// </summary>
        public void UpdateAutoCompletion() {
            uxInputText.AutoCompleteMode = AutoCompleteMode.Append;
            uxInputText.AutoCompleteSource = AutoCompleteSource.CustomSource;
            var sr = new AutoCompleteStringCollection();
            sr.AddRange(Context.Current.AutoCompleteSource);

            uxInputText.AutoCompleteCustomSource = sr;
        }


        /// <summary>
        /// Clear listbox
        /// </summary>
        private void ClearList() {
            uxListBox.Items.Clear();
            uxListBox.Height = 0;
            Height = uxInputText.Size.Height + 19;
        }

        /// <summary>
        /// Handle KeyUp on input box using mode's
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnInputTextBoxKeyUp(object sender, KeyEventArgs e) {
            e.SuppressKeyPress = true;
            if (e.KeyCode == Keys.Escape) {
                CleanLauncher();
            } else if (e.KeyCode == Keys.Tab) {
                TabPressed();
            }

            switch (Mode) {
                case DisplayData.Default:
                    DefaultMode(e);
                    break;
                case DisplayData.Question:
                    break;
                case DisplayData.Launcher:
                    break;
                case DisplayData.PopUp:
                    break;
            }

            uxInputText.Focus();
        }

        /// <summary>
        /// Handle default input on launcher
        /// </summary>
        /// <param name="e"></param>
        private void DefaultMode(KeyEventArgs e) {
            Debug.WriteLine("DM");
            if (e.KeyCode == Keys.Enter) {
                ClearList();
                var alias = uxInputText.Text;
                HideForm();
                Context.Current.Start(alias);
            } else if (e.KeyCode == Keys.Down) {
                _updateList = false;
                uxListBox.SelectedIndex = Math.Min(uxListBox.SelectedIndex + 1, uxListBox.Items.Count - 1);
            } else if (e.KeyCode == Keys.Up) {
                _updateList = false;
                if (uxListBox.SelectedIndex - 1 < 0) {
                    ChangeText(_originalAlias);
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

        /// <summary>
        /// Clean launcher text, list and hide form
        /// </summary>
        private void CleanLauncher() {
            if (!string.IsNullOrEmpty(uxInputText.Text)) {
                uxInputText.Text = "";
            } else
                HideForm();
            ClearList();
        }

        /// <summary>
        /// Tab pressed event
        /// </summary>
        private void TabPressed() {
            ChangeText(uxInputText.Text + " ");
            UpdateList(updateLauncherText: true);
        }

        /// <summary>
        /// Update suggestions list.
        /// </summary>
        /// <param name="sug"></param>
        private void UpdateList(List<string> sug = null, bool updateLauncherText = false) {
            if (!string.IsNullOrEmpty(uxInputText.Text)) {
                var suggestions = sug ?? Context.Current.Suggestions.Where(x => x.Contains(uxInputText.Text)).ToList();
                uxListBox.Items.Clear();
                Height = uxInputText.Size.Height + 19;

                if (suggestions.Count < 1)
                    return;

                if (updateLauncherText) {
                    ChangeText(suggestions[0] + " ");
                    UpdateList();
                }

                foreach (var a in suggestions)
                    uxListBox.Items.Add(a);
                uxListBox.Height = Math.Min(suggestions.Count, 8) * ( uxInputText.Size.Height + 10 );
                Height = uxInputText.Size.Height + 19 + uxListBox.Height;
            }
            uxInputText.Focus();
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

        /// <summary>
        /// Change launcher text and set index to end
        /// </summary>
        /// <param name="text"></param>
        private void ChangeText(string text) {
            uxInputText.Text = text;
            uxInputText.SelectionStart = uxInputText.Text.Length;
            uxInputText.SelectionLength = 0;
        } 

        /// <summary>
        /// Calback from plugins to show data on launcher
        /// </summary>
        /// <param name="text"></param>
        /// <param name="dd"></param>
        /// <param name="choices"></param>
        /// <param name="callback"></param>
        public void ShowData(string text, 
            DisplayData dd = DisplayData.Launcher, 
            List<string> choices = null, 
            Action<string> callback = null) {
            
            // TODO: create popup
            if (dd == DisplayData.Launcher) {
                ChangeText(text);
            } else if (dd == DisplayData.PopUp) {

            } else if (dd == DisplayData.Question) {
                ChangeText(text);
                PluginCallback = callback;
                PluginChoices = choices;
                DisplayQuestion();
            }
            Show();
            Focus();
            uxInputText.Focus();
        }

        /// <summary>
        /// Display question on launcher
        /// </summary>
        private void DisplayQuestion() {
            Mode = DisplayData.Question;
            uxInputText.Enabled = false;
            UpdateList(PluginChoices);
        }

        private void OnDeactivate(object sender, EventArgs e) {
            HideForm();
        }

        private void OnInputTextBoxMouseDown(object sender, MouseEventArgs e) {
            CleanLauncher();
        }

        private void reloadToolStripMenuItem_Click(object sender, EventArgs e) {
            Context.Current.Handymans = HandymansManager.Load();
        }

        /// <summary>
        /// Draw suggestions
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
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
                location.X = ( uxListBox.Width - 18 - (int)stringSize.Width ) / 2;
                location.Y += 5;

                g.DrawString(itemText, e.Font, itemTextColorBrush, location);

                // Clean up
                backgroundColorBrush.Dispose();
                itemTextColorBrush.Dispose();
            }

            e.DrawFocusRectangle();
        }

        private void uxListBox_SelectedIndexChanged(object sender, EventArgs e) {
            if (uxListBox.SelectedIndex >= 0 && uxListBox.SelectedIndex < uxListBox.Items.Count && Mode != DisplayData.Question) {
                var itemText = uxListBox.Items[uxListBox.SelectedIndex].ToString();
                ChangeText(itemText + " ");
                uxInputText.Focus();
            }
        }

        private void uxListBox_KeyUp(object sender, KeyEventArgs e) {
            if (Mode == DisplayData.Question) {
                if (e.KeyCode == Keys.Enter) {
                    var selectedText = uxListBox.GetItemText(uxListBox.SelectedItem);
                    ClearList();
                    Mode = DisplayData.Default;
                    uxInputText.Text = "";
                    HideForm();
                    uxInputText.Enabled = true;
                    //PluginCallback?.Invoke(selectedText);
                    if (PluginCallback != null)
                        PluginCallback.Invoke(selectedText);
                    else
                        ClearList();
                } else if (e.KeyCode == Keys.Escape) {
                    uxInputText.Text = "";
                    ClearList();
                }
            }
        }

        private void uxInputText_KeyDown(object sender, KeyEventArgs e) {
            if (e.KeyCode == Keys.Tab) {
                e.SuppressKeyPress = true;
            }
        }
    }
}