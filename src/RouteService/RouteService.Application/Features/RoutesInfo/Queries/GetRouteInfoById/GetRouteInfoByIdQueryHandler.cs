using AutoMapper;
using MediatR;
using RouteService.Application.Exceptions;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.RoutesInfo.Queries.GetRouteInfoById
{
    public class GetRouteInfoByIdQueryHandler : IRequestHandler<GetRouteInfoByIdQuery, RouteInfoDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public GetRouteInfoByIdQueryHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<RouteInfoDto> Handle(GetRouteInfoByIdQuery request, CancellationToken cancellationToken)
        {
            var routeInfo = await _unitOfWork.RouteInfoRepository.GetByIdAsync(request.RouteInfoId);

            if (routeInfo == null)
                throw new NotFoundException("RouteInfoId", request.RouteInfoId); 

            return _mapper.Map<RouteInfoDto>(routeInfo);
        }
    }
}
