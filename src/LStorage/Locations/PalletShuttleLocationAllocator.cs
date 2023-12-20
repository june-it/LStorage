using LStorage.Locations.SortingAlgorithms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Locations
{
    /// <summary>
    /// 表示穿梭式货架库位分配服务
    /// </summary>
    public class PalletShuttleLocationAllocator : LocationAllocatorBase
    {
        private readonly IQuerier<Location> _locationQuerier;
        private readonly DistanceSortingAlgorithm _distanceSortingAlgorithm;
        private readonly CustomRCLDSortingAlgorithm _customRCLDSortingAlgorithm;
        public PalletShuttleLocationAllocator(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _locationQuerier = serviceProvider.GetRequiredService<IQuerier<Location>>();
            _distanceSortingAlgorithm = serviceProvider.GetRequiredService<DistanceSortingAlgorithm>();
            _customRCLDSortingAlgorithm = serviceProvider.GetRequiredService<CustomRCLDSortingAlgorithm>();
        }
        public override ShelfType[] ShelfTypes => new[] { ShelfType.PalletShuttleRacking };

        public override async Task<AllocateLocationResult> AllocateAsync(AllocateLocationContext context, CancellationToken cancellationToken = default)
        {
            if (!ShelfTypes.Contains(context.ToShelf.Type))
            {
                throw new InvalidOperationException($"货架库位分配服务{GetType().Name}不支持类型{context.ToShelf.Type}货架库位分配。");
            }
            // 获取货架上的空库位
            var locations = await _locationQuerier.GetListAsync(x => x.ShelfId == context.ToShelf.Id && x.PalletCount == 0);

            // 如果未指定排/列/层/深且为内部分配库位时，推荐距离最近的货位（指定库位所在列、所在层）
            if (context.ToShelf.Code == context.FromShelf.Code
                && !context.Input.Row.HasValue
                && !context.Input.Column.HasValue
                && !context.Input.Layer.HasValue
                && !context.Input.Depth.HasValue)
            {
                context.Input.Layer = context.FromLocation.RCLD.Layer;
                context.Input.Column = context.FromLocation.RCLD.Column;

                // 内部移库排除同层同排同列的库位
                locations = locations.Where(x => !(x.RCLD.Layer == context.FromLocation.RCLD.Layer && x.RCLD.Row == context.FromLocation.RCLD.Row && x.RCLD.Column == context.FromLocation.RCLD.Column)).ToList();
            }


            // 筛选依赖深库位
            locations = FilterByDependencyLocations(locations, context.ToShelf.IOType);


            if (!context.Input.Row.HasValue
                && !context.Input.Column.HasValue
                && !context.Input.Layer.HasValue
                && !context.Input.Depth.HasValue)
            {
                locations = _customRCLDSortingAlgorithm.Sort(locations, context.Input.SortingItems);
            }
            else
            {
                locations = _distanceSortingAlgorithm.Sort(locations, new LocationRCLD()
                {
                    Row = context.Input.Row ?? 0,
                    Column = context.Input.Column ?? 0,
                    Layer = context.Input.Layer ?? 0,
                    Depth = context.Input.Depth ?? 0,
                }, context.Input.SortingItems?.Select(x => x.Dimension).ToArray());
            }

            return new AllocateLocationResult(locations?.FirstOrDefault());
        }


    }

}
