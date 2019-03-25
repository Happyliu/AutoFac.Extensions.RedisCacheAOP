

namespace RedisCacheAOP.Core.Cache
{
    public class CacheTypeCategory
    {
        private CacheTypeCategory(string value) { Value = value; }

        public string Value { get; set; }

        public static CacheTypeCategory AbsoluteExpiration { get { return new CacheTypeCategory("absolute"); } }
        public static CacheTypeCategory SlidingExpiration { get { return new CacheTypeCategory("sliding"); } }
    }
}
