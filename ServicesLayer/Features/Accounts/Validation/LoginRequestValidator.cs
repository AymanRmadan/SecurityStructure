using FluentValidation;
using ServicesLayer.Features.Accounts.Requests;

namespace Shared.Service.Features.Account.Validators
{
    public class LoginRequestValidator : AbstractValidator<LoginRequest>
    {
        public LoginRequestValidator()
        {

            RuleFor(t => t.UserName)
                .NotEmpty().WithMessage("يجب ألا يكون اليوزرنيم فارغًا");
            RuleFor(t => t.Password)
               .NotEmpty().WithMessage("يجب ألا تكون كلمة المرور فارغة");
        }
    }
}
