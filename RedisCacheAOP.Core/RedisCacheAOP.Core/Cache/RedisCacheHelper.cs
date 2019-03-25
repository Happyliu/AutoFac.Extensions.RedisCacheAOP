using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;
using System;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace RedisCacheAOP.Core.Cache
{
    public static class RedisCacheHelper
    {
        public async static Task SetAsync<T>(this IDistributedCache distributedCache, ILogger<string> logger, string key, T value, DistributedCacheEntryOptions options, CancellationToken token = default(CancellationToken))
        {
            try
            {
                await distributedCache.SetAsync(logger, key, ToByteArray<T>(value), options, token);
            }
            catch (Exception e)
            {
                logger.LogError("async set value to redis has error : {0}", e.Message);
            }
        }

        public async static Task<T> GetAsync<T>(this IDistributedCache distributedCache, ILogger<string> logger, string key, CancellationToken token = default(CancellationToken)) where T : class
        {
            try
            {
                var result = await distributedCache.GetAsync(key);
                return FromByteArray<T>(result);
            }
            catch (Exception e)
            {
                logger.LogError("async get value to redis has error : {0}", e.Message);
                return null;
            }
        }

        public static void Set<T>(this IDistributedCache distributedCache, ILogger<string> logger, string key, T value, DistributedCacheEntryOptions options)
        {
            try
            {
                distributedCache.Set(key, ToByteArray<T>(value), options);
            }
            catch (Exception e)
            {
                logger.LogError("set value to redis has error : {0}", e.Message);
            }

        }

        public static T Get<T>(this IDistributedCache distributedCache, ILogger<string> logger, string key) where T : class
        {
            try
            {
                var result = distributedCache.Get(key);
                return FromByteArray<T>(result);
            }
            catch (Exception e)
            {
                logger.LogError("get value to redis has error : {0}", e.Message);
                return null;
            }
        }

        public static void Set(this IDistributedCache distributedCache, ILogger<string> logger, string key, Object value, DistributedCacheEntryOptions options)
        {
            try
            {
                distributedCache.Set(key, ToByteArray(value), options);
            }
            catch (Exception e)
            {
                logger.LogError("set value to redis has error : {0}", e.Message);
            }

        }

        public static Object Get(this IDistributedCache distributedCache, ILogger<string> logger, string key, Type returnType)
        {
            try
            {
                var result = distributedCache.Get(key);
                return FromByteArray(result, returnType);
            }
            catch (Exception e)
            {
                logger.LogError("get value to redis has error : {0}", e.Message);
                return null;
            }
        }

        public static void Set<T>(this IDistributedCache distributedCache, ILogger logger, string key, T value, DistributedCacheEntryOptions options)
        {
            try
            {
                distributedCache.Set(key, ToByteArray<T>(value), options);
            }
            catch (Exception e)
            {
                logger.LogError("set value to redis has error : {0}", e.Message);
            }

        }

        public static T Get<T>(this IDistributedCache distributedCache, ILogger logger, string key) where T : class
        {
            try
            {
                var result = distributedCache.Get(key);
                return FromByteArray<T>(result);
            }
            catch (Exception e)
            {
                logger.LogError("get value to redis has error : {0}", e.Message);
                return null;
            }
        }

        public static byte[] ToByteArray<T>(T obj)
        {
            if (obj == null)
                return null;
            var jsonString = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        public static byte[] ToByteArray(Object obj)
        {
            if (obj == null)
                return null;
            var jsonString = JsonConvert.SerializeObject(obj);
            return Encoding.UTF8.GetBytes(jsonString);
        }

        public static T FromByteArray<T>(byte[] data)
        {
            if (data == null)
                return default(T);
            var jsonString = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject<T>(jsonString);
        }

        public static Object FromByteArray(byte[] data, Type returnType)
        {
            if (data == null)
                return default(Type);
            var jsonString = Encoding.UTF8.GetString(data);
            return JsonConvert.DeserializeObject(jsonString, returnType);
        }
    }
}
