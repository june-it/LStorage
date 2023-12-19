using LStorage;
using LStorage.Inventories;
using LStorage.Locations;
using LStorage.Locations.SortingAlgorithms;
using System;

namespace Microsoft.Extensions.DependencyInjection
{
    public static class IServiceCollectionExtensions
    {
        public static IServiceCollection AddLStorage(this IServiceCollection services, Action<ILStorageBuilder> configure)
        {
            var defaultLStorageBuilder = new DefaultLStorageBuilder(services);
            configure?.Invoke(defaultLStorageBuilder);
            services.AddScoped<ILocationAllocationService, LocationAllocationService>();
            services.AddTransient<DistanceSortingAlgorithm>();
            services.AddTransient<CustomRCLDSortingAlgorithm>();
            services.AddScoped<IInventoryAllocationService, InventoryAllocationService>();
            return services;
        }
    }
}
