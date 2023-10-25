using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmlakOfisiSitesi.Controllers
{
    [Authorize(Policy = "Admin")]
    public class HousingTypeController : Controller
    {
        private readonly IRepository<HousingType> _housingTypeRepository;
        private readonly IValidator<HousingTypeViewModel> _housingTypeValidator;

        public HousingTypeController(IRepository<HousingType> housingTypeRepository, IValidator<HousingTypeViewModel> housingTypeValidator)
        {
            _housingTypeRepository = housingTypeRepository;
            _housingTypeValidator = housingTypeValidator;
        }


        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<HousingType> housingTypes = _housingTypeRepository.GetAll();

            return View(housingTypes);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(HousingTypeViewModel housingTypeViewModel)
        {
            var validationResult = await _housingTypeValidator.ValidateAsync(housingTypeViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(housingTypeViewModel);
            }

            HousingType housingType = new HousingType
            {
                Name = housingTypeViewModel.Name,
                IsActive = false
            };

            await _housingTypeRepository.Add(housingType);

            return RedirectToAction("List", "HousingType");
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            HousingType housingType = _housingTypeRepository.GetById(id);

            if (housingType == null) return NotFound();

            HousingTypeViewModel housingTypeViewModel = new HousingTypeViewModel
            {
                Id = housingType.Id,
                Name = housingType.Name,
                IsActive = false
            };

            return View(housingTypeViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(HousingTypeViewModel housingTypeViewModel)
        {
            var validationResult = await _housingTypeValidator.ValidateAsync(housingTypeViewModel);


            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(housingTypeViewModel);
            }

            HousingType housingType = _housingTypeRepository.GetById(housingTypeViewModel.Id);
            housingType.Name = housingTypeViewModel.Name;
            housingType.IsActive = housingTypeViewModel.IsActive;

            await _housingTypeRepository.Update(housingType);

            return RedirectToAction("List", "HousingType");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            HousingType housingType = _housingTypeRepository.GetById(id);

            if (housingType == null) return NotFound();

            await _housingTypeRepository.Remove(housingType);

            return Ok();
        }
    }
}
