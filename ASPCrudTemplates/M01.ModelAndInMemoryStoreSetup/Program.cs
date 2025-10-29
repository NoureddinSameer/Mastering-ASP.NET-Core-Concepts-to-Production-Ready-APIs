using M01.ModelAndInMemoryStoreSetup.Store;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSingleton<ProductStore>();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.Run();
