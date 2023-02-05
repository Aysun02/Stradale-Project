using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.Services.Concrete;

namespace Web.Controllers
{
    public class MenuController : Controller
    {
        private readonly IMenuService _menuService;

        public MenuController(IMenuService menuService)
        {
            _menuService = menuService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _menuService.GetAllAsync();
            return View(model);
        }
    }
}
