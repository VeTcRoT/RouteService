namespace RouteService.Domain.Interfaces.Repositories
{
    public interface IUnitOfWork
    {
        IRouteRepository RouteRepository { get; }
        Task SaveAsync();
    }
}
