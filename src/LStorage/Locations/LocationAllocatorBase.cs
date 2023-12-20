using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Locations
{
    public abstract class LocationAllocatorBase : ILocationAllocator
    {
        public LocationAllocatorBase(IServiceProvider serviceProvider)
        {
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            Logger = loggerFactory?.CreateLogger(GetType().Name);
        }

        public abstract ShelfType[] ShelfTypes { get; }
        protected ILogger Logger { get; }
        public abstract Task<AllocateLocationResult> AllocateAsync(AllocateLocationContext context, CancellationToken cancellationToken = default);

        /// <summary>
        /// 筛选深依赖的库位列表
        /// </summary>
        /// <param name="locations">库位列表</param> 
        /// <param name="shelfIOType">货架存取类型</param>
        /// <returns></returns>
        public virtual List<Location> FilterByDependencyLocations(IList<Location> locations, ShelfIOType shelfIOType)
        {
            var availableLocations = new List<Location>();
            foreach (var location in locations)
            {
                switch (shelfIOType)
                {
                    case ShelfIOType.FILO:
                        // 深位大于货位的深位不能出现满库位
                        if (!locations.Any(x =>
                            x.RCLD.Depth < location.RCLD.Depth
                            && x.RCLD.Layer == location.RCLD.Layer
                            && x.RCLD.Column == location.RCLD.Column
                            && x.RCLD.Row == location.RCLD.Row
                            && x.PalletCount == 1) && location.PalletCount == 0)
                        {
                            availableLocations.Add(location);
                        }
                        break;
                    case ShelfIOType.FIFO:
                    case ShelfIOType.BSIO:
                        // 货位深位两侧不能同时出现满库位
                        if (!(locations.Any(x =>
                            x.RCLD.Depth < location.RCLD.Depth
                            && x.RCLD.Layer == location.RCLD.Layer
                            && x.RCLD.Column == location.RCLD.Column
                            && x.RCLD.Row == location.RCLD.Row
                            && x.PalletCount == 1)
                            &&
                        locations.Any(x =>
                            x.RCLD.Depth > location.RCLD.Depth
                            && x.RCLD.Layer == location.RCLD.Layer
                            && x.RCLD.Column == location.RCLD.Column
                            && x.RCLD.Row == location.RCLD.Row
                            && x.PalletCount == 1)) && location.PalletCount == 0)
                        {
                            availableLocations.Add(location);
                        }
                        break;
                }
            }
            return availableLocations;
        }
    }
}
