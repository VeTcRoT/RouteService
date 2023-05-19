using AutoMapper;
using MediatR;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.Routes.Queries.ListAllRides
{
    public class ListAllRidesQueryHandler : IRequestHandler<ListAllRidesQuery, IEnumerable<ListAllRidesDto>>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public ListAllRidesQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<ListAllRidesDto>> Handle(ListAllRidesQuery request, CancellationToken cancellationToken)
        {
            var allRides = await _unitOfWork.RouteRepository.ListAllRidesAsync();

            return _mapper.Map<IEnumerable<ListAllRidesDto>>(allRides);
        }
    }
}
