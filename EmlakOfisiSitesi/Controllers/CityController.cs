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
    public class CityController : Controller
    {
        private readonly IRepository<City> _cityRepository;
        private readonly IValidator<CityViewModel> _cityValidator;

        public CityController(IRepository<City> cityRepository, IValidator<CityViewModel> cityValidator)
        {
            _cityRepository = cityRepository;
            _cityValidator = cityValidator;
        }

        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<City> cities = _cityRepository.GetAll();

            return View(cities);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(CityViewModel cityViewModel)
        {
            var validationResult = await _cityValidator.ValidateAsync(cityViewModel);


            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(cityViewModel);
            }

            City city = new City
            {
                Name = cityViewModel.Name,
                IsActive = false,
            };

            await _cityRepository.Add(city);

            return RedirectToAction("List", "City");
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            City city = _cityRepository.GetById(id);

            if (city == null) return NotFound();

            CityViewModel cityViewModel = new CityViewModel
            {
                Id = city.Id,
                Name = city.Name,
                IsActive = city.IsActive,
            };

            return View(cityViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(CityViewModel cityViewModel)
        {
            var validationResult = await _cityValidator.ValidateAsync(cityViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(cityViewModel);
            }

            City city = _cityRepository.GetById(cityViewModel.Id);
            city.Name = cityViewModel.Name;
            city.IsActive = cityViewModel.IsActive;

            await _cityRepository.Update(city);

            return RedirectToAction("List", "City");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            City city = _cityRepository.GetById(id);

            if (city == null) return NotFound();

            await _cityRepository.Remove(city);

            return Ok();
        }
    }
}
