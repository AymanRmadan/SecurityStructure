using FluentValidation;
using ServicesLayer.Features.Accounts.Requests;

namespace ServicesLayer.Features.Accounts.Validation
{
    public class ResetPasswordRequestValidator : AbstractValidator<ResetPasswordRequest>
    {
        public ResetPasswordRequestValidator()
        {
            //RuleFor(t => t.Email)
            //   .NotEmpty().WithMessage("يجب ألا يكون البريد الإلكتروني فارغًا");
            //RuleFor(t => t.Token)
            //    .NotEmpty().WithMessage("يجب ألا يكون الرمز فارغًا");

            //RuleFor(p => p.NewPassword)
            //   .NotEmpty().WithMessage("كلمة المرور الجديدة لا يمكن أن تكون فارغة")
            //   .MinimumLength(8).WithMessage("يجب أن يكون طول كلمة المرور الجديدة على الأقل 8 أحرف")
            //   .MaximumLength(16).WithMessage("يجب ألا يتجاوز طول كلمة المرور الجديدة 16 حرفًا")
            //   .Matches(@"[A-Z]+").WithMessage("يجب أن تحتوي كلمة المرور الجديدة على حرف كبير على الأقل")
            //   .Matches(@"[a-z]+").WithMessage("يجب أن تحتوي كلمة المرور الجديدة على حرف صغير على الأقل")
            //   .Matches(@"[0-9]+").WithMessage("يجب أن تحتوي كلمة المرور الجديدة على رقم واحد على الأقل")
            //   .Matches(@"[@$!%*?&:=+\-/#%.()<>\{\}_~,;]+").WithMessage("يجب أن تحتوي كلمة المرور الجديدة على حرف رمزي واحد على الأقل");


            //RuleFor(t => t.ConfirmPassword)
            // .NotEmpty().WithMessage("يجب ألا تكون كلمة تأكيد المرور فارغة")
            // .Equal(model => model.NewPassword).WithMessage("كلمة المرور غير متطابقة");
        }
    }
}
