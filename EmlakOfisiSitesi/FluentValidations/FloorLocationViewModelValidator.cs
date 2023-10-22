using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class FloorLocationViewModelValidator : AbstractValidator<FloorLocationViewModel>
    {
        public FloorLocationViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Kat konumu adı boş olamaz.");
        }
    }
}
