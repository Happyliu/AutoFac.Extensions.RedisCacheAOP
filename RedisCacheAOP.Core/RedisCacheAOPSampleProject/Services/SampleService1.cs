using RedisCacheAOP.Core.Cache;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace RedisCacheAOPSampleProject.Services
{
    public class SampleService1 : ISampleService1, IService
    {

        public string GetValueFromSampleService1()
        {
            return "Value1";
        }
    }
}
