var builder = WebApplication.CreateBuilder(args);

var configData = new Dictionary<string, string?> 
{
    { "MaxConnections" ,  "10" }
};

builder.Configuration.AddInMemoryCollection(configData);

var app = builder.Build();

app.MapGet("/{key}", (string key, IConfiguration config) => 
{
    return config[key];
});

app.Run();
