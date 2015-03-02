using System;
using System.Configuration;

using Enyim.Caching;
using Enyim.Caching.Configuration;
using Enyim.Caching.Memcached;

namespace TestConsoleApp.Memcached
{
    public class Runner : IRunner
    {
        public void RunProgram()
        {
            const string key = "testkey";
            var cacheClient = new MemcachedClient(ConfigurationManager.GetSection("enyim.com/memcached") as IMemcachedClientConfiguration);
            cacheClient.Store(StoreMode.Add, key, "testvalue");
            var item = cacheClient.Get(key);
            Console.WriteLine(item);

            cacheClient.Store(StoreMode.Set, key, "something else");
            item = cacheClient.Get(key);
            Console.WriteLine(item);
        }
    }
}
