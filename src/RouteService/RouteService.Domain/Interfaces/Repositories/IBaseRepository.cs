namespace RouteService.Domain.Interfaces.Repositories
{
    public interface IBaseRepository<T> where T : class
    {
        Task<T?> GetByIdAsync(int id);
        Task<IEnumerable<T>> ListAllAsync();
        Task<T> CreateAsync(T entity);
        void Update(T entity);
        void Delete(T entity);
    }
}
