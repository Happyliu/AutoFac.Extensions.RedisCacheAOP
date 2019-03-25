using RedisCacheAOP.Core.Cache;

namespace RedisCacheAOPSampleProject.Services
{

    public interface ISampleService1
    {
        [Cache(30)]
        string GetValueFromSampleService1();
    }
}