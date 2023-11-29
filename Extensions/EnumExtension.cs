using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Reflection;
using System.Text;

namespace XT.Common.Extensions
{
    /// <summary>
    /// <seealso cref="Enum"/> 扩展类
    /// </summary>
    public static class EnumExtensions
    {
        /// <summary>
        /// 将枚举转为由code、desc键值对组成的集合,<para></para>
        /// 如: EnumTest{ [Description("A描述")]A, B, C}，默认转换为: <para></para>
        /// {<para></para>
        /// (0,"A描述"),<para></para>
        /// (1,"B"),<para></para>
        /// (2,"C")<para></para>
        /// }<para></para>
        /// </summary>
        /// <param name="item">枚举项</param>
        /// <param name="useNameForKeyIfNoDescription">当枚举项上不存在[Description]特性时,是否使用枚举的Key值作为描述</param>
        /// <remarks>枚举的code是数字，key是定义时的名称，desc是[Description]中的值</remarks>
        /// <returns></returns>
        public static List<(int code, string desc)> ToCodeDescriptionList(this Enum item, bool useNameForKeyIfNoDescription = true)
        {
            return item.ToCodeKeyDescriptionList(useNameForKeyIfNoDescription).Select(i => (i.code, i.desc)).ToList();
        }

        /// <summary>
        /// 将枚举转为由code、key键值对组成的集合,<para></para>
        /// 如: EnumTest{ [Description("A描述")]A, B, C}，默认转换为: <para></para>
        /// {<para></para>
        /// (0,"A"),<para></para>
        /// (1,"B"),<para></para>
        /// (2,"C")<para></para>
        /// }<para></para>
        /// </summary>
        /// <param name="item">枚举项</param>
        /// <remarks>枚举的code是数字，key是定义时的名称，desc是[Description]中的值</remarks>
        /// <returns></returns>
        public static List<(int code, string key)> ToCodeKeyList(this Enum item)
        {
            return item.ToCodeKeyDescriptionList(false).Select(i => (i.code, i.key)).ToList();
        }

        /// <summary>
        /// 将枚举转为由key、code键值对组成的集合,<para></para>
        /// 如: EnumTest{ [Description("A描述")]A, B, C}，默认转换为: <para></para>
        /// {<para></para>
        /// ("A",0),<para></para>
        /// ("B",1),<para></para>
        /// ("C",2)<para></para>
        /// }<para></para>
        /// </summary>
        /// <remarks>枚举的code是数字，key是定义时的名称，desc是[Description]中的值</remarks>
        public static List<(string key, int code)> ToKeyCodeList(this Enum item)
        {
            return item.ToCodeKeyDescriptionList(false).Select(i => (i.key, i.code)).ToList();
        }

        /// <summary>
        /// 将枚举转为由key、desc键值对组成的集合,<para></para>
        /// 如: EnumTest{ [Description("A描述")]A, B, C}，默认转换为: <para></para>
        /// {<para></para>
        /// ("A","A描述"),<para></para>
        /// ("B","B"),<para></para>
        /// ("C","C")<para></para>
        /// }<para></para>
        /// </summary>
        /// <param name="item">枚举项</param>
        /// <param name="useNameForKeyIfNoDescription">当枚举项上不存在[Description]特性时,是否使用枚举的key值作为描述</param>
        /// <remarks>枚举的code是数字，key是定义时的名称，desc是[Description]中的值</remarks>
        /// <returns></returns>
        public static List<(string key, string desc)> ToKeyDescriptionList(this Enum item, bool useNameForKeyIfNoDescription = true)
        {
            return item.ToCodeKeyDescriptionList(useNameForKeyIfNoDescription).Select(i => (i.key, i.desc)).ToList();
        }

