using LStorage.Locations.SortingAlgorithms;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Locations
{
    /// <summary>
    /// 表示单层货架/单层地推货架库位分配服务
    /// </summary>
    public class SingleLayerLocationAllocator : LocationAllocatorBase
    {
        private readonly IQuerier<Location> _locationQuerier;
        private readonly DistanceSortingAlgorithm _distanceSortingAlgorithm;
        private readonly LocationSortingAlgorithm _locationSortingAlgorithm;
        public SingleLayerLocationAllocator(IServiceProvider serviceProvider) : base(serviceProvider)
        {
            _locationQuerier = serviceProvider.GetRequiredService<IQuerier<Location>>();
            _distanceSortingAlgorithm = serviceProvider.GetRequiredService<DistanceSortingAlgorithm>();
            _locationSortingAlgorithm = serviceProvider.GetRequiredService<LocationSortingAlgorithm>();
        }

        public override ShelfType[] ShelfTypes =>
            new ShelfType[] {
                ShelfType.SingleLayer,
                ShelfType.SingleLayerStack
            };

        public override async Task<AllocateLocationOutput> AllocateAsync(AllocateLocationContext context, CancellationToken cancellationToken = default)
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
            }

            if (!context.Input.Row.HasValue
                           && !context.Input.Column.HasValue
                           && !context.Input.Layer.HasValue
                           && !context.Input.Depth.HasValue)
            {
                locations = _locationSortingAlgorithm.Sort(locations, context.Input.SortingItems);
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

            return new AllocateLocationOutput(locations?.FirstOrDefault());
        }
    }
}
