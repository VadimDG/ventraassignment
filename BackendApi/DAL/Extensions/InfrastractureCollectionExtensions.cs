using Intfastructure.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace Intfastructure.Extensions
{
    public static class InfrastractureCollectionExtensions
    {
        public static IServiceCollection AddBackendPersistence(this IServiceCollection services)
        {
            services.AddDbContext<BackendDbContext>(options =>
                options.UseInMemoryDatabase("BackendInMemory"));

            services.AddScoped<ISkuRepository, SkuRepository>();
            services.AddScoped<ISubskuRepository, SubskuRepository>();
            
            var sp = services.BuildServiceProvider();
            using var scope = sp.CreateScope();
            var ctx = scope.ServiceProvider.GetRequiredService<BackendDbContext>();
            ctx.Database.EnsureCreated();

            return services;
        }
    }
}