using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

namespace EmlakOfisiSitesi.Controllers
{
    [Authorize]
    public class FacadeController : Controller
    {
        private readonly IRepository<Facade> _facadeRepository;
        private readonly IValidator<FacadeViewModel> _facadeValidator;

        public FacadeController(IRepository<Facade> facadeRepository, IValidator<FacadeViewModel> facadeValidator)
        {
            _facadeRepository = facadeRepository;
            _facadeValidator = facadeValidator;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult List()
        {
            IEnumerable<Facade> facades = _facadeRepository.GetAll();

            return View(facades);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Create(FacadeViewModel facadeViewModel)
        {
            var validationResult = await _facadeValidator.ValidateAsync(facadeViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(facadeViewModel);
            }

            Facade facade = new Facade
            {
                Name = facadeViewModel.Name,
                IsActive = false,
            };

            await _facadeRepository.Add(facade);

            return RedirectToAction("List", "Facade");
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Update(Guid id)
        {
            Facade facade = _facadeRepository.GetById(id);

            if (facade == null)
                return NotFound();

            FacadeViewModel facadeViewModel = new FacadeViewModel
            {
                Id = facade.Id,
                Name = facade.Name,
                IsActive = facade.IsActive,
            };

            return View(facadeViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update(FacadeViewModel facadeViewModel)
        {
            var validationResult = await _facadeValidator.ValidateAsync(facadeViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(facadeViewModel);
            }

            Facade facade = _facadeRepository.GetById(facadeViewModel.Id);
            facade.Name = facadeViewModel.Name;
            facade.IsActive = facadeViewModel.IsActive;

            await _facadeRepository.Update(facade);

            return RedirectToAction("List", "Facade");
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Facade facade = _facadeRepository.GetById(id);

            if (facade == null)
                return NotFound();

            await _facadeRepository.Remove(facade);

            return Ok();
        }
    }
}
