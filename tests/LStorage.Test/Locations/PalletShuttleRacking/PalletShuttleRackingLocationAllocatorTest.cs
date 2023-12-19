using LStorage.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace LStorage.Test.Locations.PalletShuttleRacking
{
    /// <summary>
    /// 穿梭式货架测试
    /// </summary>
    public class PalletShuttleRackingLocationAllocatorTest
    {
        protected IServiceProvider ServiceProvider { get; private set; }
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection()
                .AddLogging()
                .AddLStorage(x =>
                {
                    x.AddQuerier<InMemoryPalletShuttleRackingAreaQuerier, Area>();
                    x.AddQuerier<InMemoryPalletShuttleRackingShelfQuerier, Shelf>();
                    x.AddQuerier<InMemoryPalletShuttleRackingLocationQuerier, Location>();
                    x.AddLocationAllocator<PalletShuttleLocationAllocator>();
                });
            ServiceProvider = services.BuildServiceProvider();
        }

        /// <summary>
        /// 立库内部移库库位分配
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task InternalAllocate()
        {
            var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocationService>();
            var result = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
            {
                FromCode = "A1-S1-001-001-001-01",
                ToShelfCode = "S1"
            });
            // 不为空
            Assert.IsNotNull(result.Location);
            // 不能分配同层同排同列的库位
            Assert.False(result.Location.RCLD.Layer == 1 && result.Location.RCLD.Row == 1 && result.Location.RCLD.Column == 1);
            Assert.Pass();
        }
        /// <summary>
        /// 外部库位进入立库库位分配
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ExternalAllocate()
        {
            var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocationService>();
            var result = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
            {
                FromCode = "A1-S2-001-001-001-01",
                ToShelfCode = "S1"
            });
            // 不为空
            Assert.IsNotNull(result.Location);
            // 不能分配同层同排同列的库位
            Assert.True(result.Location.RCLD.Layer == 1 && result.Location.RCLD.Row == 1 && result.Location.RCLD.Column == 1);
            Assert.Pass();
        }
    }

}
