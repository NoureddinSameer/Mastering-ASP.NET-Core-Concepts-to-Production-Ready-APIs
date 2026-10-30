using M03.CachingHybrid.Data;
using M03.CachingHybrid.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Hybrid;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

// L2 Redis
builder.Services.AddStackExchangeRedisCache(options =>
{
    options.Configuration = builder.Configuration.GetConnectionString("Redis");
    options.InstanceName = "M03:";
});

// builder.Services.AddDistributedSqlServerCache(options =>
// {
//     options.ConnectionString = builder.Configuration.GetConnectionString("SqlCache");
//     options.SchemaName = "dbo";
//     options.TableName = "CacheEntries";
// });

builder.Services.AddHybridCache(options =>
{
    options.DefaultEntryOptions = new HybridCacheEntryOptions
    {
        Expiration = TimeSpan.FromMinutes(10), // L2, L3 
        LocalCacheExpiration = TimeSpan.FromSeconds(30)// L1
        // ,Flags = HybridCacheEntryFlags.DisableLocalCache
        // ,Flags = HybridCacheEntryFlags.DisableDistributedCache
    };
});


builder.Services.AddScoped<IProductService, ProductService>();

builder.Services.AddDbContext<AppDbContext>(options =>
{
    options.UseSqlite("Data Source = app.db");
});

var app = builder.Build();

app.MapControllers();

app.Run();