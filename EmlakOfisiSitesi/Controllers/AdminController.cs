using EmlakOfisiSitesi.FluentValidations;
using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmlakOfisiSitesi.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AdminController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly IRepository<Admin> _adminRepository;
        private readonly IValidator<AdminRegisterViewModel> _adminRegisterValidator;

        public AdminController(UserManager<IdentityUser> userManager, IValidator<AdminRegisterViewModel> adminRegisterValidator, IRepository<Admin> adminRepository)
        {
            _userManager = userManager;
            _adminRegisterValidator = adminRegisterValidator;
            _adminRepository = adminRepository;
        }

        public async Task<IActionResult> AdminList()
        {
            IEnumerable<Admin> admins = _adminRepository.GetAll();
            return View(admins);



        }
        public IActionResult AddAdmin()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminRegisterViewModel model)
        {
            var validationResult = await _adminRegisterValidator.ValidateAsync(model);

            if (validationResult.IsValid)
            {
                var user = new IdentityUser { UserName = model.UserName, Email = model.Email };
                var result = await _userManager.CreateAsync(user, model.Password);

                if (result.Succeeded)
                {
                    await _userManager.AddToRoleAsync(user, "Admin");

                    return RedirectToAction("AdminList");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError("", error.Description);
                }
            }

            return View("AddAdmin", model);
        }
    }
}
