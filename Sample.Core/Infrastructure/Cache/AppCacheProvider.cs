using System;
using System.Collections.Generic;
using System.Runtime.Caching;

namespace Sample.Core.Infrastructure.Cache
{
    public class AppCacheProvider : ICacheProvider
    {
        private readonly List<string> _records = new List<string>();

        public T Get<T>(string key) where T : class
        {
            return MemoryCache.Default.Get(key) as T;
        }

        public void Store(string key, object cachedObject, DateTime expiration)
        {
            if (!_records.Contains(key))
                _records.Add(key);
            MemoryCache.Default.Add(key, cachedObject, expiration);
        }

        public void Remove(string key)
        {
            if (_records.Contains(key))
                _records.Remove(key);
            MemoryCache.Default.Remove(key);
        }

        public void Clear()
        {
            _records.ForEach(x => MemoryCache.Default.Remove(x));
            _records.Clear();
        }
    }
}