
var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/api/products", () => Results.Ok());

app.MapPost("/api/products", () => Results.Ok());

app.MapPut("/api/products/{id}", (Guid id) => Results.NoContent());

app.MapPatch("/api/products/{id}", (Guid id) => Results.NoContent());

app.MapDelete("/api/products/{id}", (Guid id) => Results.NoContent());

// This minimal APIs is same as app.MapGet("/api/products", () => Results.Ok());
app.MapMethods("/api/products", ["GET"], () => Results.Ok());

app.MapMethods("/api/products", ["OPTIONS"], () => Results.NoContent());

app.MapMethods("/api/products", ["HEAD"], () => Results.NoContent());

app.Run();
