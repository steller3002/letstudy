using FluentValidation;
using Letstudy.Models;

namespace Letstudy.Requests;

public record RegisterRequest(
    string Name,
    string Surname,
    string Email,
    string Password,
    string ConfirmPassword,
    UserRole Role
);

public class RegisterRequestValidator : AbstractValidator<RegisterRequest>
{
    public RegisterRequestValidator()
    {
        RuleFor(x => x.Name)
            .NotEmpty().WithMessage("Это обязательное поле");
        RuleFor(x => x.Surname)
            .NotEmpty().WithMessage("Это обязательное поле");
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("Email адрес обязателен")
            .EmailAddress().WithMessage("Неверный формат адреса");
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("Пароль обязателен");
        RuleFor(x => x.ConfirmPassword)
            .NotEmpty().WithMessage("Подтверждение пароля обязательно")
            .Equal(x => x.Password).WithMessage("Пароли не совпадают");
        RuleFor(x => x.Role)
            .IsInEnum().WithMessage("Неверный тип роли");
    }
}