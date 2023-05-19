using MediatR;
using RouteService.Application.Exceptions;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.Routes.Commands.DeleteRide
{
    public class DeleteRideCommandHandler : IRequestHandler<DeleteRideCommand>
    {
        private readonly IUnitOfWork _unitOfWork;

        public DeleteRideCommandHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task Handle(DeleteRideCommand request, CancellationToken cancellationToken)
        {
            var ride = await _unitOfWork.RouteRepository.GetByIdAsync(request.Id);

            if (ride == null) 
                throw new NotFoundException(nameof(Ride), request.Id);

            _unitOfWork.RouteRepository.Delete(ride);

            await _unitOfWork.SaveAsync();
        }
    }
}
