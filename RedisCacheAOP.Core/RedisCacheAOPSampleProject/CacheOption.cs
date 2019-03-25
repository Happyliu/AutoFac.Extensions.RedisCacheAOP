using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCacheAOPSampleProject
{
    public enum CacheType
    {
        InMemory,
        Redis
    }

    public class CacheOptions
    {
        public CacheType Type { get; set; }
        public string RedisUrl { get; set; }
        public string RedisInstance { get; set; }
    }
}
