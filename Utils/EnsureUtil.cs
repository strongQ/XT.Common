using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using XT.Common.Extensions;

namespace XT.Common.Utils
{
    /// <summary>
    /// 当条件不满足时将抛出 <see langword="throw"/> 异常的Helper类。
    /// </summary>
    /// <remarks>常用在单元测试或参数合法性校验中</remarks>
    public static class EnsureUtil
    {
        /// <summary>
        /// 确保给定表达式为 <see langword="true"/>.
        /// </summary>
        /// <typeparam name="TException">引发的异常类型</typeparam>
        /// <param name="condition">测试/确保条件</param>
        /// <param name="message">异常消息</param>
        /// <exception>
        ///     当<cref>TException</cref> <paramref name="condition"/> is <see langword="false"/>时抛出。
        /// </exception>
        public static void That<TException>(bool condition, string message = "给定条件为false。") where TException : Exception
        {
            if (!condition) { throw (TException)Activator.CreateInstance(typeof(TException), message); }
        }

        /// <summary>
        /// 确保给定<paramref name="condition"/>是<see langword="true"/>。
        /// </summary>
        /// <param name="condition">测试条件</param>
        /// <param name="message">异常消息</param>
        /// <exception cref="ArgumentException">
        ///     当<paramref name="condition"/>为<see langword="false"/>时抛出。
        /// </exception>
        public static void That(bool condition, string message = "给定条件为false。") =>
            That<ArgumentException>(condition, message);

        /// <summary>
        /// 确保给定<paramref name="condition"/>是<see langword="false"/>。
        /// </summary>
        /// <typeparam name="TException">引发的异常类型</typeparam>
        /// <param name="condition">测试条件</param>
        /// <param name="message">异常消息</param>
        /// <exception> 
        ///     当<paramref name="condition"/>为<see langword="false"/>时抛出。
        /// </exception>
        public static void Not<TException>(bool condition, string message = "给定条件为true。") where TException : Exception =>
            That<TException>(!condition, message);

        /// <summary>
        /// 确保给定<paramref name="condition"/>是<see langword="false"/>。
        /// </summary>
        /// <param name="condition">测试条件</param>
        /// <param name="message">异常消息</param>
        /// <exception cref="ArgumentException">
        ///     当<paramref name="condition"/>为<see langword="false"/>时抛出。
        /// </exception>
        public static void Not(bool condition, string message = "给定条件为true。") =>
            Not<ArgumentException>(condition, message);

        /// <summary>
        /// 确保给定<see langword="object"/>不为空。
        /// </summary>
        /// <typeparam name="T">给定的类型 <see langword="object"/> 。</typeparam>
        /// <param name="value"> <see langword="object"/>的值，以检查<see langword="null"/>引用。</param>
        /// <param name="argName">参数名称。</param>
        /// <exception cref="ArgumentNullException">
        ///     当<paramref name="value"/>为空时抛出
        /// </exception>
        /// <returns><typeparamref name="T"/>。</returns>
        public static T NotNull<T>(T value, string argName) where T : class
        {
            if (argName.IsNullOrEmptyOrWhiteSpace()) { argName = "Invalid"; }

            That<ArgumentNullException>(value != null, argName);
            return value;
        }

        /// <summary>
        /// 确保给定对象相等。
        /// </summary>
        /// <typeparam name="T">要比较的对象类型</typeparam>
        /// <param name="left">左边的项</param>
        /// <param name="right">右边的项</param>
        /// <param name="message">异常消息</param>
        /// <exception cref="ArgumentException">
        ///     当<paramref name="left"/>不等于<paramref name="right"/>时抛出
        /// </exception>
        /// <remarks>空值将导致抛出异常</remarks>
        public static void Equal<T>(T left, T right, string message = "值必须相等。") =>
            That<ArgumentException>(Comparer<T>.Default.Compare(left, right) == 0, message);

