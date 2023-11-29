using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using XT.Common.Extensions;
using XT.Common.Utils;

namespace XT.Common.Extensions
{
    /// <summary>
    /// <see cref="IEnumerable{T}"/> 扩展类
    /// </summary>
    public static class EnumerableExtension
    {

        #region ToConcurrentDictionary
        /// <summary>
        /// 将当前集合对象转成并发字典(属性名->属性值)
        /// </summary>
        public static ConcurrentDictionary<TKey, TValue> ToConcurrentDictionary<T, TKey, TValue>(this IEnumerable<T> collections, Func<T, TKey> keySelector, Func<T, TValue> valueSelector)
        {
            var dic = new ConcurrentDictionary<TKey, TValue>();
            foreach (var collect in collections)
            {
                var key = keySelector(collect);
                var value = valueSelector(collect);
                dic.TryAdd(key, value);
            }
            return dic;
        }
        #endregion

        /// <summary>
        /// 简化 Skip((pageIndex-1) * pageSize).Take(pageSize)调用,页码从1开始
        /// </summary>
        public static IEnumerable<T> GetPage<T>(this IEnumerable<T> sequence, int pageIndex, int pageSize) =>
            sequence.Skip((pageIndex - 1) * pageSize).Take(pageSize);

        /// <summary>
        /// 判断集合中是否存在元素
        /// </summary>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> sequence) =>
            sequence != null && sequence.Any();

        /// <summary>
        /// 判断集合是否为空
        /// </summary>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> sequence) =>
            sequence == null || !sequence.Any();

        /// <summary>
        /// 将当前集合中的元素根据指定的分隔符(<paramref name="separator"/>)拼接成字符串
        /// </summary>
        public static string ToStringSeparated<T>(this IEnumerable<T> sequence, string separator)
        {
            EnsureUtil.NotNull(sequence, nameof(sequence));

            if (!sequence.Any()) { return string.Empty; }
            return string.Join(separator, sequence);
        }

        /// <summary>
        /// 将当前集合中的元素根据指定的分隔符(<paramref name="delimiter"/>)拼接成字符串
        /// </summary>
        public static string ToCharSeparated<T>(this IEnumerable<T> sequence, char delimiter) =>
            sequence.ToStringSeparated(delimiter.ToString());

        /// <summary>
        /// 将当前集合中的元素根据分隔符(<c>,</c>)拼接成字符串
        /// </summary>
        public static string ToCommaSeparated<T>(this IEnumerable<T> sequence) =>
            sequence.ToCharSeparated(',');

        /// <summary>
        /// 针对集合中的每个元素进行调用
        /// </summary>
        public static void ForEach<T>(this IEnumerable<T> sequence, Action<T> action)
        {
            EnsureUtil.NotNull(action, nameof(action));
            foreach (var item in sequence) { action(item); }
        }

        /// <summary>
        /// 从集合中随机选取一个元素
        /// </summary>
        public static T SelectRandom<T>(this IEnumerable<T> sequence)
        {
            var random = new Random(Guid.NewGuid().GetHashCode());
            return sequence.SelectRandom(random);
        }

        /// <summary>
        /// 根据指定的随机数生成器，从当前集合中随机选取一个元素
        /// </summary>
        public static T SelectRandom<T>(this IEnumerable<T> sequence, Random random)
        {
            if (sequence is ICollection<T> collection)
            {
                return collection.ElementAt(random.Next(collection.Count));
            }

            var count = 1;
            var selected = default(T);

            foreach (var element in sequence)
            {
                if (random.Next(count++) == 0)
                {
                    // Select the current element with 1/count probability
                    selected = element;
                }
            }
            return selected;
        }

        /// <summary>
        /// 从当前集合中随机选取所有的元素生成新的集合
        /// </summary>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> sequence)
        {
            EnsureUtil.NotNull(sequence, nameof(sequence));
            return sequence.Randomize(new Random(Guid.NewGuid().GetHashCode()));
        }

