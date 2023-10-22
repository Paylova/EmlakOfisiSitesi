using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmlakOfisiSitesi.Controllers
{
    [Authorize]
    public class BuildingAgeController : Controller
    {
        private readonly IRepository<BuildingAge> _buildingAgeRepository;
        private readonly IValidator<BuildingAgeViewModel> _buildingAgeValidator;

        public BuildingAgeController(IRepository<BuildingAge> buildingAgeRepository, IValidator<BuildingAgeViewModel> buildingAgeValidator)
        {
            _buildingAgeRepository = buildingAgeRepository;
            _buildingAgeValidator = buildingAgeValidator;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult List()
        {
            IEnumerable<BuildingAge> buildingAges = _buildingAgeRepository.GetAll();

            return View(buildingAges);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create(BuildingAgeViewModel buildingAgeViewModel)
        {
            var validationResult = await _buildingAgeValidator.ValidateAsync(buildingAgeViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(buildingAgeViewModel);
            }

            BuildingAge buildingAge = new BuildingAge
            {
                Name = buildingAgeViewModel.Name,
                Min = Convert.ToInt32(buildingAgeViewModel.Min),
                Max = Convert.ToInt32(buildingAgeViewModel.Max),
                IsAndOver = buildingAgeViewModel.IsAndOver,
                IsActive = false
            };

            await _buildingAgeRepository.Add(buildingAge);

            return RedirectToAction("List", "BuildingAge");
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Update(Guid id)
        {
            BuildingAge buildingAge = _buildingAgeRepository.GetById(id);

            if (buildingAge == null)
                return NotFound();

            BuildingAgeViewModel buildingAgeViewModel = new BuildingAgeViewModel
            {
                Id = buildingAge.Id,
                Name = buildingAge.Name,
                Min = Convert.ToInt32(buildingAge.Min),
                Max = Convert.ToInt32(buildingAge.Max),
                IsAndOver = buildingAge.IsAndOver,
                IsActive = buildingAge.IsActive
            };

            return View(buildingAgeViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update(BuildingAgeViewModel buildingAgeViewModel)
        {
            var validationResult = await _buildingAgeValidator.ValidateAsync(buildingAgeViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(buildingAgeViewModel);
            }

            BuildingAge buildingAge = _buildingAgeRepository.GetById(buildingAgeViewModel.Id);
            buildingAge.Name = buildingAgeViewModel.Name;
            buildingAge.Min = Convert.ToInt32(buildingAgeViewModel.Min);
            buildingAge.Max = Convert.ToInt32(buildingAgeViewModel.Max);
            buildingAge.IsAndOver = buildingAgeViewModel.IsAndOver;
            buildingAge.IsActive = buildingAgeViewModel.IsActive;

            await _buildingAgeRepository.Update(buildingAge);

            return RedirectToAction("List", "BuildingAge");
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            BuildingAge buildingAge = _buildingAgeRepository.GetById(id);

            if (buildingAge == null)
                return NotFound();

            await _buildingAgeRepository.Remove(buildingAge);

            return Ok();
        }

    }
}