        /// <summary>
        /// 确保给定的对象不相等。
        /// </summary>
        /// <typeparam name="T">要比较的对象类型</typeparam>
        /// <param name="left">左边的项</param>
        /// <param name="right">右边的项</param>
        /// <param name="message">异常消息</param>
        /// <exception cref="ArgumentException">
        ///     当<paramref name="left"/>等于<paramref name="right"/>时抛出
        /// </exception>
        /// <remarks>空值将导致抛出异常</remarks>
        public static void NotEqual<T>(T left, T right, string message = "值不能相等。") =>
            That<ArgumentException>(Comparer<T>.Default.Compare(left, right) != 0, message);

        /// <summary>
        /// 确保给定的<paramref name="collection"/>不为null或空。
        /// </summary>
        /// <typeparam name="T">集合类型。</typeparam>
        /// <param name="collection">检查集合。</param>
        /// <param name="message">异常消息</param>
        /// <returns>评估的集合。</returns>
        /// <exception cref="ArgumentNullException">
        ///     当<paramref name="collection"/>为null时抛出。
        /// </exception>
        /// <exception cref="ArgumentException">
        ///     当<paramref name="collection"/>为空时抛出。
        /// </exception>
        public static ICollection<T> NotNullOrEmpty<T>(ICollection<T> collection, string message = "Collection must not be null or empty.")
        {
            NotNull(collection, nameof(collection));
            Not<ArgumentException>(!collection.Any(), message);
            return collection;
        }

        /// <summary>
        /// 确保给定的字符串不是<see langword="null"/>或为空或空白。
        /// </summary>
        /// <param name="value"><c>字符串</c> <paramref name="value"/>检查。</param>
        /// <param name="message">异常消息</param>
        /// <returns>如果不为null，空白或空白，则返回的值。</returns>
        /// <exception cref="ArgumentException">
        ///     当<paramref name="value"/>为null，空白或空白时抛出。
        /// </exception>
        public static string NotNullOrEmptyOrWhiteSpace(string value, string message = "String must not be null, empty or whitespace.")
        {
            That<ArgumentException>(value.IsNotNullOrEmptyOrWhiteSpace(), message);
            return value;
        }

        /// <summary>
        /// 确保给定<see cref="DirectoryInfo"/>存在。
        /// </summary>
        /// <param name="directoryInfo">表示要检查是否存在的目录的DirectoryInfo对象。</param>
        /// <returns>如果目录存在，则返回DirectoryInfo。</returns>
        /// <exception cref="ArgumentNullException">
        ///     当<paramref name="directoryInfo"/>为空时抛出。
        /// </exception>
        /// <exception cref="DirectoryNotFoundException">
        ///     当<paramref name="directoryInfo"/>未找到时抛出。
        /// </exception>
        /// <exception cref="IOException">
        ///     磁盘驱动器等设备尚未准备就绪。
        /// </exception>
        public static DirectoryInfo Exists(DirectoryInfo directoryInfo)
        {
            NotNull(directoryInfo, nameof(directoryInfo));

            directoryInfo.Refresh();
            That<DirectoryNotFoundException>(directoryInfo.Exists, $"找不到：'{directoryInfo.FullName}'.");
            return directoryInfo;
        }

        /// <summary>
        /// 确保给定<paramref name="fileInfo"/>存在。
        /// </summary>
        /// <param name="fileInfo">代表要检查是否存在的文件的FileInfo对象。</param>
        /// <returns>如果文件存在，则返回FileInfo。</returns>
        /// <exception cref="ArgumentNullException">
        ///     Thrown when <paramref name="fileInfo"/> is null.
        /// </exception>
        /// <exception cref="FileNotFoundException">
        ///     当<paramref name="fileInfo"/>不存在时抛出。
        /// </exception>
        public static FileInfo Exists(FileInfo fileInfo)
        {
            NotNull(fileInfo, nameof(fileInfo));

            fileInfo.Refresh();
            That<FileNotFoundException>(fileInfo.Exists, $"找不到：'{fileInfo.FullName}'.");
            return fileInfo;
        }
    }
}
