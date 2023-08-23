namespace Infrastructure
{
    using Application.Contracts.Repositories;
    using Application.Contracts.Repositories.Base;
    using Infrastructure.Persistence;
    using Infrastructure.Repositories;
    using Infrastructure.Repositories.Common;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    public static class InfraestructureServiceRegistration
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"), serverVersion);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped<IProspectoRepository, ProspectoRepository>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));
            

            return services;
        }

    }
}