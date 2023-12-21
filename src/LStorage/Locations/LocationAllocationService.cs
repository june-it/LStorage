using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Locations
{
    public class LocationAllocationService : ILocationAllocationService
    {
        private readonly IQuerier<Area> _areaQuerier;
        private readonly IQuerier<Shelf> _shelfQuerier;
        private readonly IQuerier<Location> _locationQuerier;
        private static Dictionary<ShelfType, ILocationAllocator> _cache = new Dictionary<ShelfType, ILocationAllocator>();
        public LocationAllocationService(IServiceProvider serviceProvider)
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


        public async virtual Task<AllocateLocationOutput> AllocateAsync(AllocateLocationInput input, CancellationToken cancellationToken = default)
        {

            var fromLocation = await _locationQuerier.GetAsync(x => x.Code == input.FromCode) ?? throw new ArgumentException($"库位{input.FromCode}信息不存在。");
            var fromArea = await _areaQuerier.GetAsync(x => x.Id == fromLocation.AreaId) ?? throw new ArgumentException($"区域{fromLocation.AreaId}信息不存在。");
            var fromShelf = await _shelfQuerier.GetAsync(x => x.Code == fromLocation.ShelfId) ?? throw new ArgumentException($"货架{fromLocation.ShelfId}信息不存在。");
            if (!(string.IsNullOrEmpty(input.ToAreaCode) || string.IsNullOrEmpty(input.ToShelfCode)))
                throw new ArgumentNullException("ToShelfCode", "区域编码或货架编码必须设置一项。");


            if (!string.IsNullOrEmpty(input.ToShelfCode))
            {
                var toShelf = await _shelfQuerier.GetAsync(x => x.Code == input.ToShelfCode);
                var toArea = await _areaQuerier.GetAsync(x => x.Id == toShelf.AreaId);
                return await AllocateByShelfAsync(toShelf, fromArea, fromShelf, fromLocation, toArea, input, cancellationToken);
            }
            else
            {
                var toArea = await _areaQuerier.GetAsync(x => x.Code == input.ToAreaCode) ?? throw new ArgumentException($"区域{fromLocation.Id}信息不存在。");
                // 当指定货架不存在时，则按货架编号进行排序 
                var sheleves = await _shelfQuerier.GetListAsync(x => x.AreaId == toArea.Id, x => x.Code);
                if (sheleves == null || !sheleves.Any())
                    throw new InvalidOperationException($"区域{input.ToAreaCode}无任何货架。");
                AllocateLocationOutput allocatedLocation = null;
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

        protected async virtual Task<AllocateLocationOutput> AllocateByShelfAsync(Shelf toShelf, Area fromArea, Shelf fromShelf, Location fromLocation, Area toArea, AllocateLocationInput input, CancellationToken cancellationToken = default)
        {
            if (toShelf == null)
                throw new InvalidOperationException("未查找任何可用货架。");

            if (!_cache.TryGetValue(toShelf.Type, out var allocator))
            {
                throw new InvalidOperationException($"{toShelf.Type}未注册分配服务。");
            }
            return await allocator.AllocateAsync(new AllocateLocationContext(fromArea, fromShelf, fromLocation, toArea, toShelf, input), cancellationToken);

        }
    }
}
