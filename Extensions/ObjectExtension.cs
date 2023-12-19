
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using XT.Common.Extensions;
using XT.Common.Converters;

namespace XT.Common.Extensions
{
    public static class ObjectExtension
    {
        private static readonly TimeSpan timeSpan = TimeSpan.FromHours(8);


        #region 私有方法,使用Convert类进行转换
        private static List<TypeCode> _baseTypes = new List<TypeCode>
        {
            TypeCode.Byte,TypeCode.SByte,
            TypeCode.Int16,TypeCode.UInt16,
            TypeCode.Int32,TypeCode.UInt32,
            TypeCode.Int64,TypeCode.UInt64,
            TypeCode.Single,TypeCode.Double,TypeCode.Decimal,
            TypeCode.Char,TypeCode.Boolean,TypeCode.String,TypeCode.DateTime
        };

        private static object _baseConvert(TypeCode typeCode, object value)
        {
            switch (typeCode)
            {
                case TypeCode.Byte:
                    {
                        return Convert.ToByte(value);
                    }
                case TypeCode.SByte:
                    {
                        return Convert.ToSByte(value);
                    }
                case TypeCode.Int16:
                    {
                        return Convert.ToInt16(value);
                    }
                case TypeCode.UInt16:
                    {
                        return Convert.ToUInt16(value);
                    }
                case TypeCode.Int32:
                    {
                        return Convert.ToInt32(value);
                    }
                case TypeCode.UInt32:
                    {
                        return Convert.ToUInt32(value);
                    }
                case TypeCode.Int64:
                    {
                        return Convert.ToInt64(value);
                    }
                case TypeCode.UInt64:
                    {
                        return Convert.ToUInt64(value);
                    }
                case TypeCode.Single:
                    {
                        return Convert.ToSingle(value);
                    }
                case TypeCode.Double:
                    {
                        return Convert.ToDouble(value);
                    }
                case TypeCode.Decimal:
                    {
                        return Convert.ToDecimal(value);
                    }
                case TypeCode.Char:
                    {
                        return Convert.ToChar(value);
                    }
                case TypeCode.Boolean:
                    {
                        return Convert.ToBoolean(value);
                    }
                case TypeCode.String:
                    {
                        return Convert.ToString(value);
                    }
                case TypeCode.DateTime:
                    {
                        return Convert.ToDateTime(value);
                    }
                default:
                    {
                        return null;
                    }
            }
        }
        #endregion

        #region 通用转换方法 object.To<T>()
        /// <summary>
        /// 通用转换方法
        /// </summary>
        /// <param name="value">待转换对象</param>
        /// <param name="parameters">转换时用到的参数,目前仅用于string -> DateTime/DateTimeOffset</param>
        public static T To<T>(this object value, params object[] parameters)
        {
            if (value is T) return (T)value;
            Type type = typeof(T);
            if (value == null || value is DBNull)
            {
                //引用类型: 返回默认 即: null
                if (!type.IsValueType) return default;
                //可空类型: 返回默认 即: null
                if (type.IsNullable()) return default;
            }
            return (T)value.To(type, parameters);
            //return To(value, type, parameters).To<T>();
        }

        /// <summary>
        /// 通用转换方法
        /// </summary>
        /// <param name="value">待转换对象</param>
        /// <param name="type">目的类型</param>
        /// <param name="parameters">转换时用到的参数,目前仅用于string -> DateTime/DateTimeOffset</param>
        /// <returns></returns>
        public static object To(this object value, Type type, params object[] parameters)
        {
            //null -> 其他,DBNull -> 其他 (DataTable中使用)
            if (value == null || value is DBNull)
            {
                //引用类型: 返回默认 即: null
                if (!type.IsValueType) return null;
                //可空类型: 返回默认 即: null
                if (type.IsNullable()) return null;
            }
            //子类 -> 父类
            if (type.IsAssignableFrom(value.GetType()))
            {
                return value;
            }

            if (type.IsNullable()) type = type.GenericTypeArguments[0];

