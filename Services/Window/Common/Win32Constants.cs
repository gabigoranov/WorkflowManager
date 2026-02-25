using System;

namespace WorkflowManager.Services.Window.Common;

public static class Win32Constants
{ 
    // ReSharper disable once InconsistentNaming
    // Sets the window style to open as maximized
    public const IntPtr WS_MAXIMIZE = (IntPtr) 0x00010000L;
    
    // An enumeration containing all the possible SW values.
    public enum SW : int
    {
        HIDE = 0,
        SHOWNORMAL = 1,
        SHOWMINIMIZED = 2,
        SHOWMAXIMIZED = 3,
        SHOWNOACTIVATE = 4,
        SHOW = 5,
        MINIMIZE = 6,
        SHOWMINNOACTIVE = 7,
        SHOWNA = 8,
        RESTORE = 9,
        SHOWDEFAULT = 10
    }
    
    [Flags]
    public enum SendMessageTimeoutFlags : uint
    {
        SMTO_NORMAL             = 0x0,
        SMTO_BLOCK              = 0x1,
        SMTO_ABORT_IF_HUNG        = 0x2,
        SMTO_NO_TIMEOUT_IF_NOTHUNG = 0x8,
        SMTO_ERROR_ON_EXIT = 0x20
    }
}