using Core.Entities;
using Data_Access.Repositories.Abstarct;
using Data_Access.Repositories.Concrete;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Web.Areas.Admin.Services.Abstarct;
using Web.Areas.Admin.ViewModels.Category;
using Web.Areas.Admin.ViewModels.WiseExpression;

namespace Web.Areas.Admin.Services.Concrete
{
    public class WiseExpressionService : IWiseExpressionService
    {
        private readonly IWiseExpressionRepository _wiseExpressionRepository;
        private readonly ModelStateDictionary _modelState;

        public WiseExpressionService(IWiseExpressionRepository wiseExpressionRepository,
            IActionContextAccessor actionContextAccessor)
        {
            _wiseExpressionRepository = wiseExpressionRepository;
            _modelState = actionContextAccessor.ActionContext.ModelState;
        }


        public async Task<WiseExpressionIndexVM> GetAllAsync()
        {
            var model = new WiseExpressionIndexVM
            {
                WiseExpression = await _wiseExpressionRepository.GetAllAsync()
            };
            return model;
        }


        public async Task<bool> CreateAsync(WiseExpressionCreateVM model)
        {
            var isExist = await _wiseExpressionRepository.AnyAsync(w => w.Text.Trim().ToLower() == model.Text.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Text", "This Expression already is exist");
                return false;
            }
            var expression = new WiseExpression
            {
                Text = model.Text,
                Author = model.Author,
                CreatedAt = DateTime.Now
            };
            await _wiseExpressionRepository.CreateAsync(expression);
            return true;
        }


        public async Task<WiseExpressionUpdateVM> GetUpdateModelAsync(int id)
        {
            var expression = await _wiseExpressionRepository.GetAsync(id);

            if (expression != null)
            {
                var model = new WiseExpressionUpdateVM
                {
                    Id = expression.Id,
                    Text = expression.Text,
                    Author = expression.Author
                };
                return model;

            }
            return null;
        }


        public async Task<bool> UpdateAsync(WiseExpressionUpdateVM model)
        {
            var isExist = await _wiseExpressionRepository.AnyAsync(w => w.Text.Trim().ToLower() == model.Text.Trim().ToLower());
            if (isExist)
            {
                _modelState.AddModelError("Text", "This Expression already is exist");
                return false;
            }
            var expression = await _wiseExpressionRepository.GetAsync(model.Id);
            if (expression == null) return false;

            expression.Text = model.Text;
            expression.Author = model.Author;
            expression.ModifiedAt = DateTime.Now;
            await _wiseExpressionRepository.UpdateAsync(expression);

            return true;
        }


        public async Task DeleteAsync(int id)
        {
            var expression = await _wiseExpressionRepository.GetAsync(id);
            if (expression != null)
            {
                await _wiseExpressionRepository.DeleteAsync(expression);
            }
        }
        
    }
}
