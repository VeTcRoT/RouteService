using FluentValidation;
using RouteService.Application.Features.Routes.Commands.CreateRide;

namespace RouteService.Application.Features.Routes.Commands.UpdateRide
{
    public class UpdateRideCommandValidator : AbstractValidator<UpdateRideCommand>
    {
        public UpdateRideCommandValidator()
        {
            RuleFor(c => c.Id)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(c => c.RouteId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .Length(10).WithMessage("{PropertyName} length should be equal to 10.");

            RuleFor(c => c.DepartureTime)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(c => c.ArrivalTime)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();

            RuleFor(c => c)
                .Must(AreDateTimesValid).WithMessage("DepartureTime must not be equal or greater than ArrivalTime.")
                .Must(SeatsCheck).WithMessage("SeatsAvailble can't be greater than NumberOfSeats.");

            RuleFor(c => c.From)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(3).WithMessage("{PropertyName} length must be equal or greater than 3.")
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");

            RuleFor(c => c.To)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(3).WithMessage("{PropertyName} length must be equal or greater than 3.")
                .MaximumLength(30).WithMessage("{PropertyName} must not exceed 30 characters.");

            RuleFor(c => c.NumberOfSeats)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .GreaterThanOrEqualTo(30).WithMessage("{PropertyName} must be greater than or equal to 30.")
                .LessThanOrEqualTo(100).WithMessage("{PropertyName} must be less than or equal to 100.");

            RuleFor(c => c.SeatsAvailable)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull();
        }
        private bool AreDateTimesValid(UpdateRideCommand c)
        {
            return c.DepartureTime < c.ArrivalTime;
        }
        private bool SeatsCheck(UpdateRideCommand c)
        {
            return c.SeatsAvailable <= c.NumberOfSeats;
        }
    }
}
