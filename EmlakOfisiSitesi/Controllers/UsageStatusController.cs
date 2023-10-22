using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmlakOfisiSitesi.Controllers
{
    public class UsageStatusController : Controller
    {
        private readonly IRepository<UsageStatus> _usageStatusRepository;
        private readonly IValidator<UsageStatusViewModel> _usageStatusValidator;

        public UsageStatusController(IRepository<UsageStatus> usageStatusRepository, IValidator<UsageStatusViewModel> usageStatusValidator)
        {
            _usageStatusRepository = usageStatusRepository;
            _usageStatusValidator = usageStatusValidator;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult List()
        {
            IEnumerable<UsageStatus> usageStatuses = _usageStatusRepository.GetAll();

            return View(usageStatuses);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create(UsageStatusViewModel usageStatusViewModel)
        {
            var validationResult = await _usageStatusValidator.ValidateAsync(usageStatusViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(usageStatusViewModel);
            }

            UsageStatus usageStatus = new UsageStatus
            {
                Name = usageStatusViewModel.Name,
                IsActive = false
            };

            await _usageStatusRepository.Add(usageStatus);

            return RedirectToAction("List", "UsageStatus");
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Update(Guid id)
        {
            UsageStatus usageStatus = _usageStatusRepository.GetById(id);

            if (usageStatus == null)
                return NotFound();

            UsageStatusViewModel usageStatusViewModel = new UsageStatusViewModel
            {
                Id = usageStatus.Id,
                Name = usageStatus.Name,
                IsActive = usageStatus.IsActive,
            };

            return View(usageStatusViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update(UsageStatusViewModel usageStatusViewModel)
        {
            var validationResult = await _usageStatusValidator.ValidateAsync(usageStatusViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(usageStatusViewModel);
            }

            UsageStatus usageStatus = _usageStatusRepository.GetById(usageStatusViewModel.Id);
            usageStatus.Name = usageStatusViewModel.Name;
            usageStatus.IsActive = usageStatusViewModel.IsActive;

            await _usageStatusRepository.Update(usageStatus);

            return RedirectToAction("List", "UsageStatus");
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            UsageStatus usageStatus = _usageStatusRepository.GetById(id);

            if (usageStatus != null)
                return NotFound();

            await _usageStatusRepository.Remove(usageStatus);

            return Ok();
        }
    }
}
