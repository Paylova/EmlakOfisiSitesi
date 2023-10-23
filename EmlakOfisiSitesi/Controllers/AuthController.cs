using EmlakOfisiSitesi.ViewModels;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using FluentValidation;
using EmlakOfisiSitesi.Models.Entities;

namespace EmlakOfisiSitesi.Controllers
{

    public class AuthController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IValidator<LoginViewModel> _loginValidator;
        private readonly IValidator<RegisterViewModel> _registerValidator;

        public AuthController(UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, RoleManager<IdentityRole> roleManager, IValidator<LoginViewModel> loginValidator, IValidator<RegisterViewModel> registerValidator)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
            _loginValidator = loginValidator;
            _registerValidator = registerValidator;
        }

        [HttpGet]
        public IActionResult Login()
        {

            return View(new LoginViewModel());
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            var validationResult = await _loginValidator.ValidateAsync(loginViewModel);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View(loginViewModel);
            }
            var result = await _signInManager.PasswordSignInAsync(loginViewModel.UserName, loginViewModel.Password, false, false);
            if (result.Succeeded)
            {
                var user = await _userManager.FindByNameAsync(loginViewModel.UserName);
                if (user != null)
                {
                    var userId = user.Id;

                    Response.Cookies.Append("userId", userId);
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.NameIdentifier, user.Id)
                    };

                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    var authProperties = new AuthenticationProperties
                    {
                        IsPersistent = true,
                        ExpiresUtc = DateTimeOffset.UtcNow.AddMinutes(30)
                    };

                    await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity), authProperties);

                    return RedirectToAction("Index", "Admin");
                }
                return NotFound();
            }

            else
            {
                TempData["ErrorMessage"] = "Kullanıcı adı veya şifreniz yanlıştır";
                return View(loginViewModel);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Logout()
        {
            await _signInManager.SignOutAsync();
            if (Request.Cookies.ContainsKey("userId"))
            {
                Response.Cookies.Delete("userId");
            }
            return RedirectToAction("Login", "Auth");
        }


        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            var validationResult = await _registerValidator.ValidateAsync(registerViewModel);
            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                {
                    ModelState.AddModelError("", error.ErrorMessage);
                }

                return View(registerViewModel);
            }
            var agent = new Agent
            {
                CompanyName = registerViewModel.CompanyName,
                Name = registerViewModel.Name,
                Surname = registerViewModel.Surname,
                UserName = registerViewModel.UserName,
                PhoneNumber = registerViewModel.PhoneNumber,
                Email = registerViewModel.Email,
            };
            var result = await _userManager.CreateAsync(agent, "123456");
            if (result.Succeeded)
            {
                if (!await _roleManager.RoleExistsAsync("Agent"))
                {
                    var role = new IdentityRole("Agent");
                    await _roleManager.CreateAsync(role);
                }
                await _userManager.AddToRoleAsync(agent, "Agent");
                return RedirectToAction("Login","Auth");
            }
            return View();
        }
    }
}
