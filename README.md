# XT.Common

# 1、介绍
通用程序集，只能添加通用的方法，不要引入第三方pack。
```
 // nuget删除包
 dotnet nuget delete XT.Common 1.0.7 -k oy2ox -s https://api.nuget.org/v3/index.json

 // nuget推送包
dotnet nuget push XT.Common.1.0.7.nupkg -k oy2oxt -s https://api.nuget.org/v3/index.json
```

# 2、功能
## 2.1、AppSettings配置
- new()初始化，GetObjData<T>()获取对象配置
## 2.2、特性
- Page,页面特性，Blazor自动生成页面菜单，用法 用法：Page("/ecs/floor1", "一楼", "mdi-home-floor-1", true)
- Order,排序特性，用于属性排序
## 2.3、雪花id生成器
-  初始化 new IdHelperBootstrapper().SetWorkderId(1).Boot();
- IdHelper.getId()
## 2.4、Utils工具
- ComputerUtil 获取计算机内存cpu等信息。
- DateTimeUtil 获取星期几等
## 2.5、Serivce辅助
- ApiConfigService 使用要求： AddOriginHttpClient注册http服务
- TimerService 封装定时器
## 2.6、枚举
- Sql 分类
## 2.7、Extension扩展方法
- Action 封装try catch,Get|Post HttpData，AddServiceInjects注入服务。
- Byte byte[] 转 ushort[],SetBit设置某个偏移位置的BIT值 
- Enumerable 集合操作
- Enum 枚举操作
- Sql sql模板替换
- Type 反射
- Xml xml操作

