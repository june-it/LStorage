namespace LStorage.Test.Inventories
{
    public class InMemoryShelfQuerier : QuerierBase<Shelf>
    {

        public override IQueryable<Shelf> GetAll()
        {
            return new List<Shelf>() { new Shelf()
            {
                Id="1",
                Code = "S1",
                AreaId = "1",
                Type = ShelfType.PalletShuttleRacking,
                IOType = ShelfIOType.FILO
            }}.AsQueryable();
        }
    }
}
