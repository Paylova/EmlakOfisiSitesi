using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace EmlakOfisiSitesi.Controllers
{

    public class AgentController : Controller
    {
        private readonly IRepository<Agent> _agentRepository;
        private readonly IValidator<AgentViewModel> _agentValidator;
        private readonly IValidator<ChangePasswordViewModel> _changePasswordValidator;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly IValidator<RegisterViewModel> _registerValidator;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AgentController(IRepository<Agent> agentRepository, IValidator<AgentViewModel> agentValidator, IValidator<ChangePasswordViewModel> changePasswordValidator, UserManager<IdentityUser> userManager, SignInManager<IdentityUser> signInManager, IValidator<RegisterViewModel> registerValidator, RoleManager<IdentityRole> roleManager)
        {
            _agentRepository = agentRepository;
            _agentValidator = agentValidator;
            _changePasswordValidator = changePasswordValidator;
            _userManager = userManager;
            _signInManager = signInManager;
            _registerValidator = registerValidator;
            _roleManager = roleManager;
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult List()
        {
            IEnumerable<Agent> agents = _agentRepository.GetAll();

            return View(agents);
        }

        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult Update(Guid id)
        {
            Agent agent = _agentRepository.GetById(id);

            if (agent == null)
                return NotFound();

            AgentViewModel agentViewModel = new AgentViewModel
            {
                Id = agent.Id,
                Name = agent.Name,
                CompanyName = agent.CompanyName,
                Email = agent.Email,
                Surname = agent.Surname,
                UserName = agent.UserName,
            };

            return View(agentViewModel);
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Update(AgentViewModel agentViewModel)
        {
            var validationResult = await _agentValidator.ValidateAsync(agentViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(agentViewModel);
            }

            Agent agent = _agentRepository.GetById(Guid.Parse(agentViewModel.Id));

            agent.Name = agentViewModel.Name;
            agent.Surname = agentViewModel.Surname;
            agent.UserName = agentViewModel.UserName;
            agent.CompanyName = agentViewModel.CompanyName;
            agent.Email = agentViewModel.Email;

            await _agentRepository.Update(agent);

            return RedirectToAction("List", "Agent");
        }

        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> Delete(Guid id)
        {
            Agent agent = _agentRepository.GetById(id);

            if (agent == null)
                return NotFound();

            await _agentRepository.Remove(agent);

            return Ok(agent);
        }
        [HttpGet]
        [Authorize(Roles = "Agent")]
        public IActionResult ChangePassword()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Agent")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel changePasswordViewModel)
        {
            var validationResult = await _changePasswordValidator.ValidateAsync(changePasswordViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

                return View(changePasswordViewModel);
            }
            var userId = Request.Cookies["userId"];
            if (!string.IsNullOrEmpty(userId))
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user != null)
                {
                    var changePasswordResult = await _userManager.ChangePasswordAsync(user, changePasswordViewModel.OldPassword, changePasswordViewModel.Password);
                    if (changePasswordResult.Succeeded)
                    {
                        await _signInManager.RefreshSignInAsync(user);
                        return RedirectToAction("Index", "Admin");
                    }
                }
            }

            return View();
        }
        [HttpGet]
        [Authorize(Policy = "Admin")]
        public IActionResult AddAgent()
        {
            return View();
        }
        [HttpPost]
        [Authorize(Policy = "Admin")]
        public async Task<IActionResult> AddAgent(RegisterViewModel registerViewModel)
        {
            var validationResult = await _registerValidator.ValidateAsync(registerViewModel);

            if (!validationResult.IsValid)
            {
                foreach (var error in validationResult.Errors)
                    ModelState.AddModelError("", error.ErrorMessage);

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

                return RedirectToAction("Login", "Auth");
            }
            return View();
        }


    }
}
