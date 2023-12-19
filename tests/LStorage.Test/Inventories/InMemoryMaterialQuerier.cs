namespace LStorage.Test.Inventories
{
    public class InMemoryMaterialQuerier : QuerierBase<Material>
    {

        public override IQueryable<Material> GetAll()
        {
            return new List<Material>() { new Material()
            {
                Id="1",
                Code = "M1"
            },
            new Material()
            {
                Id="2",
                Code = "M2"
            }}.AsQueryable();
        }
    }
}
