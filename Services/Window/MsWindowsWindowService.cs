using System;
using System.Threading.Tasks;
using WorkflowManager.Models.Common;
using WorkflowManager.Services.Window.Common;

namespace WorkflowManager.Services.Window;

/// <summary>
/// An implementation of the IWindowService, geared towards Microsoft Windows
/// </summary>
public class MsWindowsWindowService : IWindowService
{
    /// <summary>
    /// A method used to poll a window until it is fully loaded and ready to be manipulated
    /// </summary>
    /// <param name="process">The process to monitor</param>
    /// <param name="timeoutMs">The maximum allotted time in milliseconds</param>
    /// <returns>The main window handle of the loaded process</returns>
    /// <exception cref="TimeoutException">If the timeout is reached</exception>
    private static async Task<IntPtr> WaitForMainWindowHandleAsync(System.Diagnostics.Process process,
        int timeoutMs = 10000)
    {
        var stopWatch = System.Diagnostics.Stopwatch.StartNew();

        while (stopWatch.ElapsedMilliseconds < timeoutMs)
        {
            process.Refresh(); // Refresh the process state
            if (process.MainWindowHandle != IntPtr.Zero)
                return process.MainWindowHandle;

            await Task.Delay(50); // Non-blocking wait
        }

        throw new TimeoutException($"Could not find MainWindowHandle for {process.ProcessName} within {timeoutMs}ms");
    }

    /// <inheritdoc/>
    public async Task SetUpWindow(System.Diagnostics.Process process, ProcessWindowPreferences windowPreferences)
    {
        // wait until the process is fully loaded, otherwise manipulations don't work
        IntPtr mainWindowHandle = await WaitForMainWindowHandleAsync(process);

        SetWindowDimensions(
            windowPreferences.Width,
            windowPreferences.Height,
            windowPreferences.CoordX,
            windowPreferences.CoordY,
            windowPreferences.IsMaximized,
            mainWindowHandle);
    }

    /// <inheritdoc/>
    public void SetWindowDimensions(int width, int height, int coordX, int coordY, bool isMaximized, IntPtr hWnd)
    {
        // Firstly, move the window so that if it is restored by the user it has the right dimensions
        Win32Reference.MoveWindow(hWnd, coordX, coordY, width, height, true);
        
        SetWindowSWState(hWnd, isMaximized);
    }
    
    /// <inheritdoc/>
    public void SetWindowSWState(IntPtr hWnd, bool isMaximized)
    {
        // Then, restore or maximize the window
        if (isMaximized) Win32Reference.ShowWindowAsync(hWnd, (int)Win32Constants.SW.SHOWMAXIMIZED);
        else Win32Reference.ShowWindowAsync(hWnd, (int)Win32Constants.SW.RESTORE);
    }

    /// <inheritdoc/>
    public Task SetWindowMonitor(string monitor, IntPtr hWnd)
    {
        throw new System.NotImplementedException();
    }

    /// <inheritdoc/>
    public Task RegisterWindowHook()
    {
        throw new System.NotImplementedException();
    }
}