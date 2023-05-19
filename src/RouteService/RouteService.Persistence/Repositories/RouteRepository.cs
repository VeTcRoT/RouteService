using Microsoft.EntityFrameworkCore;
using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Persistence.Repositories
{
    public class RouteRepository : BaseRepository<Ride>, IRouteRepository
    {
        public RouteRepository(RouteServiceDbContext dbContext) : base(dbContext) { }

        public async Task<Ride?> BookRideAsync(string routeId, string from, string to, int numberOfSeats)
        {
            var ride = await _dbContext.Rides
                .Where(r => r.From == from && r.SeatsAvailable >= numberOfSeats && r.RouteId == routeId).Include(r => r.RouteInfo)
                .Join(
                    _dbContext.Rides.Where(r => r.To == to && r.SeatsAvailable >= numberOfSeats && r.RouteId == routeId),
                    routeFrom => routeFrom.RouteId,
                    routeTo => routeTo.RouteId,
                    (routeFrom, routeTo) => new { RouteFrom = routeFrom, RouteTo = routeTo }
                )
                .FirstOrDefaultAsync();

            if (ride == null)
                return null;

            var ridesToChange = await _dbContext.Rides
                .Where(r => r.RouteId == routeId && r.DepartureTime >= ride.RouteFrom.DepartureTime && r.ArrivalTime <= ride.RouteTo.ArrivalTime)
                .ToListAsync();

            var checkRidesForAvailableSeats = ridesToChange.Where(r => r.SeatsAvailable >= numberOfSeats).ToList();

            if (ridesToChange.Count != checkRidesForAvailableSeats.Count)
                return null;

            foreach (Ride route in ridesToChange)
                route.SeatsAvailable -= numberOfSeats;

            await _dbContext.SaveChangesAsync();

            var routeToReturn = new Ride()
            {
                RouteId = routeId,
                DepartureTime = ride.RouteFrom.DepartureTime,
                ArrivalTime = ride.RouteTo.ArrivalTime,
                From = from,
                To = to,
                NumberOfSeats = ride.RouteFrom.NumberOfSeats,
                SeatsAvailable = ride.RouteFrom.SeatsAvailable,
                RouteInfo = ride.RouteFrom.RouteInfo
            };

            return routeToReturn;
        }

        public async Task<IEnumerable<Ride>?> GetAvailableRoutesByQueryAsync(string from, string to, DateTime departureTime, int numberOfSeats)
        {
            List<Ride> routes = new ();

            var commonRoutes = await _dbContext.Rides
                .Where(r => r.From == from && r.DepartureTime == departureTime && r.SeatsAvailable >= numberOfSeats).Include(r => r.RouteInfo)
                .Join(
                    _dbContext.Rides.Where(r => r.To == to && r.SeatsAvailable >= numberOfSeats),
                    routeFrom => routeFrom.RouteId,
                    routeTo => routeTo.RouteId,
                    (routeFrom, routeTo) => new { RouteFrom = routeFrom, RouteTo = routeTo }
                )
                .ToListAsync();

            var ridesToCheck = new List<Ride>();

            foreach (var ride in commonRoutes)
            {
                ridesToCheck = await _dbContext.Rides
                    .Where(r => r.RouteId == ride.RouteFrom.RouteId && r.DepartureTime >= ride.RouteFrom.DepartureTime && r.ArrivalTime <= ride.RouteTo.ArrivalTime)
                    .ToListAsync();
            }
                
            var checkRidesForAvailableSeats = ridesToCheck.Where(r => r.SeatsAvailable >= numberOfSeats).ToList();

            if (ridesToCheck.Count != checkRidesForAvailableSeats.Count)
                return null;

            foreach (var route in commonRoutes)
            {
                var neededRoute = new Ride()
                {
                    Id = route.RouteFrom.Id,
                    RouteId = route.RouteFrom.RouteId,
                    DepartureTime = route.RouteFrom.DepartureTime,
                    ArrivalTime = route.RouteTo.ArrivalTime,
                    From = route.RouteFrom.From,
                    To = route.RouteTo.To,
                    NumberOfSeats = route.RouteFrom.NumberOfSeats,
                    SeatsAvailable = route.RouteFrom.SeatsAvailable,
                    RouteInfo = route.RouteFrom.RouteInfo
                };

                routes.Add(neededRoute);
            }

            return routes;
        }

        public async Task<bool> RouteIdExistsAsync(string routeId)
        {
            var route = await _dbContext.Rides.FirstOrDefaultAsync(r => r.RouteId == routeId);

            return route == null ? false : true;
        }

        public async Task<Ride> GetRideByIdAsync(int rideId)
        {
            var ride = await _dbContext.Rides.Include(r => r.RouteInfo).FirstOrDefaultAsync(r => r.Id == rideId);

            return ride;
        }

        public async Task<IEnumerable<Ride>> ListAllRidesAsync()
        {
            var rides = await _dbContext.Rides.Include(r => r.RouteInfo).OrderByDescending(r => r.RouteId).ToListAsync();

            return rides;
        }
    }
}
