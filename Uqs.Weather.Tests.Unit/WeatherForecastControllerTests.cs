using Microsoft.Extensions.Logging.Abstractions;
using Uqs.Weather.Controllers;

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
        //Assign
        var logger = NullLogger<WeatherForecastController>.Instance;
        var controller = new WeatherForecastController(null!, logger, null!, null!);

        //Act
        double actual = controller.ConvertCToF(c);

        //Assert
        Assert.Equal(f, actual, 1);
    }
}