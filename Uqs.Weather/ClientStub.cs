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

            System.Diagnostics.Debug.WriteLine(now.ToString());

            for (int i = 0; i < DAYS; i++)
            {
                res.List[i] = new Forecast();
                res.List[i].Dt = now.AddDays(i);
                res.List[i].Dt = now.AddHours(12);
                res.List[i].Main = new Main
                {
                    Temp = Random.Shared.Next(-20, 55)
                };
                System.Diagnostics.Debug.WriteLine(res.List[i].Dt.ToString());
            }
            return Task.FromResult(res);
        }
    }
}
