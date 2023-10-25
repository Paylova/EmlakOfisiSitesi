using EmlakOfisiSitesi.Models.Entities;
using EmlakOfisiSitesi.Repositories;
using EmlakOfisiSitesi.ViewModels;
using FluentValidation;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EmlakOfisiSitesi.Controllers
{
    [Authorize(Policy = "Admin")]
    public class AgentController : Controller
    {
        readonly private IRepository<Agent> _agentRepository;
        readonly private IValidator<AgentViewModel> _agentValidator;

        public AgentController(IRepository<Agent> agentRepository, IValidator<AgentViewModel> agentValidator)
        {
            _agentRepository = agentRepository;
            _agentValidator = agentValidator;
        }

        [HttpGet]

        public IActionResult List()
        {
            IEnumerable<Agent> agents = _agentRepository.GetAll();

            return View(agents);
        }

        [HttpGet]
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
        public async Task<IActionResult> Delete(Guid id)
        {
            Agent agent = _agentRepository.GetById(id);

            if (agent == null)
                return NotFound();

            await _agentRepository.Remove(agent);

            return Ok(agent);
        }
    }
}
