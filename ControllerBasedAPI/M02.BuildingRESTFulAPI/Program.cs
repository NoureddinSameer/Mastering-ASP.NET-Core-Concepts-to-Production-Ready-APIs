
using System.Text.Json.Serialization;
using M02.BuildingRESTFulAPI.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
    // if we want to use PATCH before you write add NewtonsoftJson we have to run this line in terminal "dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson"
    .AddNewtonsoftJson(options =>
    {
        options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    })
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

builder.Services.AddSingleton<ProductRepository>();

var app = builder.Build();

app.MapControllers();

app.Run();
