using Core.Utilities.FileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.Services.Concrete;
using Web.Areas.Admin.ViewModels.HomeGallery;

namespace Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeGalleryController : Controller
    {
        private readonly IHomeGalleryService _homeGalleryService;
        private readonly IFileService _fileService;

        public HomeGalleryController(IHomeGalleryService homeGalleryService, IFileService fileService)
        {
            _homeGalleryService = homeGalleryService;
            _fileService = fileService;
        }


        #region Index
        public async Task<IActionResult> Index()
        {
            var model = await _homeGalleryService.GetAllAsync();
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HomeGalleryCreateVM model)
        {
            var isSucceeded = await _homeGalleryService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var model = await _homeGalleryService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, HomeGalleryUpdateVM model)
        {

            if (id != model.Id) return NotFound();
            var isSucceeded = await _homeGalleryService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _homeGalleryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
        #endregion

    }
}
