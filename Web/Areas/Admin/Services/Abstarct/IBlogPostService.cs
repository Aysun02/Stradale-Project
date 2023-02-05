using Web.Areas.Admin.ViewModels.BlogPost;

namespace Web.Areas.Admin.Services.Abstarct
{
    public interface IBlogPostService
    {
        Task<BlogPostIndexVM> GetAllAsync();
        Task<bool> CreateAsync(BlogPostCreateVM model);
        Task<BlogPostUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(BlogPostUpdateVM model);
        Task DeleteAsync(int id);
    }
}
