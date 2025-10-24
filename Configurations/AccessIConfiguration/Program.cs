var builder = WebApplication.CreateBuilder(args);
 
var app = builder.Build();

app.Map("/get-value-by-key", (IConfiguration config) => 
{
    return config["ServiceName"];
});

app.Map("/get-value-by-path", (IConfiguration config) => 
{
    return config["ConnectionStrings:DefaultConnection"];
});

app.Map("/get-connection-string", (IConfiguration config) => 
{
    return config.GetConnectionString("DefaultConnection");
});

app.Map("/get-value", (IConfiguration config) => 
{
    return config.GetValue<string>("ServiceName");
});

app.Map("/get", (IConfiguration config) => 
{
    var appSettings = config.GetSection(AppSettings.Name).Get<AppSettings>();
    return appSettings;
});

app.Map("/bind", (IConfiguration config) => 
{
    AppSettings appSettings = new();
    
    config.GetSection(AppSettings.Name).Bind(appSettings);
    
    return appSettings;
});


app.Run();


public class AppSettings
{
    public const string Name = "AppSettings";
    public TimeSpan OpenAt {get; set;}
    public TimeSpan CloseAt {get; set;}
    public TimeSpan DaysOpen {get; set;}
    public bool EnableOnlineBooking {get; set;}
    public int MaxDailyAppointments {get; set;}
}