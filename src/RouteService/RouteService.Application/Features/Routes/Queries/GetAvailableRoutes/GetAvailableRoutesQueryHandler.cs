using MediatR;
using RouteService.Application.Exceptions;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.Routes.Queries.GetAvailableRoutes
{
    public class GetAvailableRoutesQueryHandler : IRequestHandler<GetAvailableRoutesQuery, IEnumerable<Route>?>
    {
        private readonly IUnitOfWork _unitOfWork;

        public GetAvailableRoutesQueryHandler(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<IEnumerable<Route>?> Handle(GetAvailableRoutesQuery request, CancellationToken cancellationToken)
        {
            var validator = new GetAvailableRoutesQueryValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            return await _unitOfWork.RouteRepository.GetAvailableRoutesByQueryAsync(request.From, request.To, request.DepartureTime, request.NumberOfSeats);
        }
    }
}
