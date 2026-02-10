using System.ComponentModel.DataAnnotations;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkflowManager.Models.Common;

namespace WorkflowManager.ViewModels.Binding;

public partial class WebsiteProcessBindingModel : ProcessBindingModel
{
    public WebsiteProcessBindingModel()
    {
        Discriminator = ProcessType.WebsiteProcess;
    }

    [ObservableProperty]
    [Required] 
    private string _uRL;
}