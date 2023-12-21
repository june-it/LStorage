using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Locations
{
    /// <summary>
    /// 表示库位分配接口
    /// </summary>
    public interface ILocationAllocator
    {
        /// <summary>
        /// 获取可分配的货架
        /// </summary>
        ShelfType[] ShelfTypes { get; }
        /// <summary>
        /// 分配空库位
        /// </summary>
        /// <param name="context"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<AllocateLocationOutput> AllocateAsync(AllocateLocationContext context, CancellationToken cancellationToken = default);
    }

}
