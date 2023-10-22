using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class FacadeViewModelValidator : AbstractValidator<FacadeViewModel>
    {
        public FacadeViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Yüzey adı boş olamaz.");
        }
    }
}
