using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Inventories
{
    /// <summary>
    /// 表示穿梭式货架依赖库位查找器
    /// </summary>
    public class PalletShuttleLocationDependencyFinder : ILocationDependencyFinder
    {
        public ShelfType[] ShelfTypes => new[] { ShelfType.PalletShuttleRacking };
        private readonly IQuerier<Location> _locationsQuerier;
        private readonly IQuerier<Shelf> _shelfQuerier;
        public PalletShuttleLocationDependencyFinder(IServiceProvider serviceProvider)
        {
            _locationsQuerier = serviceProvider.GetRequiredService<IQuerier<Location>>();
            _shelfQuerier = serviceProvider.GetRequiredService<IQuerier<Shelf>>();
        }
        /// <summary>
        /// 获取依赖库位列表
        /// </summary>
        /// <param name="location">目标库位</param>
        /// <param name="cancellationToken">取消令牌</param>
        /// <returns></returns>
        public virtual async Task<IList<Location>> GetDependentLocationsAsync(Location location, CancellationToken cancellationToken)
        {
            var shelf = await _shelfQuerier.GetAsync(x => x.Id == location.ShelfId, cancellationToken) ?? throw new ArgumentException($"货架{location.ShelfId}信息不存在。");

            switch (shelf.IOType)
            {
                case ShelfIOType.FILO:
                    // 深位大于货位的深位不能出现满库位
                    return await _locationsQuerier.GetListAsync(x => x.RCLD.Depth < location.RCLD.Depth
                        && x.RCLD.Layer == location.RCLD.Layer
                        && x.RCLD.Column == location.RCLD.Column
                        && x.RCLD.Row == location.RCLD.Row
                        && x.PalletCount >= 1);
                case ShelfIOType.FIFO:
                default:
                    // 深位小于货位的深位不能出现满库位
                    return await _locationsQuerier.GetListAsync(x => x.RCLD.Depth > location.RCLD.Depth
                        && x.RCLD.Layer == location.RCLD.Layer
                        && x.RCLD.Column == location.RCLD.Column
                        && x.RCLD.Row == location.RCLD.Row
                        && x.PalletCount >= 1);
            }
        }
    }
}
