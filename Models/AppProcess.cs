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
    public override async Task Execute()
    {
        await Task.Run(() =>
        {
            try
            {
                ProcessStartInfo startInfo = new ProcessStartInfo
                {
                    FileName = Directory,
                    // -NoProfile: prevents loading user profiles for speed
                    // -Command: tells PS to execute the following string and exit
                    Arguments = $"-NoProfile {ArgumentDirectory}",
                    UseShellExecute = false,
                    CreateNoWindow = true, // Set to false if you want to see the popup
                    RedirectStandardOutput = true,
                    RedirectStandardError = true
                };

                using (System.Diagnostics.Process process = System.Diagnostics.Process.Start(startInfo)!)
                {
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    if (process.ExitCode != 0)
                    {
                        Debug.WriteLine($"Error: {error}");
                    }
                    else
                    {
                        Debug.WriteLine($"Output: {output}");
                    }
                }
            }
            catch (Exception ex)
            {
                Debug.WriteLine($"Failed to run PowerShell: {ex.Message}");
            }
        });
    }
}