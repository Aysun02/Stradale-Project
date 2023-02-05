using Core.Entities;
using Core.Utilities.FileService;
using Data_Access.Contexts;
using Data_Access.Repositories.Abstarct;
using Data_Access.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.ViewModels.Information;
using Web.Areas.Admin.ViewModels.Offer;

namespace Web.Areas.Admin.Services.Concrete
{
    public class OfferService : IOfferService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly IOfferRepository _offerRepository;
        private readonly IOfferPhotoRepository _offerPhotoRepository;
        private readonly ModelStateDictionary _modelState;

        public OfferService(AppDbContext context,
      IWebHostEnvironment webHostEnvironment,
      IFileService fileService,
      IOfferRepository offerRepository,
      IOfferPhotoRepository offerPhotoRepository,
         IActionContextAccessor actionContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _offerPhotoRepository = offerPhotoRepository;
            _offerRepository = offerRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<OfferIndexVM> GetAllAsync()
        {
            var model = new OfferIndexVM()
            {
                Offers = await _offerRepository.GetAllAsync()
            };
            return model;
        }


        public async Task<bool> CreateAsync(OfferCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _offerRepository.AnyAsync(o => o.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This Offer already exist");
                return false;
            }
            if (!_fileService.IsImage(model.Photo1) && _fileService.IsImage(model.Photo1))
            {
                _modelState.AddModelError("MainPhotoName", "Files must be image format");
                return false;

            }
            if (!_fileService.CheckSize(model.Photo1, 500) && !_fileService.CheckSize(model.Photo2, 500))
            {
                _modelState.AddModelError("MainPhoto", "File size must be less than 500kB");
                return false;
            }

            var offer = new Offer
            {
                Title = model.Title,
                Text = model.Text,
                CoffeeType1 = model.CoffeeType1,
                Offer1 = model.Offer1,
                Photo1 = await _fileService.UploadAsync(model.Photo1, _webHostEnvironment.WebRootPath),
                CoffeeType2 = model.CoffeeType2,
                Offer2 = model.Offer2,
                Photo2 = await _fileService.UploadAsync(model.Photo2, _webHostEnvironment.WebRootPath),
                CreatedAt = DateTime.Now
            };

            await _offerRepository.CreateAsync(offer);
            return true;
        }


        public async Task DeleteAsync(int id)
        {
            var offer = await _offerRepository.GetAsync(id);
            if (offer != null)
            {
                foreach (var photo in await _offerPhotoRepository.GetAllAsync())
                {
                    _fileService.Delete(photo.Name, _webHostEnvironment.WebRootPath);
                }

                await _offerRepository.DeleteAsync(offer);
            }
        }


        public async Task<bool> UpdateAsync(OfferUpdateVM model)
        {
            var isExist = await _offerRepository.AnyAsync(o => o.Title.Trim().ToLower() == model.Title.Trim().ToLower() && o.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This Offer is already exist");
                return false;
            }
            var offer = await _offerRepository.GetAsync(model.Id);
            if (offer == null) return false;

            offer.Title = model.Title;
            offer.Text= model.Text;
            offer.CoffeeType1 = model.CoffeeType1;
            offer.Offer1 = model.Offer1;
            offer.CoffeeType2 = model.CoffeeType2;
            offer.Offer2 = model.Offer2;
            offer.ModifiedAt = DateTime.Now;
            await _offerRepository.UpdateAsync(offer);
            if (model.Photo1 != null && model.Photo2 != null)


                if (!_fileService.IsImage(model.Photo1) && !_fileService.IsImage(model.Photo2))
                {
                    _modelState.AddModelError("Photo", "Please Choose Image format");
                    return false;
                }
            if (!_fileService.CheckSize(model.Photo1, 300) && !_fileService.CheckSize(model.Photo2, 300))
            {
                _modelState.AddModelError("Photo", "Photo`s size is bigger than 300kB");
                return false;
            }

            _fileService.Delete(offer.Photo1, _webHostEnvironment.WebRootPath);
            _fileService.Delete(offer.Photo2, _webHostEnvironment.WebRootPath);
            offer.Photo1 = await _fileService.UploadAsync(model.Photo1, _webHostEnvironment.WebRootPath);
            offer.Photo2 = await _fileService.UploadAsync(model.Photo2, _webHostEnvironment.WebRootPath);

            return true;
        }


        public async Task<OfferUpdateVM> GetUpdateModelAsync(int id)
        {
            var offer = await _offerRepository.GetAsync(id);

            if (offer != null)
            {
                var model = new OfferUpdateVM
                {
                    Id = offer.Id,
                    Title = offer.Title,
                    Text = offer.Text,
                    CoffeeType1= offer.CoffeeType1,
                    Offer1 = offer.Offer1,
                    CoffeeType2= offer.CoffeeType2,
                    Offer2 = offer.Offer2
                };
                return model;
            }
            return null;
        }
    }
}
