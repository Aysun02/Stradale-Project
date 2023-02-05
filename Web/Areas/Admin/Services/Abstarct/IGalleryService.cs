using Web.Areas.Admin.ViewModels.Gallery;

namespace Web.Areas.Admin.Services.Abstarct
{
    public interface IGalleryService
    {
        Task<GalleryIndexVM> GetAllAsync();
        Task<bool> CreateAsync(GalleryCreateVM model);
        Task<GalleryUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(GalleryUpdateVM model);
        Task DeleteAsync(int id);
    }
}
