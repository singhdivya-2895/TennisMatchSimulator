using FluentValidation;
using RestApi.DTO;

namespace RestApi.Validator
{
    public class TennisMatchDtoValidator : AbstractValidator<TennisMatchDto>
    {
        public TennisMatchDtoValidator()
        {
            RuleFor(dto => dto.Players)
            .NotNull().WithMessage("Players must not be null.")
            .NotEmpty().WithMessage("Players must not be empty.")
            .Must(players => players.Length == 2).WithMessage("Players count must be exactly 2.");

            RuleFor(dto => dto.Points)
                .NotNull().WithMessage("Points must not be null.")
                .NotEmpty().WithMessage("Points must not be empty.");
        }
    }
}
