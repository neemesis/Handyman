using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Text;
using System.Windows.Forms;
using Slave.Framework.Entities;

namespace Slave.Core.Forms
{
	public partial class Launcher : Form {
	    private Font MyFont = null;
	    private PrivateFontCollection fonts = new PrivateFontCollection();

	    [System.Runtime.InteropServices.DllImport("gdi32.dll")]
	    private static extern IntPtr AddFontMemResourceEx(IntPtr pbFont, uint cbFont,
	        IntPtr pdv, [System.Runtime.InteropServices.In] ref uint pcFonts);

        private Launcher()
		{
			InitializeComponent();

		    byte[] fontData = Properties.Resources.Ubuntu_L;
		    IntPtr fontPtr = System.Runtime.InteropServices.Marshal.AllocCoTaskMem(fontData.Length);
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

		public static Launcher Current
		{
			get
			{
				if (_singleton == null)
				{
					lock (_syncRoot)
					{
						if (_singleton == null)
						{
							_singleton = new Launcher();
						}
					}
				}

				return _singleton;
			}
		}
		#endregion
		
		protected override void OnLoad(EventArgs e)
		{
			base.OnLoad(e);
			Size = new Size(uxInputText.Size.Width + 20, uxInputText.Size.Height + 20);
		    BackColor = uxInputText.BackColor;

			UpdateAutoCompletion();

			// position on bottom right
			StartPosition = FormStartPosition.Manual;
			//Left = Screen.PrimaryScreen.WorkingArea.Width - Width;
			//Top = Screen.PrimaryScreen.WorkingArea.Height - Height;
		    Left = (Screen.PrimaryScreen.WorkingArea.Width - uxInputText.Size.Width) / 2;
		    Top = (Screen.PrimaryScreen.WorkingArea.Height - uxInputText.Size.Height) / 2;
		}

		protected override void OnShown(EventArgs e)
		{
			base.OnShown(e);

			UpdateAutoCompletion();
		}

		public void UpdateAutoCompletion()
		{
			uxInputText.AutoCompleteMode = AutoCompleteMode.Append;
			uxInputText.AutoCompleteSource = AutoCompleteSource.CustomSource;
			var sr = new AutoCompleteStringCollection();
			sr.AddRange(Context.Current.AutoCompleteSource);

			uxInputText.AutoCompleteCustomSource = sr;
		}
		

		private void OnInputTextBoxKeyUp(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Enter)
			{
				var alias = uxInputText.Text;
				HideForm();
				Context.Current.Start(alias);
			}
			else if (e.KeyCode == Keys.Escape)
			{
				HideForm();
			}
		}

		/// <summary>
		/// Hides the form.
		/// </summary>
		private void HideForm()
		{
			Hide();

			Properties.Settings.Default.Position = Location;
			Properties.Settings.Default.Size = Size;
			Properties.Settings.Default.Save();			
		}

		//protected override void OnClosing(CancelEventArgs e)
		//{
		//    base.OnClosing(e);

		//    Properties.Settings.Default.Position = this.Location;
		//    Properties.Settings.Default.Size = this.Size;
		//    Properties.Settings.Default.Save();
		
		//}

		
		private void OnExitToolStripMenuItemClick(object sender, EventArgs e)
		{
			Context.Current.Exit();
		}

		private void OnHideToolStripMenuItemClick(object sender, EventArgs e)
		{
			if (Visible)
			{
				HideForm();
			}
			else
			{
				Show();
			}
			
		}

		private void OnSetupToolStripMenuItemClick(object sender, EventArgs e)
		{
			Context.Current.Setup();
		}

		private void OnHelpToolStripMenuItemClick(object sender, EventArgs e)
		{
			Context.Current.Help();
		}

		private void OnNewSlaveToolStripMenuItemClick(object sender, EventArgs e)
		{
			Context.Current.ShowNewSlaveForm();
		}

		private void uxInputContextMenuStrip_Opening(object sender, CancelEventArgs e)
		{
			// update the show/hide input bar
			uxHideToolStripMenuItem.Text = Visible ? "Hide command line" : "Show command line";
			
			// adding Slaves
			uxSlavesToolStripMenuItem.DropDownItems.Clear();
			Context.Current.Slaves.Sort((w1, w2) => w1.Alias.CompareTo(w2.Alias));
			foreach (var word in Context.Current.Slaves)
			{
				var item = new ToolStripMenuItem(word.Alias);
				item.Click += new EventHandler(delegate(object source, EventArgs args)
				{
					Context.Current.Start(item.Text);
				});
				uxSlavesToolStripMenuItem.DropDownItems.Add(item);
			}
		}

        private void OnDeactivate(object sender, EventArgs e) {
            HideForm();
        }
    }
}