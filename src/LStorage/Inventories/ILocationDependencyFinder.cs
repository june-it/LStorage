using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Inventories
{
    /// <summary>
    /// 表示库位依赖项查找器接口
    /// </summary>
    public interface ILocationDependencyFinder
    {
        /// <summary>
        /// 获取适用的货架类型集合
        /// </summary>
        ShelfType[] ShelfTypes { get; }
        /// <summary>
        /// 获取依赖库位列表
        /// </summary>
        /// <param name="location">目标库位</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<IList<Location>> GetDependentLocationsAsync(Location location, CancellationToken cancellationToken);
    }
}
