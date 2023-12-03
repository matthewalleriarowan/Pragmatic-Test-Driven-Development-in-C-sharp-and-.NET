using Microsoft.Extensions.Logging.Abstractions;
using Uqs.Weather.Controllers;
using Uqs.Weather.Wrappers;

var logger = NullLogger<WeatherForecastController>.Instance;
var nowWrapper = new NowWrapper();

var controller = new WeatherForecastController(null!, logger, nowWrapper);

double f1 = controller.ConvertCToF(-1.0);
if (f1 != 30.20d) throw new Exception("Invalid");
double f2 = controller.ConvertCToF(1.2);
if (f2 != 34.16d) throw new Exception("Invalid");
System.Diagnostics.Debug.WriteLine("Tests Passed!");