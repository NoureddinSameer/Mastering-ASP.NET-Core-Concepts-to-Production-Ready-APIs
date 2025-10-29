
using M03.RazorPagesCRUD.Store;
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();

builder.Services.AddSingleton<ProductStore>();

var app = builder.Build();


app.UseRouting();

app.MapRazorPages();

app.Run();
