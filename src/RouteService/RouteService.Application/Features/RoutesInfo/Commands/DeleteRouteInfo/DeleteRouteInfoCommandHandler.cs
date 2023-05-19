using MediatR;
using RouteService.Application.Exceptions;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.RoutesInfo.Commands.DeleteRouteInfo
{
    public class DeleteRouteInfoCommandHandler : IRequestHandler<DeleteRouteInfoCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRouteInfoCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteRouteInfoCommand request, CancellationToken cancellationToken)
        {
            var routeInfo = await _unitOfWork.RouteInfoRepository.GetByIdAsync(request.RouteInfoId);

            if (routeInfo == null)
                throw new NotFoundException("RouteInfo", request.RouteInfoId);

            _unitOfWork.RouteInfoRepository.Delete(routeInfo);

            await _unitOfWork.SaveAsync();
        }
    }
}
