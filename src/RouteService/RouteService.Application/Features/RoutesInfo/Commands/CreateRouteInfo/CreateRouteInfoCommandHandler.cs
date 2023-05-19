using AutoMapper;
using MediatR;
using RouteService.Application.Exceptions;
using RouteService.Domain.Dtos;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.RoutesInfo.Commands.CreateRouteInfo
{
    public class CreateRouteInfoCommandHandler : IRequestHandler<CreateRouteInfoCommand, RouteInfoDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRouteInfoCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<RouteInfoDto> Handle(CreateRouteInfoCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateRouteInfoCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            var routeInfo = await _unitOfWork.RouteInfoRepository.CreateAsync(_mapper.Map<Route>(request));

            await _unitOfWork.SaveAsync();

            return _mapper.Map<RouteInfoDto>(routeInfo);
        }
    }
}
