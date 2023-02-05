using Web.Areas.Admin.ViewModels.Drink;

namespace Web.Areas.Admin.Services.Abstarct
{
    public interface IDrinkService
    {
        Task<DrinkIndexVM> GetAllAsync();
        Task<bool> CreateAsync(DrinkCreateVM model);
        Task<DrinkCreateVM> GetCreateModelAsync();
        Task<DrinkUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(DrinkUpdateVM model);
        Task DeleteAsync(int id);
    }
}
