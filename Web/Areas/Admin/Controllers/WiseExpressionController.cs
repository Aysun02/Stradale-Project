using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.Services.Concrete;
using Web.Areas.Admin.ViewModels.Category;
using Web.Areas.Admin.ViewModels.WiseExpression;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class WiseExpressionController : Controller
    {
        private readonly IWiseExpressionService _wiseExpressionService;

        public WiseExpressionController(IWiseExpressionService wiseExpressionService)
        {
            _wiseExpressionService = wiseExpressionService;
        }


        #region Index
        public async Task<IActionResult> Index()
        {
            var model = await _wiseExpressionService.GetAllAsync();
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
        public async Task<IActionResult> Create(WiseExpressionCreateVM model)
        {
            if (!ModelState.IsValid) return View(model);
            var isSucceeded = await _wiseExpressionService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);

        }
        #endregion


        #region Update
        [HttpGet]
        public async Task<IActionResult> Update(int id)
        {
            var model = await _wiseExpressionService.GetUpdateModelAsync(id);
            if (model == null) return BadRequest();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, WiseExpressionUpdateVM model)
        {
            if (id != model.Id) return NotFound();
            var isSucceeded = await _wiseExpressionService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion


        #region Delete
        [HttpPost]
        public async Task<IActionResult> Delete(int id)
        {
            await _wiseExpressionService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));
        }
        #endregion
    }
}
