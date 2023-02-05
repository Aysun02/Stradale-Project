using Core.Utilities.FileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.Services.Concrete;
using Web.Areas.Admin.ViewModels.Gallery;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class GalleryController : Controller
    {
        private readonly IGalleryService _galleryService;
        private readonly IFileService _fileService;

        public GalleryController(IGalleryService galleryService, IFileService fileService)
        {
            _galleryService = galleryService;
            _fileService = fileService;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var model = await _galleryService.GetAllAsync();
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
        public async Task<IActionResult> Create(GalleryCreateVM model)
        {
            var isSucceeded = await _galleryService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var model = await _galleryService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, GalleryUpdateVM model)
        {

            if (id != model.Id) return NotFound();
            var isSucceeded = await _galleryService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _galleryService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
        #endregion
    }
}
