using Data_Access.Repositories.Abstarct;
using Data_Access.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Web.Areas.Admin.Services.Abstarct;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class GalleryElementService : IGalleryElementService
    {
        private readonly IGalleryElementRepository _galleryElementRepository;

        public GalleryElementService(IGalleryElementRepository galleryElementRepository, IActionContextAccessor actionContextAccessor)
        {
            _galleryElementRepository = galleryElementRepository;
        }

        public async Task<GalleryIndexVM> GetAllAsync()
        {
            var model = new GalleryIndexVM
            {
                GalleryElements = await _galleryElementRepository.GetAllAsync(),
            };
            return model;
        }
    }
}
