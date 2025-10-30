var builder = WebApplication.CreateBuilder(args);




var app = builder.Build();

app.MapGet("/product/{id}", (int id) => $"Product {id}");

app.MapGet("/date/{year}-{month}-{day}", (int year, int month, int day)
    => $"Date is {new DateOnly(year, month, day):yyyy-MM-dd}");


app.MapGet("/{controller=Home}", (string? controller) => controller);


app.MapGet("/users/{id?}", (int? id) => id is null ? "All users" : $"User {id}");

app.MapGet("/a{b}c{d}", (string b, string d) => $"b: {b}, d: {d}");

app.MapGet("/single/{*slug}", (string slug) => $"Slug: {slug}");

app.MapGet("/double/{**slug}", (string slug) => $"Slug: {slug}");

app.Run();
