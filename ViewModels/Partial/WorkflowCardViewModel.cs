using System;
using System.Threading.Tasks;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using WorkflowManager.Models;
using WorkflowManager.Models.Common;
using WorkflowManager.Services.Navigation;
using WorkflowManager.Services.Window;
using WorkflowManager.Services.Workflow;
using WorkflowManager.Services.WorkflowState;

namespace WorkflowManager.ViewModels.Partial;

public partial class WorkflowCardViewModel(
    Workflow workflow,
    IWorkflowService workflowService,
    IWorkflowStateService workflowState,
    IMapper mapper,
    INavigationService navigation,
    IWindowService windowService,
    Action<WorkflowCardViewModel> onDeleteRequested)
    : ObservableObject
{
    [ObservableProperty] private Workflow _workflow = workflow;
    [ObservableProperty] private bool _isExecutingWorkflow;

    /// <summary>
    /// Navigates to edit workflow view
    /// </summary>
    [RelayCommand]
    private void EditWorkflow()
    {
        workflowState.SelectedWorkflow = Workflow;
        navigation.Navigate<WorkflowEditorViewModel>();
    }

    [RelayCommand]
    private async Task DeleteWorkflow()
    {
        await workflowService.DeleteWorkflowAsync(Workflow.Id);
        onDeleteRequested.Invoke(this);
    }

    /// <summary>
    /// Executes each process in the workflow
    /// </summary>
    [RelayCommand]
    private async Task StartWorkflow()
    {
        if (IsExecutingWorkflow) return;

        try
        {
            IsExecutingWorkflow = true;

            foreach (var step in Workflow.Processes)
            {
                System.Diagnostics.Process? process = await step.Execute();
                ProcessWindowPreferences prefs = mapper.Map<ProcessWindowPreferences>(step);
                if(process != null)
                {
                    await windowService.SetUpWindow(process, prefs);
                }
                    
            }

            Workflow = await workflowService.UpdateWorkflowLastStartupAsync(Workflow.Id);
        }
        finally
        {
            IsExecutingWorkflow = false;
        }
    }
}