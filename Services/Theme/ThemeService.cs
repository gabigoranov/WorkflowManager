using System;
using System.IO;
using System.Linq;
using System.Text.Json;
using Avalonia;
using Avalonia.Markup.Xaml.Styling;
using Avalonia.Styling;

namespace WorkflowManager.Services.Theme;

/// <summary>
/// Defines the available visual themes for the application.
/// </summary>
public enum AppTheme { Light, Dark }

/// <summary>
/// Service responsible for managing the application's visual theme, 
/// handling live style-swapping, and persisting user preferences.
/// </summary>
public class ThemeService : IThemeService
{
    // Asset paths using the 'avares' protocol to point to internal project resources
    private const string DarkThemePath = "avares://WorkflowManager/Styles/Themes/Dark.axaml";
    private const string LightThemePath = "avares://WorkflowManager/Styles/Themes/Light.axaml";
    
    // Path where the user's theme preference is saved on disk
    private readonly string _configPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "themeSettings.json");
    
    // Reference to the main Application instance required to modify global resources
    private Application? _app;

    /// <summary>
    /// Gets the currently active theme.
    /// </summary>
    public AppTheme CurrentTheme { get; private set; }

    /// <summary>
    /// Connects the service to the Application and applies the user's saved theme.
    /// Should be called during App.OnFrameworkInitializationCompleted.
    /// </summary>
    /// <param name="app">The current Avalonia Application instance.</param>
    public void Initialize(Application app)
    {
        _app = app;
        // Load the preference from disk and apply it immediately to prevent "white flash" on startup
        ApplyTheme(LoadSavedTheme());
    }

    /// <summary>
    /// Switches the application theme and saves the choice to disk.
    /// </summary>
    /// <param name="theme">The theme to switch to.</param>
    public void SetTheme(AppTheme theme)
    {
        // Avoid unnecessary work if the theme isn't actually changing
        if (CurrentTheme == theme) return;
        
        ApplyTheme(theme);
        SaveTheme(theme);
    }

    /// <summary>
    /// Performs the heavy lifting of swapping ResourceDictionaries at runtime.
    /// </summary>
    private void ApplyTheme(AppTheme theme)
    {
        if (_app == null) return;

        CurrentTheme = theme;

        // 1. Update the 'RequestedThemeVariant'. 
        // This tells Avalonia's built-in controls (like Buttons and TextBoxes) 
        // whether they should use their internal Dark or Light defaults.
        _app.RequestedThemeVariant = theme == AppTheme.Dark ? ThemeVariant.Dark : ThemeVariant.Light;

        // 2. Access the 'MergedDictionaries'.
        // In your App.axaml, resources are organized in a hierarchy. We need to reach
        // into that list to swap out your custom color/brush definitions.
        var mergedDicts = _app.Resources.MergedDictionaries;

        // 3. Cleanup: Find and remove the "Old" theme file.
        // We filter the list for any ResourceInclude that points to our theme folder.
        var existingThemes = mergedDicts.OfType<ResourceInclude>()
            .Where(r => r.Source != null && 
                        (r.Source.OriginalString.Contains("Themes/Dark.axaml") || 
                         r.Source.OriginalString.Contains("Themes/Light.axaml")))
            .ToList();

        foreach (var themeDict in existingThemes)
        {
            mergedDicts.Remove(themeDict);
        }

        // 4. Injection: Add the "New" theme file.
        var uriString = theme == AppTheme.Dark ? DarkThemePath : LightThemePath;
    
        // We create a new ResourceInclude pointing to the .axaml file
        var newTheme = new ResourceInclude(new Uri("avares://WorkflowManager/Styles"))
        {
            Source = new Uri(uriString)
        };

        // We insert at Index 0. This ensures that these theme colors are loaded first,
        // allowing other dictionaries (like Border.axaml or Spacing.axaml) to 
        // reference the colors defined within the theme file.
        mergedDicts.Insert(0, newTheme);
    }

    /// <summary>
    /// Persists the theme selection to a JSON file.
    /// </summary>
    private void SaveTheme(AppTheme theme)
    {
        try 
        {
            var json = JsonSerializer.Serialize(new ThemeConfig { SelectedTheme = theme.ToString() });
            File.WriteAllText(_configPath, json);
        }
        catch (Exception ex)
        {
            // Logging would go here in a production app
            Console.WriteLine($"Failed to save theme: {ex.Message}");
        }
    }

    /// <summary>
    /// Reads the theme selection from the JSON file. Defaults to Dark if file is missing/corrupt.
    /// </summary>
    private AppTheme LoadSavedTheme()
    {
        if (!File.Exists(_configPath)) return AppTheme.Dark;
        
        try
        {
            var json = File.ReadAllText(_configPath);
            var data = JsonSerializer.Deserialize<ThemeConfig>(json);
            
            return Enum.TryParse<AppTheme>(data?.SelectedTheme, out var theme) 
                ? theme 
                : AppTheme.Dark;
        }
        catch 
        { 
            return AppTheme.Dark; 
        }
    }

    /// <summary>
    /// Simple DTO (Data Transfer Object) for JSON serialization.
    /// </summary>
    private class ThemeConfig 
    { 
        public string SelectedTheme { get; set; } = "Dark"; 
    }
}