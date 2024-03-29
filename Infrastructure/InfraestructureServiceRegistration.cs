﻿namespace Infrastructure
{
    using Application.Contracts.ApiExterna;
    using Application.Contracts.ApiExterna.Abstractions;
    using Application.Contracts.Repositories;
    using Application.Contracts.Repositories.Base;
    using Infrastructure.Options.ApiExternas;
    using Infrastructure.Persistence;
    using Infrastructure.Repositories.ApisExternas;
    using Infrastructure.Repositories.ApisExternas.Abstractions;
    using Infrastructure.Repositories.Persistence;
    using Infrastructure.Repositories.Persistence.Common;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    public static class InfraestructureServiceRegistration
    {
        public static IServiceCollection AddInfraestructureServices(this IServiceCollection services, IConfiguration configuration)
        {

            ConfigureApiExternaOptions(services);

            var serverVersion = new MySqlServerVersion(new Version(8, 0, 28));
            services.AddDbContext<ApplicationDbContext>(options =>
            {
                options.UseMySql(configuration.GetConnectionString("DefaultConnection"), serverVersion);
            });
            services.AddScoped<IUnitOfWork, UnitOfWork>();
            services.AddScoped(typeof(IRepositoryBase<>), typeof(RepositoryBase<>));

            services.AddScoped<IProspectoRepository, ProspectoRepository>();
            services.AddScoped<IVendedorRepository, VendedorRepository>();
            services.AddScoped<IMaestroProspectoRepository, MaestroProspectoRepository>();
            services.AddScoped<IReportesRepository, ReportesRepository>();

            services.AddScoped<IClienteProvider, ClienteProvider>();
            services.AddScoped<IBitrix24ApiService, Bitrix24ApiService>();
            services.AddScoped<IStateRepository, StateRepository>();
            services.AddScoped<IMediosRepository, MediosRepository>();
            


            return services;
        }

        private static void ConfigureApiExternaOptions(IServiceCollection services) =>
            services.ConfigureOptions<ApiExternaOptionsSetup>();
    }
}