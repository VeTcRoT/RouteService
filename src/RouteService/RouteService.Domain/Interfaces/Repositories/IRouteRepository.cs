using RouteService.Domain.Entities;

namespace RouteService.Domain.Interfaces.Repositories
{
    public interface IRouteRepository : IBaseRepository<Ride>
    {
        Task<IEnumerable<Ride>?> GetAvailableRoutesByQueryAsync(string from, string to, DateTime departureTime, int numberOfSeats);
        Task<Ride?> BookRideAsync(string routeId, string from, string to, int numberOfSeats);
        Task<bool> RouteIdExistsAsync(string routeId);
        Task<Ride> GetRideByIdAsync(int rideId);
        Task<IEnumerable<Ride>> ListAllRidesAsync();
    }
}
