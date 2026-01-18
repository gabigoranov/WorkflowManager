using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using ReactiveUI;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Workflow;

namespace WorkflowManager.ViewModels;

public class CreateWorkflowViewModel : ViewModelBase
{
    private readonly IWorkflowService  _workflowService;
    private ObservableCollection<Workflow> _workflows = new();

    public ObservableCollection<Workflow> Workflows
    {
        get => _workflows;
        set => this.RaiseAndSetIfChanged(ref _workflows, value);
    }
    public ReactiveCommand<Unit, Unit> CreateWorkflowCommand { get; }

    public CreateWorkflowViewModel(IWorkflowService workflowService)
    {
        _workflowService = workflowService;

        CreateWorkflowCommand = ReactiveCommand.CreateFromTask(CreateWorkflowAsync);
    }

    private async Task CreateWorkflowAsync()
    {
        // The create button is clicked, therefore the form is filled out
        
        Debug.WriteLine("testing");
        await Task.CompletedTask;
    }
}