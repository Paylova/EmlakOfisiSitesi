using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmlakOfisiSitesi.Controllers
{
    public class HeatingTypeController : Controller
    {
        private readonly IRepository<HeatingType> _heatingTypeRepository;
        private readonly IValidator<HeatingTypeViewModel> _heatingTypeValidator;

        public HeatingTypeController(IRepository<HeatingType> heatingTypeRepository, IValidator<HeatingTypeViewModel> heatingTypeValidator)
        {
            _heatingTypeRepository = heatingTypeRepository;
            _heatingTypeValidator = heatingTypeValidator;
        }


        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult List()
        {
            IEnumerable<HeatingType> heatingTypes = _heatingTypeRepository.GetAll();

            return View(heatingTypes);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create(HeatingTypeViewModel heatingTypeViewModel)
        {
            var validationResult = await _heatingTypeValidator.ValidateAsync(heatingTypeViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(heatingTypeViewModel);
            }

            HeatingType heatingType = new HeatingType
            {
                Name = heatingTypeViewModel.Name,
                IsActive = false

            };

            await _heatingTypeRepository.Add(heatingType);

            return RedirectToAction("List", "HeatingType");
        }


        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Update(Guid id)
        {
            HeatingType heatingType = _heatingTypeRepository.GetById(id);

            if (heatingType == null)
                return NotFound();

            HeatingTypeViewModel heatingTypeViewModel = new HeatingTypeViewModel
            {
                Id = heatingType.Id,
                Name = heatingType.Name,
                IsActive = heatingType.IsActive
            };

            return View(heatingTypeViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update(HeatingTypeViewModel heatingTypeViewModel)
        {
            var validationResult = await _heatingTypeValidator.ValidateAsync(heatingTypeViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(heatingTypeViewModel);
            }

            HeatingType heatingType = _heatingTypeRepository.GetById(heatingTypeViewModel.Id);
            heatingType.Name = heatingTypeViewModel.Name;
            heatingType.IsActive = heatingTypeViewModel.IsActive;

            await _heatingTypeRepository.Update(heatingType);

            return RedirectToAction("List", "HeatingType");
        }


        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            HeatingType heatingType = _heatingTypeRepository.GetById(id);

            if (heatingType == null)
                return NotFound();

            await _heatingTypeRepository.Remove(heatingType);

            return Ok();
        }
    }
}

