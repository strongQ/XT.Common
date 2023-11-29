using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml.Linq;
using XT.Common.Utils;

namespace XT.Common.Extensions
{
    /// <summary>
    /// <see cref="string"/> 的扩展类。
    /// </summary>
    public static class StringExtensions
    {
        /*
         * window下 Path.GetInvalidFileNameChars() 返回41个无效字符，Path.GetInvalidPathChars() 返回36个无效字符
         * linux下  Path.GetInvalidFileNameChars() 返回2个无效字符，Path.GetInvalidPathChars() 返回1个无效字符
         * 所以，为了文件和路径命名更加安全，统一文件命名采用window下的41个无效字符、路径命名采用window下的36个无效字符!
         */

        private static readonly char[] InvalidFileNameCharacters = new char[41] { '"', '<', '>', '|', '\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d', '\u001e', '\u001f', ':', '*', '?', '\\', '/' };
        private static readonly char[] InvalidPathCharacters = new char[36] { '"', '<', '>', '|', '\0', '\u0001', '\u0002', '\u0003', '\u0004', '\u0005', '\u0006', '\a', '\b', '\t', '\n', '\v', '\f', '\r', '\u000e', '\u000f', '\u0010', '\u0011', '\u0012', '\u0013', '\u0014', '\u0015', '\u0016', '\u0017', '\u0018', '\u0019', '\u001a', '\u001b', '\u001c', '\u001d', '\u001e', '\u001f' };


        public static List<string> SplitCsv(this string csvList, bool nullOrWhitespaceInputReturnsNull = false)
        {
            if (string.IsNullOrWhiteSpace(csvList))
                return nullOrWhitespaceInputReturnsNull ? null : new List<string>();

            return csvList
                .TrimEnd(',')
                .Split(',')
                .AsEnumerable()
                .Select(s => s.Trim())
                .ToList();
        }
        /// <summary>
        /// string.Format 的简化方法
        /// <example>
        /// <para>示例：</para>
        /// <code>
        /// "{0} is {1}".Format("He","Tom");
        /// //Output: "He is Tom"
        /// </code>
        /// </example>
        /// </summary>        
        /// <param name="value"></param>
        /// <param name="paras"><seealso cref="string.Format(string, object[])"/>的格式化参数</param>
        /// <returns></returns>
        public static string Format(this string value, params string[] paras)
        {
            if (value is null) return value;
            return string.Format(value, paras);
        }

        /// <summary>
        /// string.IsNullOrEmpty 的简化方法
        /// </summary>
        public static bool IsNullOrEmpty(this string value) => string.IsNullOrEmpty(value);

        /// <summary>
        /// string.IsNullOrEmpty 的简化方法(逆向)
        /// </summary>
        public static bool IsNotNullOrEmpty(this string value) => !value.IsNullOrEmpty();

        /// <summary>
        /// string.IsNullOrWhiteSpace 的简化方法
        /// </summary>
        public static bool IsNullOrEmptyOrWhiteSpace(this string value) => string.IsNullOrWhiteSpace(value);

        /// <summary>
        /// string.IsNullOrWhiteSpace 的简化方法(逆向)
        /// </summary>
        /// <param name="value">The string to test.</param>
        /// <returns>
        /// <see langword="true"/> 如果指定的参数不为null或空字符串（""）；除此以外， <see langword="false"/>.
        /// </returns>
        public static bool IsNotNullOrEmptyOrWhiteSpace(this string value)
            => !value.IsNullOrEmptyOrWhiteSpace();

        /// <summary>
        /// 将字符串解析为布尔值，有效输入为： <c>true|false|yes|no|1|0</c>。
        /// </summary>
        /// <remarks>输入被解析为不区分大小写。</remarks>
        public static bool TryParseAsBool(this string value, out bool result)
        {
            EnsureUtil.NotNull(value, nameof(value));

            const StringComparison CompPolicy = StringComparison.OrdinalIgnoreCase;

            if (value.Equals("true", CompPolicy)
                || value.Equals("yes", CompPolicy)
                || value.Equals("1", CompPolicy))
            {
                result = true;
                return true;
            }

            if (value.Equals("false", CompPolicy)
                || value.Equals("no", CompPolicy)
                || value.Equals("0", CompPolicy))
            {
                result = false;
                return true;
            }

            result = false;
            return false;
        }

