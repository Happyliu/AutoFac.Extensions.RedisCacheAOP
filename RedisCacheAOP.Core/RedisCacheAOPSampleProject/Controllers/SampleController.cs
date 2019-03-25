using Autofac;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Caching.Distributed;
using RedisCacheAOPSampleProject.Services;
using System.Text;

namespace RedisCacheAOPSampleProject.Controllers
{
    [Route("sample")]
    public class SampleController : Controller
    {
        private ISampleService1 _service1;

        public SampleController(ISampleService1 service1)
        {
            _service1 = service1;
        }

        [HttpGet]
        public string index()
        {
            var result = _service1.GetValueFromSampleService1();
            return result;
        }

    }
}
