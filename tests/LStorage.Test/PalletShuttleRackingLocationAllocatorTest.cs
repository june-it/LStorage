using LStorage.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace LStorage.Test
{
    public class PalletShuttleRackingLocationAllocatorTest : TestBase
    {

        [Test]
        public async Task InternalAllocate()
        {
            var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocatorService>();
            await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
            {
                FromCode = "A1-T1-001-001-001-01",
                ToShelfCode = "S1"
            });
            Assert.Pass();
        }
    }
}
