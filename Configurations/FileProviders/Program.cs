var builder = WebApplication.CreateBuilder(args);

builder.Configuration.AddJsonFile("mycustomconfig.json",
optional: false,
reloadOnChange: true
);

builder.Configuration.AddIniFile("config.ini",
optional: false,
reloadOnChange: true
);

var app = builder.Build();

app.MapGet("/{key}", (string key, IConfiguration config) => 
{
    return config[key];
});

app.MapGet("/ini/{key}", (string key, IConfiguration config) => 
{
    return config[key];
});


app.Run();
