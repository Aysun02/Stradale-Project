using Data_Access.Repositories.Abstarct;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class BlogService : IBlogService
    {
        private readonly IBlogPostRepository _blogPostRepository;

        public BlogService(IBlogPostRepository blogPostRepository, IActionContextAccessor actionContextAccessor)
        {
            _blogPostRepository = blogPostRepository;
        }

        public async Task<BlogIndexVM> GetAllAsync()
        {
            var model = new BlogIndexVM
            {
                BlogPosts = await _blogPostRepository.GetAllAsync(),
            };
            return model;
        }
    }
}
