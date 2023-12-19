namespace LStorage.Test.Inventories
{
    internal class InMemoryInventoryQuerier : QuerierBase<Inventory>
    {
        public override IQueryable<Inventory> GetAll()
        {
            var inventories = new List<Inventory>();
            for (int i = 1; i <= 10; i++)
            {
                inventories.Add(new Inventory()
                {
                    Id = i.ToString(),
                    InboundTime = DateTime.Now.AddHours(-i),
                    MaterialId = new Random().Next(1, 3) == 1 ? "1" : "2",
                    PalletId = i.ToString(),
                    Qty = new Random().Next(1, 10)
                });
            }
            return inventories.AsQueryable();
        }
    }
}
