using System;
using System.Drawing;
using System.Windows.Forms;

namespace Slave.ScratchPadPlugin
{
	public partial class ScratchPad : Form
	{
		//private string m_file;

		public ScratchPad()
		{
			InitializeComponent();

			Size = new Size(300, Screen.PrimaryScreen.WorkingArea.Height);
			FormClosing += new FormClosingEventHandler(ScratchPad_FormClosing);

			//m_file = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments) + "\\SlavesScratchPad.txt";
			//if (System.IO.File.Exists(m_file))
			//{
			//    richTextBox1.LoadFile(m_file, RichTextBoxStreamType.PlainText);
			//}

			richTextBox1.Text = Properties.Settings.Default.ScratchPadText;
		}

		void ScratchPad_FormClosing(object sender, FormClosingEventArgs e)
		{
			if (e.CloseReason == CloseReason.UserClosing)
			{
				e.Cancel = true;
				Hide();
			}
			else
			{
				Properties.Settings.Default.Save();
				//richTextBox1.SaveFile(m_file, RichTextBoxStreamType.PlainText);
			}
		}

		#region Singleton

		private static volatile ScratchPad _singleton;
		private static readonly object _syncRoot = new Object();

		public static ScratchPad Current
		{
			get
			{
				if (_singleton == null)
				{
					lock (_syncRoot)
					{
						if (_singleton == null)
						{
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