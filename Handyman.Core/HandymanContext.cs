using System;
using System.Windows.Forms;
using System.Drawing;
using System.Diagnostics;

using Handyman.Core.Forms;
using Handyman.Core.Helpers;
using Handyman.Framework.Components;

namespace Handyman.Core {
    /// <summary>
    /// HandymanContext.
    /// This class has several jobs:
    ///		- Create the NotifyIcon UI
    ///		- Manage the Input form that pops up
    ///		- Determines when the Application should exit
    /// </summary>
    public class HandymanContext : ApplicationContext {
        private System.ComponentModel.IContainer _components;    // a list of _components to dispose when the context is disposed
        private NotifyIcon _notifyIcon;    // the icon that sits in the system tray 
        private SystemHotkey _systemHotkey;
        private SystemHotkey _addWord;

        /// <summary>
        /// This class should be created and passed into Application.Run( ... )
        /// </summary>
        public HandymanContext() {
            // create the notify icon and it's associated context menu
            InitializeContext();

            Console.WriteLine("# Handymans: " + Context.Current.Handymans.Count);
            _notifyIcon.ContextMenuStrip = Launcher.Current.ContextMenuStrip;
        }

        /// <summary>
        /// Create the NotifyIcon UI, the ContextMenu for the NotifyIcon and an Exit menu item. 
        /// </summary>
        private void InitializeContext() {
            _components = new System.ComponentModel.Container();
            _notifyIcon = new NotifyIcon(_components);
            _systemHotkey = new SystemHotkey(_components);
            _addWord = new SystemHotkey(_components);

            _notifyIcon.DoubleClick += OnNotifyIconDoubleClick;
            _notifyIcon.Icon = Properties.Resources.if_robot_88068;
            _notifyIcon.Text = "Handymans Master at your service!";
            _notifyIcon.Visible = true;

            // m_SystemHotkey
            _systemHotkey.Shortcut = Properties.Settings.Default.TypeWordHotKey; //Shortcut.AltBcksp;
            _systemHotkey.Pressed += OnSystemHotkeyPressed;

            // m_AddWordSystemHotkey
            _addWord.Shortcut = Properties.Settings.Default.AddWordHotKey; //Shortcut.CtrlF11;
            _addWord.Pressed += OnAddWordSystemHotkeyPressed;

            Application.ApplicationExit += OnApplicationExit;
        }

        /// <summary>
        /// When the application context is disposed,; dispose things like the notify icon.
        /// </summary>
        /// <param name="disposing"></param>
        protected override void Dispose(bool disposing) {
            if (disposing) {
                _components?.Dispose();
                _notifyIcon?.Dispose();
            }
        }

        private static void OnApplicationExit(object sender, EventArgs e) {
            HandymansManager.Save(Context.Current.Handymans);
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

            Context.AddActiveApplicationHandyman(appExeName, appExePath);
        }

        /// <summary>
        /// This function will either create a new Form or activate the existing one, bringing the 
        /// window to front.
        /// </summary>
        private void ShowForm() {
            if (Launcher.Current.Visible) {
                Launcher.Current.Activate();
                Launcher.Current.BringToFront();
                Launcher.Current.Focus();
            } else {
                Launcher.Current.Show();
                Launcher.Current.BringToFront();
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
