using Microsoft.Extensions.DependencyInjection;
using RouteService.Domain.Interfaces.Repositories;
using RouteService.Persistence.Repositories;

namespace RouteService.Persistence
{
    public static class PersistenceServiceRegistration
    {
        public static IServiceCollection AddPersistenceServices(this IServiceCollection services) 
        {
            services.AddScoped(typeof(IBaseRepository<>), typeof(BaseRepository<>));
            services.AddScoped<IRouteRepository, RouteRepository>();
            services.AddScoped<IUnitOfWork, UnitOfWork>();

            return services;
        }
    }
}
