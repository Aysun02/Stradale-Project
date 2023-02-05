using Core.Entities;
using Core.Utilities.FileService;
using Data_Access.Contexts;
using Data_Access.Repositories.Abstarct;
using Data_Access.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Numerics;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.ViewModels.Gallery;

namespace Web.Areas.Admin.Services.Concrete
{
    public class GalleryService : IGalleryService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly IGalleryElementRepository _galleryElementRepository;
        private readonly IGalleryPhotoRepository _galleryPhotoRepository;
        private readonly ModelStateDictionary _modelState;

        public GalleryService(AppDbContext context,
            IWebHostEnvironment webHostEnvironment,
            IFileService fileService,
            IGalleryElementRepository galleryElementRepository,
          IGalleryPhotoRepository galleryPhotoRepository,
               IActionContextAccessor actionContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _galleryPhotoRepository = galleryPhotoRepository;
            _galleryElementRepository = galleryElementRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }
        public async Task<GalleryIndexVM> GetAllAsync()
        {
            var model = new GalleryIndexVM()
            {
                GalleryElements = await _galleryElementRepository.GetAllAsync()
            };
            return model;
        }

        public async Task<bool> CreateAsync(GalleryCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _galleryElementRepository.AnyAsync(g => g.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This Title already is exist");
                return false;
            }
            if (!_fileService.IsImage(model.Photo))
            {
                _modelState.AddModelError("MainPhotoName", "File must be img formatt");
                return false;

            }
            if (!_fileService.CheckSize(model.Photo, 500))
            {
                _modelState.AddModelError("MainPhoto", "File Size is More than Requested");
                return false;

            }

            var element = new GalleryElement
            {
                Title = model.Title,
                CreatedAt = DateTime.Now,
                Photo = await _fileService.UploadAsync(model.Photo, _webHostEnvironment.WebRootPath)

            };

            await _galleryElementRepository.CreateAsync(element);
            return true;
        }

        public async Task DeleteAsync(int id)
        {
            var element = await _galleryElementRepository.GetAsync(id);
            if (element != null)
            {
                foreach (var photo in await _galleryPhotoRepository.GetAllAsync())
                {
                    _fileService.Delete(photo.Name, _webHostEnvironment.WebRootPath);
                }

                await _galleryElementRepository.DeleteAsync(element);
            }
        }

        public async Task<bool> UpdateAsync(GalleryUpdateVM model)
        {
            var isExist = await _galleryElementRepository.AnyAsync(g => g.Title.Trim().ToLower() == model.Title.Trim().ToLower() && g.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This Title already is exist");
                return false;
            }
            var element = await _galleryElementRepository.GetAsync(model.Id);
            if (element == null) return false;

            element.Title = model.Title;
            element.ModifiedAt = DateTime.Now;
            await _galleryElementRepository.UpdateAsync(element);
            if (model.Photo != null)


                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Photo", "Please choose Image format");
                    return false;
                }
            if (!_fileService.CheckSize(model.Photo, 300))
            {
                _modelState.AddModelError("Photo", "Image size is more than 300kB");
                return false;
            }

            _fileService.Delete(element.Photo, _webHostEnvironment.WebRootPath);
            element.Photo = await _fileService.UploadAsync(model.Photo, _webHostEnvironment.WebRootPath);

            return true;
        }
        public async Task<GalleryUpdateVM> GetUpdateModelAsync(int id)
        {
            var element = await _galleryElementRepository.GetAsync(id);

            if (element != null)
            {
                var model = new GalleryUpdateVM
                {
                    Id = element.Id,
                    Title = element.Title,
                };
                return model;
            }
            return null;
        }

    }
}
