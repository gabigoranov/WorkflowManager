using System.Threading.Tasks;

namespace WorkflowManager.Services.Dialog;

public interface IDialogService
{
    /// <summary>
    /// Opens a dialog window to select a folder/directory.
    /// </summary>
    /// <returns>The directory path represented as string, or null if canceled.</returns>
    Task<string?> SelectFolderAsync();
    
    /// <summary>
    /// Prompts the user to select an app.
    /// </summary>
    /// <returns>The path to the selected app or null</returns>
    Task<string?> PickExecutableAsync();

    /// <summary>
    /// Picks a document or file to be opened by a process.
    /// </summary>
    /// <returns>The path to the selected document or null</returns>
    Task<string?> PickDocumentAsync();

}
