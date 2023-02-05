using Web.Areas.Admin.ViewModels.Information;

namespace Web.Areas.Admin.Services.Abstarct
{
    public interface IInformationService
    {
        Task<InformationIndexVM> GetAllAsync();
        Task<bool> CreateAsync(InformationCreateVM model);
        Task<InformationUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(InformationUpdateVM model);
        Task DeleteAsync(int id);
    }
}
