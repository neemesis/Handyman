#region Using

using System;
using System.Drawing;
using System.Runtime.InteropServices;

#endregion

namespace Slave.ScreenShotPlugin
{
	/// <summary>
	/// Helper class to do a screenshot of a specified window handle
	/// </summary>
	public class Capture
	{
		#region Invoke APIs
		[StructLayout(LayoutKind.Sequential)]
		private struct Rect
		{
			public readonly int Left;
			public readonly int Top;
			public readonly int Right;
			public readonly int Bottom;
		}
		[DllImport("GDI32")]
		private static extern int DeleteDC(IntPtr hdc);
		[DllImport("user32")]
		private static extern long ReleaseDC(IntPtr hwnd, IntPtr hdc);
		[DllImport("gdi32")]
		private static extern int DeleteObject(IntPtr hObj);
		[DllImport("gdi32")]
		private static extern int CreateCompatibleDC(IntPtr hDc);
		[DllImport("user32")]
		private static extern int GetWindowDC(IntPtr hwnd);
		[DllImport("gdi32")]
		private static extern int SelectObject(IntPtr hDc, IntPtr hObject);
		[DllImport("gdi32")]
		private static extern int CreateCompatibleBitmap(IntPtr hDc, IntPtr nWidth, IntPtr nHeight);
		[DllImport("user32")]
		private static extern Int32 GetWindowRect(IntPtr hwnd, ref Rect lpRect);
		[DllImport("user32")]
		private static extern bool IsWindow(IntPtr hwnd);
		[DllImport("gdi32.dll")]
		private static extern bool BitBlt(IntPtr hdcDest, int nXDest, int nYDest, int nWidth, int nHeight, IntPtr hdcSrc, int
		nXSrc, int nYSrc, Int32 dwRop);
		#endregion

		#region Create Bitmap
		public static Bitmap GrabWindow(IntPtr hWnd)
		{
			try
			{
				long hWindowDc, hOffscreenDc, nWidth, nHeight, hBitmap, hOldBmp;
				Rect rec;
				rec = new Rect();
				Bitmap myBitmap;
				myBitmap = new Bitmap(640, 480);
				if (hWnd.ToInt32() != 0 && IsWindow(hWnd))
				{
					hWindowDc = GetWindowDC(hWnd);
					if (hWindowDc.ToString() != null)
					{
						if (GetWindowRect(hWnd, ref rec).ToString() != null)
						{
							nWidth = rec.Right - rec.Left;
							nHeight = rec.Bottom - rec.Top;
							hOffscreenDc = CreateCompatibleDC(new IntPtr(hWindowDc));
							if (hOffscreenDc.ToString() != null)
							{
								hBitmap = CreateCompatibleBitmap(new IntPtr(hWindowDc), new IntPtr(nWidth), new IntPtr(nHeight));
								if (hBitmap.ToString() != null)
								{
									hOldBmp = SelectObject(new IntPtr(hOffscreenDc), new IntPtr(hBitmap));
									BitBlt(new IntPtr(hOffscreenDc), 0, 0, (int)nWidth, (int)nHeight, new IntPtr(hWindowDc), 0, 0, 13369376);
									myBitmap = Image.FromHbitmap(new IntPtr(hBitmap));
									DeleteObject(new IntPtr(SelectObject(new IntPtr(hOffscreenDc), new IntPtr(hOldBmp))));
								}
								DeleteDC(new IntPtr(hOffscreenDc));
							}
						}
						ReleaseDC(hWnd, new IntPtr(hWindowDc));
						GC.Collect();
					}
				}
				return myBitmap;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
		#endregion
	}
}
