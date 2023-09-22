using FluentValidation;
using People.Application.CQRS.Commands.AccountCommands.Requests;
using People.Domain.Constants;

namespace People.Application.Validators.AccountValidators;

public class EditAccountCommandValidator : AbstractValidator<EditAccountCommand>
{
    public EditAccountCommandValidator()
    {
        RuleFor(account => account.Email)
            .Empty().When(model => string.IsNullOrWhiteSpace(model.Email))
            .EmailAddress();

        RuleFor(p => p.Phone)
            .Empty().When(model => string.IsNullOrWhiteSpace(model.Phone))
            .MinimumLength(10)
            .MaximumLength(30)
            .Matches(RegexPatterns.PhoneNumber)
            ;

        RuleFor(account => account.Name)
            .NotEmpty()
            .MaximumLength(255);

        RuleFor(account => account.Address)
            .Empty().When(model => string.IsNullOrWhiteSpace(model.Address))
            .MinimumLength(2)
            .MaximumLength(255);
    }
}