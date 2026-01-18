using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Reactive;
using System.Threading.Tasks;
using Avalonia.Controls;
using ReactiveUI;
using WorkflowManager.Models;
using WorkflowManager.Services.Common.Workflow;
using Process = WorkflowManager.Models.Process;

namespace WorkflowManager.ViewModels;

public partial class HomeViewModel : ViewModelBase
{
    private readonly IWorkflowService  _workflowService;
    private ObservableCollection<Workflow> _workflows = new();

    public ObservableCollection<Workflow> Workflows
    {
        get => _workflows;
        set => this.RaiseAndSetIfChanged(ref _workflows, value);
    }
    public ReactiveCommand<Unit, Unit> CreateWorkflowCommand { get; }
    public ReactiveCommand<Unit, Unit> EditWorkflowCommand { get; }
    public ReactiveCommand<Unit, Unit> DeleteWorkflowCommand { get; }

    public HomeViewModel(IWorkflowService workflowService)
    {
        _workflowService = workflowService;
        _workflows = new ObservableCollection<Workflow>(GenerateWorkflows());
        
        CreateWorkflowCommand = ReactiveCommand.CreateFromTask(CreateWorkflowAsync);
        EditWorkflowCommand = ReactiveCommand.CreateFromTask(EditWorkflowAsync);
        DeleteWorkflowCommand = ReactiveCommand.CreateFromTask(DeleteWorkflowASync);
    }

    private async Task CreateWorkflowAsync()
    {
        Debug.WriteLine("testing");
        await Task.CompletedTask;
    }
    
    private async Task EditWorkflowAsync()
    {
        Debug.WriteLine("testing");
        await Task.CompletedTask;
    }
    
    private async Task DeleteWorkflowASync()
    {
        Debug.WriteLine("testing");
        await Task.CompletedTask;
    }
    
    public static List<Workflow> GenerateWorkflows(int workflowCount = 5, int processesPerWorkflow = 3)
    {
        var workflows = new List<Workflow>();

        for (int i = 1; i <= workflowCount; i++)
        {
            var workflow = new Workflow
            {
                Id = i,
                Title = $"Workflow {i}",
                Status = i % 2 == 0 ? "Completed" : "Draft",
                Processes = new List<Process>()
            };

            for (int j = 1; j <= processesPerWorkflow; j++)
            {
                var process = new Process
                {
                    Id = (i - 1) * processesPerWorkflow + j,
                    Title = $"Process {j} of Workflow {i}",
                    Directory = $@"C:\Workflows\Workflow{i}\Process{j}",
                    WorkflowId = workflow.Id,
                    Workflow = workflow,
                    Command = j % 2 == 0 ? $"echo Process {j}" : null
                };

                workflow.Processes.Add(process);
            }

            workflows.Add(workflow);
        }

        return workflows;
    }
    
    
}