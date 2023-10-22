using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmlakOfisiSitesi.Controllers
{
    public class DateOfAdvertisementController : Controller
    {
        private readonly IRepository<DateOfAdvertisement> _dateOfAdvertisementRepository;
        private readonly IValidator<DateOfAdvertisementViewModel> _dateOfAdvertisementValidator;

        public DateOfAdvertisementController(IRepository<DateOfAdvertisement> dateOfAdvertisementRepository, IValidator<DateOfAdvertisementViewModel> dateOfAdvertisementValidator)
        {
            _dateOfAdvertisementRepository = dateOfAdvertisementRepository;
            _dateOfAdvertisementValidator = dateOfAdvertisementValidator;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult List()
        {
            IEnumerable<DateOfAdvertisement> dateOfAdvertisements = _dateOfAdvertisementRepository.GetAll();

            return View(dateOfAdvertisements);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create(DateOfAdvertisementViewModel dateOfAdvertisementViewModel)
        {
            var validationResult = await _dateOfAdvertisementValidator.ValidateAsync(dateOfAdvertisementViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(dateOfAdvertisementViewModel);
            }

            DateOfAdvertisement dateOfAdvertisement = new DateOfAdvertisement
            {
                Name = dateOfAdvertisementViewModel.Name,
                Day = Convert.ToInt32(dateOfAdvertisementViewModel.Day),
                IsActive = false,
            };

            await _dateOfAdvertisementRepository.Add(dateOfAdvertisement);

            return RedirectToAction("List", "DateOfAdvertisement");
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Update(Guid id)
        {
            DateOfAdvertisement dateOfAdvertisement = _dateOfAdvertisementRepository.GetById(id);

            if (dateOfAdvertisement == null)
                return NotFound();

            DateOfAdvertisementViewModel dateOfAdvertisementViewModel = new DateOfAdvertisementViewModel
            {
                Id = dateOfAdvertisement.Id,
                Day = Convert.ToInt32(dateOfAdvertisement.Day),
                IsActive = dateOfAdvertisement.IsActive,
                Name = dateOfAdvertisement.Name
            };

            return View(dateOfAdvertisementViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update(DateOfAdvertisementViewModel dateOfAdvertisementViewModel)
        {
            var validationResult = await _dateOfAdvertisementValidator.ValidateAsync(dateOfAdvertisementViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(dateOfAdvertisementViewModel);
            }

            DateOfAdvertisement dateOfAdvertisement = _dateOfAdvertisementRepository.GetById(dateOfAdvertisementViewModel.Id);
            dateOfAdvertisement.Day = Convert.ToInt32(dateOfAdvertisementViewModel.Day);
            dateOfAdvertisement.Name = dateOfAdvertisementViewModel.Name;
            dateOfAdvertisement.IsActive = dateOfAdvertisementViewModel.IsActive;

            await _dateOfAdvertisementRepository.Update(dateOfAdvertisement);

            return RedirectToAction("List", "DateOfAdvertisement");
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            DateOfAdvertisement dateOfAdvertisement = _dateOfAdvertisementRepository.GetById(id);

            if (dateOfAdvertisement == null)
                return NotFound();

            await _dateOfAdvertisementRepository.Remove(dateOfAdvertisement);

            return Ok();
        }
    }
}
