using LStorage.Inventories;
using Microsoft.Extensions.DependencyInjection;

namespace LStorage.Test.Inventories
{
    /// <summary>
    /// 库存分配测试
    /// </summary>
    public class InventoryTest
    {
        protected IServiceProvider ServiceProvider { get; private set; }
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection()
                .AddLogging()
                .AddLStorage(x =>
                {
                    x.AddQuerier<InMemoryAreaQuerier, Area>();
                    x.AddQuerier<InMemoryShelfQuerier, Shelf>();
                    x.AddQuerier<InMemoryLocationQuerier, Location>();
                    x.AddQuerier<InMemoryMaterialQuerier, Material>();
                    x.AddQuerier<InMemoryPalletQuerier, Pallet>();
                    x.AddQuerier<InMemoryInventoryQuerier, Inventory>();

                    x.AddLocationDependencyFinder<PalletShuttleLocationDependencyFinder>();
                    x.AddLocationDependencyFinder<SingleLayerLocationDependencyFinder>();
                });
            ServiceProvider = services.BuildServiceProvider();
        }

        [Test]
        public async Task Allocate()
        {
            var locationAllocatorService = ServiceProvider.GetRequiredService<IInventoryAllocationService>();
            var result = await locationAllocatorService.AllocateAsync(new AllocateInventoryInput()
            {
                MaterialCode = "M1",
                Qty = 3
            });
            // 不为空
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.Sum(x => x.Inventory.Qty) >= 1);
        }
        /// <summary>
        /// 优先存在依赖库位的库存
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task AllocateOrderByDependentCount()
        {
            var locationAllocatorService = ServiceProvider.GetRequiredService<IInventoryAllocationService>();
            var result = await locationAllocatorService.AllocateAsync(new AllocateInventoryInput()
            {
                MaterialCode = "M1",
                Qty = 3,
                SortingItems = new[]
                {
                    new AllocateInventorySorting( AllocateInventorySortingDimension.DependentCount, AllocateInventorySortingDirection.Descending)
                }
            });
            // 不为空
            Assert.IsTrue(result.Count > 0);
            Assert.IsTrue(result.Sum(x => x.Inventory.Qty) >= 1);
        }
    }
}
