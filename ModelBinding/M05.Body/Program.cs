


using M05.Body.Requests;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
.AddXmlSerializerFormatters();



var app = builder.Build();



app.MapControllers();

app.MapPost("/product-minimal", (ProductRequest request) =>
{
    return Results.Ok(request);
});

app.Run();
