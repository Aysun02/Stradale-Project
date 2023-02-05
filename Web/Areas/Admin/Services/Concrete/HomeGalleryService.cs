using Core.Entities;
using Core.Utilities.FileService;
using Data_Access.Contexts;
using Data_Access.Repositories.Abstarct;
using Data_Access.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.ViewModels.BlogPost;
using Web.Areas.Admin.ViewModels.HomeGallery;

namespace Web.Areas.Admin.Services.Concrete
{
    public class HomeGalleryService : IHomeGalleryService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly IHomeGalleryRepository _homeGalleryRepository;
        private readonly IHomeGalleryPhotoRepository _homeGalleryPhotoRepository;
        private readonly ModelStateDictionary _modelState;

        public HomeGalleryService(AppDbContext context,
      IWebHostEnvironment webHostEnvironment,
      IFileService fileService,
      IHomeGalleryRepository homeGalleryRepository,
      IHomeGalleryPhotoRepository homeGalleryPhotoRepository,
         IActionContextAccessor actionContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _homeGalleryPhotoRepository = homeGalleryPhotoRepository;
            _homeGalleryRepository = homeGalleryRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<HomeGalleryIndexVM> GetAllAsync()
        {
            var model = new HomeGalleryIndexVM()
            {
                HomeGalleries = await _homeGalleryRepository.GetAllAsync()
            };
            return model;
        }


        public async Task<bool> CreateAsync(HomeGalleryCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _homeGalleryRepository.AnyAsync(h => h.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This Gallery Item already exist");
                return false;
            }
            if (!_fileService.IsImage(model.Photo))
            {
                _modelState.AddModelError("MainPhotoName", "File must be image format");
                return false;

            }
            if (!_fileService.CheckSize(model.Photo, 500))
            {
                _modelState.AddModelError("MainPhoto", "File size must be less than 500kB");
                return false;
            }

            var element = new HomeGallery
            {
                Title = model.Title,
                Ordinal = model.Ordinal,
                CreatedAt = DateTime.Now,
                Photo = await _fileService.UploadAsync(model.Photo, _webHostEnvironment.WebRootPath)

            };

            await _homeGalleryRepository.CreateAsync(element);
            return true;
        }


        public async Task DeleteAsync(int id)
        {
            var element = await _homeGalleryRepository.GetAsync(id);
            if (element != null)
            {
                foreach (var photo in await _homeGalleryPhotoRepository.GetAllAsync())
                {
                    _fileService.Delete(photo.Name, _webHostEnvironment.WebRootPath);
                }

                await _homeGalleryRepository.DeleteAsync(element);
            }
        }


        public async Task<bool> UpdateAsync(HomeGalleryUpdateVM model)
        {
            var isExist = await _homeGalleryRepository.AnyAsync(h => h.Title.Trim().ToLower() == model.Title.Trim().ToLower() && h.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This Gallery is already exist");
                return false;
            }
            var gallery = await _homeGalleryRepository.GetAsync(model.Id);
            if (gallery == null) return false;

            gallery.Title = model.Title;
            gallery.Ordinal = model.Ordinal;
            gallery.ModifiedAt = DateTime.Now;
            await _homeGalleryRepository.UpdateAsync(gallery);
            if (model.Photo != null)


                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Photo", "Please Choose Image format");
                    return false;
                }
            if (!_fileService.CheckSize(model.Photo, 300))
            {
                _modelState.AddModelError("Photo", "Photo`s size is bigger than 300kB");
                return false;
            }

            _fileService.Delete(gallery.Photo, _webHostEnvironment.WebRootPath);
            gallery.Photo = await _fileService.UploadAsync(model.Photo, _webHostEnvironment.WebRootPath);

            return true;
        }


        public async Task<HomeGalleryUpdateVM> GetUpdateModelAsync(int id)
        {
            var gallery = await _homeGalleryRepository.GetAsync(id);

            if (gallery != null)
            {
                var model = new HomeGalleryUpdateVM
                {
                    Id = gallery.Id,
                    Title = gallery.Title,
                    Ordinal = gallery.Ordinal,
                };
                return model;
            }
            return null;
        }
    }
}
