using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using System.Threading.Tasks;
using WorkflowManager.Models.Common;
using Process = WorkflowManager.Models.Common.Process;

namespace WorkflowManager.Models;

public class WebsiteProcess : Process
{
    public WebsiteProcess()
    {
        Discriminator = ProcessType.WebsiteProcess;
    }
    
    [Required]
    public string URL { get; set; }
    
    public override Task Execute()
    {
        // Use ProcessStartInfo to enable ShellExecute
        var psi = new ProcessStartInfo
        {
            FileName = URL,
            UseShellExecute = true
        };

        System.Diagnostics.Process.Start(psi);
        
        return Task.CompletedTask;
    }
}