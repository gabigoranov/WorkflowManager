using System;
using System.Collections.Generic;
using Microsoft.Extensions.DependencyInjection;
using WorkflowManager.Models.Common;
using WorkflowManager.Services.Dialog;
using WorkflowManager.ViewModels.Binding;

namespace WorkflowManager.ViewModels.Common;

/// <summary>
/// A centralized static declaration of all the possible process form binding models.
/// </summary>
public class ProcessRegistry(IServiceProvider provider)
{
    public ProcessBindingModel Create(ProcessType type)
    {
        return type switch
        {
            ProcessType.CommandProcess => provider.GetRequiredService<CommandProcessBindingModel>(),
            ProcessType.WebsiteProcess => provider.GetRequiredService<WebsiteProcessBindingModel>(),
            ProcessType.AppProcess => provider.GetRequiredService<AppProcessBindingModel>(),
            _ => throw new ArgumentOutOfRangeException(nameof(type))
        };
    }
}

