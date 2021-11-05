using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;
using Server.Infratructure;

namespace Server.Controllers
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

        public WeatherForecastController(ILogger<WeatherForecastController> logger)
        {
            _logger = logger;
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

    [ApiController]
    [Route("[controller]")]
    public class SendController : ControllerBase
    {
        private readonly IHubContext<NotifyHub> _notifyHub;

        public SendController(IHubContext<NotifyHub> notifyHub)
        {
            _notifyHub = notifyHub;
        }
        
        [HttpPost]
        public async Task<IActionResult> Send(string message)
        {
            await _notifyHub.Clients.All.SendAsync(method: "message", "anonymous", message);
            return Ok();
        }
    }
}