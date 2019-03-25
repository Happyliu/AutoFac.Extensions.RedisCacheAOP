using RedisCacheAOP.Core.Cache;

namespace RedisCacheAOPSampleProject.Services
{
    public interface ISampleService2
    {
        [Cache(60)]
        string GetValueFromSampleService2();
    }
}