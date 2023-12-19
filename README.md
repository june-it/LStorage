# LStorage
 类库提供库位分配、入库、出库、移库等WMS的库位管理功能

## 术语说明

### 排
货架俯视图中，垂直于运输通道连续库位为排,沿同一方向递增即可
![排](/shotsnaps/terms-01.png) 
### 列
货架俯视图中，平行于运输通道库位为列，沿同一方向递增即可。
![列](/shotsnaps/terms-02.png) 
### 层
垂直于地面的库位层，由下到上层序号递增。
### 深
货架俯视图中，同一个排垂直方向上一列位深方向，由远到近深序号递增。
![列](/shotsnaps/terms-04.png) 

## 库位编号说明

A1-S1-001-001-001-01 => A1（区域编号） + S1（货架编号） + 001（排序号） + 001（列序号） +  001（层序号） + 01（深序号）


## 空库位分配

### 参数说明
| 参数名      | 描述  |  是否必填  |
| ----------- | ----------- | ----------- |
| FromCode      | 获取或设置来源库位编码       |   是  |
| ToAreaCode   | 获取或设置分配区域编码        |  是  |
| ToShelfCode   | 获取或设置分配货架编码        |  否  |
| Row   | 获取或设置分配排序号        |  否  |
| Column   | 获取或设置分配列序号        |  否  |
| Layer   | 获取或设置分配层序号        |  否  |
| Depth   | 获取或设置分配深序号        |  否  |
| SortingItems   | 获取或设置库位排序方式数组，默认按层升序/排升序/层升序/深降序，可通过排序数组的顺序进行排序优先级，      |  否  |

### 使用示例

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
    // 穿梭式货架（立库）
    x.AddLocationAllocator<PalletShuttleLocationAllocator>();
});

```
1. 立库内部移库库位分配

```
var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocationService>();
var location = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
{
    FromCode = "A1-S1-001-001-001-01",
    ToShelfCode = "S1"
});

```
测试中库位`A1-S1-001-001-001-03`设置了托盘，分配结果为`A1-S1-002-001-001-06`

如图

![立库内部移库库位分配](/shotsnaps/example-01.png) 

2. 外部库位进入立库库位分配

```
var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocationService>();
var location = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
{
    FromCode = "A1-S2-001-001-001-01",
    ToShelfCode = "S1"
});

```
测试中库位`A1-S1-001-001-001-03`设置了托盘，分配结果为`A1-S1-001-001-001-02`

如图
![立库内部移库库位分配](/shotsnaps/example-02.png) 


#### 单层货架/单层地堆式货架

##### 服务注册
``` 
service.AddLStorage(x =>
{
    // 添加区域、货架、货位
    x.AddQuery<InMemorySingleLayerStackAreaQuerier, Area>();
    x.AddQuery<InMemorySingleLayerStackShelfQuerier, Shelf>();
    x.AddQuery<InMemorySingleLayerStackLocationQuerier, Location>();
    // 注册货架分配服务
    // 单层货架
    x.AddLocationAllocator<SingleLayerLocationAllocator>();
});

```

1. 区域内部移库分配

```
var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocationService>();
var location = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
{
    FromCode = "A2-S2-001-001-001-01",
    ToAreaCode = "A2"
});
```
分配结果如图
![区域内部移库分配](/shotsnaps/example-03.png) 

2. 单层货架间移库分配

```
var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocationService>();
var location = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
{
    FromCode = "A2-S2-001-001-001-01",
    ToShelfCode = "S3"
});
```
分配结果如图
![单层货架间移库分配](/shotsnaps/example-04.png) 

3. 其他区域分配进单层货架分配库位

```
var locationAllocatorService = ServiceProvider.GetRequiredService<ILocationAllocationService>();
var location = await locationAllocatorService.AllocateAsync(new AllocateLocationInput()
{
    FromCode = "A3-S4-001-001-001-01",
    ToAreaCode = "A2"
});
```
分配结果如图
![其他区域分配进单层货架分配库位](/shotsnaps/example-05.png) 