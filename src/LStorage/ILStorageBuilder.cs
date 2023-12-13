using LStorage.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace LStorage
{
    public interface ILStorageBuilder
    {
        IServiceCollection Services { get; }

        ILStorageBuilder AddDbProvider<TDbProvider>()
            where TDbProvider : class, IDbProvider;
        ILStorageBuilder AddLocationAllocator<TLocationAllocator>()
            where TLocationAllocator : class, ILocationAllocator;
    }
}
