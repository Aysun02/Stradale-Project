using WebApp.ViewModels.HomeMainSlider;

namespace WebApp.Services.Abstract
{
    public interface IHomeMainSliderService
    {
        Task<HomeMainSliderIndexVM> GetAllAsync();
        Task<bool> CreateAsync(HomeMainSliderCreateVM model);
     
        Task<bool> UpdateAsync(HomeMainSliderUpdateVM model);
        Task DeleteAsync(int id);
        Task<HomeMainSliderUpdateVM> GetUpdateModelAsync(int id);
    }
}
