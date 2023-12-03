using System.Collections;
using Uqs.Weather;

var builder = WebApplication.CreateBuilder(args);
var logServices = builder.Services.Where(x => x.ServiceType.Name.Contains("Log")).ToArray();
for (int i = 0; i < logServices.Length; i++)
{
    System.Diagnostics.Debug.WriteLine(logServices[i].ServiceType.Name);
}
System.Diagnostics.Debug.WriteLine(logServices.Length);

// Add services to the container.

builder.Services.AddSingleton<Client>( 
    _=> {
    string apiKey = builder.Configuration["OpenWeather:Key"];
    HttpClient httpClient = new HttpClient();
    return new Client(apiKey, httpClient);
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
