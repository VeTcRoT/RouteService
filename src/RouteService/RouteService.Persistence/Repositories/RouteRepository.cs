using Microsoft.EntityFrameworkCore;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Persistence.Repositories
{
    public class RouteRepository : BaseRepository<Route>, IRouteRepository
    {
        public RouteRepository(RouteServiceDbContext dbContext) : base(dbContext) { }

        public async Task<Route?> BookRideAsync(string routeId, string from, string to, int numberOfSeats)
        {
            var ride = await _dbContext.Routes
                .Where(r => r.From == from && r.SeatsAvailable >= numberOfSeats && r.RouteId == routeId)
                .Join(
                    _dbContext.Routes.Where(r => r.To == to && r.SeatsAvailable >= numberOfSeats && r.RouteId == routeId),
                    routeFrom => routeFrom.RouteId,
                    routeTo => routeTo.RouteId,
                    (routeFrom, routeTo) => new { RouteFrom = routeFrom, RouteTo = routeTo }
                )
                .FirstOrDefaultAsync();

            if (ride == null)
                return null;

            var ridesToChange = await _dbContext.Routes
                .Where(r => r.RouteId == routeId && r.DepartureTime >= ride.RouteFrom.DepartureTime && r.ArrivalTime <= ride.RouteTo.ArrivalTime)
                .ToListAsync();

            var checkRidesForAvailableSeats = ridesToChange.Where(r => r.SeatsAvailable >= numberOfSeats).ToList();

            if (ridesToChange.Count != checkRidesForAvailableSeats.Count)
                return null;

            foreach (Route route in ridesToChange)
                route.SeatsAvailable -= numberOfSeats;

            await _dbContext.SaveChangesAsync();

            var routeToReturn = new Route()
            {
                RouteId = routeId,
                DepartureTime = ride.RouteFrom.DepartureTime,
                ArrivalTime = ride.RouteTo.ArrivalTime,
                From = from,
                To = to,
                NumberOfSeats = ride.RouteFrom.NumberOfSeats,
                SeatsAvailable = ride.RouteFrom.SeatsAvailable + numberOfSeats,
                ExtraInfo = ride.RouteFrom.ExtraInfo + "\n" + ride.RouteTo.ExtraInfo
            };

            return routeToReturn;
        }

        public async Task<IEnumerable<Route>?> GetAvailableRoutesByQueryAsync(string from, string to, DateTime departureTime, int numberOfSeats)
        {
            List<Route> routes = new ();

            var commonRoutes = await _dbContext.Routes
                .Where(r => r.From == from && r.DepartureTime == departureTime && r.SeatsAvailable >= numberOfSeats)
                .Join(
                    _dbContext.Routes.Where(r => r.To == to && r.SeatsAvailable >= numberOfSeats),
                    routeFrom => routeFrom.RouteId,
                    routeTo => routeTo.RouteId,
                    (routeFrom, routeTo) => new { RouteFrom = routeFrom, RouteTo = routeTo }
                )
                .ToListAsync();

            var ridesToCheck = new List<Route>();

            foreach (var ride in commonRoutes)
            {
                ridesToCheck = await _dbContext.Routes
                    .Where(r => r.RouteId == ride.RouteFrom.RouteId && r.DepartureTime >= ride.RouteFrom.DepartureTime && r.ArrivalTime <= ride.RouteTo.ArrivalTime)
                    .ToListAsync();
            }
                
            var checkRidesForAvailableSeats = ridesToCheck.Where(r => r.SeatsAvailable >= numberOfSeats).ToList();

            if (ridesToCheck.Count != checkRidesForAvailableSeats.Count)
                return null;

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
                    NumberOfSeats = route.RouteFrom.NumberOfSeats,
                    SeatsAvailable = route.RouteFrom.SeatsAvailable,
                    ExtraInfo = route.RouteFrom.ExtraInfo + route.RouteTo.ExtraInfo,
                };

                routes.Add(neededRoute);
            }

            return routes;
        }

        public async Task<bool> RouteIdExistsAsync(string routeId)
        {
            var route = await _dbContext.Routes.FirstOrDefaultAsync(r => r.RouteId == routeId);

            return route == null ? false : true;
        }
    }
}
