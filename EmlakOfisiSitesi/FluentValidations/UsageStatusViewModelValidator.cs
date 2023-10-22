using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class UsageStatusViewModelValidator : AbstractValidator<UsageStatusViewModel>
    {
        public UsageStatusViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Kullanım durumu adı boş olamaz.");
        }
    }
}
