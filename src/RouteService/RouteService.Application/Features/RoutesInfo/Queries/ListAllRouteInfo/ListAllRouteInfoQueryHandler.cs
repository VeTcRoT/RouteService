using MediatR;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.RoutesInfo.Queries.ListAllRouteInfo
{
    public class ListAllRouteInfoQueryHandler : IRequestHandler<ListAllRouteInfoQuery, IEnumerable<Route>>
    {
        private readonly IUnitOfWork _unitOfWork;

        public ListAllRouteInfoQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Route>> Handle(ListAllRouteInfoQuery request, CancellationToken cancellationToken)
        {
            return await _unitOfWork.RouteInfoRepository.ListAllAsync();
        }
    }
}
