using AutoMapper;
using MediatR;
using RouteService.Application.Exceptions;
using RouteService.Application.Features.Routes.Queries.GetAvailableRoutes;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.Routes.Queries.GetRideById
{
    public class GetRideByIdQueryHandler : IRequestHandler<GetRideByIdQuery, RideDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetRideByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<RideDto> Handle(GetRideByIdQuery request, CancellationToken cancellationToken)
        {
            var ride = await _unitOfWork.RouteRepository.GetRideByIdAsync(request.RideId);

            if (ride == null)
                throw new NotFoundException(nameof(Ride), request.RideId);

            return _mapper.Map<RideDto>(ride);
        }
    }
}
