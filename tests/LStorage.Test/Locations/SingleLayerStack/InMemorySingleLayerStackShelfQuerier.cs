namespace LStorage.Test.Locations.SingleLayerStack
{
    public class InMemorySingleLayerStackShelfQuerier : QuerierBase<Shelf>
    {

        public override IQueryable<Shelf> GetAll()
        {
            return new List<Shelf>() { new Shelf()
            {
                Code = "S1",
                AreaId = "1",
                Type = ShelfType.PalletShuttleRacking,
                IOType = ShelfIOType.FILO
            },
            new Shelf()
            {
                Code = "S2",
                AreaId= "2",
                Type = ShelfType.SingleLayer,
                IOType = ShelfIOType.FILO
            },
            new Shelf()
            {
                Code = "S3",
                AreaId = "2",
                Type = ShelfType.SingleLayer,
                IOType = ShelfIOType.FILO
            }}.AsQueryable();
        }
    }
}
