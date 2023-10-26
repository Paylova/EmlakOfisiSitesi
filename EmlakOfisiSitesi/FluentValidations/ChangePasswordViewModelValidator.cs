using EmlakOfisiSitesi.ViewModels;
using FluentValidation;

namespace EmlakOfisiSitesi.FluentValidations
{
    public class ChangePasswordViewModelValidator : AbstractValidator<ChangePasswordViewModel>
    {
        public ChangePasswordViewModelValidator()
        {
            RuleFor(model => model.OldPassword)
                .NotEmpty().WithMessage("Eski şifre gereklidir.");

            RuleFor(model => model.Password)
                .NotEmpty().WithMessage("Yeni şifre gereklidir.")
                .MinimumLength(6).WithMessage("Şifre en az 6 karakter olmalıdır.");

            RuleFor(model => model.ConfirmPassword)
                .Equal(model => model.Password).WithMessage("Şifreler uyuşmuyor.");
        }
    }
}
