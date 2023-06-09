﻿using FluentValidation;

namespace RouteService.Application.Features.Routes.Commands.BookRide
{
    public class BookRideCommandValidator : AbstractValidator<BookRideCommand>
    {
        public BookRideCommandValidator()
        {
            RuleFor(q => q.RouteId)
                .NotEmpty().WithMessage("{PropertyName} is required.")
                .NotNull()
                .MinimumLength(3).WithMessage("{PropertyName} length should be equal greater to 3.")
                .MaximumLength(64).WithMessage("{PropertyName} length should not exceed 30 characters.");

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
