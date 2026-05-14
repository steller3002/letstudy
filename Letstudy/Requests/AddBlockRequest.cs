using FluentValidation;

namespace Letstudy.Requests;

public record AddBlockRequest(
    BlockType BlockType,
    string Url,
    int Order
);

public enum BlockType { Text, Image, Exercise }

public class AddBlockRequestValidator : AbstractValidator<AddBlockRequest>
{
    public AddBlockRequestValidator()
    {
        RuleFor(x => x.BlockType)
            .IsInEnum().WithMessage("Неверный тип блока");
        RuleFor(x => x.Url)
            .NotEmpty().WithMessage("Ссылка на изображение обязательна");
        RuleFor(x => x.Order)
            .GreaterThan(0).WithMessage("Порядок блока должен быть больше 0");
    }
}