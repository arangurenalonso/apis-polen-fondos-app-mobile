namespace Presentation
{
    using Application;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.Extensions.DependencyInjection;
    using Presentation.Errors;
    using Presentation.Filtros;
    using Presentation.Middleware;
    public static class PresentationServiceRegistration
    {
        public static IServiceCollection AddPresentationServices(this IServiceCollection services)
        {
            services.AddApplicationServices();
            //Controlar el error que está siendo producido como resultado de la validación del modelo (model binding) 
            services.Configure<ApiBehaviorOptions>(options =>
            {
                options.InvalidModelStateResponseFactory = context =>
                {
                    var errors = context.ModelState
                        .Where(e => e.Value.Errors.Count > 0)
                        .ToDictionary(
                            kvp => kvp.Key,
                            kvp => kvp.Value.Errors.Select(e => e.ErrorMessage).ToArray()
                        );
                    return new BadRequestObjectResult(new CodeErrorException(400, "Error en el ingreso de datos", errors));
                };
            });

            services.AddScoped<CustomAuthorizationFilter>();
            return services;

        }
        public static IApplicationBuilder UsePresentationMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionMiddleware>();
            return app;
        }
    }
}