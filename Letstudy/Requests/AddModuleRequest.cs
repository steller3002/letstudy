using FluentValidation;
using Letstudy.Models;

namespace Letstudy.Requests;

public record AddModuleRequest(
    string Title
);

public class AddModuleRequestValidator : AbstractValidator<AddModuleRequest>
{
    public AddModuleRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Заголовок модуля обязателен");
    }
}