        /// <summary>
        /// 尝试从 <paramref name="tagName"/> 提取标签 <paramref name="input"/> 之间的值。
        /// <para>示例：</para>
        /// </summary>
        /// <remarks>此方法不区分大小写。</remarks>
        /// <param name="input">输入字符串。</param>
        /// <param name="tagName">标签的值将被返回，例如: <c>span, img</c>.</param>
        /// <param name="value">提取的值。</param>
        /// <returns><c>True</c> 如果成功，否则 <c>False</c>。</returns>
        public static bool TryExtractValueFromTag(this string input, string tagName, out string value)
        {
            EnsureUtil.NotNull(input, nameof(input));
            EnsureUtil.NotNull(tagName, nameof(tagName));

            var pattern = $"<{tagName}[^>]*>(.*)</{tagName}>";
            var match = Regex.Match(input, pattern, RegexOptions.IgnoreCase);

            if (match.Success)
            {
                value = match.Groups[1].ToString();
                return true;
            }

            value = null;
            return false;
        }

        /// <summary>
        /// 允许在<see langword="null" />合并操作中使用字符串。
        /// </summary>
        /// <param name="value">要检查的字符串值。</param>
        /// <returns>
        /// 如果 <paramref name="value" /> 为空或原始的 <paramref name="value" />，则为null。
        /// </returns>
        public static string NullIfEmpty(this string value) => value == string.Empty ? null : value;

        /// <summary>
        /// 返回一个字符串数组，其中包含此 <paramref name="value"/> 中的修剪后的子字符串。
        /// 由提供的 <paramref name="separators"/> 分隔符。
        /// </summary>
        public static string[] SplitAndTrim(this string value, params char[] separators)
        {
            EnsureUtil.NotNull(value, nameof(value));
            return value.Trim()
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray();
        }

        /// <summary>
        /// 返回一个字符串数组，其中包含此 <paramref name="value"/> 中的修剪后的子字符串。
        /// 由提供的 <paramref name="separators"/> 分隔符。
        /// </summary>
        public static string[] SplitAndTrim(this string value, params string[] separators)
        {
            EnsureUtil.NotNull(value, nameof(value));
            return value.Trim()
                .Split(separators, StringSplitOptions.RemoveEmptyEntries)
                .Select(s => s.Trim())
                .ToArray();
        }

        /// <summary>
        /// 应用举例: "1,2,4".SplitAndTrimTo&lt;int&gt;(",") => {1,2,4}
        /// </summary>
        /// <remarks>
        /// 注意: <br/>当字符串为null时,返回空集合,不抛异常；<br />
        /// 当转换失败后报异常,如: "1,2,ok,4".SplitAndTrimTo&lt;int&gt;(",")抛出异常（DotNetCommon 2.7.1修改，DotNetCommon.Core 1.7.1修改）
        /// </remarks>
        public static List<T> SplitAndTrimTo<T>(this string value, params string[] separators)
        {
            if (value == null) return new List<T>();
            EnsureUtil.NotNull(value, nameof(value));
            var arr = value.SplitAndTrim(separators);
            var res = new List<T>();

            foreach (var item in arr)
            {
                res.Add(item.To<T>());
            }

            return res;
        }

        /// <summary>
        /// 检查 <paramref name="input"/> 是否包含 <paramref name="stringSegement"/> 字符串
        /// </summary>
        public static bool Contains(this string input, string stringSegement, StringComparison comparison = StringComparison.OrdinalIgnoreCase) =>
            input.IndexOf(stringSegement, comparison) >= 0;

        /// <summary>
        /// 检查给定的 <paramref name="input"/> 是否匹配任何潜在的匹配项。
        /// </summary>
        public static bool EqualsAnyIgnoreCase(this string input, params string[] stringSegement) => stringSegement.Any(x => StringComparer.OrdinalIgnoreCase.Equals(x, input));

