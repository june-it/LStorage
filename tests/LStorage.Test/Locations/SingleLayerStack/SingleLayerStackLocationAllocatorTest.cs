﻿using LStorage.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace LStorage.Test.Locations.SingleLayerStack
{
    /// <summary>
    /// 单层地堆式货架测试
    /// </summary>
    public class SingleLayerStackLocationAllocatorTest : TestBase
    {
        protected IServiceProvider ServiceProvider { get; private set; }
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection()
                .AddLogging()
                .AddLStorage(x =>
                {
                    x.AddQuerier<InMemorySingleLayerStackAreaQuerier, Area>();
                    x.AddQuerier<InMemorySingleLayerStackShelfQuerier, Shelf>();
                    x.AddQuerier<InMemorySingleLayerStackLocationQuerier, Location>();
                    x.AddLocationAllocator<SingleLayerLocationAllocator>();
                });
            ServiceProvider = services.BuildServiceProvider();
        }
        /// <summary>
        /// 区域内部移库分配
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task InternalAllocate()
        {
            var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocationService>();
            var result = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
            {
                FromCode = "A2-S2-001-001-001-01",
                ToAreaCode = "A2"
            });
            // 不为空
            Assert.IsNotNull(result.Location);
            Assert.True(result.Location.ShelfId == "2");
            Assert.True(result.Location.RCLD.Layer == 1 && result.Location.RCLD.Row == 2 && result.Location.RCLD.Column == 1);
            Assert.Pass();
        }
        /// <summary>
        ///货架间移库分配
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task InternalByShelfAllocate()
        {
            var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocationService>();
            var result = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
            {
                FromCode = "A2-S2-001-001-001-01",
                ToShelfCode = "S3"
            });
            // 不为空
            Assert.IsNotNull(result.Location);
            Assert.True(result.Location.ShelfId == "3");
            Assert.True(result.Location.RCLD.Layer == 1 && result.Location.RCLD.Row == 1 && result.Location.RCLD.Column == 1);
            Assert.Pass();
        }
        /// <summary>
        /// 立库分配进单层货架分配库位
        /// </summary>
        /// <returns></returns>
        [Test]
        public async Task ExternalAllocate()
        {
            var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocationService>();
            var result = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
            {
                FromCode = "A1-S1-001-001-001-01",
                ToAreaCode = "A2"
            });
            // 不为空
            Assert.IsNotNull(result.Location);
            Assert.True(result.Location.AreaId == "2");
            Assert.True(result.Location.RCLD.Layer == 1 && result.Location.RCLD.Row == 2 && result.Location.RCLD.Column == 1);
            Assert.Pass();
        }
    }
}
