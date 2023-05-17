using Microsoft.EntityFrameworkCore;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Persistence.Repositories
{
    public class RouteRepository : BaseRepository<Route>, IRouteRepository
    {
        public RouteRepository(RouteServiceDbContext dbContext) : base(dbContext) { }

        public Task<Route> BookRideAsync(string routeId, string from, string to, int numberOfSeats)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<Route>?> GetAvailableRoutesByQueryAsync(string from, string to, DateTime departureTime, int numberOfSeats)
        {
            List<Route> routes = await _dbContext.Routes
                .Where(r => r.From == from && r.To == to && r.DepartureTime == departureTime && r.SeatsAvailable >= numberOfSeats).ToListAsync();

            if (routes.Count == 0)
            {
                routes = new List<Route>();

                var commonRoutes = await _dbContext.Routes
                    .Where(r => r.From == from && r.DepartureTime == departureTime && r.SeatsAvailable >= numberOfSeats)
                    .Join(
                        _dbContext.Routes.Where(r => r.To == to && r.SeatsAvailable >= numberOfSeats),
                        routeFrom => routeFrom.RouteId,
                        routeTo => routeTo.RouteId,
                        (routeFrom, routeTo) => new { RouteFrom = routeFrom, RouteTo = routeTo }
                    )
                    .ToListAsync();

                foreach (var route in commonRoutes)
                {
                    var neededRoute = new Route()
                    {
                        Id = route.RouteFrom.Id,
                        RouteId = route.RouteFrom.RouteId,
                        DepartureTime = route.RouteFrom.DepartureTime,
                        ArrivalTime = route.RouteTo.ArrivalTime,
                        From = route.RouteFrom.From,
                        To = route.RouteTo.To,
                        SeatsAvailable = route.RouteFrom.SeatsAvailable,
                        ExtraInfo = route.RouteFrom.ExtraInfo + route.RouteTo.ExtraInfo,
                    };

                    routes.Add(neededRoute);
                }
                
            }

            return routes;
        }
    }
}
