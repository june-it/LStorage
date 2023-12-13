using LStorage.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace LStorage
{
    public class DefaultLStorageBuilder : ILStorageBuilder
    {
        public IServiceCollection Services { get; }
        public DefaultLStorageBuilder(IServiceCollection services)
        {
            Services = services;
        }

        public ILStorageBuilder AddDbProvider<TDbProvider>()
            where TDbProvider : class, IDbProvider
        {
            Services.AddTransient<IDbProvider, TDbProvider>();
            return this;
        }
        public ILStorageBuilder AddLocationAllocator<TLocationAllocator>()
            where TLocationAllocator : class, ILocationAllocator
        {
            Services.AddTransient<ILocationAllocator, TLocationAllocator>();
            return this;
        }
    }
}
