using AutoMapper;
using MediatR;
using RouteService.Application.Exceptions;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.Routes.Commands.CreateRide
{
    public class CreateRideCommandHandler : IRequestHandler<CreateRideCommand, CreateRideDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRideCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateRideDto> Handle(CreateRideCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateRideCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var ride = _mapper.Map<Ride>(request);

            var createdRide = await _unitOfWork.RouteRepository.CreateAsync(ride);

            return _mapper.Map<CreateRideDto>(createdRide);
        }
    }
}