        /// <summary>
        /// 根据指定的随机数生成器，从当前集合中随机选取所有的元素生成新的集合
        /// </summary>
        public static IEnumerable<T> Randomize<T>(this IEnumerable<T> sequence, Random random)
        {
            EnsureUtil.NotNull(sequence, nameof(sequence));
            EnsureUtil.NotNull(random, nameof(random));

            var buffer = sequence.ToArray();
            for (var i = 0; i < buffer.Length; i++)
            {
                var j = random.Next(i, buffer.Length);
                yield return buffer[j];

                buffer[j] = buffer[i];
            }
        }

#if ! NET6_0_OR_GREATER
        /// <summary>
        /// 当前集合去重后生成一个新的集合, 使用<seealso cref="EqualityComparer{TKey}.Default"/>对<paramref name="selector"/>返回的值进行比较以判断是否是重复元素
        /// </summary>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> sequence, Func<T, TKey> selector) =>
            sequence.DistinctBy(selector, EqualityComparer<TKey>.Default);

        /// <summary>
        /// 当前集合去重后生成一个新的集合, 使用<paramref name="comparer"/>对<paramref name="selector"/>返回的值进行比较以判断是否是重复元素
        /// </summary>
        public static IEnumerable<T> DistinctBy<T, TKey>(this IEnumerable<T> sequence,
            Func<T, TKey> selector,
            IEqualityComparer<TKey> comparer)
        {
            var keys = new HashSet<TKey>(comparer);
            foreach (var item in sequence)
            {
                if (keys.Add(selector(item)))
                {
                    yield return item;
                }
            }
        }
#endif

        /// <summary>
        /// 将当前集合转为<seealso cref="IList{T}"/>
        /// </summary>
        public static IList<T> SpeculativeToList<T>(this IEnumerable<T> sequence) => sequence as IList<T> ?? sequence.ToList();

        /// <summary>
        /// 将当前集合转为<seealso cref="IReadOnlyList{T}"/>
        /// </summary>
        public static IReadOnlyList<T> SpeculativeToReadOnlyList<T>(this IEnumerable<T> sequence) => sequence as IReadOnlyList<T> ?? sequence.ToList();

        /// <summary>
        /// 将当前集合转为数组
        /// </summary>
        public static T[] SpeculativeToArray<T>(this IEnumerable<T> sequence) => sequence as T[] ?? sequence.ToArray();

        /// <summary>
        /// 将当前集合转为只读的<seealso cref="IEnumerable{T}"/>
        /// </summary>
        public static IEnumerable<T> ToReadOnlySequence<T>(this IEnumerable<T> sequence)
        {
            EnsureUtil.NotNull(sequence, nameof(sequence));
            return sequence is IReadOnlyList<T> ? sequence : sequence.Skip(0);
        }

        /// <summary>
        /// 将当前集合转为另一个允许遍历时处理异常的集合
        /// </summary>
        /// <returns></returns>
        public static IEnumerable<T> HandleExceptionWhenYieldReturning<T>(
            this IEnumerable<T> sequence,
            Func<Exception, bool> exceptionPredicate,
            Action<Exception> actionToExecuteOnException)
        {
            EnsureUtil.NotNull(exceptionPredicate, nameof(exceptionPredicate));
            EnsureUtil.NotNull(actionToExecuteOnException, nameof(actionToExecuteOnException));

            var enumerator = sequence.GetEnumerator();

            while (true)
            {
                T result;
                try
                {
                    if (!enumerator.MoveNext()) { break; }
                    result = enumerator.Current;
                }
                catch (Exception e)
                {
                    if (exceptionPredicate(e))
                    {
                        actionToExecuteOnException(e);
                        yield break;
                    }
                    throw;
                }
                yield return result;
            }

            enumerator.Dispose();
        }


        /// <summary>
        /// 按字段属性判等取交集
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <param name="second"></param>
        /// <param name="condition"></param>
        /// <param name="first"></param>
        /// <returns></returns>
        public static IEnumerable<TFirst> IntersectBy<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, bool> condition)
        {
            return first.Where(f => second.Any(s => condition(f, s)));
        }

        /// <summary>
        /// 按字段属性判等取交集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector)
        {
            return first.IntersectBy(second, keySelector, null);
        }

