using System.Collections.ObjectModel;
using System.Linq;
using AutoMapper;
using CommunityToolkit.Mvvm.ComponentModel;
using WorkflowManager.Services.Navigation;
using WorkflowManager.Services.Window;
using WorkflowManager.Services.Workflow;
using WorkflowManager.Services.WorkflowState;

namespace WorkflowManager.ViewModels.Partial;

public partial class WorkflowListViewModel : ViewModelBase
{
    [ObservableProperty] private ObservableCollection<WorkflowCardViewModel> _workflowCards;

    public WorkflowListViewModel(
        IWorkflowService workflowService,
        IWorkflowStateService workflowState,
        INavigationService navigation,
        IMapper mapper,
        IWindowService windowService)
    {
        var models = workflowService.GetAllWorkflows();

        var viewModels = models.Select(w => new WorkflowCardViewModel(
            w,
            workflowService,
            workflowState,
            mapper,
            navigation,
            windowService,
            OnCardDeleted)
        );

        _workflowCards = new ObservableCollection<WorkflowCardViewModel>(viewModels);
    }

    private void OnCardDeleted(WorkflowCardViewModel card)
    {
        WorkflowCards.Remove(card);
    }
}