        /// <summary>
        /// 将枚举转为由enum、desc键值对组成的集合,<para></para>
        /// 如: EnumTest{ [Description("A描述")]A, B, C}，默认转换为: <para></para>
        /// {<para></para>
        /// (A,"A描述"),<para></para>
        /// (B,"B"),<para></para>
        /// (C,"C")<para></para>
        /// }<para></para>
        /// </summary>
        /// <param name="item">枚举项</param>
        /// <param name="useNameForKeyIfNoDescription">当枚举项上不存在[Description]特性时,是否使用枚举的key值作为描述</param>
        /// <remarks>枚举的code是数字，key是定义时的名称，desc是[Description]中的值</remarks>
        /// <returns></returns>
        public static List<(T key, string desc)> ToItemDescriptionList<T>(this T item)
        {
            List<(T key, string desc)> list = new List<(T key, string desc)>();

            List<T> datas = new List<T>();

            Type enumType = item.GetType();
            string[] fieldstrs = Enum.GetNames(enumType);
            var tps = Enum.GetValues(enumType);
            foreach (var name in fieldstrs)
            {
                var fieldItem = enumType.GetField(name);
                var one = fieldItem.GetRawConstantValue().ToWithDefault(0);
                T valueData = default;
                foreach (var tp in tps)
                {
                    if (Convert.ToInt32((T)Enum.Parse(enumType, tp.ToString())) == one)
                    {
                        valueData = (T)tp;
                        break;
                    }
                }

                list.Add((
                    valueData,
                            fieldItem.GetCustomAttribute<DescriptionAttribute>()?.Description ?? fieldItem.Name)
                    );

            }

            return list;
        }


        /// <summary>
        /// 将枚举转为由code、key、desc组成元组的集合,<para></para>
        /// 如: EnumTest{ [Description("A描述")]A, B, C}，默认转换为: <para></para>
        /// {<para></para>
        /// (0,"A","A描述"),<para></para>
        /// (1,"B","B"),<para></para>
        /// (2,"C","C")<para></para>
        /// }<para></para>
        /// </summary>
        /// <param name="item">枚举项</param>
        /// <param name="useNameForKeyIfNoDescription">当枚举项上不存在[Description]特性时,是否使用枚举的key值作为描述</param>
        /// <remarks>枚举的code是数字，key是定义时的名称，desc是[Description]中的值</remarks>
        /// <returns></returns>
        public static List<(int code, string key, string desc)> ToCodeKeyDescriptionList(this Enum item, bool useNameForKeyIfNoDescription = true)
        {
            List<(int code, string key, string desc)> list = new List<(int code, string key, string desc)>();

            Type enumType = item.GetType();
            string[] fieldstrs = Enum.GetNames(enumType);
            foreach (var name in fieldstrs)
            {
                var fieldItem = enumType.GetField(name);
                list.Add((
                            fieldItem.GetRawConstantValue().ToWithDefault(0),
                            fieldItem.Name,
                            fieldItem.GetCustomAttribute<DescriptionAttribute>()?.Description ?? (useNameForKeyIfNoDescription ? fieldItem.Name : "")
                        ));
            }
            return list;
        }

        /// <summary>
        /// 将枚举转为由 code、key、desc,refer 组成元组的集合,<para></para>
        /// 如: EnumTest{ [Description("A描述")]A, [ReferenceClass(typeof(SocketsHttpHandler))]B, C}，默认转换为: <para></para>
        /// {<para></para>
        /// (0,"A","A描述",null),<para></para>
        /// (1,"B","B",typeof(SocketsHttpHandler)),<para></para>
        /// (2,"C","C",null)<para></para>
        /// }<para></para>
        /// </summary>
        /// <param name="item">枚举项</param>
        /// <param name="useNameForKeyIfNoDescription">当枚举项上不存在[Description]特性时,是否使用枚举的key值作为描述</param>
        /// <remarks>枚举的code是数字，key是定义时的名称，desc是[Description]中的值</remarks>
        /// <returns></returns>
        public static List<(int code, string key, string desc, Type refer)> ToCodeKeyDescriptionReferenceClassList(this Enum item, bool useNameForKeyIfNoDescription = true)
        {
            List<(int code, string key, string desc, Type refer)> list = new List<(int code, string key, string desc, Type refer)>();

            Type enumType = item.GetType();
            string[] fieldstrs = Enum.GetNames(enumType);
            foreach (var name in fieldstrs)
            {
                var fieldItem = enumType.GetField(name);
                list.Add((
                            fieldItem.GetRawConstantValue().ToWithDefault(0),
                            fieldItem.Name,
                            fieldItem.GetCustomAttribute<DescriptionAttribute>()?.Description ?? (useNameForKeyIfNoDescription ? fieldItem.Name : ""),
                            fieldItem.GetCustomAttribute<ReferenceClassAttribute>()?.Type
                        ));
            }
            return list;
        }

