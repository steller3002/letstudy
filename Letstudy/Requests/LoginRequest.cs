using FluentValidation;

namespace Letstudy.Requests;

public record LoginRequest(
    string Email,
    string Password
);

public class LoginRequestValidator : AbstractValidator<LoginRequest>
{
    public LoginRequestValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email адрес обязателен")
            .EmailAddress().WithMessage("Неверный формат адреса");
        RuleFor(x => x.Password)
            .MinimumLength(4).WithMessage("Пароль обязателен");
    }
}