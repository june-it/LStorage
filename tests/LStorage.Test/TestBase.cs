using LStorage.Locations;
using Microsoft.Extensions.DependencyInjection;

namespace LStorage.Test
{
    public abstract class TestBase
    {
        protected IServiceProvider ServiceProvider { get; private set; }
        [SetUp]
        public void Setup()
        {
            var services = new ServiceCollection()
                .AddLogging()
                .AddLStorage(x =>
                {
                    x.AddDbProvider<InMemoryDbProvider>();
                    x.AddLocationAllocator<PalletShuttleRackingLocationAllocator>();
                });
            ServiceProvider = services.BuildServiceProvider();
        }
    }
}