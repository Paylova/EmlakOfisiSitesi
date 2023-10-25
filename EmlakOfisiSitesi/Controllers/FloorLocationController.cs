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
    public class FloorLocationController : Controller
    {
        private readonly IRepository<FloorLocation> _floorLocationRepository;
        private readonly IValidator<FloorLocationViewModel> _floorLocationValidator;

        public FloorLocationController(IRepository<FloorLocation> floorLocationRepository, IValidator<FloorLocationViewModel> floorLocationValidator)
        {
            _floorLocationRepository = floorLocationRepository;
            _floorLocationValidator = floorLocationValidator;
        }

        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<FloorLocation> floorLocations = _floorLocationRepository.GetAll();

            return View(floorLocations);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(FloorLocationViewModel floorLocationViewModel)
        {
            var validationResult = await _floorLocationValidator.ValidateAsync(floorLocationViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(floorLocationViewModel);
            }

            FloorLocation floorLocation = new FloorLocation
            {
                Name = floorLocationViewModel.Name,
                IsActive = false
            };

            await _floorLocationRepository.Add(floorLocation);

            return RedirectToAction("List", "FloorLocation");
        }


        [HttpGet]
        public IActionResult Update(Guid id)
        {
            FloorLocation floorLocation = _floorLocationRepository.GetById(id);

            if (floorLocation == null) return NotFound();

            FloorLocationViewModel floorLocationViewModel = new FloorLocationViewModel
            {
                Id = floorLocation.Id,
                Name = floorLocation.Name,
                IsActive = floorLocation.IsActive,
            };

            return View(floorLocationViewModel);
        }


        [HttpPost]
        public async Task<IActionResult> Update(FloorLocationViewModel floorLocationViewModel)
        {
            var validationResult = await _floorLocationValidator.ValidateAsync(floorLocationViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(floorLocationViewModel);
            }

            FloorLocation floorLocation = _floorLocationRepository.GetById(floorLocationViewModel.Id);
            floorLocation.Name = floorLocationViewModel.Name;
            floorLocation.IsActive = floorLocationViewModel.IsActive;

            await _floorLocationRepository.Update(floorLocation);

            return RedirectToAction("List", "FloorLocation");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            FloorLocation floorLocation = _floorLocationRepository.GetById(id);

            if (floorLocation == null) return NotFound();

            await _floorLocationRepository.Remove(floorLocation);

            return Ok();
        }
    }
}
