using FluentValidation;

namespace RouteService.Application.Features.RoutesInfo.Commands.CreateRouteInfo
{
    public class CreateRouteInfoCommandValidator : AbstractValidator<CreateRouteInfoCommand>
    {
        public CreateRouteInfoCommandValidator()
        {
            RuleFor(c => c.ExtraInfo)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .MinimumLength(20).WithMessage("{PropertyName} length must be greater or equal to 20.")
                .MaximumLength(1000).WithMessage("{PropertyName} length must not exceed 1000 characters.");
        }
    }
}
