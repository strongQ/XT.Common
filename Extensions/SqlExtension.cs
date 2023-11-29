using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Text.RegularExpressions;

namespace XT.Common.Extensions
{
    public static class SqlExtension
    {
        /// <summary>
        /// sql模板替换，变量@，可变参数在[]中
        /// </summary>
        /// <param name="template"></param>
        /// <param name="parameter"></param>
        /// <returns></returns>
        public static string ToRealSql(this string template, object parameter)
        {
            Dictionary<string, object> dictionary = null;
            if (parameter is Dictionary<string, object> dic)
            {
                dictionary = new Dictionary<string, object>();
                foreach (var key in dic.Keys)
                {
                    if (dic[key] != null && dic[key].ToString() != "")
                    {
                        dictionary.Add(key, dic[key]);
                    }
                }
            }
            else
            {

                dictionary = parameter.GetType().GetProperties().Where(x => x.GetValue(parameter) != null && x.GetValue(parameter).ToString() != "").ToDictionary(q => q.Name, q => q.GetValue(parameter));
            }

            if (dictionary == null)
            {
                return template;
            }
            if (template.IndexOf("@") >= 0)
            {
                MatchCollection matchCollection = new Regex("\\[(.+?)\\]").Matches(template);
                if (matchCollection.Count() > 0)
                {
                    foreach (object matchObj in matchCollection)
                    {
                        if (dictionary.Any(x => matchObj.ToString().ToUpper().Contains(x.Key.ToString().ToUpper())))
                        {



                            template = template.Replace(matchObj.ToString(), matchObj.ToString().TrimStart('[').TrimEnd(']'));


                        }
                        else
                        {
                            template = template.Replace(matchObj.ToString(), string.Empty);
                        }
                    }

                }
                //    foreach (object obj1 in matchCollection)
                //    {
                //        if (dictionary.Any(x=>x.Key.ToUpper()==obj1.ToString().ToUpper()))
                //        {
                //            string upper = obj1.ToString().ToUpper().Replace(" ", "");
                //            if (!upper.Contains("LIKE@"))
                //            {
                //                template = template.Replace(obj1.ToString(), obj1.ToString().TrimStart('[').TrimEnd(']'));
                //            }
                //            else
                //            {
                //                IEnumerable<KeyValuePair<string, object>> enumerable3 = Enumerable.Where<KeyValuePair<string, object>>(dictionary, new Func<KeyValuePair<string, object>, bool>((KeyValuePair<string, obj> x) => obj1.ToString().ToUpper().Contains(x.Key.ToUpper())));
                //                upper = obj1.ToString().ToUpper();
                //                foreach (KeyValuePair<string, object> keyValuePair in enumerable3)
                //                {
                //                    upper = upper.ToString().ToUpper().Replace(keyValuePair.Key.ToUpper(), String.Format("'%{0}%'", keyValuePair.Value));
                //                }
                //                template = template.Replace(obj1.ToString(), upper.ToString().TrimStart('[').TrimEnd(']'));
                //            }
                //        }
                //        else
                //        {
                //            template = template.Replace(obj1.ToString(), String.Empty);
                //        }
                //    }
                //}

            }

            return template;
        }

        /// <summary>
        /// sql模板解析变量
        /// </summary>
        /// <param name="template"></param>
        /// <returns></returns>
        public static List<string> ToSqlParameters(this string template)
        {
            List<string> parameters = new List<string>();

            MatchCollection matchCollection = new Regex("\\@(.+?)\\]").Matches(template);

            if (matchCollection.Count() > 0)
            {
                return matchCollection.Select(x => x.Value.Replace("@", "").Replace("]", "")).ToList();
            }
            else
            {
                return new List<string>();
            }

        }
    }

}
