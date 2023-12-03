using System.Collections;
using Uqs.Weather;
using Uqs.Weather.Wrappers;

var builder = WebApplication.CreateBuilder(args);
var logServices = builder.Services.Where(x => x.ServiceType.Name.Contains("Log")).ToArray();
for (int i = 0; i < logServices.Length; i++)
{
    System.Diagnostics.Debug.WriteLine(logServices[i].ServiceType.Name);
}
System.Diagnostics.Debug.WriteLine(logServices.Length);

// Add services to the container.

builder.Services.AddSingleton<IClient>( 
    _=> {
        bool isLoad =
        bool.Parse(builder.Configuration["LoadTest:IsActive"]);
        if (isLoad) return new ClientStub();
        else {
            string apiKey = builder.Configuration["OpenWeather:Key"];
            HttpClient httpClient = new();
            return new Client(apiKey, httpClient);
        }
    });

builder.Services.AddSingleton<INowWrapper>(
    _ => {
        return new NowWrapper();
    });

builder.Services.AddTransient<IRandomWrapper>(
    _ => {
        return new RandomWrapper();
    });


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
