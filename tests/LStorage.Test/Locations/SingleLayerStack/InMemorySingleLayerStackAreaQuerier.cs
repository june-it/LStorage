namespace LStorage.Test.Locations.SingleLayerStack
{
    public class InMemorySingleLayerStackAreaQuerier : QuerierBase<Area>
    {
        public override IQueryable<Area> GetAll()
        {
            return new List<Area>() { new Area()
            {
                Id="1",
                Code = "A1"
            },new Area()
            {
                Id="2",
                Code = "A2"
            }}.AsQueryable();
        }
    }
}
