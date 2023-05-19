using RouteService.Domain.Entities;

namespace RouteService.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IRouteRepository RouteRepository { get; }
        IBaseRepository<Route> RouteInfoRepository { get; }
        Task SaveAsync();
    }
}
