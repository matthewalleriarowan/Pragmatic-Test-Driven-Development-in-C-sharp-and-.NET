using AdamTibi.OpenWeather;

namespace Uqs.Weather
{
    public class ClientStub : IClient
    {
        private readonly DateTime _now;
        private readonly IEnumerable<double> _sevenDaysTemps;

        public ClientStub(DateTime now, IEnumerable<double> sevenDaysTemps) { 
            _now = now;
            _sevenDaysTemps = sevenDaysTemps;
        }

        public ClientStub()
        {
            _now = new DateTime(2022, 1, 1).Date;
            _sevenDaysTemps = new List<double>() {
                Random.Shared.Next(-20, 55),
                Random.Shared.Next(-20, 55),
                Random.Shared.Next(-20, 55),
                Random.Shared.Next(-20, 55),
                Random.Shared.Next(-20, 55),
                Random.Shared.Next(-20, 55),
                Random.Shared.Next(-20, 55)
            };
        }

        public Task<WeatherResponse> WeatherCallAsync(decimal latitude, decimal longitude, Units unit)
        {
            const int DAYS = 7;
            WeatherResponse res = new WeatherResponse();
            res.List = new Forecast[DAYS];

            for (int i = 0; i < DAYS; i++)
            {
                res.List[i] = new Forecast();
                res.List[i].Dt = _now.AddHours(12 + 24 * i);
                res.List[i].Main = new Main
                {
                    Temp = _sevenDaysTemps.ElementAt(i)
                };
            }
            return Task.FromResult(res);
        }
    }
}
