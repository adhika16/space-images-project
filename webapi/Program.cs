using AspNetCoreRateLimit;
using StackExchange.Redis;
using webapi.Services;
using webapi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

// needed to load configuration from appsettings.json
builder.Services.AddOptions();

// needed to store rate limit counters and ip rules
builder.Services.AddMemoryCache();

//load general configuration from appsettings.json
builder.Services.Configure<IpRateLimitOptions>(builder.Configuration.GetSection("IpRateLimiting"));

//load ip rules from appsettings.json
builder.Services.Configure<IpRateLimitPolicies>(builder.Configuration.GetSection("IpRateLimitPolicies"));

// inject counter and rules stores
builder.Services.AddInMemoryRateLimiting();
    //builder.Services.AddDistributedRateLimiting<AsyncKeyLockProcessingStrategy>();
    //builder.Services.AddDistributedRateLimiting<RedisProcessingStrategy>();
    //builder.Services.AddRedisRateLimiting();

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

// configuration (resolvers, counter key builders)
builder.Services.AddSingleton<IRateLimitConfiguration, RateLimitConfiguration>();

// // Retrieve Redis connection string from configuration
// string redisConnectionString = builder.Configuration.GetConnectionString("Redis");
// // Configure Redis connection
// builder.Services.AddSingleton<IConnectionMultiplexer>(ConnectionMultiplexer.Connect(redisConnectionString));

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

app.UseIpRateLimiting();

app.UseCors("myAppCors");

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
