using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Locations
{
    public abstract class LocationAllocatorBase : ILocationAllocator
    {
        protected IDbProvider DbProvider { get; }
        protected ILogger Logger { get; }
        public LocationAllocatorBase(IServiceProvider serviceProvider)
        {
            DbProvider = serviceProvider.GetRequiredService<IDbProvider>();
            var loggerFactory = serviceProvider.GetService<ILoggerFactory>();
            Logger = loggerFactory?.CreateLogger(GetType().Name);
        }

        public abstract ShelfType[] ShelfTypes { get; }
        public abstract Task<IList<Location>> AllocateAsync(AllocateLocationContext context, CancellationToken cancellationToken = default);
    }
}