        /// <summary>
        /// 返回枚举值描述,优先从<seealso cref="DescriptionAttribute"/>特性中获取,支持位枚举
        /// </summary>
        /// <param name="item">枚举项</param>
        /// <param name="useNameForKeyIfNoDescription">当枚举项上不存在[Description]特性时,是否使用枚举的key值作为描述</param>
        /// <returns></returns>
        public static string ToDescription(this Enum item, bool useNameForKeyIfNoDescription = true)
        {
            var desc = "";
            string name = item.ToString();
            var type = item.GetType();
            if (type.CustomAttributes.FirstOrDefault(_ => _.AttributeType == typeof(FlagsAttribute)) != null)
            {
                //位枚举
                var fields = type.GetFields().ToList();
                foreach (var field in fields)
                {
                    if (field.Name == "value__") continue;
                    if (item.HasFlag((Enum)Enum.Parse(type, field.Name)))
                    {
                        var t = item.GetType().GetField(field.Name)?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? (useNameForKeyIfNoDescription ? field.Name : "");
                        if (string.IsNullOrWhiteSpace(desc)) desc = t;
                        else desc += ", " + t;
                    }
                }
            }
            else
            {
                //普通枚举
                desc = item.GetType().GetField(name)?.GetCustomAttribute<DescriptionAttribute>()?.Description ?? (useNameForKeyIfNoDescription ? name : "");
            }

            return desc ?? name;
        }

        /// <summary>
        /// 返回枚举项引用的Type
        /// <code>
        /// enum EnumTest{ [ReferenceClass(typeof(ClassA))]A,[ReferenceClass(typeof(ClassB))]B}
        /// </code>
        /// </summary>
        /// <param name="item">枚举项</param>
        /// <returns></returns>
        public static Type ToReferType(this Enum item)
        {
            Type refer = null;
            string name = item.ToString();
            var type = item.GetType();
            if (type.CustomAttributes.FirstOrDefault(_ => _.AttributeType == typeof(FlagsAttribute)) != null)
            {
                //位枚举
                throw new Exception($"复合枚举请调用 ToReferTypes() 方法!");
            }
            else
            {
                //普通枚举
                refer = item.GetType().GetField(name)?.GetCustomAttribute<ReferenceClassAttribute>()?.Type;
            }
            return refer;
        }

        /// <summary>
        /// 返回枚举项引用的Type
        /// <code>
        /// enum EnumTest{ [ReferenceClass(typeof(ClassA))]A,[ReferenceClass(typeof(ClassB))]B}
        /// </code>
        /// </summary>
        /// <param name="item">枚举项</param>
        /// <returns></returns>
        public static List<Type> ToReferTypes(this Enum item)
        {
            List<Type> refers = new List<Type>();
            string name = item.ToString();
            var type = item.GetType();
            if (type.CustomAttributes.FirstOrDefault(_ => _.AttributeType == typeof(FlagsAttribute)) != null)
            {
                //位枚举
                var fields = type.GetFields().ToList();
                foreach (var field in fields)
                {
                    if (field.Name == "value__") continue;
                    if (item.HasFlag((Enum)Enum.Parse(type, field.Name)))
                    {
                        var t = item.GetType().GetField(field.Name)?.GetCustomAttribute<ReferenceClassAttribute>()?.Type;
                        refers.Add(t);
                    }
                }
            }
            else
            {
                //普通枚举
                var t = item.GetType().GetField(name)?.GetCustomAttribute<ReferenceClassAttribute>()?.Type;
                refers.Add(t);
            }
            return refers;
        }

