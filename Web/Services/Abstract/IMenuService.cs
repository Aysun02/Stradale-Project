using Web.ViewModels;

namespace Web.Services.Abstract
{
    public interface IMenuService
    {
        Task<MenuIndexVM> GetAllAsync();
    }
}
