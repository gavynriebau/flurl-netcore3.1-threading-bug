using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Flurl.Http;

namespace Web.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class WeatherForecastController : ControllerBase
    {
        public WeatherForecastController()
        {
        }

        [HttpGet]
        public async Task<string> Get()
        {
            // Inside the integration test, this call should return a mocked HTTP response but doesn't.
            // Instead an actual call to the non-existent URL is attempted which causes an exception to be thrown.
            var test = await "http://nonexistenturl-asdjhasdhj.com".PostJsonAsync(new {
                FakeData = "stuff"
            });
            test.EnsureSuccessStatusCode();
            return await test.Content.ReadAsStringAsync();
        }
    }
}
