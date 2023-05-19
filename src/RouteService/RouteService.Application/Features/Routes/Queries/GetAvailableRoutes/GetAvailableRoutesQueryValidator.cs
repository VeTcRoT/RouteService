using FluentValidation;

namespace RouteService.Application.Features.Routes.Queries.GetAvailableRoutes
{
    public class GetAvailableRoutesQueryValidator : AbstractValidator<GetAvailableRoutesQuery>
    {
        public GetAvailableRoutesQueryValidator()
        {
            RuleFor(q => q.From)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(3).WithMessage("{PropertyName} length should be equal greater to 3.")
                .MaximumLength(30).WithMessage("{PropertyName} length should not exceed 30 characters.");

            RuleFor(q => q.To)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(3).WithMessage("{PropertyName} length should be equal greater to 3.")
                .MaximumLength(30).WithMessage("{PropertyName} length should not exceed 30 characters.");

            RuleFor(q => q.NumberOfSeats)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .LessThanOrEqualTo(5).WithMessage("{PropertyName} should not exceed 5.");
        }
    }
}
