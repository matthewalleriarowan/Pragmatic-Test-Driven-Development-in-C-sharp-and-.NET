using AdamTibi.OpenWeather;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

namespace Uqs.Weather.Controllers;

[ApiController]
[Route("[controller]")]
public class WeatherForecastController : ControllerBase
{
    private readonly IClient _client;
    private readonly ILogger<WeatherForecastController> _logger;
    
    private int FORECAST_DAYS = 5;
    private decimal GREENWICH_LAT = 51.4934m;
    private decimal GREENWICH_LON = 0.0098m;

    public WeatherForecastController(IClient client, ILogger<WeatherForecastController> logger)
    {
        _client = client;
        _logger = logger;
    }

    private static readonly string[] Summaries = new[]
    {
        "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
    };

    private string MapFeelToTemp(int tempC)
    {
        if (tempC <= 0) return Summaries.First();
        int summariesIndex = (tempC / 5) + 1;
        if (summariesIndex >= Summaries.Length) return Summaries.Last();
        return Summaries[summariesIndex];
    }

    [HttpGet(Name = "GetRealWeatherForecast")]
    public async Task<IEnumerable<WeatherForecast>> GetReal()
    {
        WeatherResponse res = await _client.WeatherCallAsync(GREENWICH_LAT, GREENWICH_LON, Units.Metric);

        var noon = TimeSpan.FromHours(12);

        IEnumerable<WeatherForecast> wfs = res.List
            .Where(x => x.Dt.TimeOfDay == noon)
            .Select(x => new WeatherForecast()
            {
                Date = x.Dt,
                TemperatureC = (int)Math.Round(x.Main.Temp),
                Summary = MapFeelToTemp((int)Math.Round(x.Main.Temp))
            })
        // .Skip(1) // drop a current day
        .Take(FORECAST_DAYS)
        .ToList();

        return wfs;
    }

    [HttpGet("ConvertCToF")]
    public double ConvertCToF(double c)
    {
        double f = c * (9d / 5d) + 32;
        _logger.LogInformation("Conversion requested.");
        return f;
    }
}
