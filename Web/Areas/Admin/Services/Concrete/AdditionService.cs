using Core.Entities;
using Data_Access.Repositories.Abstarct;
using Data_Access.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.AspNetCore.Mvc.Rendering;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.ViewModels.Addition;
using Web.Areas.Admin.ViewModels.Drink;

namespace Web.Areas.Admin.Services.Concrete
{
    public class AdditionService : IAdditionService
    {
        private readonly IAdditionRepository _additionRepository;
        private readonly ICategoryRepository _categoryRepository;
        private readonly IWebHostEnvironment _webHostEnviroment;
        private readonly ModelStateDictionary _modelState;


        public AdditionService(IAdditionRepository additionRepository,
            IActionContextAccessor actionContextAccessor,
            ICategoryRepository categoryRepository,
            IWebHostEnvironment webHostEnviroment)
        {
            _additionRepository = additionRepository;
            _categoryRepository = categoryRepository;
            _webHostEnviroment = webHostEnviroment;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<AdditionIndexVM> GetAllAsync()
        {
            var model = new AdditionIndexVM()
            {
                Additions = await _additionRepository.GetAllWithCategoryAsync()
            };
            return model;
        }


        public async Task<bool> CreateAsync(AdditionCreateVM model)
        {
            if (!_modelState.IsValid) return false;
            var isExist = await _additionRepository.AnyAsync(a => a.Ingredient.Trim().ToLower() == model.Ingredient.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Ingredient", "This Ingredient is already exist");
                return false;
            }

            var addition = new Addition
            {
                Ingredient = model.Ingredient,
                Price = model.Price,
                CategoryId = model.CategoryId,
                CreatedAt = DateTime.Now,
            };
            await _additionRepository.CreateAsync(addition);

            return true;
        }


        public async Task<AdditionCreateVM> GetCreateModelAsync()
        {
            var categories = await _categoryRepository.GetAllAsync();

            var model = new AdditionCreateVM
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
            var addition = await _additionRepository.GetAsync(id);
            if (addition != null)
            {
                await _additionRepository.DeleteAsync(addition);
            }
        }


        public async Task<AdditionUpdateVM> GetUpdateModelAsync(int id)
        {
            var addition = await _additionRepository.GetAsync(id);

            if (addition != null)
            {
                var categories = await _categoryRepository.GetAllAsync();

                var model = new AdditionUpdateVM
                {
                    Id = addition.Id,
                    Ingredient = addition.Ingredient,
                    Price = addition.Price,
                    Categories = categories.Select(c => new SelectListItem
                    {
                        Text = c.Title,
                        Value = c.Id.ToString()
                    }).ToList(),
                    CategoryId = addition.CategoryId,
                };
                return model;

            }
            return null;
        }


        public async Task<bool> UpdateAsync(AdditionUpdateVM model)
        {
            var isExist = await _additionRepository.AnyAsync(a => a.Ingredient.Trim().ToLower() == model.Ingredient.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Ingredient", "This Addition already is exist");
                return false;
            }
            var addition = await _additionRepository.GetAsync(model.Id);
            if (addition == null) return false;

            addition.Ingredient = model.Ingredient;
            addition.Price = model.Price;
            addition.ModifiedAt = DateTime.Now;
            addition.CategoryId = model.CategoryId;

            await _additionRepository.UpdateAsync(addition);

            return true;
        }
    }
}
