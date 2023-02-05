using Web.Areas.Admin.ViewModels.HomeGallery;

namespace Web.Areas.Admin.Services.Abstarct
{
    public interface IHomeGalleryService
    {
        Task<HomeGalleryIndexVM> GetAllAsync();
        Task<bool> CreateAsync(HomeGalleryCreateVM model);
        Task<HomeGalleryUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(HomeGalleryUpdateVM model);
        Task DeleteAsync(int id);
    }
}