            //其他 -> string
            if (type == typeof(string))
            {
                if (value is DateTime dt)
                {
                    if (parameters.IsNotNullOrEmpty())
                    {
                        return dt.ToString(parameters[0].ToString());
                    }
                    return dt.ToString();
                }
                if (value is DateTimeOffset offset)
                {
                    if (parameters.IsNotNullOrEmpty())
                    {
                        return offset.ToString(parameters[0].ToString());
                    }
                    return offset.ToString();
                }
#if NET6_0_OR_GREATER
                if (value is DateOnly dateOnly)
                {
                    if (parameters.IsNotNullOrEmpty())
                    {
                        return dateOnly.ToString(parameters[0].ToString());
                    }
                    return dateOnly.ToString();
                }
                if (value is TimeOnly timeOnly)
                {
                    if (parameters.IsNotNullOrEmpty())
                    {
                        return timeOnly.ToString(parameters[0].ToString());
                    }
                    return timeOnly.ToString();
                }
#endif
                if (value is Guid guid)
                {
                    if (parameters.IsNotNullOrEmpty())
                    {
                        return guid.ToString(parameters[0].ToString());
                    }
                    return guid.ToString();
                }
                object obj = value?.ToString();
                return obj;
            }

            if (type.IsValueType)
            {
                //值类型
                #region 枚举
                if (type.IsEnum)
                {
                    //枚举转换
                    return Enum.Parse(type, value?.ToString(), true);
                }
                #endregion
                #region 字符串转日期
                if (value is string && (type == typeof(DateTime) || type == typeof(DateTimeOffset)))
                {
                    if (type == typeof(DateTime))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            value = DateTime.ParseExact(value.ToString(), parameters[0].ToString(), null);
                        }
                        else
                        {
                            value = DateTime.Parse(value.ToString());
                        }
                        return value;
                    }
                    if (type == typeof(DateTimeOffset))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            value = DateTimeOffset.ParseExact(value.ToString(), parameters[0].ToString(), null);
                        }
                        else
                        {
                            value = DateTimeOffset.Parse(value.ToString());
                        }
                        return value;
                    }
                }
                #endregion
                #region DateOnly & TimeOnly & DateTime & DateTimeOffset & TimeSpan
#if NET6_0_OR_GREATER
                // string => DateOnly or TimeOnly
                if (value is string && (type == typeof(DateOnly) || type == typeof(TimeOnly)))
                {
                    if (type == typeof(DateOnly))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            value = DateOnly.ParseExact(value.ToString(), parameters[0].ToString(), null);
                        }
                        else
                        {
                            value = DateOnly.Parse(value.ToString());
                        }
                        return value;
                    }
                    if (type == typeof(TimeOnly))
                    {
                        if (parameters != null && parameters.Length > 0)
                        {
                            value = TimeOnly.ParseExact(value.ToString(), parameters[0].ToString(), null);
                        }
                        else
                        {
                            value = TimeOnly.Parse(value.ToString());
                        }
                        return value;
                    }
                }

                //DateTime or DateTimeOffset or TimeSpan => DateOnly or TimeOnly
                if ((value is DateTime || value is DateTimeOffset || value is TimeSpan) && (type == typeof(DateOnly) || type == typeof(TimeOnly)))
                {
                    if (type == typeof(DateOnly))
                    {
                        if (value is DateTime dt)
                        {
                            value = DateOnly.FromDateTime(dt);
                        }
                        else if (value is DateTimeOffset dto)
                        {
                            value = DateOnly.FromDateTime(dto.DateTime);
                        }
                        else if (value is TimeSpan ts)
                        {
                            throw new Exception("无法从TimeSpan转到DateOnly!");
                        }
                        return value;
                    }
                    if (type == typeof(TimeOnly))
                    {
                        if (value is DateTime dt)
                        {
                            value = TimeOnly.FromDateTime(dt);
                        }
                        else if (value is DateTimeOffset dto)
                        {
                            value = TimeOnly.FromDateTime(dto.DateTime);
                        }
                        else if (value is TimeSpan ts)
                        {
                            value = TimeOnly.FromTimeSpan(ts);
                        }
                        return value;
                    }
                }

                //DateOnly or TimeOnly => DateTime or DateTimeOffset or TimeSpan
                if ((value is DateOnly || value is TimeOnly) && (type == typeof(DateTime) || type == typeof(DateTimeOffset) || type == typeof(TimeSpan)))
                {
                    if (type == typeof(DateTime))
                    {
                        if (value is DateOnly dateOnly)
                        {
                            value = new DateTime(dateOnly.Year, dateOnly.Month, dateOnly.Day);
                        }
                        else if (value is TimeOnly timeOnly)
                        {
                            value = new DateTime(1970, 1, 1, timeOnly.Hour, timeOnly.Minute, timeOnly.Second, timeOnly.Millisecond);
                        }
                        return value;
                    }
                    if (type == typeof(DateTimeOffset))
                    {
                        if (value is DateOnly dateOnly)
                        {
                            value = new DateTimeOffset(dateOnly.Year, dateOnly.Month, dateOnly.Day, 0, 0, 0, timeSpan);
                        }
                        else if (value is TimeOnly timeOnly)
                        {
                            value = new DateTimeOffset(1970, 1, 1, timeOnly.Hour, timeOnly.Minute, timeOnly.Second, timeOnly.Millisecond, timeSpan);
                        }
                        return value;
                    }
                    if (type == typeof(TimeSpan))
                    {
                        if (value is DateOnly)
                        {
                            throw new Exception("无法从 DateOnly 转到TimeSpan!");
                        }
                        else if (value is TimeOnly timeOnly)
                        {
                            value = new TimeSpan(0, timeOnly.Hour, timeOnly.Minute, timeOnly.Second, timeOnly.Millisecond);
                        }
                        return value;
                    }
                }

