namespace LStorage.Test.SingleLayerStack
{
    public class InMemorySingleLayerStackShelfQuerier : QuerierBase<Shelf>
    {

        public override IQueryable<Shelf> GetAll()
        {
            return (new List<Shelf>() { new Shelf()
            {
                Code = "S1",
                AreaCode = "A1",
                ShelfType = ShelfType.PalletShuttleRacking,
                IOType = ShelfIOType.FILO
            },
            new Shelf()
            {
                Code = "S2",
                AreaCode = "A2",
                ShelfType = ShelfType.SingleLayerStack,
                IOType = ShelfIOType.FILO
            },
            new Shelf()
            {
                Code = "S3",
                AreaCode = "A2",
                ShelfType = ShelfType.SingleLayerStack,
                IOType = ShelfIOType.FILO
            }}).AsQueryable();
        }
    }
}
