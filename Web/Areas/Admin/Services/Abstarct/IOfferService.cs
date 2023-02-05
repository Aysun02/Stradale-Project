using Web.Areas.Admin.ViewModels.Offer;

namespace Web.Areas.Admin.Services.Abstarct
{
    public interface IOfferService
    {
        Task<OfferIndexVM> GetAllAsync();
        Task<bool> CreateAsync(OfferCreateVM model);
        Task<OfferUpdateVM> GetUpdateModelAsync(int id);
        Task<bool> UpdateAsync(OfferUpdateVM model);
        Task DeleteAsync(int id);
    }
}
