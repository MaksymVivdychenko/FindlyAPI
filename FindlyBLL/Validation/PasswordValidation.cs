using FindlyBLL.DTOs.UserDtos;
using FluentValidation;

namespace FindlyBLL.Validation;

public class UserRegisterValidation : AbstractValidator<RegisterUserDto>
{
    public UserRegisterValidation()
    {
        RuleFor(x => x.Password).PasswordStrength();
    }
}

public class UserChangePasswordValidation : AbstractValidator<UserChangePasswordDto>
{
    public UserChangePasswordValidation()
    {
        RuleFor(x => x.NewPassword).PasswordStrength();
    }
}

public static class PasswordRuleBuilderExtension
{
    public static IRuleBuilderOptions<T, string> PasswordStrength<T>(this IRuleBuilder<T, string> ruleBuilder)
    {
        var options = ruleBuilder
            .NotEmpty().WithMessage("Пароль не має бути порожнім")
            .MinimumLength(8).WithMessage("Мінімум 8 символів")
            .Matches("[A-Z]").WithMessage("Потрібна велика літера")
            .Matches("[a-z]").WithMessage("Потрібна мала літера")
            .Matches("[0-9]").WithMessage("Потрібна цифра")
            .Matches("[^a-zA-Z0-9]").WithMessage("Потрібен спецсимвол");
        return options;
    }
}