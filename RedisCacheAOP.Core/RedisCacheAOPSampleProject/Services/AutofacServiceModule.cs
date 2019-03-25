using Autofac;
using Autofac.Extras.DynamicProxy;
using RedisCacheAOP.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace RedisCacheAOPSampleProject.Services
{
    public class AutofacServiceModule : Autofac.Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterAssemblyTypes(Assembly.GetExecutingAssembly())
            .Where(t => t.GetInterfaces().Contains(typeof(IService)))
            .As(t => t.GetInterfaces().FirstOrDefault(i => i.Name == "I" + t.Name))
            .EnableInterfaceInterceptors().InterceptedBy(typeof(CacheInterceptor)).InstancePerLifetimeScope();
            builder.RegisterType<CacheInterceptor>();
        }
    }
}