#endif
                #endregion

                #region DateTimeOffset 转 DateTime
                if (value?.GetType() == typeof(DateTimeOffset) && type == typeof(DateTime))
                {
                    return ((DateTimeOffset)value).DateTime;
                }
                #endregion
                #region DateTime 转 DateTimeOffset
                if (value?.GetType() == typeof(DateTime) && type == typeof(DateTimeOffset))
                {
                    return new DateTimeOffset((DateTime)value);
                }
                #endregion
                #region TimeSpan 转 DateTime DateTimeOffset
                if (value is TimeSpan time && (type == typeof(DateTimeOffset) || type == typeof(DateTime)))
                {
                    if (type == typeof(DateTime))
                    {
                        value = new DateTime(1970, 01, 01, time.Hours, time.Minutes, time.Seconds, time.Milliseconds);
                    }
                    else if (type == typeof(DateTimeOffset))
                    {
                        value = new DateTimeOffset(1970, 01, 01, time.Hours, time.Minutes, time.Seconds, time.Milliseconds, timeSpan);
                    }
                    return value;
                }
                #endregion
                #region 字符串转bool
                if (type == typeof(bool) && value is string)
                {
                    var tmp = value.ToString().ToUpper();
                    if (new string[] { "OK", "YES", "TRUE", "1", "是" }.Contains(tmp)) return true;
                    else return false;
                }
                #endregion
                #region 可以使用Convert.ToXXX的转换
                var typeCode = type.GetTypeCode();
                if (_baseTypes.Contains(typeCode))
                {
                    return _baseConvert(typeCode, value);
                }
                #endregion
                #region 其他
                var converter = TypeDescriptor.GetConverter(type);
                return converter.ConvertFrom(value);
                #endregion
            }
            //else
            //{
            //    //兼容从json字符串反序列化
            //    if (value is string && type.IsClass) return JsonConvert.DeserializeObject(value.ToString(), type);
            //    //引用类型
            //    return value;
            //}
            return null;
        }

        /// <summary>
        /// 通用转换方法
        /// </summary>
        public static T ToWithDefault<T>(this object value, T defaultValue, params object[] parameters)
        {
            if (value is T) return (T)value;
            Type type = typeof(T);
            if (value == null || value is DBNull)
            {
                //引用类型: 返回默认 即: null
                if (!type.IsValueType) return default;
                //可空类型: 返回默认 即: null
                if (type.IsNullable()) return default;
            }
            return (T)value.ToWithDefault(type, defaultValue, parameters);
        }

        /// <summary>
        /// 通用转换方法
        /// </summary>
        public static object ToWithDefault(this object value, Type type, object defaultValue, params object[] parameters)
        {
            if (type == null) return defaultValue;
            if (value == null || value is DBNull)
            {
                //引用类型: 返回默认 即: null
                if (!type.IsValueType) return null;
                //可空类型: 返回默认 即: null
                if (type.IsNullable()) return null;
            }

            try
            {
                return value.To(type, parameters);
            }
            catch
            {

                return defaultValue;
            }
        }
        #endregion


        /// <summary>
        /// 判断类型是否实现某个泛型
        /// </summary>
        /// <param name="type">类型</param>
        /// <param name="generic">泛型类型</param>
        /// <returns>bool</returns>
        public static bool HasImplementedRawGeneric(this Type type, Type generic)
        {
            // 检查接口类型
            var isTheRawGenericType = type.GetInterfaces().Any(IsTheRawGenericType);
            if (isTheRawGenericType) return true;

            // 检查类型
            while (type != null && type != typeof(object))
            {
                isTheRawGenericType = IsTheRawGenericType(type);
                if (isTheRawGenericType) return true;
                type = type.BaseType;
            }

            return false;

            // 判断逻辑
            bool IsTheRawGenericType(Type type) => generic == (type.IsGenericType ? type.GetGenericTypeDefinition() : type);
        }

        /// <summary>
        /// 将字典转化为QueryString格式
        /// </summary>
        /// <param name="dict"></param>
        /// <param name="urlEncode"></param>
        /// <returns></returns>
        public static string ToQueryString(this Dictionary<string, string> dict, bool urlEncode = true)
        {
            return string.Join("&", dict.Select(p => $"{(urlEncode ? p.Key?.UrlEncode() : "")}={(urlEncode ? p.Value?.UrlEncode() : "")}"));
        }
        /// <summary>
        /// 对象转换为QueryString格式
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="urlEncode"></param>
        /// <returns></returns>
        public static string ToQueryString(this object obj, bool urlEncode = true)
        {
            var dictionary = obj.GetType()
    .GetProperties(BindingFlags.Instance | BindingFlags.Public)
    .ToDictionary(prop => prop.Name, prop => prop.GetValue(obj, null)?.ToString());
            return dictionary.ToQueryString(urlEncode);
        }

        /// <summary>
        /// 将字符串URL编码
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static string UrlEncode(this string str)
        {
            return string.IsNullOrEmpty(str) ? "" : System.Web.HttpUtility.UrlEncode(str, Encoding.UTF8);
        }

        /// <summary>
        /// 对象序列化成Json字符串
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string ToJson(this object obj)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNamingPolicy = JsonNamingPolicy.CamelCase,
                DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull

            };
             var time = new DateTimeParseConverter();
             options.Converters.Add(time);
            return JsonSerializer.Serialize(obj, options);
        }

        /// <summary>
        /// Json字符串反序列化成对象
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="json"></param>
        /// <returns></returns>
        public static T ToObject<T>(this string json)
        {
            JsonSerializerOptions options = new JsonSerializerOptions()
            {
                PropertyNameCaseInsensitive = true
            };
               var time = new DateTimeParseConverter();
              options.Converters.Add(time);
            return JsonSerializer.Deserialize<T>(json, options);
        }

        /// <summary>
        /// 将object转换为long，若失败则返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static long ParseToLong(this object obj)
        {
            try
            {
                return long.Parse(obj.ToString());
            }
            catch
            {
                return 0L;
            }
        }

        /// <summary>
        /// 将object转换为long，若失败则返回指定值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static long ParseToLong(this string str, long defaultValue)
        {
            try
            {
                return long.Parse(str);
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 将object转换为double，若失败则返回0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double ParseToDouble(this object obj)
        {
            try
            {
                return double.Parse(obj.ToString());
            }
            catch
            {
                return 0;
            }
        }

        /// <summary>
        /// JsonElement 转 Object
        /// </summary>
        /// <param name="jsonElement"></param>
        /// <returns></returns>
        internal static object ToObject(this JsonElement jsonElement)
        {
            switch (jsonElement.ValueKind)
            {
                case JsonValueKind.String:
                    return jsonElement.GetString();

                case JsonValueKind.Undefined:
                case JsonValueKind.Null:
                    return default;

                case JsonValueKind.Number:
                    return jsonElement.GetDecimal();

                case JsonValueKind.True:
                case JsonValueKind.False:
                    return jsonElement.GetBoolean();

                case JsonValueKind.Object:
                    var enumerateObject = jsonElement.EnumerateObject();
                    var dic = new Dictionary<string, object>();
                    foreach (var item in enumerateObject)
                    {
                        dic.Add(item.Name, item.Value.ToObject());
                    }
                    return dic;

                case JsonValueKind.Array:
                    var enumerateArray = jsonElement.EnumerateArray();
                    var list = new List<object>();
                    foreach (var item in enumerateArray)
                    {
                        list.Add(item.ToObject());
                    }
                    return list;

                default:
                    return default;
            }
        }

        /// <summary>
        /// 将object转换为double，若失败则返回指定值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static double ParseToDouble(this object str, double defaultValue)
        {
            try
            {
                return double.Parse(str.ToString());
            }
            catch
            {
                return defaultValue;
            }
        }

        /// <summary>
        /// 将string转换为DateTime，若失败则返回日期最小值
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        public static DateTime ParseToDateTime(this string str)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    return DateTime.MinValue;
                }
                if (str.Contains('-') || str.Contains('/'))
                {
                    return DateTime.Parse(str);
                }
                else
                {
                    int length = str.Length;
                    switch (length)
                    {
                        case 4:
                            return DateTime.ParseExact(str, "yyyy", System.Globalization.CultureInfo.CurrentCulture);

                        case 6:
                            return DateTime.ParseExact(str, "yyyyMM", System.Globalization.CultureInfo.CurrentCulture);

                        case 8:
                            return DateTime.ParseExact(str, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

                        case 10:
                            return DateTime.ParseExact(str, "yyyyMMddHH", System.Globalization.CultureInfo.CurrentCulture);

                        case 12:
                            return DateTime.ParseExact(str, "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);

                        case 14:
                            return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);

                        default:
                            return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    }
                }
            }
            catch
            {
                return DateTime.MinValue;
            }
        }

        /// <summary>
        /// 将string转换为DateTime，若失败则返回默认值
        /// </summary>
        /// <param name="str"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static DateTime ParseToDateTime(this string str, DateTime? defaultValue)
        {
            try
            {
                if (string.IsNullOrWhiteSpace(str))
                {
                    return defaultValue.GetValueOrDefault();
                }
                if (str.Contains('-') || str.Contains('/'))
                {
                    return DateTime.Parse(str);
                }
                else
                {
                    int length = str.Length;
                    switch (length)
                    {
                        case 4:
                            return DateTime.ParseExact(str, "yyyy", System.Globalization.CultureInfo.CurrentCulture);

                        case 6:
                            return DateTime.ParseExact(str, "yyyyMM", System.Globalization.CultureInfo.CurrentCulture);

                        case 8:
                            return DateTime.ParseExact(str, "yyyyMMdd", System.Globalization.CultureInfo.CurrentCulture);

                        case 10:
                            return DateTime.ParseExact(str, "yyyyMMddHH", System.Globalization.CultureInfo.CurrentCulture);

                        case 12:
                            return DateTime.ParseExact(str, "yyyyMMddHHmm", System.Globalization.CultureInfo.CurrentCulture);

                        case 14:
                            return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);

                        default:
                            return DateTime.ParseExact(str, "yyyyMMddHHmmss", System.Globalization.CultureInfo.CurrentCulture);
                    }
                }
            }
            catch
            {
                return defaultValue.GetValueOrDefault();
            }
        }

        /// <summary>
        /// 将一个对象转换为指定类型
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="obj"></param>
        /// <returns></returns>
        internal static T ChangeType<T>(this object obj)
        {
            return (T)obj.ChangeType(typeof(T));
        }

        /// <summary>
        /// 将一个对象转换为指定类型
        /// </summary>
        /// <param name="obj">待转换的对象</param>
        /// <param name="type">目标类型</param>
        /// <returns>转换后的对象</returns>
        internal static object ChangeType(this object obj, Type type)
        {
            if (type == null) return obj;
            if (type == typeof(string)) return obj?.ToString();
            if (type == typeof(Guid) && obj != null) return Guid.Parse(obj.ToString());
            if (obj == null) return type.IsValueType ? Activator.CreateInstance(type) : null;

            var underlyingType = Nullable.GetUnderlyingType(type);
            if (type.IsAssignableFrom(obj.GetType())) return obj;
            else if ((underlyingType ?? type).IsEnum)
            {
                if (underlyingType != null && string.IsNullOrWhiteSpace(obj.ToString())) return null;
                else return Enum.Parse(underlyingType ?? type, obj.ToString());
            }
            // 处理DateTime -> DateTimeOffset 类型
            else if (obj.GetType().Equals(typeof(DateTime)) && (underlyingType ?? type).Equals(typeof(DateTimeOffset)))
            {
                return ((DateTime)obj).ConvertToDateTimeOffset();
            }
            // 处理 DateTimeOffset -> DateTime 类型
            else if (obj.GetType().Equals(typeof(DateTimeOffset)) && (underlyingType ?? type).Equals(typeof(DateTime)))
            {
                return ((DateTimeOffset)obj).ConvertToDateTime();
            }
            else if (typeof(IConvertible).IsAssignableFrom(underlyingType ?? type))
            {
                try
                {
                    return Convert.ChangeType(obj, underlyingType ?? type, null);
                }
                catch
                {
                    return underlyingType == null ? Activator.CreateInstance(type) : null;
                }
            }
            else
            {
                var converter = TypeDescriptor.GetConverter(type);
                if (converter.CanConvertFrom(obj.GetType())) return converter.ConvertFrom(obj);

                var constructor = type.GetConstructor(Type.EmptyTypes);
                if (constructor != null)
                {
                    var o = constructor.Invoke(null);
                    var propertys = type.GetProperties();
                    var oldType = obj.GetType();

                    foreach (var property in propertys)
                    {
                        var p = oldType.GetProperty(property.Name);
                        if (property.CanWrite && p != null && p.CanRead)
                        {
                            property.SetValue(o, p.GetValue(obj, null).ChangeType(property.PropertyType), null);
                        }
                    }
                    return o;
                }
            }
            return obj;
        }

        /// <summary>
        /// 将 DateTime 转换成 DateTimeOffset
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset ConvertToDateTimeOffset(this DateTime dateTime)
        {
            return DateTime.SpecifyKind(dateTime, DateTimeKind.Local);
        }
        /// <summary>
        /// 判断是否是元组类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        internal static bool IsValueTuple(this Type type)
        {
            return type.Namespace == "System" && type.Name.Contains("ValueTuple`");
        }

        /// <summary>
        /// 判断是否是富基元类型
        /// </summary>
        /// <param name="type">类型</param>
        /// <returns></returns>
        internal static bool IsRichPrimitive(this Type type)
        {
            // 处理元组类型
            if (type.IsValueTuple()) return false;

            // 处理数组类型，基元数组类型也可以是基元类型
            if (type.IsArray) return type.GetElementType().IsRichPrimitive();

            // 基元类型或值类型或字符串类型
            if (type.IsPrimitive || type.IsValueType || type == typeof(string)) return true;

            if (type.IsGenericType && type.GetGenericTypeDefinition() == typeof(Nullable<>)) return type.GenericTypeArguments[0].IsRichPrimitive();

            return false;
        }

        /// <summary>
        /// 将 DateTime? 转换成 DateTimeOffset?
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTimeOffset? ConvertToDateTimeOffset(this DateTime? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ConvertToDateTimeOffset() : DateTimeOffset.MinValue;
        }

        /// <summary>
        /// 将 DateTimeOffset 转换成本地 DateTime
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime ConvertToDateTime(this DateTimeOffset dateTime)
        {
            if (dateTime.Offset.Equals(TimeSpan.Zero))
                return dateTime.UtcDateTime;
            if (dateTime.Offset.Equals(TimeZoneInfo.Local.GetUtcOffset(dateTime.DateTime)))
                return dateTime.ToLocalTime().DateTime;
            else
                return dateTime.DateTime;
        }

        /// <summary>
        /// 将 DateTimeOffset? 转换成本地 DateTime?
        /// </summary>
        /// <param name="dateTime"></param>
        /// <returns></returns>
        public static DateTime? ConvertToDateTime(this DateTimeOffset? dateTime)
        {
            return dateTime.HasValue ? dateTime.Value.ConvertToDateTime() : DateTime.MinValue;
        }

    }
}
