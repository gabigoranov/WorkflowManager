using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Workflow;

namespace WorkflowManager.ViewModels;

public partial class MainWindowViewModel : ViewModelBase
{
    private readonly IWorkflowService  _workflowService;
    public ReactiveCommand<Unit, Unit> CreateWorkflowCommand { get; }

    public MainWindowViewModel(IWorkflowService workflowService)
    {
        _workflowService = workflowService;

        CreateWorkflowCommand = ReactiveCommand.CreateFromTask(CreateWorkflowAsync);
    }

    private async Task CreateWorkflowAsync()
    {
        Debug.WriteLine("testing");
        await Task.CompletedTask;
    }
}
