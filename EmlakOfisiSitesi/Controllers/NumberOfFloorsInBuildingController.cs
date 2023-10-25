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
    public class NumberOfFloorsInBuildingController : Controller
    {

        private readonly IRepository<NumberOfFloorsInBuilding> _numberOfFloorsInBuildingRepository;
        private readonly IValidator<NumberOfFloorsInBuildingViewModel> _numberOfFloorsInBuildingValidator;

        public NumberOfFloorsInBuildingController(IRepository<NumberOfFloorsInBuilding> numberOfFloorsInBuildingRepository, IValidator<NumberOfFloorsInBuildingViewModel> numberOfFloorsInBuildingValidator)
        {
            _numberOfFloorsInBuildingRepository = numberOfFloorsInBuildingRepository;
            _numberOfFloorsInBuildingValidator = numberOfFloorsInBuildingValidator;
        }

        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<NumberOfFloorsInBuilding> numberOfFloorsInBuildings = _numberOfFloorsInBuildingRepository.GetAll();

            return View(numberOfFloorsInBuildings);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NumberOfFloorsInBuildingViewModel numberOfFloorsInBuildingViewModel)
        {
            var validationResult = await _numberOfFloorsInBuildingValidator.ValidateAsync(numberOfFloorsInBuildingViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(numberOfFloorsInBuildingViewModel);
            }

            NumberOfFloorsInBuilding numberOfFloorsInBuilding = new NumberOfFloorsInBuilding
            {
                Name = numberOfFloorsInBuildingViewModel.Name,
                Max = numberOfFloorsInBuildingViewModel.Max,
                Min = numberOfFloorsInBuildingViewModel.Min,
                IsActive = false
            };

            await _numberOfFloorsInBuildingRepository.Add(numberOfFloorsInBuilding);

            return RedirectToAction("List", "NumberOfFloorsInBuilding");
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            NumberOfFloorsInBuilding numberOfFloorsInBuilding = _numberOfFloorsInBuildingRepository.GetById(id);

            if (numberOfFloorsInBuilding == null) return NotFound();

            NumberOfFloorsInBuildingViewModel numberOfFloorsInBuildingViewModel = new NumberOfFloorsInBuildingViewModel
            {
                Id = numberOfFloorsInBuilding.Id,
                Name = numberOfFloorsInBuilding.Name,
                Max = numberOfFloorsInBuilding.Max,
                Min = numberOfFloorsInBuilding.Min,
                IsActive = numberOfFloorsInBuilding.IsActive
            };

            return View(numberOfFloorsInBuildingViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(NumberOfFloorsInBuildingViewModel numberOfFloorsInBuildingViewModel)
        {
            var validationResult = await _numberOfFloorsInBuildingValidator.ValidateAsync(numberOfFloorsInBuildingViewModel);


            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(numberOfFloorsInBuildingViewModel);
            }

            NumberOfFloorsInBuilding numberOfFloorsInBuilding = _numberOfFloorsInBuildingRepository.GetById(numberOfFloorsInBuildingViewModel.Id);
            numberOfFloorsInBuilding.Name = numberOfFloorsInBuildingViewModel.Name;
            numberOfFloorsInBuilding.Min = numberOfFloorsInBuildingViewModel.Min;
            numberOfFloorsInBuilding.Max = numberOfFloorsInBuildingViewModel.Max;
            numberOfFloorsInBuilding.IsActive = numberOfFloorsInBuilding.IsActive;

            await _numberOfFloorsInBuildingRepository.Update(numberOfFloorsInBuilding);

            return RedirectToAction("List", "NumberOfFloorsInBuilding");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            NumberOfFloorsInBuilding numberOfFloorsInBuilding = _numberOfFloorsInBuildingRepository.GetById(id);

            if (numberOfFloorsInBuilding == null) return NotFound();

            await _numberOfFloorsInBuildingRepository.Remove(numberOfFloorsInBuilding);

            return Ok();
        }
    }
}