        /// <summary>
        /// 检查给定的 <paramref name="input"/> 是否匹配任何潜在的匹配项。
        /// </summary>
        public static bool EqualsAny(this string input, params string[] stringSegement) => stringSegement.Any(x => StringComparer.Ordinal.Equals(x, input));

        /// <summary>
        /// 将<paramref name="input"/> 截断为 <paramref name="maxLength"/> 的最大长度
        /// 并将截断的部分替换为 <paramref name="suffix"/>
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="maxLength">截断前要保留的字符的总长度。</param>
        /// <param name="suffix">要添加到截断的 <paramref name="input"/> 末尾的后缀</param>
        public static string Truncate(this string input, int maxLength, string suffix = "")
        {
            EnsureUtil.NotNull(input, nameof(input));
            EnsureUtil.NotNull(suffix, nameof(suffix));

            if (maxLength < 0) { return input; }
            if (maxLength == 0) { return string.Empty; }

            var chars = input.Take(maxLength).ToArray();

            if (chars.Length != input.Length)
            {
                return new string(chars) + suffix;
            }

            return new string(chars);
        }

        /// <summary>
        /// 从给定的字符串中删除不同类型的换行符。
        /// </summary>
        /// <param name="input">输入字符串。</param>
        /// <returns>给定的输入去除任何换行符。</returns>
        public static string RemoveNewLines(this string input)
        {
            EnsureUtil.NotNull(input, nameof(input));
            return input.Replace("\n", string.Empty).Replace("\r", string.Empty);
        }

        /// <summary>
        /// 分隔PascalCase字符串。
        /// </summary>
        /// <example> "ThisIsPascalCase".SeparatePascalCase(); // returns "This Is Pascal Case" </example>
        /// <param name="value">分割格式</param>
        /// <returns>原始字符串在每个大写字符上分开。</returns>
        public static string SeparatePascalCase(this string value)
        {
            EnsureUtil.NotNullOrEmptyOrWhiteSpace(value);
            return Regex.Replace(value, "([A-Z])", " $1").Trim();
        }

        /// <summary>
        /// 将字符串转换为Pascal大小写
        /// </summary>
        /// <param name="input">给定的输入。</param>
        /// <returns>给定的 <paramref name="input"/> 转换为 Pascal Case.</returns>
        public static string ToPascalCase(this string input)
        {
            EnsureUtil.NotNull(input, nameof(input));

            var cultureInfo = Thread.CurrentThread.CurrentCulture;
            var textInfo = cultureInfo.TextInfo;
            return textInfo.ToTitleCase(input);
        }

        /// <summary>
        /// 将 <paramref name="input"/> 与 <paramref name="target"/> 进行比较, 
        /// 比较是区分大小写的。
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="target">目标字符串</param>
        public static bool IsEqualTo(this string input, string target)
        {
            if (input is null && target is null) { return true; }
            if (input is null || target is null) { return false; }
            if (input.Length != target.Length) { return false; }

            return string.CompareOrdinal(input, target) == 0;
        }

        /// <summary>
        /// 方便的方法将参数打印到 <c>System.Console</c>.
        /// </summary>
        /// <param name="input">输入字符串</param>
        /// <param name="args">参数</param>
        public static void Print(this string input, params object[] args) => Console.WriteLine(input, args);

        /// <summary>
        /// 一种将英文数字转换为中文数字的方法。
        /// </summary>
        public static string ToChineseNumber(this string input)
        {
            EnsureUtil.NotNull(input, nameof(input));
            return input
                .Replace("0", "零")
                .Replace("1", "一")
                .Replace("2", "二")
                .Replace("3", "三")
                .Replace("4", "四")
                .Replace("5", "五")
                .Replace("6", "六")
                .Replace("7", "七")
                .Replace("8", "八")
                .Replace("9", "九");
        }

