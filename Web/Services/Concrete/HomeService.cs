using Data_Access.Repositories.Abstarct;
using Data_Access.Repositories.Concrete;
using DataAccess.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class HomeService : IHomeService
    {
        private readonly IHomeMainSliderRepository _homeMainSliderRepository;
        private readonly IHomeGalleryRepository _homeGalleryRepository;
        private readonly IInformationRepository _informationRepository;
        private readonly IOfferRepository _offerRepository;
        private readonly IWiseExpressionRepository _wiseExpressionRepository;

        public HomeService(IHomeMainSliderRepository homeMainSliderRepository,
           IHomeGalleryRepository homeGalleryRepository,
           IInformationRepository informationRepository,
           IOfferRepository offerRepository,
           IWiseExpressionRepository wiseExpressionRepository,  IActionContextAccessor actionContextAccessor)
        {
            _homeMainSliderRepository = homeMainSliderRepository;
            _homeGalleryRepository = homeGalleryRepository;
            _informationRepository = informationRepository;
            _offerRepository = offerRepository;
            _wiseExpressionRepository = wiseExpressionRepository;
        }


        public async Task<HomeIndexVM> GetAllAsync()
        {
            var model = new HomeIndexVM
            {
                HomeMainSliders = await _homeMainSliderRepository.GetAllAsync(),
                HomeGalleries = await _homeGalleryRepository.GetAllAsync(),
                Informations = await _informationRepository.GetAllAsync(),
                Offers = await _offerRepository.GetAllAsync(),
                WiseExpression = await _wiseExpressionRepository.GetAllAsync(),
            };
            return model;
        }
    }
}