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
    public class WeatherForecastController : ControllerBase
    {
        private static readonly string[] Summaries = new[]
        {
            "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
        };

        private readonly ILogger<WeatherForecastController> _logger;
        private readonly IGreeterService _greeterService;

        public WeatherForecastController(ILogger<WeatherForecastController> logger, IGreeterService greeterService)
        {
            _logger = logger;
            _greeterService = greeterService;
        }       


        [HttpGet("greeter")]
        public async Task<IActionResult> GetGreeter()
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


        [HttpGet]
        public IEnumerable<WeatherForecast> Get()
        {
            var rng = new Random();
            return Enumerable.Range(1, 5).Select(index => new WeatherForecast
            {
                Date = DateTime.Now.AddDays(index),
                TemperatureC = rng.Next(-20, 55),
                Summary = Summaries[rng.Next(Summaries.Length)]
            })
            .ToArray();
        }
    }
}
