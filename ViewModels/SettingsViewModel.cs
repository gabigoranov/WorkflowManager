using CommunityToolkit.Mvvm.ComponentModel;
using WorkflowManager.Services.Startup;
using WorkflowManager.Services.Theme;

namespace WorkflowManager.ViewModels;

public partial class SettingsViewModel(IStartupService startupService, IThemeService themeService) : ViewModelBase
{
    [ObservableProperty]
    private bool _isStartupEnabled = startupService.IsEnabled();
    
    [ObservableProperty]
    private bool _isDarkModeEnabled = true;

    // The generator calls this automatically when IsStartupEnabled changes
    partial void OnIsStartupEnabledChanged(bool value)
    {
        if (value) startupService.Enable();
        else startupService.Disable();
    }

    partial void OnIsDarkModeEnabledChanged(bool value)
    {
        themeService.SetTheme(value ? AppTheme.Dark : AppTheme.Light);
    }
}