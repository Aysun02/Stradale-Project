using Core.Entities;
using Core.Utilities.FileService;
using Data_Access.Contexts;
using Data_Access.Repositories.Abstarct;
using Data_Access.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Numerics;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.ViewModels.BlogPost;

namespace Web.Areas.Admin.Services.Concrete
{
    public class BlogPostService : IBlogPostService
    {
        private readonly AppDbContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly IFileService _fileService;
        private readonly IBlogPostRepository _blogPostRepository;
        private readonly IBlogPhotoRepository _blogPhotoRepository;
        private readonly ModelStateDictionary _modelState;

        public BlogPostService(AppDbContext context,
           IWebHostEnvironment webHostEnvironment,
           IFileService fileService,
           IBlogPostRepository blogPostRepository,
         IBlogPhotoRepository blogPhotoRepository,
              IActionContextAccessor actionContextAccessor)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
            _fileService = fileService;
            _blogPhotoRepository = blogPhotoRepository;
            _blogPostRepository = blogPostRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<BlogPostIndexVM> GetAllAsync()
        {
            var model = new BlogPostIndexVM()
            {
                BlogPosts = await _blogPostRepository.GetAllAsync()
            };
            return model;
        }


        public async Task<bool> CreateAsync(BlogPostCreateVM model)
        {
            if (!_modelState.IsValid) return false;

            var isExist = await _blogPostRepository.AnyAsync(b => b.Title.Trim().ToLower() == model.Title.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Title", "This Blog theme already shared");
                return false;
            }
            if (!_fileService.IsImage(model.Photo))
            {
                _modelState.AddModelError("MainPhotoName", "File must be image format");
                return false;

            }
            if (!_fileService.CheckSize(model.Photo, 500))
            {
                _modelState.AddModelError("MainPhoto", "File size must be less than 500kB");
                return false;
            }

            var Post = new BlogPost
            {
                Date = model.Date,
                Title = model.Title,
                Description = model.Description,
                CreatedAt = DateTime.Now,
                Photo = await _fileService.UploadAsync(model.Photo, _webHostEnvironment.WebRootPath)

            };

            await _blogPostRepository.CreateAsync(Post);
            return true;
        }


        public async Task DeleteAsync(int id)
        {
            var post = await _blogPostRepository.GetAsync(id);
            if (post != null)
            {
                foreach (var photo in await _blogPhotoRepository.GetAllAsync())
                {
                    _fileService.Delete(photo.Name, _webHostEnvironment.WebRootPath);
                }

                await _blogPostRepository.DeleteAsync(post);
            }
        }


        public async Task<bool> UpdateAsync(BlogPostUpdateVM model)
        {
            var isExist = await _blogPostRepository.AnyAsync(b => b.Title.Trim().ToLower() == model.Title.Trim().ToLower() && b.Id != model.Id);
            if (isExist)
            {
                _modelState.AddModelError("Title", "This Post is already shared");
                return false;
            }
            var post = await _blogPostRepository.GetAsync(model.Id);
            if (post == null) return false;

            post.Date = model.Date;
            post.Description = model.Description;
            post.Title = model.Title;
            post.ModifiedAt = DateTime.Now;
            await _blogPostRepository.UpdateAsync(post);
            if (model.Photo != null)


                if (!_fileService.IsImage(model.Photo))
                {
                    _modelState.AddModelError("Photo", "Please Choose Image format");
                    return false;
                }
            if (!_fileService.CheckSize(model.Photo, 300))
            {
                _modelState.AddModelError("Photo", "Photo`s size is bigger than 300kB");
                return false;
            }

            _fileService.Delete(post.Photo, _webHostEnvironment.WebRootPath);
            post.Photo = await _fileService.UploadAsync(model.Photo, _webHostEnvironment.WebRootPath);

            return true;
        }


        public async Task<BlogPostUpdateVM> GetUpdateModelAsync(int id)
        {
            var post = await _blogPostRepository.GetAsync(id);

            if (post != null)
            {


                var model = new BlogPostUpdateVM
                {
                    Id = post.Id,
                    Date = post.Date,
                    Title = post.Title,
                    Description = post.Description,
                };
                return model;

            }
            return null;
        }
    }
}

