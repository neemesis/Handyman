using SHDocVw;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms;

namespace Handyman.Framework.Components {
    /// <summary>
    /// Summary description for Win32.
    /// </summary>
    public class User32 {
        [DllImport("user32.dll")]
        public static extern bool RegisterHotKey(IntPtr hWnd, int id, int fsModifiers, int vlc);
        [DllImport("user32.dll")]
        public static extern bool UnregisterHotKey(IntPtr hWnd, int id);
    }

    public class Kernel32 {
        [DllImport("kernel32.dll")]
        public static extern int GlobalAddAtom(string name);
        [DllImport("kernel32.dll")]
        public static extern int GlobalDeleteAtom(int atom);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalLock(IntPtr hMem);
        [DllImport("kernel32.dll")]
        public static extern bool GlobalUnlock(IntPtr hMem);
    }

    public enum Modifiers { ModAlt = 0x0001, ModControl = 0x0002, ModShift = 0x0004, ModWin = 0x0008 }
    public enum Msgs {
        WmNull = 0x0000,
        WmCreate = 0x0001,
        WmDestroy = 0x0002,
        WmMove = 0x0003,
        WmSize = 0x0005,
        WmActivate = 0x0006,
        WmSetfocus = 0x0007,
        WmKillfocus = 0x0008,
        WmEnable = 0x000A,
        WmSetredraw = 0x000B,
        WmSettext = 0x000C,
        WmGettext = 0x000D,
        WmGettextlength = 0x000E,
        WmPaint = 0x000F,
        WmClose = 0x0010,
        WmQueryendsession = 0x0011,
        WmQuit = 0x0012,
        WmQueryopen = 0x0013,
        WmErasebkgnd = 0x0014,
        WmSyscolorchange = 0x0015,
        WmEndsession = 0x0016,
        WmShowwindow = 0x0018,
        WmWininichange = 0x001A,
        WmSettingchange = 0x001A,
        WmDevmodechange = 0x001B,
        WmActivateapp = 0x001C,
        WmFontchange = 0x001D,
        WmTimechange = 0x001E,
        WmCancelmode = 0x001F,
        WmSetcursor = 0x0020,
        WmMouseactivate = 0x0021,
        WmChildactivate = 0x0022,
        WmQueuesync = 0x0023,
        WmGetminmaxinfo = 0x0024,
        WmPainticon = 0x0026,
        WmIconerasebkgnd = 0x0027,
        WmNextdlgctl = 0x0028,
        WmSpoolerstatus = 0x002A,
        WmDrawitem = 0x002B,
        WmMeasureitem = 0x002C,
        WmDeleteitem = 0x002D,
        WmVkeytoitem = 0x002E,
        WmChartoitem = 0x002F,
        WmSetfont = 0x0030,
        WmGetfont = 0x0031,
        WmSethotkey = 0x0032,
        WmGethotkey = 0x0033,
        WmQuerydragicon = 0x0037,
        WmCompareitem = 0x0039,
        WmGetobject = 0x003D,
        WmCompacting = 0x0041,
        WmCommnotify = 0x0044,
        WmWindowposchanging = 0x0046,
        WmWindowposchanged = 0x0047,
        WmPower = 0x0048,
        WmCopydata = 0x004A,
        WmCanceljournal = 0x004B,
        WmNotify = 0x004E,
        WmInputlangchangerequest = 0x0050,
        WmInputlangchange = 0x0051,
        WmTcard = 0x0052,
        WmHelp = 0x0053,
        WmUserchanged = 0x0054,
        WmNotifyformat = 0x0055,
        WmContextmenu = 0x007B,
        WmStylechanging = 0x007C,
        WmStylechanged = 0x007D,
        WmDisplaychange = 0x007E,
        WmGeticon = 0x007F,
        WmSeticon = 0x0080,
        WmNccreate = 0x0081,
        WmNcdestroy = 0x0082,
        WmNccalcsize = 0x0083,
        WmNchittest = 0x0084,
        WmNcpaint = 0x0085,
        WmNcactivate = 0x0086,
        WmGetdlgcode = 0x0087,
        WmSyncpaint = 0x0088,
        WmNcmousemove = 0x00A0,
        WmNclbuttondown = 0x00A1,
        WmNclbuttonup = 0x00A2,
        WmNclbuttondblclk = 0x00A3,
        WmNcrbuttondown = 0x00A4,
        WmNcrbuttonup = 0x00A5,
        WmNcrbuttondblclk = 0x00A6,
        WmNcmbuttondown = 0x00A7,
        WmNcmbuttonup = 0x00A8,
        WmNcmbuttondblclk = 0x00A9,
        WmKeydown = 0x0100,
        WmKeyup = 0x0101,
        WmChar = 0x0102,
        WmDeadchar = 0x0103,
        WmSyskeydown = 0x0104,
        WmSyskeyup = 0x0105,
        WmSyschar = 0x0106,
        WmSysdeadchar = 0x0107,
        WmKeylast = 0x0108,
        WmImeStartcomposition = 0x010D,
        WmImeEndcomposition = 0x010E,
        WmImeComposition = 0x010F,
        WmImeKeylast = 0x010F,
        WmInitdialog = 0x0110,
        WmCommand = 0x0111,
        WmSyscommand = 0x0112,
        WmTimer = 0x0113,
        WmHscroll = 0x0114,
        WmVscroll = 0x0115,
        WmInitmenu = 0x0116,
        WmInitmenupopup = 0x0117,
        WmMenuselect = 0x011F,
        WmMenuchar = 0x0120,
        WmEnteridle = 0x0121,
        WmMenurbuttonup = 0x0122,
        WmMenudrag = 0x0123,
        WmMenugetobject = 0x0124,
        WmUninitmenupopup = 0x0125,
        WmMenucommand = 0x0126,
        WmCtlcolormsgbox = 0x0132,
        WmCtlcoloredit = 0x0133,
        WmCtlcolorlistbox = 0x0134,
        WmCtlcolorbtn = 0x0135,
        WmCtlcolordlg = 0x0136,
        WmCtlcolorscrollbar = 0x0137,
        WmCtlcolorstatic = 0x0138,
        WmMousemove = 0x0200,
        WmLbuttondown = 0x0201,
        WmLbuttonup = 0x0202,
        WmLbuttondblclk = 0x0203,
        WmRbuttondown = 0x0204,
        WmRbuttonup = 0x0205,
        WmRbuttondblclk = 0x0206,
        WmMbuttondown = 0x0207,
        WmMbuttonup = 0x0208,
        WmMbuttondblclk = 0x0209,
        WmMousewheel = 0x020A,
        WmParentnotify = 0x0210,
        WmEntermenuloop = 0x0211,
        WmExitmenuloop = 0x0212,
        WmNextmenu = 0x0213,
        WmSizing = 0x0214,
        WmCapturechanged = 0x0215,
        WmMoving = 0x0216,
        WmDevicechange = 0x0219,
        WmMdicreate = 0x0220,
        WmMdidestroy = 0x0221,
        WmMdiactivate = 0x0222,
        WmMdirestore = 0x0223,
        WmMdinext = 0x0224,
        WmMdimaximize = 0x0225,
        WmMditile = 0x0226,
        WmMdicascade = 0x0227,
        WmMdiiconarrange = 0x0228,
        WmMdigetactive = 0x0229,
        WmMdisetmenu = 0x0230,
        WmEntersizemove = 0x0231,
        WmExitsizemove = 0x0232,
        WmDropfiles = 0x0233,
        WmMdirefreshmenu = 0x0234,
        WmImeSetcontext = 0x0281,
        WmImeNotify = 0x0282,
        WmImeControl = 0x0283,
        WmImeCompositionfull = 0x0284,
        WmImeSelect = 0x0285,
        WmImeChar = 0x0286,
        WmImeRequest = 0x0288,
        WmImeKeydown = 0x0290,
        WmImeKeyup = 0x0291,
        WmMousehover = 0x02A1,
        WmMouseleave = 0x02A3,
        WmCut = 0x0300,
        WmCopy = 0x0301,
        WmPaste = 0x0302,
        WmClear = 0x0303,
        WmUndo = 0x0304,
        WmRenderformat = 0x0305,
        WmRenderallformats = 0x0306,
        WmDestroyclipboard = 0x0307,
        WmDrawclipboard = 0x0308,
        WmPaintclipboard = 0x0309,
        WmVscrollclipboard = 0x030A,
        WmSizeclipboard = 0x030B,
        WmAskcbformatname = 0x030C,
        WmChangecbchain = 0x030D,
        WmHscrollclipboard = 0x030E,
        WmQuerynewpalette = 0x030F,
        WmPaletteischanging = 0x0310,
        WmPalettechanged = 0x0311,
        WmHotkey = 0x0312,
        WmPrint = 0x0317,
        WmPrintclient = 0x0318,
        WmHandheldfirst = 0x0358,
        WmHandheldlast = 0x035F,
        WmAfxfirst = 0x0360,
        WmAfxlast = 0x037F,
        WmPenwinfirst = 0x0380,
        WmPenwinlast = 0x038F,
        WmApp = 0x8000,
        WmUser = 0x0400,
        WmDdeInitiate = 0x03E0,
        WmDdeTerminate,
        WmDdeAdvise,
        WmDdeUnadvise,
        WmDdeAck,
        WmDdeData,
        WmDdeRequest,
        WmDdePoke,
        WmDdeExecute
    }


