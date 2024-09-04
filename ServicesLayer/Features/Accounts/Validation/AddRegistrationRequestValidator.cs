using FluentValidation;
using ServicesLayer.Features.Accounts.Requests;

public class AddRegistrationRequestValidator : AbstractValidator<AddRegistrationRequest>
{

    public AddRegistrationRequestValidator()
    {
        RuleFor(t => t.Email)
             .NotEmpty().WithMessage("{PropertyName} must not be empty")
             .EmailAddress()
             // .MustAsync()
             .WithMessage("البريد الإلكتروني مستخدم بالفعل");

        RuleFor(p => p.Password)
                 .NotEmpty().WithMessage("كلمة المرور لا يمكن أن تكون فارغة")
               .MinimumLength(8).WithMessage("يجب أن يكون طول كلمة المرور على الأقل 8 أحرف")
               .MaximumLength(16).WithMessage("يجب ألا يتجاوز طول كلمة المرور 16 حرفًا")
               .Matches(@"[A-Z]+").WithMessage("يجب أن تحتوي كلمة المرور على حرف كبير على الأقل")
               .Matches(@"[a-z]+").WithMessage("يجب أن تحتوي كلمة المرور على حرف صغير على الأقل")
               .Matches(@"[0-9]+").WithMessage("يجب أن تحتوي كلمة المرور على رقم واحد على الأقل")
               .Matches(@"[@$!%*?&:=+\-/#%.()<>\{\}_~,;]+")
               .WithMessage("يجب أن تحتوي كلمة المرور على حرف رمزي واحد على الأقل");

        RuleFor(t => t.ConfirmPassword)
           .NotEmpty().WithMessage("يجب ألا يكون تأكيد كلمة المرور فارغًا")
           .Equal(model => model.Password).WithMessage("كلمة المرور غير متطابقة");
    }

}
