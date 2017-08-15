using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

using Slave.Core.Forms;
using Slave.Core.Helpers;
using Slave.Framework.Components;

namespace Slave.Core {
    /// <summary>
    /// CalendarApplicationContext.
    /// This class has several jobs:
    ///		- Create the NotifyIcon UI
    ///		- Manage the Input form that pops up
    ///		- Determines when the Application should exit
    /// </summary>
    public class SlaveContext : ApplicationContext {
        private System.ComponentModel.IContainer components;    // a list of components to dispose when the context is disposed
        private NotifyIcon _mNotifyIcon;    // the icon that sits in the system tray 
        private SystemHotkey _mSystemHotkey;
        private SystemHotkey _mAddWordSystemHotkey;

        /// <summary>
        /// This class should be created and passed into Application.Run( ... )
        /// </summary>
        public SlaveContext() {
            // create the notify icon and it's associated context menu
            InitializeContext();

            //m_MainForm = new LauncherForm();
            //Console.WriteLine(Forms.LauncherForm.Current.Name);
            Console.WriteLine("# Slaves: " + Context.Current.Slaves.Count);

            _mNotifyIcon.ContextMenuStrip = Launcher.Current.ContextMenuStrip;
        }

        /// <summary>
        /// Create the NotifyIcon UI, the ContextMenu for the NotifyIcon and an Exit menu item. 
        /// </summary>
        private void InitializeContext() {
            components = new System.ComponentModel.Container();
            _mNotifyIcon = new NotifyIcon(components);
            _mSystemHotkey = new SystemHotkey(components);
            _mAddWordSystemHotkey = new SystemHotkey(components);

            _mNotifyIcon.DoubleClick += OnNotifyIconDoubleClick;
            _mNotifyIcon.Icon = Properties.Resources.if_robot_88068;
            _mNotifyIcon.Text = "Slaves Master at your service!";
            _mNotifyIcon.Visible = true;

            //
            // m_SystemHotkey
            //
            _mSystemHotkey.Shortcut = Properties.Settings.Default.TypeWordHotKey; //Shortcut.AltBcksp;
            _mSystemHotkey.Pressed += OnSystemHotkeyPressed;

            //
            // m_AddWordSystemHotkey
            //
            _mAddWordSystemHotkey.Shortcut = Properties.Settings.Default.AddWordHotKey;//Shortcut.CtrlF11;
            _mAddWordSystemHotkey.Pressed += OnAddWordSystemHotkeyPressed;

            Application.ApplicationExit += OnApplicationExit;
        }

        /// <summary>
        /// When the application context is disposed,; dispose things like the notify icon.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                components?.Dispose();

                _mNotifyIcon?.Dispose();
            }
        }

        private static void OnApplicationExit(object sender, EventArgs e) {
            SlavesManager.Save(Context.Current.Slaves);
        }

        /// <summary>
        /// When the notify icon is double clicked in the system tray, bring up a form with a calendar on it.
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void OnNotifyIconDoubleClick(object sender, EventArgs e) {
            ShowForm();
        }

        private void OnSystemHotkeyPressed(object sender, EventArgs e) {
            ShowForm();
        }

        private static void OnAddWordSystemHotkeyPressed(object sender, EventArgs e) {
            var appExePath = NativeWin32.GetFocusesApp();
            var appExeName = System.IO.Path.GetFileNameWithoutExtension(appExePath);

            Context.Current.AddActiveApplicationSlave(appExeName, appExePath);
        }

        /// <summary>
        /// This function will either create a new CalendarForm or activate the existing one, bringing the 
        /// window to front.
        /// </summary>
        private void ShowForm() {
            if (Launcher.Current.Visible) {
                Launcher.Current.Activate();
                Launcher.Current.Focus();
            } else {
                Launcher.Current.Show();
                Launcher.Current.Focus();
            }
        }

        /// <summary>
        /// If we are presently showing a mainForm, clean it up.
        /// </summary>
        protected override void ExitThreadCore() {
            if (Launcher.Current != null) {
                // before we exit, give the main form a chance to clean itself up.
                Launcher.Current.Close();
            }
            base.ExitThreadCore();
        }

    }
}
