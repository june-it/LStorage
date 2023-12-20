namespace LStorage.Test.Locations.PalletShuttleRacking
{
    public class InMemoryPalletShuttleRackingLocationQuerier : QuerierBase<Location>
    {
        public override IQueryable<Location> GetAll()
        {
            List<Location> _locations = new List<Location>();

            int id = 0;
            #region 立库
            for (int layer = 1; layer <= 1; layer++)
            {
                for (int row = 1; row <= 2; row++)
                {
                    for (int column = 1; column <= 1; column++)
                    {
                        for (int depth = 1; depth <= 6; depth++)
                        {
                            _locations.Add(new Location()
                            {
                                Id = (id + 1).ToString(),
                                Code = $"A1-S1-{row:0#0}-{column:0#0}-{layer:0#0}-{depth:0#}",
                                AreaId = "1",
                                ShelfId = "1",
                                MaxPalletCount = 1,
                                RCLD = new LocationRCLD()
                                {
                                    Column = column,
                                    Depth = depth,
                                    Layer = layer,
                                    Row = row,
                                },
                                PalletCount = 0
                            });
                        }
                    }
                }
            }
            #endregion

            #region 单层货架
            // S2
            for (int row = 1; row <= 2; row++)
            {
                for (int column = 1; column <= 1; column++)
                {
                    _locations.Add(new Location()
                    {
                        Id = (id + 1).ToString(),
                        Code = $"A2-S2-{row:0#0}-{column:0#0}-001-01",
                        AreaId = "2",
                        ShelfId = "2",
                        MaxPalletCount = 1,
                        RCLD = new LocationRCLD()
                        {
                            Column = column,
                            Depth = 1,
                            Layer = 1,
                            Row = row,
                        },
                        PalletCount = 0
                    });
                }
            }
            #endregion

            // 测试修改库位状态
            _locations.FirstOrDefault(x =>
            x.RCLD.Layer == 1
            && x.RCLD.Row == 1
            && x.RCLD.Column == 1
            && x.RCLD.Depth == 3).PalletCount = 1;


            return _locations.AsQueryable();
        }
    }
}
