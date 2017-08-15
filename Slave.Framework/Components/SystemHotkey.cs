using System;
using System.ComponentModel;
using System.Windows.Forms;

namespace Slave.Framework.Components {
    /// <summary>
    /// Handles a System Hotkey
    /// </summary>
    public class SystemHotkey : Component, IDisposable {
        private Container _components = null;
        private readonly DummyWindowWithEvent MWindow = new DummyWindowWithEvent();    //window for WM_Hotkey Messages
        private Shortcut MHotKey = Shortcut.None;
        private bool isRegistered = false;
        public event EventHandler Pressed;
        public event EventHandler Error;

        public SystemHotkey(IContainer container) {
            container.Add(this);
            InitializeComponent();
            MWindow.ProcessMessage += new MessageEventHandler(MessageEvent);
        }

        public SystemHotkey() {
            InitializeComponent();
            if (!DesignMode) {
                MWindow.ProcessMessage += new MessageEventHandler(MessageEvent);
            }
        }

        public new void Dispose() {
            if (isRegistered) {
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
            _components = new System.ComponentModel.Container();
        }
        #endregion

        protected void MessageEvent(object sender, ref Message m, ref bool handled) {   //Handle WM_Hotkey event
            if (( m.Msg == (int)Msgs.WmHotkey ) && ( m.WParam == (IntPtr)GetType().GetHashCode() )) {
                handled = true;
                System.Diagnostics.Debug.WriteLine("HOTKEY pressed!");
                if (Pressed != null)
                    Pressed(this, EventArgs.Empty);
            }
        }

        protected bool UnregisterHotkey() { //unregister hotkey
            return User32.UnregisterHotKey(MWindow.Handle, GetType().GetHashCode());
        }

        protected bool RegisterHotkey(Shortcut key) {   //register hotkey
            var mod = 0;
            var k2 = Keys.None;
            if (( (int)key & (int)Keys.Alt ) == (int)Keys.Alt) { mod += (int)Modifiers.ModAlt; k2 = Keys.Alt; }
            if (( (int)key & (int)Keys.Shift ) == (int)Keys.Shift) { mod += (int)Modifiers.ModShift; k2 = Keys.Shift; }
            if (( (int)key & (int)Keys.Control ) == (int)Keys.Control) { mod += (int)Modifiers.ModControl; k2 = Keys.Control; }
            System.Diagnostics.Debug.Write(mod.ToString() + " ");
            System.Diagnostics.Debug.WriteLine(( ( (int)key ) - ( (int)k2 ) ).ToString());

            return User32.RegisterHotKey(MWindow.Handle, GetType().GetHashCode(), (int)mod, ( (int)key ) - ( (int)k2 ));
        }

        public bool IsRegistered { get => isRegistered;
            set => isRegistered = value;
        }


        [DefaultValue(Shortcut.None)]
        public Shortcut Shortcut {
            get => MHotKey;
            set {
                if (DesignMode) { MHotKey = value; return; }    //Don't register in Designmode
                if (( isRegistered ) && ( MHotKey != value ))   //Unregister previous registered Hotkey
                {
                    if (UnregisterHotkey()) {
                        System.Diagnostics.Debug.WriteLine("Unreg: OK");
                        isRegistered = false;
                    } else {
                        if (Error != null)
                            Error(this, EventArgs.Empty);
                        System.Diagnostics.Debug.WriteLine("Unreg: ERR");
                    }
                }
                if (value == Shortcut.None) { MHotKey = value; return; }
                if (RegisterHotkey(value))  //Register new Hotkey
                {
                    System.Diagnostics.Debug.WriteLine("Reg: OK");
                    isRegistered = true;
                } else {
                    if (Error != null)
                        Error(this, EventArgs.Empty);
                    System.Diagnostics.Debug.WriteLine("Reg: ERR");
                }
                MHotKey = value;
            }
        }
    }
}
