using AutoMapper;
using MediatR;
using RouteService.Application.Exceptions;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.RoutesInfo.Commands.UpdateRouteInfo
{
    public class UpdateRouteInfoCommandHandler : IRequestHandler<UpdateRouteInfoCommand>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRouteInfoCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(UpdateRouteInfoCommand request, CancellationToken cancellationToken)
        {
            var routeInfo = await _unitOfWork.RouteInfoRepository.GetByIdAsync(request.Id);

            if (routeInfo == null) 
                throw new NotFoundException("RouteInfo", request.Id);

            _mapper.Map(request, routeInfo, typeof(UpdateRouteInfoCommand), typeof(Route));

            await _unitOfWork.SaveAsync();
        }
    }
}
