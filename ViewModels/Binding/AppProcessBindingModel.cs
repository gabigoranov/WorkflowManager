using System;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models.Common;
using Microsoft.Win32;
using System.ComponentModel.DataAnnotations;
using System.Threading.Tasks;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using Microsoft.Win32;
using WorkflowManager.Models.Common;
using WorkflowManager.Services.Dialog;
using WorkflowManager.Services.Process;
using WorkflowManager.Views;
using Process = WorkflowManager.Models.Common.Process;

namespace WorkflowManager.ViewModels.Binding;

/// <summary>
/// A Binding model that expands upon the ProcessBindingModel.
/// </summary>
public partial class AppProcessBindingModel : ProcessBindingModel
{
    public AppProcessBindingModel()
    {
        Discriminator = ProcessType.CommandProcess;
    }
    
    [ObservableProperty]
    [Required(ErrorMessage = "A directory is required")]
    private string _directory = string.Empty;

    [ObservableProperty]
    private string? _argumentDirectory = string.Empty;
    
}