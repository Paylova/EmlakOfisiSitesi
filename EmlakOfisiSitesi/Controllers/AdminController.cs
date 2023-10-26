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
        private readonly IValidator<UpdateAdminRegisterViewModel> _updateAdminRegisterValidator;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AdminController(UserManager<IdentityUser> userManager, IValidator<AdminRegisterViewModel> adminRegisterValidator, IRepository<Admin> adminRepository, RoleManager<IdentityRole> roleManager, IValidator<UpdateAdminRegisterViewModel> updateAdminRegisterValidator)
        {
            _userManager = userManager;
            _adminRegisterValidator = adminRegisterValidator;
            _adminRepository = adminRepository;
            _roleManager = roleManager;
            _updateAdminRegisterValidator = updateAdminRegisterValidator;
        }

        [HttpGet]
        public IActionResult AdminList()
        {
            IEnumerable<Admin> admins = _adminRepository.GetAll();
            return View(admins);
        }

        [HttpGet]
        public IActionResult AddAdmin()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> AddAdmin(AdminRegisterViewModel adminRegisterViewModel)
        {
            var validationResult = await _adminRegisterValidator.ValidateAsync(adminRegisterViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(adminRegisterViewModel);
            }

            var admin = new Admin
            {
                Name = adminRegisterViewModel.Name,
                Surname = adminRegisterViewModel.Surname,
                UserName = adminRegisterViewModel.UserName,
                Email = adminRegisterViewModel.Email,
            };

            var result = await _userManager.CreateAsync(admin, adminRegisterViewModel.Password);

            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("AgAdminent"))
                {
                    var role = new IdentityRole("Admin");
                    await _roleManager.CreateAsync(role);
                }
                await _userManager.AddToRoleAsync(admin, "Admin");
                return RedirectToAction("AdminList", "Admin");
            }
            return View();
        }
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Update(Guid id)
        {
            Admin admin = _adminRepository.GetById(id);

            if (admin == null)
                return NotFound();

            UpdateAdminRegisterViewModel adminRegisterViewModel = new UpdateAdminRegisterViewModel
            {
                Id = Guid.Parse(admin.Id),
                Name = admin.Name,
                Email = admin.Email,
                Surname = admin.Surname,
                UserName = admin.UserName,
            };

            return View(adminRegisterViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update(UpdateAdminRegisterViewModel adminRegisterViewModel)
        {
            var validationResult = await _updateAdminRegisterValidator.ValidateAsync(adminRegisterViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(adminRegisterViewModel);
            }

            Admin admin = _adminRepository.GetById(adminRegisterViewModel.Id);

            admin.Name = adminRegisterViewModel.Name;
            admin.Surname = adminRegisterViewModel.Surname;
            admin.UserName = adminRegisterViewModel.UserName;
            admin.Email = adminRegisterViewModel.Email;


            if (adminRegisterViewModel.OldPassword == null || adminRegisterViewModel.OldPassword == "")
            {
                await _adminRepository.Update(admin);
            }
            else
            {
                var user = await _userManager.FindByIdAsync(adminRegisterViewModel.Id.ToString());
                if (user != null)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, adminRegisterViewModel.OldPassword, adminRegisterViewModel.Password);
                    if (changePasswordResult.Succeeded)
                    {
                        return RedirectToAction("AdminList", "Admin");
                    }
                }

                await _adminRepository.Update(admin);
            }


            return RedirectToAction("AdminList", "Admin");
        }
        [HttpPost]
        public async Task<IActionResult> Delete(string id)
        {
            var user = await _userManager.FindByIdAsync(id);

            if (user == null)
            {
                return Json(new { success = false });
            }

            var result = await _userManager.DeleteAsync(user);

            if (result.Succeeded)
            {
                return Json(new { success = true });
            }
            else
            {
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }

                return Json(new { success = false });
            }
        }

    }
}
