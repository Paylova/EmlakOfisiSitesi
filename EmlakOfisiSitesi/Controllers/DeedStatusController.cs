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
    public class DeedStatusController : Controller
    {
        private readonly IRepository<DeedStatus> _deedStatusRepository;
        private readonly IValidator<DeedStatusViewModel> _deedStatusValidator;

        public DeedStatusController(IRepository<DeedStatus> deedStatusRepository, IValidator<DeedStatusViewModel> deedStatusValidator)
        {
            _deedStatusRepository = deedStatusRepository;
            _deedStatusValidator = deedStatusValidator;
        }

        [HttpGet]

        public IActionResult List()
        {
            IEnumerable<DeedStatus> deedStatuses = _deedStatusRepository.GetAll();

            return View(deedStatuses);
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(DeedStatusViewModel deedStatusViewModel)
        {
            var validationResult = await _deedStatusValidator.ValidateAsync(deedStatusViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(deedStatusViewModel);
            }

            DeedStatus deedStatus = new DeedStatus
            {
                Name = deedStatusViewModel.Name,
                IsActive = false,
            };

            await _deedStatusRepository.Add(deedStatus);

            return RedirectToAction("List", "DeedStatus");
        }


        [HttpGet]
        public IActionResult Update(Guid id)
        {
            DeedStatus deedStatus = _deedStatusRepository.GetById(id);

            if (deedStatus == null) return NotFound();

            DeedStatusViewModel deedStatusViewModel = new DeedStatusViewModel
            {
                Id = deedStatus.Id,
                Name = deedStatus.Name,
                IsActive = deedStatus.IsActive,
            };

            return View(deedStatusViewModel);
        }

        [HttpPost]
        public async Task<IActionResult> Update(DeedStatusViewModel deedStatusViewModel)
        {
            var validationResult = await _deedStatusValidator.ValidateAsync(deedStatusViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(deedStatusViewModel);
            }

            DeedStatus deedStatus = _deedStatusRepository.GetById(deedStatusViewModel.Id);
            deedStatus.Name = deedStatusViewModel.Name;
            deedStatus.IsActive = deedStatusViewModel.IsActive;

            await _deedStatusRepository.Update(deedStatus);

            return RedirectToAction("List", "DeedStatus");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(Guid id)
        {
            DeedStatus deedStatus = _deedStatusRepository.GetById(id);

            if (deedStatus == null) return NotFound();

            await _deedStatusRepository.Remove(deedStatus);

            return Ok();
        }
    }
}
