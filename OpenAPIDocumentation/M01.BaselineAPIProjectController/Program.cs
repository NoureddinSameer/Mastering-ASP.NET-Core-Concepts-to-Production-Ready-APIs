using M01.BaselineAPIProjectController;
using M01.BaselineAPIProjectController.Data;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddApplicationServices(builder.Configuration);

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
}

app.UseAuthentication();

app.UseAuthorization();

app.UseExceptionHandler();

app.UseStatusCodePages();

app.MapControllers();



using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await AppDbContextInitializer.InitializeAsync(context);
}

app.Run();
