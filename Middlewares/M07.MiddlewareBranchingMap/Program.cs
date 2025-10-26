var builder = WebApplication.CreateBuilder(args);


static void GetCommonBranch(IApplicationBuilder app)
{
    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("MW #1\n");
        await next();
    });
    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("MW #2\n");
        await next();
    });
}

static void GetBranch1(IApplicationBuilder app)
{
    GetCommonBranch(app);
    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("MW #3\n");
        await next();
    });
    app.Run(async (context) =>
    {
        await context.Response.WriteAsync("MW #4\n");
    });
}

static void GetBranch2(IApplicationBuilder app)
{
    GetCommonBranch(app);
    app.Use(async (context, next) =>
    {
        await context.Response.WriteAsync("MW #5\n");
        await next();
    });
    app.Run(async (context) =>
    {
        await context.Response.WriteAsync("MW #6\n");
    });
}

// ← DI Container goes here (Configuring Dependencies)

var app = builder.Build();

// ← Middleware setup goes here


app.Map("/branch1", GetBranch1);
app.Map("/branch2", GetBranch2);

app.Run(async context =>
{
    await context.Response.WriteAsync("Terminal Middleware");
});
app.Run();
