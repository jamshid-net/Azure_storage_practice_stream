using Microsoft.AspNetCore.Mvc;
using System.Runtime.CompilerServices;
using System.Threading;

namespace AngularApp2.Server.Controllers;
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

    [HttpGet(Name = "GetWeatherForecast")]
    public async IAsyncEnumerable<WeatherForecast> Get([EnumeratorCancellation] CancellationToken token)
    {
        var forecasts = Enumerable.Range(1, 20)
         .Select(index => new WeatherForecast
         {
             Date = DateOnly.FromDateTime(DateTime.Now.AddDays(index)),
             TemperatureC = Random.Shared.Next(-20, 55),
             Summary = Summaries[Random.Shared.Next(Summaries.Length)]
         });
        foreach (var item in forecasts)
        {
            
            await Task.Delay(1000, token);
            yield return item;
        }

    }
}
