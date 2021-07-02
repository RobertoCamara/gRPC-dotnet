using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using POC.GrpcService.ApiClient.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace POC.GrpcService.ApiClient.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class GreetController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<GreetController> _logger;
        private readonly IGreeterService _greeterService;

        public GreetController(ILogger<GreetController> logger, IGreeterService greeterService)
        {
            _logger = logger;
            _greeterService = greeterService;
        }       


        [HttpGet("hello")]
        public async Task<IActionResult> SayHello()
        {
            var result = await _greeterService.SayHelloAsync(new HelloRequest { Name = "Roberto Camara" });
            return Ok(result);
        }

        [HttpGet("guid")]
        public async Task<IActionResult> GetGuid()
        {
            var result = await _greeterService.GetGuid();
            return Ok(result);
        }
    }
}
