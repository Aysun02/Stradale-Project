using Core.Entities;
using Core.Utilities.FileService;
using Data_Access.Repositories.Abstarct;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.ViewModels.Drink;

namespace Web.Areas.Admin.Services.Concrete
{
    public class DrinkService : IDrinkService
    {
        private readonly IDrinkRepository _drinkRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly ModelStateDictionary _modelState;


        public DrinkService(IDrinkRepository drinkRepository,
            IActionContextAccessor actionContextAccessor,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment webHostEnviroment)
        {
            _drinkRepository = drinkRepository;
            _categoryRepository = categoryRepository;
            _webHostEnviroment = webHostEnviroment;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }

        public async Task<DrinkIndexVM> GetAllAsync()
        {
            var model = new DrinkIndexVM()
            {
                Drinks = await _drinkRepository.GetAllWithCategoryAsync()
            };
            return model;

        }
        public async Task<bool> CreateAsync(DrinkCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _drinkRepository.AnyAsync(d => d.DrinkName.Trim().ToLower() == model.DrinkName.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("DrinkName", "This Drink is already exist");
                return false;
            }

            var drink = new Drink
            {
                DrinkName = model.DrinkName,
                MediumPrice = model.MediumPrice,
                LargePrice = model.LargePrice,
                CategoryId = model.CategoryId,
                CreatedAt = DateTime.Now,
            };
            await _drinkRepository.CreateAsync(drink);

            return true;
        }

        public async Task<DrinkCreateVM> GetCreateModelAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var model = new DrinkCreateVM
            {
                Categories = categories.Select(c => new SelectListItem
                {
                    Text = c.Title,
                    Value = c.Id.ToString()
                }).ToList()

            }; return model;


        }

        public async Task DeleteAsync(int id)
        {
            var drink = await _drinkRepository.GetAsync(id);
            if (drink != null)
            {
                await _drinkRepository.DeleteAsync(drink);
            }
        }

        public async Task<DrinkUpdateVM> GetUpdateModelAsync(int id)
        {
            var drink = await _drinkRepository.GetAsync(id);

            if (drink != null)
            {
                var categories = await _categoryRepository.GetAllAsync();

                var model = new DrinkUpdateVM
                {
                    Id = drink.Id,
                    DrinkName = drink.DrinkName,
                    MediumPrice = drink.MediumPrice,
                    LargePrice = drink.LargePrice,
                    Categories = categories.Select(c => new SelectListItem
                    {
                        Text = c.Title,
                        Value = c.Id.ToString()
                    }).ToList(),
                    CategoryId = drink.CategoryId,
                };
                return model;

            }
            return null;
        }

        public async Task<bool> UpdateAsync(DrinkUpdateVM model)
        {
            var isExist = await _drinkRepository.AnyAsync(d => d.DrinkName.Trim().ToLower() == model.DrinkName.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("DrinkName", "This Drink already is exist");
                return false;
            }
            var drink = await _drinkRepository.GetAsync(model.Id);
            if (drink == null) return false;

            drink.DrinkName = model.DrinkName;
            drink.MediumPrice = model.MediumPrice;
            drink.LargePrice = model.LargePrice;
            drink.ModifiedAt = DateTime.Now;
            drink.CategoryId = model.CategoryId;

            await _drinkRepository.UpdateAsync(drink);

            return true;
        }

    }
}
