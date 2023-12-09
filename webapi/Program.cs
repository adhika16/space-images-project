using StackExchange.Redis;
using webapi.Services;
using webapi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var allowedOrigin = !string.IsNullOrEmpty(builder.Configuration.GetSection("MyApp:AllowedOrigins").Get<string>()) ?
    builder.Configuration.GetSection("MyApp:AllowedOrigins").Get<string>().Split(',') : new string[0];
builder.Services.AddCors(options =>
{
    options.AddPolicy("myAppCors", policy =>
    {
        policy.WithOrigins(allowedOrigin).AllowAnyHeader().AllowAnyMethod();
    });
});

builder.Services.AddTransient<INasaApiService, NasaApiService>();
builder.Services.AddHttpClient();

// Retrieve Redis connection string from configuration
string redisConnectionString = builder.Configuration.GetConnectionString("Redis");
// Configure Redis connection
builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

// Configure API key
NasaApiConfigs nasaApiConfigs = new();
builder.Configuration.GetSection("NasaApi").Bind(nasaApiConfigs);
builder.Services.AddSingleton(nasaApiConfigs);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCors("myAppCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
