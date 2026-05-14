using FluentValidation;

namespace Letstudy.Requests;

public record SubscribeToBoardRequest(
    string AccessKey
);

public class SubscribeToBoardRequestValidator : AbstractValidator<SubscribeToBoardRequest>
{
    public SubscribeToBoardRequestValidator()
    {
        RuleFor(request => request.AccessKey)
            .NotNull()
            .NotEmpty();
    }
}