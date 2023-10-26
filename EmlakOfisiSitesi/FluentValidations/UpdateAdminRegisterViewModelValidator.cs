namespace EmlakOfisiSitesi.FluentValidations
{
    using EmlakOfisiSitesi.ViewModels;
    using FluentValidation;

    public class UpdateAdminRegisterViewModelValidator : AbstractValidator<UpdateAdminRegisterViewModel>
    {
        public UpdateAdminRegisterViewModelValidator()
        {
            RuleFor(model => model.Name).NotEmpty().WithMessage("İsim alanı boş olamaz.");
            RuleFor(model => model.Surname).NotEmpty().WithMessage("Soyisim alanı boş olamaz.");
            RuleFor(model => model.UserName).NotEmpty().WithMessage("Kullanıcı adı alanı boş olamaz.");
            RuleFor(model => model.Email).NotEmpty().WithMessage("Email alanı boş olamaz.").EmailAddress().WithMessage("Geçerli bir email adresi girin.");
            RuleFor(model => model.Password);
            RuleFor(model => model.ConfirmPassword).Equal(model => model.Password).WithMessage("Yeni şifre ve onay şifresi eşleşmiyor.");
        }
    }

}
