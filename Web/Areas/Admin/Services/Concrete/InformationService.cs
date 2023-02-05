using Core.Entities;
using Core.Utilities.FileService;
using Data_Access.Contexts;
using Data_Access.Repositories.Abstarct;
using Data_Access.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.ViewModels.HomeGallery;
using Web.Areas.Admin.ViewModels.Information;

namespace Web.Areas.Admin.Services.Concrete
{
    public class InformationService : IInformationService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly IInformationRepository _informationRepository;
        private readonly IInfoPhotoRepository _infoPhotoRepository;
        private readonly ModelStateDictionary _modelState;

        public InformationService(AppDbContext context,
      IWebHostEnvironment webHostEnvironment,
      IFileService fileService,
      IInformationRepository informationRepository,
      IInfoPhotoRepository infoPhotoRepository,
         IActionContextAccessor actionContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _infoPhotoRepository = infoPhotoRepository;
            _informationRepository = informationRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<InformationIndexVM> GetAllAsync()
        {
            var model = new InformationIndexVM()
            {
                Informations = await _informationRepository.GetAllAsync()
            };
            return model;
        }


        public async Task<bool> CreateAsync(InformationCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _informationRepository.AnyAsync(i => i.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This Information already exist");
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

            var info = new Information
            {
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                Photo = await _fileService.UploadAsync(model.Photo, _webHostEnvironment.WebRootPath)

            };

            await _informationRepository.CreateAsync(info);
            return true;
        }


        public async Task DeleteAsync(int id)
        {
            var info = await _informationRepository.GetAsync(id);
            if (info != null)
            {
                foreach (var photo in await _infoPhotoRepository.GetAllAsync())
                {
                    _fileService.Delete(photo.Name, _webHostEnvironment.WebRootPath);
                }

                await _informationRepository.DeleteAsync(info);
            }
        }


        public async Task<bool> UpdateAsync(InformationUpdateVM model)
        {
            var isExist = await _informationRepository.AnyAsync(i => i.Title.Trim().ToLower() == model.Title.Trim().ToLower() && i.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This Information is already exist");
                return false;
            }
            var info = await _informationRepository.GetAsync(model.Id);
            if (info == null) return false;

            info.Title = model.Title;
            info.Description = model.Description;
            info.ModifiedAt = DateTime.Now;
            await _informationRepository.UpdateAsync(info);
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

            _fileService.Delete(info.Photo, _webHostEnvironment.WebRootPath);
            info.Photo = await _fileService.UploadAsync(model.Photo, _webHostEnvironment.WebRootPath);

            return true;
        }


        public async Task<InformationUpdateVM> GetUpdateModelAsync(int id)
        {
            var info = await _informationRepository.GetAsync(id);

            if (info != null)
            {
                var model = new InformationUpdateVM
                {
                    Id = info.Id,
                    Title = info.Title,
                    Description = info.Description,
                };
                return model;
            }
            return null;
        }
    }
}
