using AutoMapper;
using MediatR;
using RouteService.Application.Exceptions;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;
using System.Security.Cryptography;
using System.Text;

namespace RouteService.Application.Features.Routes.Commands.CreateRide
{
    public class CreateRideCommandHandler : IRequestHandler<CreateRideCommand, CreateRideDto>
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRideCommandHandler(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public async Task<CreateRideDto> Handle(CreateRideCommand request, CancellationToken cancellationToken)
        {
            var validator = new CreateRideCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
                throw new ValidationException(validationResult);

            if (request.RouteId != string.Empty)
            {
                var routeIdCheck = await _unitOfWork.RouteRepository.RouteIdExistsAsync(request.RouteId);

                if (!routeIdCheck)
                    throw new NotFoundException("RouteId does not exist.");
            }
            else
            {
                using (SHA256 sha256Hash = SHA256.Create())
                {
                    var dataToHash = request.From + request.To + DateTime.Now;

                    request.RouteId = Convert.ToHexString(
                        sha256Hash.ComputeHash(Encoding.UTF8.GetBytes(dataToHash)));
                }
            }

            var ride = _mapper.Map<Ride>(request);

            var createdRide = await _unitOfWork.RouteRepository.CreateAsync(ride);

            await _unitOfWork.SaveAsync();

            return _mapper.Map<CreateRideDto>(createdRide);
        }
    }
}
