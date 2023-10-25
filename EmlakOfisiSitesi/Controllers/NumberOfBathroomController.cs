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
    public class NumberOfBathroomController : Controller
    {
        private readonly IRepository<NumberOfBathroom> _numberOfBathroomRepository;
        private readonly IValidator<NumberOfBathroomViewModel> _numberOfBathroomValidator;

        public NumberOfBathroomController(IRepository<NumberOfBathroom> numberOfBathroomRepository, IValidator<NumberOfBathroomViewModel> numberOfBathroomValidator)
        {
            _numberOfBathroomRepository = numberOfBathroomRepository;
            _numberOfBathroomValidator = numberOfBathroomValidator;
        }

        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<NumberOfBathroom> numberOfBathrooms = _numberOfBathroomRepository.GetAll();

            return View(numberOfBathrooms);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NumberOfBathroomViewModel numberOfBathroomViewModel)
        {
            var validationResult = await _numberOfBathroomValidator.ValidateAsync(numberOfBathroomViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(numberOfBathroomViewModel);
            }

            NumberOfBathroom numberOfBathroom = new NumberOfBathroom
            {
                Name = numberOfBathroomViewModel.Name,
                Number = numberOfBathroomViewModel.Number,
                IsAndOver = numberOfBathroomViewModel.IsAndOver,
                IsActive = false
            };

            await _numberOfBathroomRepository.Add(numberOfBathroom);

            return RedirectToAction("List", "NumberOfBathroom");
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            NumberOfBathroom numberOfBathroom = _numberOfBathroomRepository.GetById(id);

            if (numberOfBathroom == null) return NotFound();

            NumberOfBathroomViewModel numberOfBathroomViewModel = new NumberOfBathroomViewModel
            {
                Id = numberOfBathroom.Id,
                Name = numberOfBathroom.Name,
                Number = numberOfBathroom.Number,
                IsAndOver = numberOfBathroom.IsAndOver,
                IsActive = numberOfBathroom.IsActive
            };

            return View(numberOfBathroomViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(NumberOfBathroomViewModel numberOfBathroomViewModel)
        {
            var validationResult = await _numberOfBathroomValidator.ValidateAsync(numberOfBathroomViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(numberOfBathroomViewModel);
            }

            NumberOfBathroom numberOfBathroom = _numberOfBathroomRepository.GetById(numberOfBathroomViewModel.Id);
            numberOfBathroom.Number = numberOfBathroomViewModel.Number;
            numberOfBathroom.Name = numberOfBathroomViewModel.Name;
            numberOfBathroom.IsAndOver = numberOfBathroomViewModel.IsAndOver;
            numberOfBathroom.IsActive = numberOfBathroomViewModel.IsActive;

            await _numberOfBathroomRepository.Update(numberOfBathroom);

            return RedirectToAction("List", "NumberOfBathroom");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            NumberOfBathroom numberOfBathroom = _numberOfBathroomRepository.GetById(id);

            if (numberOfBathroom == null) return NotFound();

            await _numberOfBathroomRepository.Remove(numberOfBathroom);

            return Ok();
        }
    }
}
