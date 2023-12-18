using LStorage.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace LStorage.Test.PalletShuttleRacking
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
                    x.AddQuery<InMemoryPalletShuttleRackingAreaQuerier, Area>();
                    x.AddQuery<InMemoryPalletShuttleRackingShelfQuerier, Shelf>();
                    x.AddQuery<InMemoryPalletShuttleRackingLocationQuerier, Location>();
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
            var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocatorService>();
            var location = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
            {
                FromCode = "A1-S1-001-001-001-01",
                ToShelfCode = "S1"
            });
            // 不为空
            Assert.IsNotNull(location);
            // 不能分配同层同排同列的库位
            Assert.False(location.RCLD.Layer == 1 && location.RCLD.Row == 1 && location.RCLD.Column == 1);
            Assert.Pass();
        }
        /// <summary>
        /// 外部库位进入立库库位分配
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ExternalAllocate()
        {
            var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocatorService>();
            var location = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
            {
                FromCode = "A1-S2-001-001-001-01",
                ToShelfCode = "S1"
            });
            // 不为空
            Assert.IsNotNull(location);
            // 不能分配同层同排同列的库位
            Assert.True(location.RCLD.Layer == 1 && location.RCLD.Row == 1 && location.RCLD.Column == 1);
            Assert.Pass();
        }
    }

}
