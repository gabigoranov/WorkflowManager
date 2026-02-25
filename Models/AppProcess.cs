using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using Process = WorkflowManager.Models.Common.Process;

namespace WorkflowManager.Models;

/// <summary>
/// A subtype of the process class with specific properties for opening apps
/// </summary>
public class AppProcess : Process
{
    [Required]
    [StringLength(255)]
    public string Directory { get; set; }
    
    [StringLength(255)]
    public string? ArgumentDirectory { get; set; }
    
    /// <summary>
    /// Executes the app with the parameter if there is one
    /// </summary>
    /// <returns>The window handle if successful</returns>
    public override async Task<System.Diagnostics.Process?> Execute()
    {
        var startInfo = new ProcessStartInfo
        {
            FileName = Directory,
            UseShellExecute = true,
            CreateNoWindow = false
        };

        if (!string.IsNullOrWhiteSpace(ArgumentDirectory))
        {
            startInfo.Arguments = $"\"{ArgumentDirectory}\"";
        }

        return System.Diagnostics.Process.Start(startInfo);
    }
}