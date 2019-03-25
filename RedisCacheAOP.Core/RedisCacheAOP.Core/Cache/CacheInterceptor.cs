using Autofac;
using Castle.DynamicProxy;
using Microsoft.Extensions.Caching.Distributed;
using Microsoft.Extensions.Logging;
using System;
using System.Reflection;

namespace RedisCacheAOP.Core.Cache
{
    public class CacheInterceptor : IInterceptor
    {
        private ILifetimeScope _scope;

        public CacheInterceptor(ILifetimeScope scope)
        {
            this._scope = scope;
        }


        public void Intercept(IInvocation invocation)
        {
            if (BeforeInvoke(invocation))
            {
                invocation.Proceed();
                AfterInvoke(invocation);
            }
        }

        /// <summary>
        /// Takes some action before the invocation proceeds.
        /// </summary>
        /// <param name="invocation">The invocation that is being intercepted.</param>
        protected virtual bool BeforeInvoke(IInvocation invocation)
        {
            var _cache = _scope.Resolve<IDistributedCache>();
            var _logger = _scope.Resolve<ILogger<string>>();
            var cacheAttribute = invocation.Method.GetCustomAttribute<CacheAttribute>();
            if (cacheAttribute != null)
            {
                try
                {
                    CacheKey key = new CacheKey(invocation.Method.ReflectedType, invocation.Method.ReturnType, invocation.Method.DeclaringType.FullName + "." + invocation.Method.Name, invocation.GenericArguments, invocation.Arguments);
                    var getresult = RedisCacheHelper.Get(_cache, _logger, key.GetHashCode().ToString(), invocation.Method.ReturnType);
                    if (getresult != null)
                    {
                        invocation.ReturnValue = getresult;
                        return false;
                    }
                    else
                    {
                        return true;
                    }
                }
                catch (Exception e)
                {
                    _logger.LogError("get cache key error : {0}", e.Message);
                    return true;
                }
            }
            return true;
        }

        /// <summary>
        /// Takes some action after the invocation proceeds.
        /// </summary>
        /// <param name="invocation">The invocation that is being intercepted.</param>
        protected virtual void AfterInvoke(IInvocation invocation)
        {
            var _cache = _scope.Resolve<IDistributedCache>();
            var _logger = _scope.Resolve<ILogger<string>>();
            var cacheAttribute = invocation.Method.GetCustomAttribute<CacheAttribute>();
            if (cacheAttribute != null)
            {
                try
                {
                    var cacheEntryOptions = new DistributedCacheEntryOptions()
                        .SetAbsoluteExpiration(TimeSpan.FromSeconds(cacheAttribute.Duration));
                    //get the method which want to call to, and set the gereric type for the method
                    CacheKey key = new CacheKey(invocation.Method.ReflectedType, invocation.Method.ReturnType, invocation.Method.DeclaringType.FullName + "." + invocation.Method.Name, invocation.GenericArguments, invocation.Arguments);
                    RedisCacheHelper.Set(_cache, _logger, key.GetHashCode().ToString(), invocation.ReturnValue, cacheEntryOptions);
                }
                catch (Exception e)
                {
                    _logger.LogError("set cache key error : {0}", e.Message);
                }

            }
        }

    }
}
