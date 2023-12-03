using AdamTibi.OpenWeather;

namespace Uqs.Weather;

public interface IClient
{
    public Task<WeatherResponse> WeatherCallAsync(decimal latitude, decimal longitude, Units unit);
}