using FluentValidation;

namespace Letstudy.Requests;

public record MoveBlockRequest(
    Guid BlockId,
    int NewOrder
);

public class MoveBlockRequestValidator : AbstractValidator<MoveBlockRequest>
{
    public MoveBlockRequestValidator()
    {
        RuleFor(r => r.BlockId)
            .NotEmpty().WithMessage("Идентификатор блока обязателен");
        RuleFor(r => r.NewOrder)
            .GreaterThan(0).WithMessage("Порядок блока должен быть больше 0");
    }
}