using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Services.Abstract;
using Web.Services.Concrete;

namespace Web.Controllers
{
  
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;

        public BlogController(IBlogService blogService)
        {
            _blogService = blogService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var model = await _blogService.GetAllAsync();
            return View(model);
        }
    }
}
