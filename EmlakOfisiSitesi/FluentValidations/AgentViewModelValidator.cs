using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class AgentViewModelValidator : AbstractValidator<AgentViewModel>
    {
        public AgentViewModelValidator()
        {
            RuleFor(agent => agent.CompanyName).NotEmpty().WithMessage("Şirket adı boş olamaz");
            RuleFor(agent => agent.Name).NotEmpty().WithMessage("Ad boş olamaz");
            RuleFor(agent => agent.Surname).NotEmpty().WithMessage("Soyad boş olamaz");
            RuleFor(agent => agent.UserName).NotEmpty().WithMessage("Kullanıcı adı boş olamaz");
            RuleFor(agent => agent.Email).NotEmpty().WithMessage("E-posta boş olamaz").EmailAddress().WithMessage("Geçerli bir e-posta adresi giriniz");
            //RuleFor(agent => agent.Password).NotEmpty().WithMessage("Şifre boş olamaz");
        }
    }
}
