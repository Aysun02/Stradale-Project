using Web.Areas.Admin.ViewModels.Addition;

namespace Web.Areas.Admin.Services.Abstarct
{
    public interface IAdditionService
    {
        Task<AdditionIndexVM> GetAllAsync();
        Task<bool> CreateAsync(AdditionCreateVM model);
        Task<AdditionCreateVM> GetCreateModelAsync();
        Task<AdditionUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(AdditionUpdateVM model);
        Task DeleteAsync(int id);
    }
}
