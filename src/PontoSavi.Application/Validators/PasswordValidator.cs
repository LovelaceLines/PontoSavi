using FluentValidation;

namespace PontoSavi.Application.Validators;

public class PasswordValidator : AbstractValidator<string>
{
    public PasswordValidator()
    {
        RuleFor(password => password)
            .MinimumLength(6).WithMessage(GlobalValidator.MinLength("Senha", 6))
            .MaximumLength(16).WithMessage(GlobalValidator.MaxLength("Senha", 16))
            // Has uppercase letter, lowercase letter, number and special character
            .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[^\da-zA-Z]).{6,16}$")
                .WithMessage("Senha inválida. Use uma senha com pelo menos uma letra maiúscula, uma letra minúscula, um número e um caractere especial.");
    }
}