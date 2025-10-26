var builder = WebApplication.CreateBuilder(args);

// ← DI Container goes here (Configuring Dependencies)

var app = builder.Build();

// ← Middleware setup goes here

// Built-in the framework
app.UseExceptionHandler();
app.UseHsts();
app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();
app.UseCors();
app.UseAuthentication();
app.UseAuthorization();

// Custom middleware
app.Use(async (context, next) => { await next.Invoke(); });

// Endpoints
app.MapGet("/", () => "Hello World!");

app.Run();
