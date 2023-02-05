using Core.Utilities.FileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.ViewModels.BlogPost;

namespace Web.Areas.Admin.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class BlogPostController : Controller
    {
        private readonly IBlogPostService _blogPostService;
        private readonly IFileService _fileService;

        public BlogPostController(IBlogPostService blogPostService, IFileService fileService)
        {
            _blogPostService = blogPostService;
            _fileService = fileService;
        }

        #region Index
        public async Task<IActionResult> Index()
        {
            var model = await _blogPostService.GetAllAsync();
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
        public async Task<IActionResult> Create(BlogPostCreateVM model)
        {
            var isSucceeded = await _blogPostService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]

        public async Task<IActionResult> Update(int id)
        {
            var model = await _blogPostService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, BlogPostUpdateVM model)
        {

            if (id != model.Id) return NotFound();
            var isSucceeded = await _blogPostService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _blogPostService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
        #endregion
    }
}
