using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class HeatingTypeViewModelValidator : AbstractValidator<HeatingTypeViewModel>
    {
        public HeatingTypeViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("Isınma tipi adı boş olamaz.");
        }
    }
}
