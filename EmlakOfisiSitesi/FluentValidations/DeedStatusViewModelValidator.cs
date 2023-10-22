using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class DeedStatusViewModelValidator : AbstractValidator<DeedStatusViewModel>
    {
        public DeedStatusViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Tapu durumu adı boş olamaz.");
        }
    }
}
