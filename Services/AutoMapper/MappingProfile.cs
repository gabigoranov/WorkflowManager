using AutoMapper;
using WorkflowManager.Models;
using WorkflowManager.Models.Common;
using WorkflowManager.ViewModels.Binding;

namespace WorkflowManager.Services.AutoMapper;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        // ============================================================
        // 1. POLYMORPHIC MAPPING (Database Entities <-> Binding Models)
        // ============================================================
        
        // This is the critical part for SetupEditMode. 
        // When mapping from a List<Process>, these .Include calls tell AutoMapper
        // to look for the specific derived types instead of just the base class.
        CreateMap<Models.Common.Process, ProcessBindingModel>()
            .Include<CommandProcess, CommandProcessBindingModel>()
            .Include<WebsiteProcess, WebsiteProcessBindingModel>();

        // The reverse: when saving back to the database
        CreateMap<ProcessBindingModel, Models.Common.Process>()
            .Include<CommandProcessBindingModel, CommandProcess>()
            .Include<WebsiteProcessBindingModel, WebsiteProcess>();

        // Specific mappings for the derived types
        CreateMap<CommandProcess, CommandProcessBindingModel>().ReverseMap();
        CreateMap<WebsiteProcess, WebsiteProcessBindingModel>().ReverseMap();

        // ============================================================
        // 2. SWAPPING MAPS (For HandleDiscriminatorChange)
        // ============================================================
        
        // Allows the UI to preserve shared fields (like Title/Description) 
        // when a user changes the Process Type in the dropdown.
        CreateMap<CommandProcessBindingModel, WebsiteProcessBindingModel>()
            .ForMember(dest => dest.Discriminator, opt => opt.MapFrom(_ => ProcessType.WebsiteProcess));

        CreateMap<WebsiteProcessBindingModel, CommandProcessBindingModel>()
            .ForMember(dest => dest.Discriminator, opt => opt.MapFrom(_ => ProcessType.CommandProcess));

        // ============================================================
        // 3. CLONING & UTILITY
        // ============================================================

        // Used when clicking "Edit" to create a working copy of a model 
        // so changes don't affect the list until "Save" is clicked.
        CreateMap<CommandProcessBindingModel, CommandProcessBindingModel>();
        CreateMap<WebsiteProcessBindingModel, WebsiteProcessBindingModel>();
        
        // Base-to-Derived Utility (Safety nets for casting)
        CreateMap<ProcessBindingModel, CommandProcessBindingModel>();
        CreateMap<ProcessBindingModel, WebsiteProcessBindingModel>();
        
        // Generic clone
        CreateMap<ProcessBindingModel, ProcessBindingModel>()
            .IncludeAllDerived();
    }
}