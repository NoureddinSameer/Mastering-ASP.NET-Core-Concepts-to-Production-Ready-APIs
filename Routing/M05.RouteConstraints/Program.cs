
using M05.RouteConstraints.Constraints;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRouting(options =>
{
    options.ConstraintMap.Add("validMonth", typeof(MonthRouteConstraint));
});

var app = builder.Build();


app.MapGet("/int/{id:int}", (int id)
    => $"Integer: {id}");

app.MapGet("/bool/{active:bool}", (bool active)
    => $"Boolean: {active}");

app.MapGet("/datetime/{dob:datetime}", (DateTime dob)
    => $"DateTime: {dob:yyyy-MM-dd h:mm:ss tt}");


app.MapGet("/decimal/{price:decimal}", (decimal price)
    => $"Decimal: {price}");

app.MapGet("/double/{weight:double}", (double weight)
    => $"Double: {weight}");

app.MapGet("/float/{weight:float}", (float weight)
    => $"Float: {weight}");

app.MapGet("/guid/{id:guid}", (Guid id)
    => $"GUID: {id}");

app.MapGet("/long/{ticks:long}", (long ticks)
    => $"Long: {ticks}");

app.MapGet("/minlength/{username:minlength(4)}", (string username)
    => $"MinLength(4): {username}");

app.MapGet("/maxlength/{filename:maxlength(8)}", (string filename)
    => $"MaxLength(8): {filename}");

app.MapGet("/length/{filename:length(12)}", (string filename)
    => $"Exact Length(12): {filename}");

app.MapGet("/lengthrange/{filename:length(8,16)}", (string filename)
    => $"Length(8-16): {filename}");

app.MapGet("/min/{age:min(18)}", (int age)
    => $"Min Age(18): {age}");

app.MapGet("/max/{age:max(120)}", (int age)
    => $"Max Age(120): {age}");

app.MapGet("/range/{age:range(18,120)}", (int age)
    => $"Range(18-120): {age}");

app.MapGet("/alpha/{name:alpha}", (string name)
    => $"Alpha: {name}");

app.MapGet("/regex/{ssn:regex(^\\d{{3}}-\\d{{2}}-\\d{{4}}$)}", (string ssn)
    => $"Regex Match (SSN): {ssn}");

app.MapGet("/required/{name:required}", (string name)
    => $"Required: {name}");

app.MapGet("/sales/month/{value:validMonth}", (int value) => $"Month: {value}");

app.Run();
