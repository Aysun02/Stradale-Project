using Data_Access.Repositories.Abstarct;
using Data_Access.Repositories.Concrete;
using DataAccess.Repositories.Abstract;
using DataAccess.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Web.Services.Abstract;
using Web.ViewModels;

namespace Web.Services.Concrete
{
    public class MenuService : IMenuService
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly IAdditionRepository _additionRepository;
        private readonly ICategoryRepository _categoryRepository;

        public MenuService(IDrinkRepository drinkRepository,
          IAdditionRepository additionRepository,
          ICategoryRepository categoryRepository, IActionContextAccessor actionContextAccessor)
        {
            _drinkRepository = drinkRepository;
            _additionRepository = additionRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<MenuIndexVM> GetAllAsync()
        {
            var model = new MenuIndexVM
            {
                Drinks = await _drinkRepository.GetAllAsync(),
                Additions = await _additionRepository.GetAllAsync(),
                Categories = await _categoryRepository.GetAllAsync()
            };
            return model;
        }
    }
}
