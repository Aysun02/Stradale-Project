﻿using Core.Utilities;
using Core.Utilities.FileService;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System.Data;
using WebApp.Services.Abstract;
using WebApp.ViewModels.HomeMainSlider;

namespace WebApp.Controllers
{
    [Area("Admin")]
    [Authorize(Roles = "Admin")]
    public class HomeMainSliderController : Controller
    {
        private readonly IHomeMainSliderService _sliderService;
        private readonly IFileService _fileService;

        public HomeMainSliderController(IHomeMainSliderService sliderService, IFileService fileService)
        {
            _sliderService = sliderService;
            _fileService = fileService;
        }
        #region Index
        public async Task<IActionResult> Index()
        {
            var model = await _sliderService.GetAllAsync();
            return View(model);
        }
        #endregion`

        #region Create
        [HttpGet]
        public async Task<IActionResult> Create()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Create(HomeMainSliderCreateVM model)
        {
            var isSucceeded = await _sliderService.CreateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Update
        [HttpGet]
       
        public async Task<IActionResult> Update(int id)
        {
            var model = await _sliderService.GetUpdateModelAsync(id);
            if (model == null) return NotFound();
            return View(model);
        }
        [HttpPost]
        public async Task<IActionResult> Update(int id, HomeMainSliderUpdateVM model)
        {

            if (id != model.Id) return NotFound();
            var isSucceeded = await _sliderService.UpdateAsync(model);
            if (isSucceeded) return RedirectToAction(nameof(Index));
            return View(model);
        }
        #endregion

        #region Delete
        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            await _sliderService.DeleteAsync(id);
            return RedirectToAction(nameof(Index));

        }
        #endregion


    }
}
