﻿using LStorage;
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
            services.AddSingleton<ILocationAllocationService, LocationAllocationService>();
            services.AddTransient<DistanceSortingAlgorithm>();
            services.AddTransient<CustomRCLDSortingAlgorithm>();
            return services;
        }
    }
}
