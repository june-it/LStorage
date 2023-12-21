namespace LStorage.Test.Inventories
{
    internal class InMemoryInventoryQuerier : QuerierBase<Inventory>
    {
        public override IQueryable<Inventory> GetAll()
        {
            var inventories = new List<Inventory>();
            for (int i = 1; i <= 5; i++)
            {
                inventories.Add(new Inventory()
                {
                    Id = i.ToString(),
                    InboundTime = DateTime.Now.AddHours(-i),
                    MaterialId = "1",
                    PalletId = i.ToString(),
                    Qty = 1
                });
            }
            for (int i = 6; i <= 10; i++)
            {
                inventories.Add(new Inventory()
                {
                    Id = i.ToString(),
                    InboundTime = DateTime.Now.AddHours(-i),
                    MaterialId = "2",
                    PalletId = i.ToString(),
                    Qty = 2
                });
            }
            return inventories.AsQueryable();
        }
    }
}
