using System;
using System.Windows.Forms;
using Slave.Framework.Components;
using Slave.Framework.Interfaces;

namespace Slave.ScreenShotPlugin {
    public class ScreenshotPlugin : ITool {
        private string _mAlias;
        private Shortcut _mHotKey;

        public ScreenshotPlugin() {
            _mAlias = "screenshot";
            _mHotKey = Shortcut.ShiftF1;
        }

        #region ITool Members

        void ITool.Initialize() {

        }

        void ITool.Execute(string[] args) {
            var hwnd = (IntPtr)NativeWin32.GetForegroundWindow();


            var image = Capture.GrabWindow(hwnd);
            var dialog = new SaveFileDialog {
                AddExtension = true,
                DefaultExt = "*.png",
                Filter = "Png files (*.png)|*.png",
                Title = "Save the screenshot..."
            };

            if (dialog.ShowDialog() == DialogResult.OK) {
                image.Save(dialog.FileName, System.Drawing.Imaging.ImageFormat.Png);
            }
        }

        string ITool.Alias {
            get => _mAlias;
            set => _mAlias = value;
        }

        Shortcut ITool.HotKey {
            get => _mHotKey;
            set => _mHotKey = value;
        }

        string ITool.Name => "Screenshot maker";

        string ITool.Description => "Take a screenshot of the current windows and save it as a PNG file";

        string ITool.Author => "John Roland";

        string ITool.Version => "1.0";

        #endregion
    }
}
