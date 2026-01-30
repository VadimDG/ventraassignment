using Application.Services.Calculate;
using Application.Services.ItemsList;
using Application.Services.Items;
using Microsoft.Extensions.DependencyInjection;

namespace Application.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddApplicationServices(this IServiceCollection services)
        {
            services.AddScoped<ICalculateService, CalculateService>();
            services.AddScoped<IItemsListService, ItemsListService>();
            services.AddScoped<IItemsService, ItemsService>();
            return services;
        }
    }
}