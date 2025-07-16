using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XT.Common.Extensions
{
    // 模拟本地存储服务
    public static class LocalStorage
    {
        private static readonly ConcurrentDictionary<string, string> _storage = new ConcurrentDictionary<string, string>();

        public static void Set(string key, string value)
        {
            if (_storage.ContainsKey(key))
            {
                _storage[key] = value;
            }
            else
            {
                _storage.TryAdd(key, value);
            }
        }

        public static string? Get(string key)
        {
            return _storage.TryGetValue(key, out var value) ? value : null;
        }
    }
}