        /// <summary>
        /// 按字段属性判等取交集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (first == null)
                throw new ArgumentNullException(nameof(first));
            if (second == null)
                throw new ArgumentNullException(nameof(second));
            if (keySelector == null)
                throw new ArgumentNullException(nameof(keySelector));
            return IntersectByIterator(first, second, keySelector, comparer);
        }

        private static IEnumerable<TSource> IntersectByIterator<TSource, TKey>(IEnumerable<TSource> first, IEnumerable<TSource> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            var set = new HashSet<TKey>(second.Select(keySelector), comparer);
            foreach (var source in first)
            {
                if (set.Remove(keySelector(source)))
                    yield return source;
            }
        }

        /// <summary>
        /// 多个集合取交集元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IEnumerable<T> IntersectAll<T>(this IEnumerable<IEnumerable<T>> source)
        {
            return source.Aggregate((current, item) => current.Intersect(item));
        }

        /// <summary>
        /// 多个集合取交集元素
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> IntersectAll<TSource, TKey>(this IEnumerable<IEnumerable<TSource>> source, Func<TSource, TKey> keySelector)
        {
            return source.Aggregate((current, item) => current.IntersectBy(item, keySelector));
        }

        /// <summary>
        /// 多个集合取交集元素
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="source"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> IntersectAll<TSource, TKey>(this IEnumerable<IEnumerable<TSource>> source, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            return source.Aggregate((current, item) => current.IntersectBy(item, keySelector, comparer));
        }

        /// <summary>
        /// 多个集合取交集元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<T> IntersectAll<T>(this IEnumerable<IEnumerable<T>> source, IEqualityComparer<T> comparer)
        {
            return source.Aggregate((current, item) => current.Intersect(item, comparer));
        }

        /// <summary>
        /// 按字段属性判等取差集
        /// </summary>
        /// <typeparam name="TFirst"></typeparam>
        /// <typeparam name="TSecond"></typeparam>
        /// <param name="second"></param>
        /// <param name="condition"></param>
        /// <param name="first"></param>
        /// <returns></returns>
        public static IEnumerable<TFirst> ExceptBy<TFirst, TSecond>(this IEnumerable<TFirst> first, IEnumerable<TSecond> second, Func<TFirst, TSecond, bool> condition)
        {
            return first.Where(f => !second.Any(s => condition(f, s)));
        }




        /// <summary>
        /// 按字段属性判等取交集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector)
        {
            return first.IntersectBy(second, keySelector, null);
        }

        /// <summary>
        /// 按字段属性判等取交集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> IntersectBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            return IntersectByIterator(first, second, keySelector, comparer);
        }

        private static IEnumerable<TSource> IntersectByIterator<TSource, TKey>(IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            var set = new HashSet<TKey>(second, comparer);
            foreach (var source in first)
            {
                if (set.Remove(keySelector(source)))
                    yield return source;
            }
        }

        /// <summary>
        /// 按字段属性判等取差集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="keySelector"></param>
        /// <returns></returns>
        public static IEnumerable<TSource> ExceptBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector)
        {
            return first.ExceptBy(second, keySelector, null);
        }

        /// <summary>
        /// 按字段属性判等取差集
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TKey"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="keySelector"></param>
        /// <param name="comparer"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentNullException"></exception>
        public static IEnumerable<TSource> ExceptBy<TSource, TKey>(this IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            if (first == null) throw new ArgumentNullException(nameof(first));
            if (second == null) throw new ArgumentNullException(nameof(second));
            if (keySelector == null) throw new ArgumentNullException(nameof(keySelector));
            return ExceptByIterator(first, second, keySelector, comparer);
        }

        private static IEnumerable<TSource> ExceptByIterator<TSource, TKey>(IEnumerable<TSource> first, IEnumerable<TKey> second, Func<TSource, TKey> keySelector, IEqualityComparer<TKey> comparer)
        {
            var set = new HashSet<TKey>(second, comparer);
            foreach (var source in first)
            {
                if (set.Add(keySelector(source)))
                    yield return source;
            }
        }


        /// <summary>
        /// 添加多个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="values"></param>
        public static void AddRange<T>(this ICollection<T> @this, params T[] values)
        {
            foreach (var obj in values)
            {
                @this.Add(obj);
            }
        }

        /// <summary>
        /// 添加多个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="values"></param>
        public static void AddRange<T>(this ICollection<T> @this, IEnumerable<T> values)
        {
            foreach (var obj in values)
            {
                @this.Add(obj);
            }
        }

        /// <summary>
        /// 添加多个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="values"></param>
        public static void AddRange<T>(this ConcurrentBag<T> @this, params T[] values)
        {
            foreach (var obj in values)
            {
                @this.Add(obj);
            }
        }

        /// <summary>
        /// 添加多个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="values"></param>
        public static void AddRange<T>(this ConcurrentQueue<T> @this, params T[] values)
        {
            foreach (var obj in values)
            {
                @this.Enqueue(obj);
            }
        }

        /// <summary>
        /// 添加符合条件的多个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <param name="values"></param>
        public static void AddRangeIf<T>(this ICollection<T> @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var obj in values)
            {
                if (predicate(obj))
                {
                    @this.Add(obj);
                }
            }
        }

        /// <summary>
        /// 添加符合条件的多个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <param name="values"></param>
        public static void AddRangeIf<T>(this ConcurrentBag<T> @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var obj in values)
            {
                if (predicate(obj))
                {
                    @this.Add(obj);
                }
            }
        }

        /// <summary>
        /// 添加符合条件的多个元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="predicate"></param>
        /// <param name="values"></param>
        public static void AddRangeIf<T>(this ConcurrentQueue<T> @this, Func<T, bool> predicate, params T[] values)
        {
            foreach (var obj in values)
            {
                if (predicate(obj))
                {
                    @this.Enqueue(obj);
                }
            }
        }

        /// <summary>
        /// 添加不重复的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="values"></param>
        public static void AddRangeIfNotContains<T>(this ICollection<T> @this, params T[] values)
        {
            foreach (T obj in values)
            {
                if (!@this.Contains(obj))
                {
                    @this.Add(obj);
                }
            }
        }

        /// <summary>
        /// 移除符合条件的元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="this"></param>
        /// <param name="where"></param>
        public static void RemoveWhere<T>(this ICollection<T> @this, Func<T, bool> @where)
        {
            foreach (var obj in @this.Where(where).ToList())
            {
                @this.Remove(obj);
            }
        }

        /// <summary>
        /// 在元素之后添加元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="condition">条件</param>
        /// <param name="value">值</param>
        public static void InsertAfter<T>(this IList<T> list, Func<T, bool> condition, T value)
        {
            foreach (var item in list.Select((item, index) => new
            {
                item,
                index
            }).Where(p => condition(p.item)).OrderByDescending(p => p.index))
            {
                if (item.index + 1 == list.Count)
                {
                    list.Add(value);
                }
                else
                {
                    list.Insert(item.index + 1, value);
                }
            }
        }

        /// <summary>
        /// 在元素之后添加元素
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <param name="index">索引位置</param>
        /// <param name="value">值</param>
        public static void InsertAfter<T>(this IList<T> list, int index, T value)
        {
            foreach (var item in list.Select((v, i) => new
            {
                Value = v,
                Index = i
            }).Where(p => p.Index == index).OrderByDescending(p => p.Index))
            {
                if (item.Index + 1 == list.Count)
                {
                    list.Add(value);
                }
                else
                {
                    list.Insert(item.Index + 1, value);
                }
            }
        }

        /// <summary>
        /// 转HashSet
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static HashSet<TResult> ToHashSet<T, TResult>(this IEnumerable<T> source, Func<T, TResult> selector)
        {
            var set = new HashSet<TResult>();
            set.UnionWith(source.Select(selector));
            return set;
        }



        /// <summary>
        /// 异步foreach
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="maxParallelCount">最大并行数</param>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static async Task ForeachAsync<T>(this IEnumerable<T> source, Func<T, Task> action, int maxParallelCount, CancellationToken cancellationToken = default)
        {
            if (Debugger.IsAttached)
            {
                foreach (var item in source)
                {
                    await action(item);
                }

                return;
            }

            var list = new List<Task>();
            foreach (var item in source)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                list.Add(action(item));
                if (list.Count(t => !t.IsCompleted) >= maxParallelCount)
                {
                    await Task.WhenAny(list);
                    list.RemoveAll(t => t.IsCompleted);
                }
            }

            await Task.WhenAll(list);
        }

        /// <summary>
        /// 异步foreach
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="action"></param>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        public static Task ForeachAsync<T>(this IEnumerable<T> source, Func<T, Task> action, CancellationToken cancellationToken = default)
        {
            if (source is ICollection<T> collection)
            {
                return collection.ForeachAsync(action, collection.Count, cancellationToken);
            }

            var list = source.ToList();
            return list.ForeachAsync(action, list.Count, cancellationToken);
        }

        /// <summary>
        /// 异步Select
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Task<TResult[]> SelectAsync<T, TResult>(this IEnumerable<T> source, Func<T, Task<TResult>> selector)
        {
            return Task.WhenAll(source.Select(selector));
        }

        /// <summary>
        /// 异步Select
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static Task<TResult[]> SelectAsync<T, TResult>(this IEnumerable<T> source, Func<T, int, Task<TResult>> selector)
        {
            return Task.WhenAll(source.Select(selector));
        }

        /// <summary>
        /// 异步Select
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="maxParallelCount">最大并行数</param>
        /// <returns></returns>
        public static async Task<List<TResult>> SelectAsync<T, TResult>(this IEnumerable<T> source, Func<T, Task<TResult>> selector, int maxParallelCount)
        {
            var results = new List<TResult>();
            var tasks = new List<Task<TResult>>();
            foreach (var item in source)
            {
                var task = selector(item);
                tasks.Add(task);
                if (tasks.Count >= maxParallelCount)
                {
                    await Task.WhenAny(tasks);
                    var completedTasks = tasks.Where(t => t.IsCompleted).ToArray();
                    results.AddRange(completedTasks.Select(t => t.Result));
                    tasks.RemoveWhere(t => completedTasks.Contains(t));
                }
            }

            results.AddRange(await Task.WhenAll(tasks));
            return results;
        }

        /// <summary>
        /// 异步Select
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="maxParallelCount">最大并行数</param>
        /// <returns></returns>
        public static async Task<List<TResult>> SelectAsync<T, TResult>(this IEnumerable<T> source, Func<T, int, Task<TResult>> selector, int maxParallelCount)
        {
            var results = new List<TResult>();
            var tasks = new List<Task<TResult>>();
            int index = 0;
            foreach (var item in source)
            {
                var task = selector(item, index);
                tasks.Add(task);
                Interlocked.Add(ref index, 1);
                if (tasks.Count >= maxParallelCount)
                {
                    await Task.WhenAny(tasks);
                    var completedTasks = tasks.Where(t => t.IsCompleted).ToArray();
                    results.AddRange(completedTasks.Select(t => t.Result));
                    tasks.RemoveWhere(t => completedTasks.Contains(t));
                }
            }

            results.AddRange(await Task.WhenAll(tasks));
            return results;
        }

        /// <summary>
        /// 异步For
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="maxParallelCount">最大并行数</param>
        /// <param name="cancellationToken">取消口令</param>
        /// <returns></returns>
        public static async Task ForAsync<T>(this IEnumerable<T> source, Func<T, int, Task> selector, int maxParallelCount, CancellationToken cancellationToken = default)
        {
            int index = 0;
            if (Debugger.IsAttached)
            {
                foreach (var item in source)
                {
                    await selector(item, index);
                    index++;
                }

                return;
            }

            var list = new List<Task>();
            foreach (var item in source)
            {
                if (cancellationToken.IsCancellationRequested)
                {
                    return;
                }

                list.Add(selector(item, index));
                Interlocked.Add(ref index, 1);
                if (list.Count >= maxParallelCount)
                {
                    await Task.WhenAny(list);
                    list.RemoveAll(t => t.IsCompleted);
                }
            }

            await Task.WhenAll(list);
        }

        /// <summary>
        /// 异步For
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="cancellationToken">取消口令</param>
        /// <returns></returns>
        public static Task ForAsync<T>(this IEnumerable<T> source, Func<T, int, Task> selector, CancellationToken cancellationToken = default)
        {
            if (source is ICollection<T> collection)
            {
                return collection.ForAsync(selector, collection.Count, cancellationToken);
            }

            var list = source.ToList();
            return list.ForAsync(selector, list.Count, cancellationToken);
        }

        /// <summary>
        /// 取最大值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static TResult MaxOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector) => source.Select(selector).DefaultIfEmpty().Max();

        /// <summary>
        /// 取最大值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TResult MaxOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, TResult defaultValue) => source.Select(selector).DefaultIfEmpty(defaultValue).Max();

        /// <summary>
        /// 取最大值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TSource MaxOrDefault<TSource>(this IQueryable<TSource> source) => source.DefaultIfEmpty().Max();

        /// <summary>
        /// 取最大值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TSource MaxOrDefault<TSource>(this IQueryable<TSource> source, TSource defaultValue) => source.DefaultIfEmpty(defaultValue).Max();

        /// <summary>
        /// 取最大值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult defaultValue) => source.Select(selector).DefaultIfEmpty(defaultValue).Max();

        /// <summary>
        /// 取最大值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static TResult MaxOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) => source.Select(selector).DefaultIfEmpty().Max();

        /// <summary>
        /// 取最大值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source) => source.DefaultIfEmpty().Max();

        /// <summary>
        /// 取最大值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TSource MaxOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue) => source.DefaultIfEmpty(defaultValue).Max();

        /// <summary>
        /// 取最小值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static TResult MinOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector) => source.Select(selector).DefaultIfEmpty().Min();

        /// <summary>
        /// 取最小值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TResult MinOrDefault<TSource, TResult>(this IQueryable<TSource> source, Expression<Func<TSource, TResult>> selector, TResult defaultValue) => source.Select(selector).DefaultIfEmpty(defaultValue).Min();

        /// <summary>
        /// 取最小值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TSource MinOrDefault<TSource>(this IQueryable<TSource> source) => source.DefaultIfEmpty().Min();

        /// <summary>
        /// 取最小值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TSource MinOrDefault<TSource>(this IQueryable<TSource> source, TSource defaultValue) => source.DefaultIfEmpty(defaultValue).Min();

        /// <summary>
        /// 取最小值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <returns></returns>
        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector) => source.Select(selector).DefaultIfEmpty().Min();

        /// <summary>
        /// 取最小值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <typeparam name="TResult"></typeparam>
        /// <param name="source"></param>
        /// <param name="selector"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TResult MinOrDefault<TSource, TResult>(this IEnumerable<TSource> source, Func<TSource, TResult> selector, TResult defaultValue) => source.Select(selector).DefaultIfEmpty(defaultValue).Min();

        /// <summary>
        /// 取最小值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source) => source.DefaultIfEmpty().Min();

        /// <summary>
        /// 取最小值
        /// </summary>
        /// <typeparam name="TSource"></typeparam>
        /// <param name="source"></param>
        /// <param name="defaultValue"></param>
        /// <returns></returns>
        public static TSource MinOrDefault<TSource>(this IEnumerable<TSource> source, TSource defaultValue) => source.DefaultIfEmpty(defaultValue).Min();

        /// <summary>
        /// 标准差
        /// </summary>
        /// <param name="source"></param>
        /// <returns></returns>
        public static double StandardDeviation(this IEnumerable<double> source)
        {
            double result = 0;
            var list = source as ICollection<double> ?? source.ToList();
            int count = list.Count();
            if (count > 1)
            {
                var avg = list.Average();
                var sum = list.Sum(d => (d - avg) * (d - avg));
                result = Math.Sqrt(sum / count);
            }

            return result;
        }

        /// <summary>
        /// 随机排序
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <returns></returns>
        public static IOrderedEnumerable<T> OrderByRandom<T>(this IEnumerable<T> source)
        {
            return source.OrderBy(_ => Guid.NewGuid());
        }

        /// <summary>
        /// 序列相等
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static bool SequenceEqual<T>(this IEnumerable<T> first, IEnumerable<T> second, Func<T, T, bool> condition)
        {
            if (first is ICollection<T> source1 && second is ICollection<T> source2)
            {
                if (source1.Count != source2.Count)
                {
                    return false;
                }

                if (source1 is IList<T> list1 && source2 is IList<T> list2)
                {
                    int count = source1.Count;
                    for (int index = 0; index < count; ++index)
                    {
                        if (!condition(list1[index], list2[index]))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            using IEnumerator<T> enumerator1 = first.GetEnumerator();
            using IEnumerator<T> enumerator2 = second.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                if (!enumerator2.MoveNext() || !condition(enumerator1.Current, enumerator2.Current))
                {
                    return false;
                }
            }

            return !enumerator2.MoveNext();
        }

        /// <summary>
        /// 序列相等
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="condition"></param>
        /// <returns></returns>
        public static bool SequenceEqual<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, bool> condition)
        {
            if (first is ICollection<T1> source1 && second is ICollection<T2> source2)
            {
                if (source1.Count != source2.Count)
                {
                    return false;
                }

                if (source1 is IList<T1> list1 && source2 is IList<T2> list2)
                {
                    int count = source1.Count;
                    for (int index = 0; index < count; ++index)
                    {
                        if (!condition(list1[index], list2[index]))
                        {
                            return false;
                        }
                    }

                    return true;
                }
            }

            using IEnumerator<T1> enumerator1 = first.GetEnumerator();
            using IEnumerator<T2> enumerator2 = second.GetEnumerator();
            while (enumerator1.MoveNext())
            {
                if (!enumerator2.MoveNext() || !condition(enumerator1.Current, enumerator2.Current))
                {
                    return false;
                }
            }

            return !enumerator2.MoveNext();
        }

        /// <summary>
        /// 对比两个集合哪些是新增的、删除的、修改的
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="condition">对比因素条件</param>
        /// <returns></returns>
        public static (List<T1> adds, List<T2> remove, List<T1> updates) CompareChanges<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, bool> condition)
        {
            first ??= new List<T1>();
            second ??= new List<T2>();
            var firstSource = first as ICollection<T1> ?? first.ToList();
            var secondSource = second as ICollection<T2> ?? second.ToList();
            var add = firstSource.ExceptBy(secondSource, condition).ToList();
            var remove = secondSource.ExceptBy(firstSource, (s, f) => condition(f, s)).ToList();
            var update = firstSource.IntersectBy(secondSource, condition).ToList();
            return (add, remove, update);
        }

        /// <summary>
        /// 对比两个集合哪些是新增的、删除的、修改的
        /// </summary>
        /// <typeparam name="T1"></typeparam>
        /// <typeparam name="T2"></typeparam>
        /// <param name="first"></param>
        /// <param name="second"></param>
        /// <param name="condition">对比因素条件</param>
        /// <returns></returns>
        public static (List<T1> adds, List<T2> remove, List<(T1 first, T2 second)> updates) CompareChangesPlus<T1, T2>(this IEnumerable<T1> first, IEnumerable<T2> second, Func<T1, T2, bool> condition)
        {
            first ??= new List<T1>();
            second ??= new List<T2>();
            var firstSource = first as ICollection<T1> ?? first.ToList();
            var secondSource = second as ICollection<T2> ?? second.ToList();
            var add = firstSource.ExceptBy(secondSource, condition).ToList();
            var remove = secondSource.ExceptBy(firstSource, (s, f) => condition(f, s)).ToList();
            var updates = firstSource.IntersectBy(secondSource, condition).Select(t1 => (t1, secondSource.FirstOrDefault(t2 => condition(t1, t2)))).ToList();
            return (add, remove, updates);
        }

        /// <summary>
        /// 将集合声明为非null集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static List<T> AsNotNull<T>(this List<T> list)
        {
            return list ?? new List<T>();
        }

        /// <summary>
        /// 将集合声明为非null集合
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list"></param>
        /// <returns></returns>
        public static IEnumerable<T> AsNotNull<T>(this IEnumerable<T> list)
        {
            return list ?? new List<T>();
        }

        /// <summary>
        /// 满足条件时执行筛选条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, bool condition, Func<T, bool> where)
        {
            return condition ? source.Where(where) : source;
        }



        /// <summary>
        /// 满足条件时执行筛选条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static IEnumerable<T> WhereIf<T>(this IEnumerable<T> source, Func<bool> condition, Func<T, bool> where)
        {
            return condition() ? source.Where(where) : source;
        }

        /// <summary>
        /// 满足条件时执行筛选条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, bool condition, Expression<Func<T, bool>> where)
        {
            return condition ? source.Where(where) : source;
        }

        /// <summary>
        /// 满足条件时执行筛选条件
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source"></param>
        /// <param name="condition"></param>
        /// <param name="where"></param>
        /// <returns></returns>
        public static IQueryable<T> WhereIf<T>(this IQueryable<T> source, Func<bool> condition, Expression<Func<T, bool>> where)
        {
            return condition() ? source.Where(where) : source;
        }

        /// <summary>
        /// 改变元素的索引位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">集合</param>
        /// <param name="item">元素</param>
        /// <param name="index">索引值</param>
        /// <exception cref="ArgumentNullException"></exception>
        public static IList<T> ChangeIndex<T>(this IList<T> list, T item, int index)
        {
            if (item is null)
            {
                throw new ArgumentNullException(nameof(item));
            }

            ChangeIndexInternal(list, item, index);
            return list;
        }

        /// <summary>
        /// 改变元素的索引位置
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="list">集合</param>
        /// <param name="condition">元素定位条件</param>
        /// <param name="index">索引值</param>
        public static IList<T> ChangeIndex<T>(this IList<T> list, Func<T, bool> condition, int index)
        {
            var item = list.FirstOrDefault(condition);
            if (item != null)
            {
                ChangeIndexInternal(list, item, index);
            }
            return list;
        }

        private static void ChangeIndexInternal<T>(IList<T> list, T item, int index)
        {
            index = Math.Max(0, index);
            index = Math.Min(list.Count - 1, index);
            list.Remove(item);
            list.Insert(index, item);
        }
    }
}
