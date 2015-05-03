using System;

namespace Sample.Core.Infrastructure.Cache
{
    public interface ICacheProvider
    {
        T Get<T>(string key) where T:class;

        void Store(string key, object cachedObject, DateTime expiration);

        void Remove(string key);

        void Clear();
    }
}