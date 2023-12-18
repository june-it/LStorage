using LStorage.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace LStorage
{
    public interface ILStorageBuilder
    {
        IServiceCollection Services { get; }

        ILStorageBuilder AddQuery<TQuerier, TModel>()
            where TQuerier : class, IQuerier<TModel>
            where TModel : class, IModel;
        ILStorageBuilder AddLocationAllocator<TLocationAllocator>()
            where TLocationAllocator : class, ILocationAllocator;
    }
}
