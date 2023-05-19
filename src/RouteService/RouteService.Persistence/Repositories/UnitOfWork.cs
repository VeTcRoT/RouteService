using RouteService.Domain.Entities;
using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRouteRepository RouteRepository { get; }

        public IBaseRepository<Route> RouteInfoRepository { get; }

        private readonly RouteServiceDbContext _dbContext;

        public UnitOfWork(IRouteRepository routeRepository, IBaseRepository<Route> routeInfoRepository, RouteServiceDbContext dbContext)
        {
            RouteRepository = routeRepository;
            RouteInfoRepository = routeInfoRepository;
            _dbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
