


using M04.BlazorSSRCRUD.Components;
using M04.BlazorSSRCRUD.Store;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents(); // SSR + SignalR

builder.Services.AddSingleton<ProductStore>();

var app = builder.Build();

app.UseRouting();

app.UseAntiforgery();

app.MapRazorComponents<App>()
   .AddInteractiveServerRenderMode();

app.Run();
