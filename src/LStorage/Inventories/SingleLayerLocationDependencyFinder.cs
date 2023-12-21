using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Inventories
{
    /// <summary>
    /// 表示货架/单层地堆式货架依赖库位查找器
    /// </summary>
    public class SingleLayerLocationDependencyFinder : ILocationDependencyFinder
    {
        public ShelfType[] ShelfTypes =>
            new ShelfType[] {
                ShelfType.SingleLayer,
                ShelfType.SingleLayerStack
            };

        public virtual async Task<IList<Location>> GetDependentLocationsAsync(Location location, CancellationToken cancellationToken)
        {
            return await Task.FromResult(new List<Location>());
        }
    }
}
