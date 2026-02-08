using Avalonia;

namespace WorkflowManager.Services.Theme;

public interface IThemeService
{

    /// <summary>
    /// Initializes the theme service with the current application
    /// </summary>
    /// <param name="app">The application instance</param>
    void Initialize(Application app);
    
    /// <summary>
    /// Updates the app theme and dynamically updates the resources in app.axaml
    /// </summary>
    /// <param name="theme"></param>
    void SetTheme(AppTheme theme);
    
    /// <summary>
    /// The current app theme
    /// </summary>
    AppTheme CurrentTheme { get; }
}