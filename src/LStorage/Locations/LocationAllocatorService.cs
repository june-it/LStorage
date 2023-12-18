using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Locations
{
    public class LocationAllocatorService : ILocationAllocatorService
    {
        private readonly IQuerier<Area> _areaQuerier;
        private readonly IQuerier<Shelf> _shelfQuerier;
        private readonly IQuerier<Location> _locationQuerier;
        private static Dictionary<ShelfType, ILocationAllocator> _cache = new Dictionary<ShelfType, ILocationAllocator>();
        public LocationAllocatorService(IServiceProvider serviceProvider)
        {
            var locationAllocators = serviceProvider.GetServices<ILocationAllocator>();
            if (!_cache.Any())
            {
                foreach (var locationAllocator in locationAllocators)
                {
                    foreach (var shelfType in locationAllocator.ShelfTypes)
                    {
                        if (!_cache.TryAdd(shelfType, locationAllocator))
                            throw new InvalidOperationException($"{shelfType}已重复注册分配服务。");
                    }

                }
            }
            _areaQuerier = serviceProvider.GetService<IQuerier<Area>>();
            _shelfQuerier = serviceProvider.GetService<IQuerier<Shelf>>();
            _locationQuerier = serviceProvider.GetService<IQuerier<Location>>();
        }
        public async Task<Location> AllocateAsync(AllocateLocationInput input, CancellationToken cancellationToken = default)
        {

            var fromLocation = await _locationQuerier.GetAsync(input.FromCode) ?? throw new ArgumentException($"库位{input.FromCode}信息不存在。");
            var fromArea = await _areaQuerier.GetAsync(fromLocation.AreaCode) ?? throw new ArgumentException($"区域{fromLocation.AreaCode}信息不存在。");
            var fromShelf = await _shelfQuerier.GetAsync(fromLocation.ShelfCode) ?? throw new ArgumentException($"货架{fromLocation.AreaCode}信息不存在。");
            if (!(string.IsNullOrEmpty(input.ToAreaCode) || string.IsNullOrEmpty(input.ToShelfCode)))
                throw new ArgumentNullException("ToShelfCode", "区域编码或货架编码必须设置一项。");


            // 优先货架编码
            if (!string.IsNullOrEmpty(input.ToShelfCode))
            {
                var toShelf = await _shelfQuerier.GetAsync(input.ToShelfCode);
                var toArea = await _areaQuerier.GetAsync(toShelf.AreaCode);
                return await AllocateByShelfAsync(toShelf, fromArea, fromShelf, fromLocation, toArea, input, cancellationToken);
            }
            else
            {
                var toArea = await _areaQuerier.GetAsync(input.ToAreaCode) ?? throw new ArgumentException($"区域{fromLocation.AreaCode}信息不存在。");
                // 当指定货架不存在时，则按货架编号进行排序 
                var sheleves = await _shelfQuerier.GetListAsync(x => x.AreaCode == input.ToAreaCode, x => x.Code);
                if (sheleves == null || !sheleves.Any())
                    throw new InvalidOperationException($"区域{input.ToAreaCode}无任何货架。");
                Location allocatedLocation = null;
                foreach (var toShelf in sheleves)
                {
                    allocatedLocation = await AllocateByShelfAsync(toShelf, fromArea, fromShelf, fromLocation, toArea, input, cancellationToken);
                    if (allocatedLocation != null)
                    {
                        break;
                    }
                }
                return allocatedLocation;
            }
        }

        private async Task<Location> AllocateByShelfAsync(Shelf toShelf, Area fromArea, Shelf fromShelf, Location fromLocation, Area toArea, AllocateLocationInput input, CancellationToken cancellationToken = default)
        {
            if (toShelf == null)
                throw new InvalidOperationException("未查找任何可用货架。");

            if (!_cache.TryGetValue(toShelf.ShelfType, out var allocator))
            {
                throw new InvalidOperationException($"{toShelf.ShelfType}未注册分配服务。");
            }
            return await allocator.AllocateAsync(new AllocateLocationContext(fromArea, fromShelf, fromLocation, toArea, toShelf, input), cancellationToken);

        }
    }
}
