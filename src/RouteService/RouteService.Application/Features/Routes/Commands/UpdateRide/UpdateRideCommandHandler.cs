﻿using AutoMapper;
using MediatR;
using RouteService.Application.Exceptions;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.Routes.Commands.UpdateRide
{
    public class UpdateRideCommandHandler : IRequestHandler<UpdateRideCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRideCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateRideCommand request, CancellationToken cancellationToken)
        {
            var validator = new UpdateRideCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var rideToUpdate = await _unitOfWork.RouteRepository.GetByIdAsync(request.Id);

            if (rideToUpdate == null)
                throw new NotFoundException(nameof(Ride), request.Id);

            _mapper.Map(request, rideToUpdate, typeof(UpdateRideCommand), typeof(Ride));

            await _unitOfWork.SaveAsync();
        }
    }
}
