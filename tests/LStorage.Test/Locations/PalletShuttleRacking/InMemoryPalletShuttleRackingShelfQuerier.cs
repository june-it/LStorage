namespace LStorage.Test.Locations.PalletShuttleRacking
{
    public class InMemoryPalletShuttleRackingShelfQuerier : QuerierBase<Shelf>
    {

        public override IQueryable<Shelf> GetAll()
        {
            return new List<Shelf>() { new Shelf()
            {
                Id="1",
                Code = "S1",
                AreaId = "1",
                ShelfType = ShelfType.PalletShuttleRacking,
                IOType = ShelfIOType.FILO
            },
            new Shelf()
            {
                Id="2",
                Code = "S2",
                AreaId = "2",
                ShelfType = ShelfType.SingleLayer,
                IOType = ShelfIOType.FILO
            }}.AsQueryable();
        }
    }
}
