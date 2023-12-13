using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace LStorage.Locations
{
    public interface ILocationAllocatorService
    {
        Task<IList<Location>> AllocateAsync(AllocateLocationInput input, CancellationToken cancellationToken = default);
    }

}
