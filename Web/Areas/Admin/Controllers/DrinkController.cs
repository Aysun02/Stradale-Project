using Core.Utilities.FileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.ViewModels.Drink;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class DrinkController : Controller
    {
        private readonly IDrinkService _drinkService;

        public DrinkController(IDrinkService drinkService)
        {
            _drinkService = drinkService;
        }


        #region Index
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _drinkService.GetAllAsync();
            return View(model);
        }
        #endregion

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var model = await _drinkService.GetCreateModelAsync();
            return View(model);


        }
        [HttpPost]
        public async Task<IActionResult> Create(DrinkCreateVM model)
        {

            var isSucceeded = await _drinkService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));

            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _drinkService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, DrinkUpdateVM model)
        {

            if (id != model.Id) return NotFound();
            var isSucceeded = await _drinkService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);

        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _drinkService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
        #endregion

    }
}
