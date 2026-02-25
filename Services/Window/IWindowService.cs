using System;
using System.Threading.Tasks;
using WorkflowManager.Models.Common;

namespace WorkflowManager.Services.Window;

/// <summary>
/// Responsible for finding, managing and hooking on to windows
/// Designed to be implemented for multiple operating systems
/// </summary>
public interface IWindowService
{
    /// <summary>
    /// A centralized method to organize everything for the window of a process
    /// </summary>
    /// <param name="windowPreferences">The preferences - eg: IsMaximized</param>
    /// <param name="process">The system diagnostics process that has been launched</param>
    /// <returns></returns>
    public Task SetUpWindow(System.Diagnostics.Process process, ProcessWindowPreferences windowPreferences);

    /// <summary>
    /// Sets window dimensions
    /// </summary>
    /// <param name="width">The width of the window in px</param>
    /// <param name="height">The height of the window in px</param>
    /// <param name="coordX">The distance from the left edge of the screen in px</param>
    /// <param name="coordY">The distance from the top edge of the screen in px</param>
    /// <param name="isMaximized">If the window should be maximized</param>
    /// <param name="hWnd">The window handle to use for managing the window</param>
    /// <returns></returns>
    public void SetWindowDimensions(int width, int height, int coordX, int coordY, bool isMaximized, IntPtr hWnd);

    /// <summary>
    /// A helper function to set the window SW state ( maximized, restored, minimized, etc )
    /// </summary>
    /// <param name="hWnd">The window handler</param>
    /// <param name="isMaximized">Whether it should be maximized or resstored</param>
    public void SetWindowSWState(IntPtr hWnd, bool isMaximized);
    

    /// <summary>
    /// Defines in which monitor a window should be opened
    /// </summary>
    /// <param name="monitor">The monitor info</param>
    /// <param name="hWnd">The window handle to use for managing the window</param>
    /// <returns></returns>
    public Task SetWindowMonitor(string monitor, IntPtr hWnd);

    public Task RegisterWindowHook();


}