        /// <summary>
        /// 将给定的 <paramref name="input"/> 压缩为 <c>Base64</c> 字符串。
        /// </summary>
        /// <param name="input">要压缩的字符串</param>
        /// <returns><c>Base64</c> 中的压缩字符串</returns>
        public static string Compress(this string input)
        {
            EnsureUtil.NotNull(input, nameof(input));

            var buffer = Encoding.UTF8.GetBytes(input);
            using var memStream = new MemoryStream();
            using var zipStream = new GZipStream(memStream, CompressionMode.Compress, true);

            zipStream.Write(buffer, 0, buffer.Length);
            zipStream.Close();

            memStream.Position = 0;

            var compressedData = new byte[memStream.Length];
            memStream.Read(compressedData, 0, compressedData.Length);

            var gZipBuffer = new byte[compressedData.Length + 4];
            Buffer.BlockCopy(compressedData, 0, gZipBuffer, 4, compressedData.Length);
            Buffer.BlockCopy(BitConverter.GetBytes(buffer.Length), 0, gZipBuffer, 0, 4);
            return Convert.ToBase64String(gZipBuffer);
        }

        /// <summary>
        /// 解压缩 <c>Base64</c> 压缩字符串。
        /// </summary>
        /// <param name="compressedInput"> 使用 <c>Base64</c> 压缩的字符串</param>
        /// <returns>未压缩的字符串</returns>
        public static string Decompress(this string compressedInput)
        {
            EnsureUtil.NotNull(compressedInput, nameof(compressedInput));

            var gZipBuffer = Convert.FromBase64String(compressedInput);
            using var memStream = new MemoryStream();

            var dataLength = BitConverter.ToInt32(gZipBuffer, 0);
            memStream.Write(gZipBuffer, 4, gZipBuffer.Length - 4);
            memStream.Position = 0;

            var buffer = new byte[dataLength];
            using (var zipStream = new GZipStream(memStream, CompressionMode.Decompress))
            {
                zipStream.Read(buffer, 0, buffer.Length);
            }

            return Encoding.UTF8.GetString(buffer);
        }

        /// <summary>
        /// 确保给定的 <paramref name="fileName"/> 可以用作文件名。
        /// </summary>
        /// <remarks>
        /// 注意：这里统一使用window平台下41个无效的字符判断，其中包含(/\:*?"&lt;>|)
        /// </remarks>
        public static bool IsValidFileName(this string fileName) =>
            fileName.IsNotNullOrEmptyOrWhiteSpace() && fileName.IndexOfAny(InvalidFileNameCharacters) == -1;

        /// <summary>
        /// 确保给定的 <paramref name="path"/> 可以用作路径
        /// </summary>
        /// <remarks>
        /// 注意: 是文件夹路径,不是url路径 <br />
        /// 这里统一使用window平台下36个无效的字符判断
        /// </remarks>
        public static bool IsValidPathName(this string path) =>
            path.IsNotNullOrEmptyOrWhiteSpace() && path.IndexOfAny(InvalidPathCharacters) == -1;

        /// <summary>
        /// 返回<see cref="Guid"/> 使用 <c>Base64</c> 编码的 <paramref name="input"/>.
        /// </summary>
        /// <remarks>
        /// 参照: <see href="https://blog.codinghorror.com/equipping-our-ascii-armor/"/>
        /// </remarks>
        public static Guid ToGuid(this string input, bool trimmed = true) =>
            trimmed ? new Guid(Convert.FromBase64String(input + "=="))
                : new Guid(Convert.FromBase64String(input));

        /// <summary>
        /// 返回出现的所有开始和结束索引
        /// 给定 <paramref name="startTag"/> 和 <paramref name="endTag"/> 
        /// 在给定的 <paramref name="input"/>。
        /// </summary>
        /// <param name="input">要搜索的输入。</param>
        /// <param name="startTag">起始标签 e.g. <c>&lt;div></c>.</param>
        /// <param name="endTag">结束标签 e.g. <c>&lt;/div></c>.</param>
        /// <returns>
        /// 一个序列 <see cref="KeyValuePair{TKey,TValue}"/>，其中键是开始位置，值是结束位置。
        /// </returns>
        public static IEnumerable<KeyValuePair<int, int>> GetStartAndEndIndexes(this string input, string startTag, string endTag)
        {
            var startIdx = 0;
            int endIdx;

            while ((startIdx = input.IndexOf(startTag, startIdx, StringComparison.Ordinal)) != -1
                && (endIdx = input.IndexOf(endTag, startIdx, StringComparison.Ordinal)) != -1)
            {
                var result = new KeyValuePair<int, int>(startIdx, endIdx);
                startIdx = endIdx;
                yield return result;
            }
        }

