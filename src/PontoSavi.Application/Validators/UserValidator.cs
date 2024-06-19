using FluentValidation;
using PontoSavi.Domain.Entities;

namespace PontoSavi.Application.Validators;

public class UserValidator : AbstractValidator<User>
{
    public UserValidator()
    {
        RuleFor(x => x.UserName)
            .MinimumLength(3).WithMessage(GlobalValidator.MinLength("Nome do usuário", 3))
            .MaximumLength(64).WithMessage(GlobalValidator.MaxLength("Nome do usuário", 64));

        RuleFor(x => x.Email)
            .EmailAddress().WithMessage(GlobalValidator.InvalidEmailFormat());
    }
}