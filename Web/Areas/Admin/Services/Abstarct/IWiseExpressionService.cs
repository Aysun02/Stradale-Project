using Web.Areas.Admin.ViewModels.WiseExpression;

namespace Web.Areas.Admin.Services.Abstarct
{
    public interface IWiseExpressionService
    {
        Task<WiseExpressionIndexVM> GetAllAsync();
        Task<bool> CreateAsync(WiseExpressionCreateVM model);
        Task<WiseExpressionUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(WiseExpressionUpdateVM model);
        Task DeleteAsync(int id);
    }
}