        /// <summary>
        /// 返回给定的 <paramref name="input"/> 编码的大小。
        /// 作为 <c>UTF-16</c> 字符（以字节为单位）。
        /// </summary>
        public static int GetSize(this string input) => input.Length * sizeof(char);

        ///// <summary>
        ///// 使用json的反序列化将字符串转为对象
        ///// </summary>
        ///// <typeparam name="T"></typeparam>
        ///// <param name="input">json字符串</param>
        ///// <param name="ignoreComment">是否忽略json中的注释</param>
        ///// <returns></returns>
        //public static T ToObject<T>(this string input, bool ignoreComment = false)
        //{
        //    if (ignoreComment && typeof(T) == typeof(JObject))
        //    {
        //        var res = JObject.Parse(input, new JsonLoadSettings
        //        {
        //            CommentHandling = CommentHandling.Ignore
        //        }).To<T>();
        //        return res;
        //    }
        //    if (ignoreComment && typeof(T) == typeof(JArray))
        //    {
        //        var res = JArray.Parse(input, new JsonLoadSettings
        //        {
        //            CommentHandling = CommentHandling.Ignore
        //        }).To<T>();
        //        return res;
        //    }
        //    return Newtonsoft.Json.JsonConvert.DeserializeObject<T>(input);
        //}

        /// <summary>
        /// 将当前字符串重复指定次数
        /// </summary>
        /// <param name="str"></param>
        /// <param name="count">指定次数</param>
        /// <returns></returns>
        public static string Repeat(this string str, int count)
        {
            if (count <= 1) return str;
            var sb = new StringBuilder();
            for (var i = 0; i < count; i++)
            {
                sb.Append(str);
            }
            return sb.ToString();
        }



        /// <summary>
        /// Base64加密
        /// 注:默认采用UTF8编码
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <returns>加密后的字符串</returns>
        public static string Base64Encode(this string source)
        {
            return source.Base64Encode(Encoding.UTF8);
        }

        /// <summary>
        /// Base64加密
        /// </summary>
        /// <param name="source">待加密的明文</param>
        /// <param name="encoding">加密采用的编码方式</param>
        /// <returns></returns>
        public static string Base64Encode(this string source, Encoding encoding)
        {
            string encode;
            byte[] bytes = encoding.GetBytes(source);
            try
            {
                encode = Convert.ToBase64String(bytes);
            }
            catch
            {
                encode = source;
            }

            return encode;
        }

        /// <summary>
        /// Base64解密
        /// 注:默认使用UTF8编码
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(this string result)
        {
            return result.Base64Decode(Encoding.UTF8);
        }

        /// <summary>
        /// Base64解密
        /// </summary>
        /// <param name="result">待解密的密文</param>
        /// <param name="encoding">解密采用的编码方式，注意和加密时采用的方式一致</param>
        /// <returns>解密后的字符串</returns>
        public static string Base64Decode(this string result, Encoding encoding)
        {
            string decode;
            byte[] bytes = Convert.FromBase64String(result);
            try
            {
                decode = encoding.GetString(bytes);
            }
            catch
            {
                decode = result;
            }

            return decode;
        }
        /// <summary>
        /// 截取字符串
        /// </summary>
        /// <param name="str"></param>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static IEnumerable<string> SplitByLength(this string str, int maxLength)
        {
            return Enumerable.Range(0, str.Length / maxLength)
                .Select(i => str.Substring(i * maxLength, Math.Min(maxLength, str.Length - i * maxLength)));
        }

        /// <summary>
        /// 判断是否为Null或者空
        /// </summary>
        /// <param name="obj">对象</param>
        /// <returns></returns>
        public static bool IsNullOrEmpty(this object obj)
        {
            if (obj == null)
                return true;
            string objStr = obj.ToString();
            return string.IsNullOrEmpty(objStr);
        }


        /// <summary>
        /// 不等于NULL？
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static bool IsNotNull(this object obj)
        {
            return obj != null;
        }
    }
}
