using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Handyman.Framework.Components {
    /// <summary>
    /// Handles a System Hotkey
    /// </summary>
    public class SystemHotkey : Component, IDisposable {
        private Container _components = null;
        private readonly EventWindow _window = new EventWindow();    //window for WM_Hotkey Messages
        private Shortcut _hotKey = Shortcut.None;
        private bool _isRegistered = false;
        public event EventHandler Pressed;
        public event EventHandler Error;

        public SystemHotkey(IContainer container) {
            container.Add(this);
            InitializeComponent();
            _window.ProcessMessage += MessageEvent;
        }

        public SystemHotkey() {
            InitializeComponent();
            if (!DesignMode) {
                _window.ProcessMessage += MessageEvent;
            }
        }

        public new void Dispose() {
            if (_isRegistered) {
                if (UnregisterHotkey())
                    System.Diagnostics.Debug.WriteLine("Unreg: OK");
            }
            System.Diagnostics.Debug.WriteLine("Disposed");
        }
        #region Component Designer generated code
        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent() {
            _components = new Container();
        }
        #endregion

        protected void MessageEvent(object sender, ref Message m, ref bool handled) {   //Handle WM_Hotkey event
            if (m.Msg == (int)Msgs.WmHotkey && m.WParam == (IntPtr)GetType().GetHashCode()) {
                handled = true;
                System.Diagnostics.Debug.WriteLine("HOTKEY pressed!");
                Pressed?.Invoke(this, EventArgs.Empty);
            }
        }

        protected bool UnregisterHotkey() { //unregister hotkey
            return User32.UnregisterHotKey(_window.Handle, GetType().GetHashCode());
        }

        protected bool RegisterHotkey(Shortcut key) {   //register hotkey
            var mod = 0;
            var k2 = Keys.None;
            if (( (int)key & (int)Keys.Alt ) == (int)Keys.Alt) { mod += (int)Modifiers.ModAlt; k2 = Keys.Alt; }
            if (( (int)key & (int)Keys.Shift ) == (int)Keys.Shift) { mod += (int)Modifiers.ModShift; k2 = Keys.Shift; }
            if (( (int)key & (int)Keys.Control ) == (int)Keys.Control) { mod += (int)Modifiers.ModControl; k2 = Keys.Control; }
            System.Diagnostics.Debug.Write(mod + " ");
            System.Diagnostics.Debug.WriteLine(( (int)key - (int)k2 ).ToString());

            return User32.RegisterHotKey(_window.Handle, GetType().GetHashCode(), mod, (int)key - (int)k2);
        }

        public bool IsRegistered {
            get => _isRegistered;
            set => _isRegistered = value;
        }


        [DefaultValue(Shortcut.None)]
        public Shortcut Shortcut {
            get => _hotKey;
            set {
                if (DesignMode) { _hotKey = value; return; }    //Don't register in Designmode
                if (_isRegistered && _hotKey != value)   //Unregister previous registered Hotkey
                {
                    if (UnregisterHotkey()) {
                        System.Diagnostics.Debug.WriteLine("Unreg: OK");
                        _isRegistered = false;
                    } else {
                        Error?.Invoke(this, EventArgs.Empty);
                        System.Diagnostics.Debug.WriteLine("Unreg: ERR");
                    }
                }
                if (value == Shortcut.None) { _hotKey = value; return; }
                if (RegisterHotkey(value))  //Register new Hotkey
                {
                    System.Diagnostics.Debug.WriteLine("Reg: OK");
                    _isRegistered = true;
                } else {
                    Error?.Invoke(this, EventArgs.Empty);
                    System.Diagnostics.Debug.WriteLine("Reg: ERR");
                }
                _hotKey = value;
            }
        }
    }
}