        /// <summary>
        /// 枚举值是否包含所有的项
        /// <list type="number">
        /// <item>
        /// 普通枚举: enum EnumTest{A,B,C}<br />
        /// <code>
        /// EnumTest.A.Contains(EnumTest.A) => true
        /// EnumTest.A.Contains(EnumTest.B) => false
        /// </code>
        /// </item>
        /// <item>
        /// 复合枚举：enum [Flags]EnumTest{A=1,B=2,C=4}<br/>
        /// <code>
        /// (EnumTest.A|EnumTest.C).Contains(EnumTest.A) => true
        /// (EnumTest.A|EnumTest.C).Contains(EnumTest.A|EnumTest.C) => true
        /// (EnumTest.A|EnumTest.C).Contains(EnumTest.B) => false
        /// (EnumTest.A|EnumTest.C).Contains(EnumTest.A|EnumTest.B) => false
        /// </code>
        /// </item>
        /// </list>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enu"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool Contains<T>(this Enum enu, T item) where T : Enum
        {
            if (enu == null) return false;
            var i = item.To<int>();
            return (enu.To<int>() & i) == i;
        }

        /// <summary>
        /// 枚举值是否包含指定的任意一项
        /// <list type="number">
        /// <item>
        /// 普通枚举: enum EnumTest{A,B,C}<br />
        /// <code>
        /// EnumTest.A.ContainsAny(EnumTest.A) => true
        /// EnumTest.A.ContainsAny(EnumTest.B) => false
        /// </code>
        /// </item>
        /// <item>
        /// 复合枚举：enum [Flags]EnumTest{A=1,B=2,C=4}<br/>
        /// <code>
        /// (EnumTest.A|EnumTest.C).ContainsAny(EnumTest.A) => true
        /// (EnumTest.A|EnumTest.C).ContainsAny(EnumTest.A|EnumTest.C) => true
        /// (EnumTest.A|EnumTest.C).ContainsAny(EnumTest.B) => false
        /// (EnumTest.A|EnumTest.C).ContainsAny(EnumTest.A|EnumTest.B) => true
        /// </code>
        /// </item>
        /// </list>
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="enu"></param>
        /// <param name="item"></param>
        /// <returns></returns>
        public static bool ContainsAny<T>(this Enum enu, T item) where T : Enum
        {
            if (enu == null) return false;
            return (item.To<int>() & enu.To<int>()) != 0;
        }

        /// <summary>
        /// 针对复合枚举,单独返回每个枚举组成的集合
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static List<int> ToIntList<T>(this T @enum) where T : Enum
        {
            var type = typeof(T);
            var list = new List<int>();
            if (type.CustomAttributes.FirstOrDefault(_ => _.AttributeType == typeof(FlagsAttribute)) != null)
            {
                //位枚举
                var fields = type.GetFields().ToList();
                foreach (var field in fields)
                {
                    if (field.Name == "value__") continue;
                    var val = (int)Enum.Parse(type, field.Name);
                    if ((val.To<int>() & @enum.To<int>()) == val) list.Add(val);
                }
            }
            else
            {
                //普通枚举
                list.Add(@enum.To<int>());
            }
            return list;
        }

        /// <summary>
        /// 针对复合枚举,单独返回每个枚举组成的集合
        /// </summary>
        /// <param name="enum"></param>
        /// <returns></returns>
        public static List<T> ToEnumList<T>(this T @enum) where T : Enum
        {
            var type = typeof(T);
            var list = new List<T>();
            if (type.CustomAttributes.FirstOrDefault(_ => _.AttributeType == typeof(FlagsAttribute)) != null)
            {
                //位枚举
                var fields = type.GetFields().ToList();
                foreach (var field in fields)
                {
                    if (field.Name == "value__") continue;
                    var tmp = Enum.Parse(type, field.Name);
                    var val = (T)tmp;
                    if ((val.To<int>() & @enum.To<int>()) == (int)tmp) list.Add(val);
                }
            }
            else
            {
                //普通枚举
                list.Add(@enum);
            }
            return list;
        }
    }

    /// <summary>
    /// 引用指定类的注解
    /// </summary>
    public class ReferenceClassAttribute : Attribute
    {
        /// <summary>
        /// 类型
        /// </summary>
        public Type Type { get; set; }

        /// <summary>
        /// 构造函数
        /// </summary>
        /// <param name="type"></param>
        public ReferenceClassAttribute(Type type)
        {
            Type = type;
        }
    }
}
