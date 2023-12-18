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

        public ILStorageBuilder AddQuery<TQuerier, TModel>()
            where TQuerier : class, IQuerier<TModel>
            where TModel : class, IModel
        {
            Services.AddTransient(typeof(IQuerier<TModel>), typeof(TQuerier));
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
