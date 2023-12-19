using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Inventories
{
    /// <summary>
    /// 表示库存分配服务
    /// </summary>
    public interface IInventoryAllocationService
    {
        /// <summary>
        /// 分配库存
        /// </summary>
        /// <param name="input"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        Task<IList<AllocateInventoryResult>> AllocateAsync(AllocateInventoryInput input, CancellationToken cancellationToken = default);
    }
}
