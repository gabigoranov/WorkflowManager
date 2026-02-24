
using System;
using Avalonia.Controls;
using System.Threading.Tasks;
using Avalonia.Platform.Storage;

namespace WorkflowManager.Services.Dialog;

// Instead of injecting a Window directly, inject a function that returns the main window
public class DialogService(Func<Avalonia.Controls.Window> getWindow) : IDialogService
{
    /// <inheritdoc/>
    public async Task<string?> SelectFolderAsync()
    {
        var dialog = new OpenFolderDialog { Title = "Select a folder" };
        return await dialog.ShowAsync(getWindow());
    }
    
    public async Task<string?> PickExecutableAsync()
    {
        // Try to get the standard Program Files path
        var programFiles = await getWindow().StorageProvider.TryGetFolderFromPathAsync(
            Environment.GetFolderPath(Environment.SpecialFolder.ProgramFiles));

        var files = await getWindow().StorageProvider.OpenFilePickerAsync(
            new FilePickerOpenOptions
            {
                Title = "Select Application",
                SuggestedStartLocation = programFiles, // Start where the apps are
                AllowMultiple = false,
                FileTypeFilter =
                [
                    new FilePickerFileType("Applications (*.exe, *.lnk)")
                    {
                        Patterns = ["*.exe", "*.lnk"],
                        MimeTypes = ["application/x-msdownload"]
                    }
                ]
            });

        return files.Count > 0 ? files[0].Path.LocalPath : null;
    }
    
    /// <inheritdoc/>
    public async Task<string?> PickDocumentAsync()
    {
        var userDocs = await getWindow().StorageProvider.TryGetFolderFromPathAsync(
            Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments));

        var files = await getWindow().StorageProvider.OpenFilePickerAsync(new FilePickerOpenOptions
        {
            Title = "Select Document or File",
            SuggestedStartLocation = userDocs,
            AllowMultiple = false,
            FileTypeFilter =
            [
                // Common Documents
                new FilePickerFileType("All Documents") 
                { 
                    Patterns = ["*.pdf", "*.txt", "*.docx", "*.xlsx", "*.pptx", "*.csv"]
                },
                // Media / Creative
                new FilePickerFileType("Images & Media") 
                { 
                    Patterns = ["*.png", "*.jpg", "*.jpeg", "*.mp4", "*.mov", "*.blend"]
                },
                // Catch-all
                FilePickerFileTypes.All
            ]
        });

        return files.Count > 0 ? files[0].Path.LocalPath : null;
    }
}
