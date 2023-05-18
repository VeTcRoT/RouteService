using AutoMapper;
using MediatR;
using RouteService.Application.Exceptions;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.Routes.Queries.GetAvailableRoutes
{
    public class GetAvailableRoutesQueryHandler : IRequestHandler<GetAvailableRoutesQuery, IEnumerable<RideDto>?>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetAvailableRoutesQueryHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<IEnumerable<RideDto>?> Handle(GetAvailableRoutesQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAvailableRoutesQueryValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var availableRides = await _unitOfWork.RouteRepository.GetAvailableRoutesByQueryAsync(request.From, request.To, request.DepartureTime, request.NumberOfSeats);

            var rideDtos = _mapper.Map<IEnumerable<RideDto>>(availableRides);

            return rideDtos;
        }
    }
}
