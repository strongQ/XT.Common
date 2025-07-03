using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Text.Json.Serialization.Metadata;
using XT.Common.Enums;

namespace XT.Common.Models.SignalR
{
    // --- 这是关键部分 ---
    // 创建一个AOT兼容的JSON序列化上下文
    // 将所有需要通过SignalR传输的类型用 [JsonSerializable] 特性标记出来
    [JsonSerializable(typeof(User))]
    [JsonSerializable(typeof(List<User>))] // 显式添加List<T>通常是好习惯
    [JsonSerializable(typeof(InformModel))]
    [JsonSerializable(typeof(RemoteLog))]
    [JsonSerializable(typeof(InformTypeEnum))]
    [JsonSerializable(typeof(LogEnum))]
    [JsonSerializable(typeof(string))] // 包含string等基础类型也是一个好习惯
    public partial class SignalRJsonContext : JsonSerializerContext
    {
       
    }
}
