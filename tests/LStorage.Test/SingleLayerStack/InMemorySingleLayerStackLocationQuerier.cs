namespace LStorage.Test.SingleLayerStack
{
    public class InMemorySingleLayerStackLocationQuerier : QuerierBase<Location>
    {
        public override IQueryable<Location> GetAll()
        {
            List<Location> _locations = new List<Location>();

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
                                Code = $"A1-S1-{row:0#0}-{column:0#0}-{layer:0#0}-{depth:0#}",
                                AreaCode = "A1",
                                ShelfCode = "S1",
                                MaxStockCount = 1,
                                RCLD = new LocationRCLD()
                                {
                                    Column = column,
                                    Depth = depth,
                                    Layer = layer,
                                    Row = row,
                                },
                                StockCount = 0
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
                        Code = $"A2-S2-{row:0#0}-{column:0#0}-001-01",
                        AreaCode = "A2",
                        ShelfCode = "S2",
                        MaxStockCount = 1,
                        RCLD = new LocationRCLD()
                        {
                            Column = column,
                            Depth = 1,
                            Layer = 1,
                            Row = row,
                        },
                        StockCount = 0
                    });
                }
            }
            // S3
            for (int row = 1; row <= 2; row++)
            {
                for (int column = 1; column <= 1; column++)
                {
                    _locations.Add(new Location()
                    {
                        Code = $"A2-S3-{row:0#0}-{column:0#0}-001-01",
                        AreaCode = "A2",
                        ShelfCode = "S3",
                        MaxStockCount = 1,
                        RCLD = new LocationRCLD()
                        {
                            Column = column,
                            Depth = 1,
                            Layer = 1,
                            Row = row,
                        },
                        StockCount = 0
                    });
                }
            }
            #endregion

            // 测试 
            _locations.FirstOrDefault(x =>
            x.ShelfCode == "S2"
            && x.RCLD.Layer == 1
            && x.RCLD.Row == 1
            && x.RCLD.Column == 1
            && x.RCLD.Depth == 1).StockCount = 1;


            return _locations.AsQueryable();
        }
    }
}
