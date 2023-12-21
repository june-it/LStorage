using LStorage.Inventories;
using LStorage.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace LStorage
{
    /// <summary>
    /// 表示LStorage服务构建接口
    /// </summary>
    public interface ILStorageBuilder
    {
        /// <summary>
        /// 获取服务集合接口
        /// </summary>
        IServiceCollection Services { get; }
        /// <summary>
        /// 添加模型查询器
        /// </summary>
        /// <typeparam name="TQuerier">查询器的类型</typeparam>
        /// <typeparam name="TModel">数据模型的类型</typeparam>
        /// <returns></returns>
        ILStorageBuilder AddQuerier<TQuerier, TModel>()
            where TQuerier : class, IQuerier<TModel>
            where TModel : class, IModel;
        /// <summary>
        /// 添加库位分配服务
        /// </summary>
        /// <typeparam name="TLocationAllocator">库位分配服务的类型</typeparam>
        /// <returns></returns>
        ILStorageBuilder AddLocationAllocator<TLocationAllocator>()
            where TLocationAllocator : class, ILocationAllocator;

        /// <summary>
        /// 添加库位依赖项查找服务
        /// </summary>
        /// <typeparam name="TLocationDependencyFinder">库位依赖项查找服务的类型</typeparam>
        /// <returns></returns>
        ILStorageBuilder AddLocationDependencyFinder<TLocationDependencyFinder>()
            where TLocationDependencyFinder : class, ILocationDependencyFinder;
    }
}
