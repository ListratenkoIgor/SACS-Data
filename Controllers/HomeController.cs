using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace DataService.Controllers
{
    [Route("controller")]
    [ApiController]
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
        }

        public string Index()
        {
            return "Dara Servicev2.1";
        }
        [HttpGet("About")]
        public string About()
        {
            return "Dara Servicev2.1";
        }
    }
}
