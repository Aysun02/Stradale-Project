using Microsoft.AspNetCore.Mvc;
using Web.Services.Abstract;
using Web.Services.Concrete;

namespace Web.Controllers
{
    public class GalleryController : Controller
    {
        private readonly IGalleryElementService _galleryElementService;

        public GalleryController(IGalleryElementService galleryElementService)
        {
            _galleryElementService = galleryElementService;
        }


        [HttpGet]
        public async Task<ActionResult> Index()
        {
            var model = await _galleryElementService.GetAllAsync();
            return View(model);
        }
    }
}
