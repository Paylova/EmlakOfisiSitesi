using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Collections.Generic;

namespace EmlakOfisiSitesi.Controllers
{
    [Authorize(Policy = "Admin")]
    public class DistrictController : Controller
    {
        private readonly IRepository<District> _districtRepository;
        private readonly IRepository<City> _cityRepository;
        private readonly IValidator<DistrictViewModel> _districtValidator;

        public DistrictController(IRepository<District> districtRepository, IValidator<DistrictViewModel> districtValidator, IRepository<City> cityRepository)
        {
            _districtRepository = districtRepository;
            _districtValidator = districtValidator;
            _cityRepository = cityRepository;
        }

        [HttpGet]

        public IActionResult List()
        {
            IEnumerable<District> districts = _districtRepository.GetAll();

            return View(districts);
        }

        [HttpGet]
        public IActionResult Create()
        {
            var cities = _cityRepository.GetAll();
            var districtViewModel = new DistrictViewModel
            {
                Cities = cities.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            return View(districtViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Create(DistrictViewModel districtViewModel)
        {
            var validationResult = await _districtValidator.ValidateAsync(districtViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                var cities = _cityRepository.GetAll();
                districtViewModel.Cities = cities.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

                return View(districtViewModel);
            }

            City city = _cityRepository.GetById(districtViewModel.CityId);
            District district = new District
            {
                IsActive = false,
                Name = districtViewModel.Name,
                City = city
            };

            await _districtRepository.Add(district);

            return RedirectToAction("List", "District");
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            District district = _districtRepository.GetById(id);

            if (district == null) return NotFound();

            var cities = _cityRepository.GetAll();

            DistrictViewModel districtViewModel = new DistrictViewModel
            {
                Id = district.Id,
                Name = district.Name,
                IsActive = district.IsActive,
                CityId = district.City.Id,
                Cities = cities.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                })
            };

            return View(districtViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DistrictViewModel districtViewModel)
        {
            var validationResult = await _districtValidator.ValidateAsync(districtViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                var cities = _cityRepository.GetAll();
                districtViewModel.Cities = cities.Select(c => new SelectListItem
                {
                    Text = c.Name,
                    Value = c.Id.ToString()
                });

                return View(districtViewModel);
            }

            City city = _cityRepository.GetById(districtViewModel.CityId);
            District district = _districtRepository.GetById(districtViewModel.Id);
            district.Name = districtViewModel.Name;
            district.City = city;
            district.IsActive = districtViewModel.IsActive;

            await _districtRepository.Update(district);

            return RedirectToAction("List", "District");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            District district = _districtRepository.GetById(id);

            if (district == null) return NotFound();

            await _districtRepository.Remove(district);

            return Ok();
        }
    }
}
