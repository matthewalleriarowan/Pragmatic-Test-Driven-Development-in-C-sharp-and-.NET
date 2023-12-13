using AdamTibi.OpenWeather;

namespace Uqs.Weather
{
    public class ClientStub : IClient
    {
        public Task<WeatherResponse> WeatherCallAsync(decimal latitude, decimal longitude, Units unit)
        {
            const int DAYS = 7;
            WeatherResponse res = new WeatherResponse();
            res.List = new Forecast[DAYS];
            DateTime now = DateTime.Now.Date;

            for (int i = 0; i < DAYS; i++)
            {
                res.List[i] = new Forecast();
                res.List[i].Dt = now.AddHours(12 + 24 * i);
                res.List[i].Main = new Main
                {
                    Temp = Random.Shared.Next(-20, 55)
                };
            }
            return Task.FromResult(res);
        }
    }
}
