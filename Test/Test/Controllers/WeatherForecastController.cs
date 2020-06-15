using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Test.Controllers
{
    /// <summary>
    /// 路由设置
    /// </summary>
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class WeatherForecastController : ControllerBase
    {        
        [HttpGet]
        public  string Get()
        {
            return "Hello Api";
        }

        [HttpGet]
        public string set()
        {
            return "Hello Api@@@@";
        }
    }
}
