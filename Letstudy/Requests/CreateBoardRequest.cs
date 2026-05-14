using FluentValidation;

namespace Letstudy.Requests;

public record CreateBoardRequest(
    string Title
);

public class CreateBoardRequestValidator : AbstractValidator<CreateBoardRequest>
{
    public CreateBoardRequestValidator()
    {
        RuleFor(x => x.Title)
            .NotEmpty().WithMessage("Заголовок доски обязателен");
    }
}