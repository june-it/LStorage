namespace LStorage.Test.Inventories
{
    public class InMemoryPalletQuerier : QuerierBase<Pallet>
    {

        public override IQueryable<Pallet> GetAll()
        {
            var pallets = new List<Pallet>();
            for (int i = 1; i <= 10; i++)
            {
                pallets.Add(new Pallet(i.ToString(), $"M{i:0#}", i.ToString()));
            }
            return pallets.AsQueryable();
        }
    }
}
