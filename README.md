# LStorage
 类库提供库位分配、入库、出库、移库等WMS的库位管理功能

## 库位编号说明

A1-S1-001-001-001-01 

=> 

A1（区域编号） + S1（货架编号） + 001（排序号） + 001（列序号） +  001（层序号） + 01（深序号）


## 使用示例

### 场景 

#### 穿梭式货架

##### 服务注册
``` 
service.AddLStorage(x =>
{
    // 添加区域、货架、货位
    x.AddQuery<InMemorySingleLayerStackAreaQuerier, Area>();
    x.AddQuery<InMemorySingleLayerStackShelfQuerier, Shelf>();
    x.AddQuery<InMemorySingleLayerStackLocationQuerier, Location>();
    // 注册货架分配服务
    x.AddLocationAllocator<SingleLayerLocationAllocator>();
});

```
1. 立库内部移库库位分配

```
var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocatorService>();
var location = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
{
    FromCode = "A1-S1-001-001-001-01",
    ToShelfCode = "S1"
});

```
分配结果如图
![立库内部移库库位分配](/shotsnaps/shotsnap-01.png) 


#### 单层货架/单层地堆式货架

