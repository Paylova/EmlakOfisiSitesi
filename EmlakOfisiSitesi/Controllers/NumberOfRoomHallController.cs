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
    public class NumberOfRoomHallController : Controller
    {
        private readonly IRepository<NumberOfRoomHall> _numberOfRoomHallRepository;
        private readonly IValidator<NumberOfRoomHallViewModel> _numberOfRoomHallValidator;

        public NumberOfRoomHallController(IRepository<NumberOfRoomHall> numberOfRoomHallRepository, IValidator<NumberOfRoomHallViewModel> numberOfRoomHallValidator)
        {
            _numberOfRoomHallRepository = numberOfRoomHallRepository;
            _numberOfRoomHallValidator = numberOfRoomHallValidator;
        }


        [HttpGet]
        public IActionResult List()
        {
            IEnumerable<NumberOfRoomHall> numberOfRoomHalls = _numberOfRoomHallRepository.GetAll();

            return View(numberOfRoomHalls);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(NumberOfRoomHallViewModel numberOfRoomHallViewModel)
        {
            var validationResult = await _numberOfRoomHallValidator.ValidateAsync(numberOfRoomHallViewModel);


            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(numberOfRoomHallViewModel);
            }

            NumberOfRoomHall numberOfRoomHall = new NumberOfRoomHall
            {
                Name = numberOfRoomHallViewModel.Name,
                HallNumber = numberOfRoomHallViewModel.HallNumber,
                RoomNumber = numberOfRoomHallViewModel.RoomNumber,
                IsAndOver = numberOfRoomHallViewModel.IsAndOver,
                IsActive = false,
            };

            await _numberOfRoomHallRepository.Add(numberOfRoomHall);

            return RedirectToAction("List", "NumberOfRoomHall");
        }

        [HttpGet]
        public IActionResult Update(Guid id)
        {
            NumberOfRoomHall numberOfRoomHall = _numberOfRoomHallRepository.GetById(id);

            if (numberOfRoomHall == null) return NotFound();

            NumberOfRoomHallViewModel numberOfRoomHallViewModel = new NumberOfRoomHallViewModel
            {
                Id = numberOfRoomHall.Id,
                Name = numberOfRoomHall.Name,
                HallNumber = numberOfRoomHall.HallNumber,
                RoomNumber = numberOfRoomHall.RoomNumber,
                IsAndOver = numberOfRoomHall.IsAndOver,
                IsActive = numberOfRoomHall.IsActive,
            };

            return View(numberOfRoomHallViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(NumberOfRoomHallViewModel numberOfRoomHallViewModel)
        {
            var validationResult = await _numberOfRoomHallValidator.ValidateAsync(numberOfRoomHallViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(numberOfRoomHallViewModel);
            }

            NumberOfRoomHall numberOfRoomHall = _numberOfRoomHallRepository.GetById(numberOfRoomHallViewModel.Id);
            numberOfRoomHall.Name = numberOfRoomHallViewModel.Name;
            numberOfRoomHall.HallNumber = numberOfRoomHallViewModel.HallNumber;
            numberOfRoomHall.RoomNumber = numberOfRoomHallViewModel.RoomNumber;
            numberOfRoomHall.IsAndOver = numberOfRoomHallViewModel.IsAndOver;
            numberOfRoomHall.IsActive = numberOfRoomHallViewModel.IsActive;

            await _numberOfRoomHallRepository.Update(numberOfRoomHall);

            return RedirectToAction("List", "NumberOfRoomHall");
        }


        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            NumberOfRoomHall numberOfRoomHall = _numberOfRoomHallRepository.GetById(id);

            if (numberOfRoomHall == null) return NotFound();

            await _numberOfRoomHallRepository.Remove(numberOfRoomHall);

            return Ok();
        }
    }
}
