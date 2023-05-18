using RouteService.Domain.Entities;

namespace RouteService.Domain.Interfaces.Repositories
{
    public interface IRouteRepository : IBaseRepository<Route>
    {
        Task<IEnumerable<Route>?> GetAvailableRoutesByQueryAsync(string from, string to, DateTime departureTime, int numberOfSeats);
        Task<Route?> BookRideAsync(string routeId, string from, string to, int numberOfSeats);
        Task<bool> RouteIdExistsAsync(string routeId);
    }
}
