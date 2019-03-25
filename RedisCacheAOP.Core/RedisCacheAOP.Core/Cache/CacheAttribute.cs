using System;

namespace RedisCacheAOP.Core.Cache
{
    [AttributeUsage(AttributeTargets.Method)]
    public class CacheAttribute : Attribute
    {
        public CacheAttribute(int duration)
        {
            CacheType = CacheTypeCategory.AbsoluteExpiration.Value;
            Duration = duration;
        }

        public string CacheType { get; }

        public int Duration { get; }
    }
}
