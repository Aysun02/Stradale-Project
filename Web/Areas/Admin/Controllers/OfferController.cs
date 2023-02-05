using Core.Utilities.FileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.Services.Concrete;
using Web.Areas.Admin.ViewModels.Information;
using Web.Areas.Admin.ViewModels.Offer;

namespace Web.Areas.Admin.Controllers
{

    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class OfferController : Controller
    {
        private readonly IOfferService _offerService;
        private readonly IFileService _fileService;

        public OfferController(IOfferService offerService, IFileService fileService)
        {
            _offerService = offerService;
            _fileService = fileService;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var model = await _offerService.GetAllAsync();
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
        public async Task<IActionResult> Create(OfferCreateVM model)
        {
            var isSucceeded = await _offerService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var model = await _offerService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, OfferUpdateVM model)
        {

            if (id != model.Id) return NotFound();
            var isSucceeded = await _offerService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _offerService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
        #endregion
    }
}
