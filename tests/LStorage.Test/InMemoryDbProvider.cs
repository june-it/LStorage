namespace LStorage.Test
{
    public class InMemoryDbProvider : IDbProvider
    {
        public IQueryable<Area> GetAllAreas()
        {
            return (new List<Area>() { new Area()
            {
               Code="A1"
            }}).AsQueryable();
        }
        public Task<Area> GetAreaAsync(string code)
        {
            return Task.FromResult(GetAllAreas().FirstOrDefault(a => a.Code == code));
        }
        public IQueryable<Location> GetAllLocations()
        {
            var locations = new List<Location>();
            for (int layer = 1; layer <= 10; layer++)
            {
                for (int row = 1; row <= 2; row++)
                {
                    for (int column = 1; column <= 10; column++)
                    {
                        for (int depth = 1; depth <= 6; depth++)
                        {
                            locations.Add(new Location()
                            {
                                Code = $"A1-T1-{row:0#0}-{column:0#0}-{layer:0#0}-{depth:0#}",
                                AreaCode = "A1",
                                ShelfCode = "S1",
                                MaxStockCount = 1,
                                RCLD = new LocationRCLD()
                                {
                                    Column = column,
                                    Depth = depth,
                                    Layer = layer,
                                    Row = row,
                                },
                                StockCount = 0
                            });
                        }
                    }
                }
            }
            return locations.AsQueryable();
        }
        public Task<Location> GetLocationAsync(string code)
        {
            return Task.FromResult(GetAllLocations().FirstOrDefault(a => a.Code == code));
        }
        public Task<Shelf> GetShelfAsync(string code)
        {
            return Task.FromResult(GetShelves().FirstOrDefault(a => a.Code == code));
        }
        public IQueryable<Shelf> GetShelves()
        {
            return (new List<Shelf>() { new Shelf()
            {
               Code="S1",
               AreaCode="A1",
               ShelfType= ShelfType.PalletShuttleRacking
            }}).AsQueryable();
        }
    }
}
