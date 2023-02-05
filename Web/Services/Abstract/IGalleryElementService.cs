using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IGalleryElementService
    {
        Task<GalleryIndexVM> GetAllAsync();
    }
}
