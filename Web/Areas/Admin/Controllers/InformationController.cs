using Core.Utilities.FileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.Services.Concrete;
using Web.Areas.Admin.ViewModels.HomeGallery;
using Web.Areas.Admin.ViewModels.Information;

namespace Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class InformationController : Controller
    {
        private readonly IInformationService _informationService;
        private readonly IFileService _fileService;

        public InformationController(IInformationService informationService, IFileService fileService)
        {
            _informationService = informationService;
            _fileService = fileService;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var model = await _informationService.GetAllAsync();
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
        public async Task<IActionResult> Create(InformationCreateVM model)
        {
            var isSucceeded = await _informationService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var model = await _informationService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, InformationUpdateVM model)
        {

            if (id != model.Id) return NotFound();
            var isSucceeded = await _informationService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _informationService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
        #endregion

    }
}
