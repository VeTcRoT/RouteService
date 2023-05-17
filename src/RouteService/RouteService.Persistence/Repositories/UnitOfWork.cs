using RouteService.Domain.Interfaces.Repositories;

namespace RouteService.Persistence.Repositories
{
    public class UnitOfWork : IUnitOfWork
    {
        public IRouteRepository RouteRepository { get; }
        private readonly RouteServiceDbContext _dbContext;

        public UnitOfWork(IRouteRepository routeRepository, RouteServiceDbContext dbContext)
        {
            RouteRepository = routeRepository;
            _dbContext = dbContext;
        }

        public async Task SaveAsync()
        {
            await _dbContext.SaveChangesAsync();
        }
    }
}
