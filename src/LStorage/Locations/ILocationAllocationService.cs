using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Locations
{
    /// <summary>
    /// 表示库位分配服务接口
    /// </summary>
    public interface ILocationAllocationService
    {
        /// <summary>
        /// 分配库位
        /// </summary>
        /// <param name="input">分配库位参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        Task<AllocateLocationOutput> AllocateAsync(AllocateLocationInput input, CancellationToken cancellationToken = default);
    }

}
