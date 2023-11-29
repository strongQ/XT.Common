using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.Json;
using System.Xml.Linq;

namespace XT.Common.Extensions
{
    public static class DictionaryExtensions
    {


        /// <summary>
        /// 将对象转字典类型，其中值返回原始类型 Type 类型
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static IDictionary<string, Tuple<Type, object>> ToDictionaryWithType(this object input)
        {
            if (input == null) return default;

            // 处理本就是字典类型
            if (input.GetType().HasImplementedRawGeneric(typeof(IDictionary<,>)))
            {
                var dicInput = (IDictionary)input;

                var dic = new Dictionary<string, Tuple<Type, object>>();
                foreach (var key in dicInput.Keys)
                {
                    var value = dicInput[key];
                    var tupleValue = value == null ?
                        new Tuple<Type, object>(typeof(object), value) :
                        new Tuple<Type, object>(value.GetType(), value);

                    dic.Add(key.ToString(), tupleValue);
                }

                return dic;
            }

            var dict = new Dictionary<string, Tuple<Type, object>>();

            // 获取所有属性列表
            foreach (var property in input.GetType().GetProperties())
            {
                dict.Add(property.Name, new Tuple<Type, object>(property.PropertyType, property.GetValue(input, null)));
            }

            // 获取所有成员列表
            foreach (var field in input.GetType().GetFields())
            {
                dict.Add(field.Name, new Tuple<Type, object>(field.FieldType, field.GetValue(input)));
            }

            return dict;
        }

        /// <summary>
        /// 获取成员值
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="member"></param>
        /// <returns></returns>
        private static object GetValue(object obj, MemberInfo member)
        {
            if (member is PropertyInfo info)
                return info.GetValue(obj, null);

            if (member is FieldInfo info1)
                return info1.GetValue(obj);

            throw new ArgumentException("Passed member is neither a PropertyInfo nor a FieldInfo.");
        }
    }
}