    /// <summary>
    /// Defines a delegate for Message handling
    /// </summary>
    public delegate void MessageEventHandler(object sender, ref Message msg, ref bool handled);

    /// <summary>
    /// Inherits from System.Windows.Form.NativeWindow. Provides an Event for Message handling
    /// </summary>
    public class NativeWindowWithEvent : NativeWindow {
        public event MessageEventHandler ProcessMessage;
        protected override void WndProc(ref Message m) {
            if (ProcessMessage != null) {
                var handled = false;
                ProcessMessage(this, ref m, ref handled);
                if (!handled)
                    base.WndProc(ref m);
            } else
                base.WndProc(ref m);
        }
    }

    /// <summary>
    /// Inherits from NativeWindowWithEvent and automatic creates/destroys of a dummy window
    /// </summary>
    public class EventWindow : NativeWindowWithEvent, IDisposable {
        public EventWindow() {
            var parms = new CreateParams();
            CreateHandle(parms);
        }
        public void Dispose() {
            if (Handle != (IntPtr)0) {
                DestroyHandle();
            }
        }
    }

    public class NativeWin32 {
        [DllImport("user32.dll")]
        public static extern int GetForegroundWindow();

        //[DllImport("user32.dll")]
        //public static extern IntPtr GetForegroundWindow();

