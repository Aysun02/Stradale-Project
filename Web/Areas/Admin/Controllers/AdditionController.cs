using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.Services.Concrete;
using Web.Areas.Admin.ViewModels.Addition;
using Web.Areas.Admin.ViewModels.Drink;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class AdditionController : Controller
    {
        private readonly IAdditionService _additionService;

        public AdditionController(IAdditionService additionService)
        {
            _additionService = additionService;
        }


        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _additionService.GetAllAsync();
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _additionService.GetCreateModelAsync();
            return View(model);


        }
        [HttpPost]
        public async Task<IActionResult> Create(AdditionCreateVM model)
        {

            var isSucceeded = await _additionService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _additionService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, AdditionUpdateVM model)
        {

            if (id != model.Id) return NotFound();
            var isSucceeded = await _additionService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);

        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _additionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
        #endregion

    }
}
