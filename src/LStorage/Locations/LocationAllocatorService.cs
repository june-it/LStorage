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
        private readonly IDbProvider _dbProvider;
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
            _dbProvider = serviceProvider.GetService<IDbProvider>();
        }
        public async Task<IList<Location>> AllocateAsync(AllocateLocationInput input, CancellationToken cancellationToken = default)
        {

            var fromLocation = await _dbProvider.GetLocationAsync(input.FromCode) ?? throw new ArgumentException($"库位{input.FromCode}信息不存在。");
            var fromArea = await _dbProvider.GetAreaAsync(fromLocation.AreaCode) ?? throw new ArgumentException($"区域{fromLocation.AreaCode}信息不存在。");
            var fromShelf = await _dbProvider.GetShelfAsync(fromLocation.ShelfCode) ?? throw new ArgumentException($"货架{fromLocation.AreaCode}信息不存在。");
            if (!(string.IsNullOrEmpty(input.ToAreaCode) || string.IsNullOrEmpty(input.ToShelfCode)))
                throw new ArgumentNullException("ToShelfCode", "区域编码或货架编码必须设置一项。");
            Area toArea = null;
            Shelf toShelf = null;
            // 优先货架编码
            if (!string.IsNullOrEmpty(input.ToShelfCode))
            {
                toShelf = await _dbProvider.GetShelfAsync(input.ToShelfCode);
                toArea = await _dbProvider.GetAreaAsync(toShelf.AreaCode);
            }
            else
            {
                toArea = await _dbProvider.GetAreaAsync(toShelf.AreaCode);
                toShelf = _dbProvider.GetShelves()
                    .Where(x => x.AreaCode == input.ToAreaCode)
                    .OrderBy(x => x.Code)
                    .FirstOrDefault();
            }

            if (!_cache.TryGetValue(toShelf.ShelfType, out var allocator))
            {
                throw new InvalidOperationException($"{toShelf.ShelfType}未注册分配服务。");
            }
            return await allocator.AllocateAsync(new AllocateLocationContext(fromArea, fromShelf, fromLocation, toArea, toShelf, input), cancellationToken);

        }
    }
}
