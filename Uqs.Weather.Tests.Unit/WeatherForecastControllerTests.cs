using Microsoft.AspNetCore.Authorization;
using Microsoft.Extensions.Logging.Abstractions;
using Uqs.Weather.Controllers;
using Uqs.Weather.Wrappers;
using Xunit;

namespace Uqs.Weather.Tests.Unit;

public class WeatherForecastControllerTests
{
    [Theory]
    [InlineData( 0      ,  32.0)]
    [InlineData( -100   ,  -148 )]
    [InlineData( -10.1  ,  13.8 )]
    [InlineData( 10     ,  50   )]
    public void ConvertCToF_C_F(double c, double f)
    {
        //Arrange
        var logger = NullLogger<WeatherForecastController>.Instance;
        var controller = new WeatherForecastController(null!, logger, null!, null!);

        //Act
        double actual = controller.ConvertCToF(c);

        //Assert
        Assert.Equal(f, actual, 1);
    }

    [Fact]
    public async void GetReal_NotToday_WFStartsNextDay()
    {
        // Arrange
        const double day2Temp = 3.3;
        const double day5Temp = 7.7;
        var today = new DateTime(2022, 1, 1);
        var realWeatherTemps = new double[] {
        2, day2Temp, 4, 5.5, 6, day5Temp, 8
        };
        var clientStub = new ClientStub(today, realWeatherTemps);
        var controller = new WeatherForecastController(clientStub, null!, null!, null!);

        // Act
        IEnumerable<WeatherForecast> wfs = await controller.GetReal();

        // Assert
        Assert.Equal(3, wfs.First().TemperatureC);
    }
    
}