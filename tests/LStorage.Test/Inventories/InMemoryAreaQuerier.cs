namespace LStorage.Test.Inventories
{
    public class InMemoryAreaQuerier : QuerierBase<Area>
    {
        public override IQueryable<Area> GetAll()
        {
            return new List<Area>() { new Area()
            {
                Id="1",
                Code = "A1"
            }}.AsQueryable();
        }
    }
}
