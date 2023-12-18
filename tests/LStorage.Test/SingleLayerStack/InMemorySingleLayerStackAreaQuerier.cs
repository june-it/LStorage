namespace LStorage.Test.SingleLayerStack
{
    public class InMemorySingleLayerStackAreaQuerier : QuerierBase<Area>
    {
        public override IQueryable<Area> GetAll()
        {
            return (new List<Area>() { new Area()
            {
                Code = "A1"
            },new Area()
            {
                Code = "A2"
            }}).AsQueryable();
        }
    }
}
