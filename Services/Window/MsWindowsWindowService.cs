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
    private static async Task<IntPtr> WaitForMainWindowHandleAsync(System.Diagnostics.Process process, int timeoutMs = 10000)
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
        // wait until the process is fully loaded, otherwise manipulations dont work
        await WaitForMainWindowHandleAsync(process);
        
        await SetWindowDimensions(
            windowPreferences.Width,
            windowPreferences.Height, 
            windowPreferences.CoordX,
            windowPreferences.CoordY,
            windowPreferences.IsMaximized,
            process.MainWindowHandle);   
    }

    /// <inheritdoc/>
    public Task SetWindowDimensions(int width, int height, int coordX, int coordY, bool isMaximized, IntPtr hWnd)
    {
        bool isSuccessful = Win32Reference.MoveWindow(hWnd, coordX, coordY, width, height, true);
        return Task.CompletedTask;
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