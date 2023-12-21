using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Inventories
{
    /// <summary>
    /// 表示库存分配服务接口
    /// </summary>
    public interface IInventoryAllocationService
    {
        /// <summary>
        /// 分配库存
        /// </summary>
        /// <param name="input">输入参数</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns>返回分配库存的结果集合</returns>
        Task<IList<AllocateInventoryOutput>> AllocateAsync(AllocateInventoryInput input, CancellationToken cancellationToken = default);
    }
}
