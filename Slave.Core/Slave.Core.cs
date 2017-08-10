using System;
using System.Windows.Forms;
using Slave.Framework.Components;

namespace Slave.Core
{
	/// <summary>
	/// CalendarApplicationContext.
	/// This class has several jobs:
	///		- Create the NotifyIcon UI
	///		- Manage the Input form that pops up
	///		- Determines when the Application should exit
	/// </summary>
	public class SlavesApplicationContext : ApplicationContext 
	{
		private System.ComponentModel.IContainer	components;						// a list of components to dispose when the context is disposed
		private System.Windows.Forms.NotifyIcon		m_NotifyIcon;				// the icon that sits in the system tray
		//private Forms.LauncherForm m_MainForm;						// the current form we're displaying
		private SystemHotkey m_SystemHotkey;
		private SystemHotkey m_AddWordSystemHotkey;

		/// <summary>
		/// This class should be created and passed into Application.Run( ... )
		/// </summary>
		public SlavesApplicationContext() 
		{
			// create the notify icon and it's associated context menu
			InitializeContext();

			//m_MainForm = new LauncherForm();
			//Console.WriteLine(Forms.LauncherForm.Current.Name);
			Console.WriteLine("# Slaves: " + Context.Current.Slaves.Count);

			m_NotifyIcon.ContextMenuStrip = Forms.LauncherForm.Current.ContextMenuStrip;
		}

		//public System.ComponentModel.IContainer Components
		//{
		//    get
		//    {
		//        this.components;
		//    }
		//}

		/// <summary>
		/// Create the NotifyIcon UI, the ContextMenu for the NotifyIcon and an Exit menu item. 
		/// </summary>
		private void InitializeContext() 
		{
			this.components = new System.ComponentModel.Container();
			this.m_NotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
			this.m_SystemHotkey = new SystemHotkey(this.components);
			this.m_AddWordSystemHotkey = new SystemHotkey(this.components);
			
			// 
			// m_NotifyIcon
			// 
			//this.m_NotifyIcon.ContextMenuStrip = this.m_NotifyIconContextMenu;
			this.m_NotifyIcon.DoubleClick += new System.EventHandler(this.OnNotifyIconDoubleClick);
			this.m_NotifyIcon.Icon = Properties.Resources.App;//new Icon(typeof(LauncherApplicationContext), "CLOCK05.ICO");
			this.m_NotifyIcon.Text = "Slaves";
			this.m_NotifyIcon.Visible = true;

			//
			// m_SystemHotkey
			//
			m_SystemHotkey.Shortcut = Properties.Settings.Default.TypeWordHotKey; //Shortcut.CtrlF12;
			m_SystemHotkey.Pressed += new EventHandler(OnSystemHotkeyPressed);

			//
			// m_AddWordSystemHotkey
			//
			m_AddWordSystemHotkey.Shortcut = Properties.Settings.Default.AddWordHotKey;//Shortcut.CtrlF11;
			m_AddWordSystemHotkey.Pressed += new EventHandler(OnAddWordSystemHotkeyPressed);

			Application.ApplicationExit += new EventHandler(OnApplicationExit);
		}

		/// <summary>
		/// When the application context is disposed,; dispose things like the notify icon.
		/// </summary>
		/// <param name="disposing"></param>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if (components != null) 
				{
					components.Dispose();
				}

				if (m_NotifyIcon != null)
				{
					m_NotifyIcon.Dispose();
				}
			}		
		}

		void OnApplicationExit(object sender, EventArgs e)
		{
			Context.Current.SaveSlaves();
		}		
				
		///// <summary>
		///// When the exit menu item is clicked, make a call to terminate the ApplicationContext.
		///// </summary>
		///// <param name="sender"></param>
		///// <param name="e"></param>
		//private void OnExitContextMenuItemClick(object sender, EventArgs e) 
		//{
		//    ExitThread();
		//}
		
		/// <summary>
		/// When the notify icon is double clicked in the system tray, bring up a form with a calendar on it.
		/// </summary>
		/// <param name="sender"></param>
		/// <param name="e"></param>
		private void OnNotifyIconDoubleClick(object sender,System.EventArgs e)
		{
			ShowForm();
		}

		private void OnSystemHotkeyPressed(object sender, EventArgs e)
		{
			ShowForm();
		}

		private void OnAddWordSystemHotkeyPressed(object sender, EventArgs e)
		{
			string appExePath = NativeWIN32.GetFocusesApp();
			string appExeName = System.IO.Path.GetFileNameWithoutExtension(appExePath);

			Context.Current.AddActiveApplicationSlave(appExeName, appExePath);
		}

		/// <summary>
		/// This function will either create a new CalendarForm or activate the existing one, bringing the 
		/// window to front.
		/// </summary>
		private void ShowForm() 
		{			
			if (Forms.LauncherForm.Current.Visible)
			{
				Forms.LauncherForm.Current.Activate();
			}
			else
			{
				Forms.LauncherForm.Current.Show();
			}
		}

		/// <summary>
		/// If we are presently showing a mainForm, clean it up.
		/// </summary>
		protected override void ExitThreadCore()
		{
			if (Forms.LauncherForm.Current != null) 
			{
				// before we exit, give the main form a chance to clean itself up.
				Forms.LauncherForm.Current.Close();
			}
			base.ExitThreadCore ();
		}
		
	}
}
