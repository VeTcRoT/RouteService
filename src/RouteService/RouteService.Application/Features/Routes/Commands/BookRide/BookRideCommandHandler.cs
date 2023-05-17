using AutoMapper;
using MediatR;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Application.Features.Routes.Commands.BookRide
{
    public class BookRideCommandHandler : IRequestHandler<BookRideCommand, BookRideDto>
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public BookRideCommandHandler(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<BookRideDto> Handle(BookRideCommand request, CancellationToken cancellationToken)
        {
            BookRideDto bookedRide = new ();

            var validator = new BookRideCommandValidator();
            var validationResult = await validator.ValidateAsync(request);

            if (validationResult.Errors.Count > 0)
            {
                bookedRide.IsSuccess = false;
                bookedRide.Errors = new List<string>();
                bookedRide.RouteId = request.RouteId;
                bookedRide.From = request.From;
                bookedRide.To = request.To;

                foreach (var error in validationResult.Errors)
                    bookedRide.Errors.Add(error.ErrorMessage);

                return bookedRide;
            }

            var routeExists = await _unitOfWork.RouteRepository.RouteIdExistsAsync(request.RouteId);

            if (!routeExists) 
            {
                bookedRide.IsSuccess = false;
                bookedRide.Errors = new List<string>
                {
                    $"RouteId({request.RouteId}) does not exist."
                };
                bookedRide.RouteId = request.RouteId;
                bookedRide.From = request.From;
                bookedRide.To = request.To;

                return bookedRide;
            }

            var ride = await _unitOfWork.RouteRepository.BookRideAsync(request.RouteId, request.From, request.To, request.NumberOfSeats);

            if (ride == null)
            {
                bookedRide.IsSuccess = false;
                bookedRide.Errors = new List<string>
                {
                    $"Route does not exist."
                };
                bookedRide.RouteId = request.RouteId;
                bookedRide.From = request.From;
                bookedRide.To = request.To;

                return bookedRide;
            }

            bookedRide.IsSuccess = true;
            bookedRide = _mapper.Map<BookRideDto>(ride);
            bookedRide.Seats = new List<SeatDto>();

            for (int i = request.NumberOfSeats + 1; i <= ride.NumberOfSeats - ride.SeatsAvailable + request.NumberOfSeats; i++)
            {
                bookedRide.Seats.Add(new SeatDto() { Number = i });
            }

            return bookedRide;
        }
    }
}
