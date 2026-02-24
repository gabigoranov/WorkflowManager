using System;
using System.Runtime.InteropServices;

namespace WorkflowManager.Services.Window.Common;

public static class Win32Reference
{
    [DllImport("user32.dll")]
    public static extern bool MoveWindow(IntPtr hWnd, int x, int y, int width, int height, bool repaint);
}