        [DllImport("user32")]
        private static extern UInt32 GetWindowThreadProcessId(Int32 hWnd, out Int32 lpdwProcessId);

        public static Int32 GetWindowProcessId(Int32 hwnd) {
            var pid = 1;
            GetWindowThreadProcessId(hwnd, out pid);
            return pid;
        }

        public static Process GetFocusedProcess() {
            var hwnd = 0;
            hwnd = GetForegroundWindow();
            return Process.GetProcessById(GetWindowProcessId(hwnd));
        }

        // http://forums.microsoft.com/MSDN/ShowPost.aspx?PostID=770697&SiteID=1
        public static string GetFocusesApp() {
            var hwnd = 0;
            hwnd = GetForegroundWindow();
            var appExePath = Process.GetProcessById(GetWindowProcessId(hwnd)).MainModule.FileName;
            Console.WriteLine(appExePath);
            return appExePath;
        }
    }

    public class EnumerateOpenedWindows {
        const int MAXTITLE = 255;

        private static List<string> lstTitles;
        private static List<IntPtr> explrPointers;

        private delegate bool EnumDelegate(IntPtr hWnd, int lParam);

        [DllImport("user32.dll")]
        public static extern IntPtr GetForegroundWindow();

        [DllImport("user32.dll", EntryPoint = "EnumDesktopWindows",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern bool EnumDesktopWindows(IntPtr hDesktop,
        EnumDelegate lpEnumCallbackFunction, IntPtr lParam);

        [DllImport("user32.dll", EntryPoint = "GetWindowText",
        ExactSpelling = false, CharSet = CharSet.Auto, SetLastError = true)]
        private static extern int _GetWindowText(IntPtr hWnd,
        StringBuilder lpWindowText, int nMaxCount);

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        static extern bool IsWindowVisible(IntPtr hWnd);

        private static bool EnumWindowsProc(IntPtr hWnd, int lParam) {
            string strTitle = GetWindowText(hWnd);
            if (strTitle != "" & IsWindowVisible(hWnd)) //
            {
                string path = Process.GetProcessById(NativeWin32.GetWindowProcessId(hWnd.ToInt32())).MainModule.FileName;
                lstTitles.Add(path);
            }
            return true;
        }

        private static bool GetExplorerWindows(IntPtr hWnd, int lParam) {
            string strTitle = GetWindowText(hWnd);
            if (strTitle != "" & IsWindowVisible(hWnd)) //
            {
                string path = Process.GetProcessById(NativeWin32.GetWindowProcessId(hWnd.ToInt32())).MainModule.FileName;
                if (path.ToLower().Contains("explorer.exe"))
                    explrPointers.Add(hWnd);
            }
            return true;
        }

        /// <summary>
        /// Return the window title of handle
        /// </summary>
        /// <param name="hWnd"></param>
        /// <returns></returns>
        public static string GetWindowText(IntPtr hWnd) {
            StringBuilder strbTitle = new StringBuilder(MAXTITLE);
            int nLength = _GetWindowText(hWnd, strbTitle, strbTitle.Capacity + 1);
            strbTitle.Length = nLength;
            return strbTitle.ToString();
        }

        /// <summary>
        /// Return titles of all visible windows on desktop
        /// </summary>
        /// <returns>List of titles in type of string</returns>
        public static string[] GetDesktopWindowsTitles() {
            lstTitles = new List<string>();
            EnumDelegate delEnumfunc = new EnumDelegate(EnumWindowsProc);
            bool bSuccessful = EnumDesktopWindows(IntPtr.Zero, delEnumfunc, IntPtr.Zero); //for current desktop

            if (bSuccessful) {
                return lstTitles.ToArray();
            } else {
                // Get the last Win32 error code
                int nErrorCode = Marshal.GetLastWin32Error();
                string strErrMsg = string.Format("EnumDesktopWindows failed with code {0}.", nErrorCode);
                throw new Exception(strErrMsg);
            }
        }

        public static IntPtr GetExplorerPointer() {
            explrPointers = new List<IntPtr>();
            EnumDelegate delEnumfunc = new EnumDelegate(GetExplorerWindows);
            bool bSuccessful = EnumDesktopWindows(IntPtr.Zero, delEnumfunc, IntPtr.Zero); //for current desktop

            if (bSuccessful) {
                return explrPointers[0];
            } else {
                // Get the last Win32 error code
                int nErrorCode = Marshal.GetLastWin32Error();
                string strErrMsg = string.Format("EnumDesktopWindows failed with code {0}.", nErrorCode);
                throw new Exception(strErrMsg);
            }
        }

        public static string GetActiveExplorerPath() {
            // get the active window
            IntPtr handle = GetExplorerPointer();

            // Required ref: SHDocVw (Microsoft Internet Controls COM Object) - C:\Windows\system32\ShDocVw.dll
            var shellWindows = new ShellWindows();

            // loop through all windows
            foreach (InternetExplorer window in shellWindows) {
                // match active window
                if (window.HWND == (int)handle) {
                    // Required ref: Shell32 - C:\Windows\system32\Shell32.dll
                    var shellWindow = window.Document as Shell32.IShellFolderViewDual2;

                    // will be null if you are in Internet Explorer for example
                    if (shellWindow != null) {
                        // Item without an index returns the current object
                        var currentFolder = shellWindow.Folder.Items().Item();

                        // special folder - use window title
                        // for some reason on "Desktop" gives null
                        if (currentFolder == null || currentFolder.Path.StartsWith("::")) {
                            // Get window title instead
                            const int nChars = 256;
                            StringBuilder Buff = new StringBuilder(nChars);
                            if (_GetWindowText(handle, Buff, nChars) > 0) {
                                return Buff.ToString();
                            }
                        } else {
                            return currentFolder.Path;
                        }
                    }

                    break;
                }
            }

            return null;
        }

        public static string GetLastActiveWindow() {
            return GetDesktopWindowsTitles()[0];
        }
    }
}
