var builder = WebApplication.CreateBuilder(args);

// ← DI Container goes here (Configuring Dependencies)

var app = builder.Build();

// ← Middleware setup goes here

// Case #1
// app.Use(async (context,next)=>
// {
//     context.Response.ContentLength = 10;
//     context.Response.Headers.Append("X-Hdr1", "val1");// Accepted, Response not started
//     context.Response.StatusCode = StatusCodes.Status206PartialContent;// Accepted, Response not started
//     await context.Response.WriteAsync("MW #1 \n");// response has begun
//     // context.Response.Headers.Append("X-Hdr1", "val1");// Protocol violation; Response has started.
//     // context.Response.StatusCode = StatusCodes.Status206PartialContent;// Protocol violation; Response has started.
//     await next();
// });
// app.Use(async (context,next)=>
// {
//     await context.Response.WriteAsync("ab\n");// response has begun
//     await next();
// });

// Case #2
app.Use(async (context,next)=>
{
    await context.Response.WriteAsync("MW #1 \n");// response has begun
    await next();
});
app.Use(async (context,next)=>
{
    if(!context.Response.HasStarted)
    {
        context.Response.StatusCode = StatusCodes.Status206PartialContent;
    }
    await next();
});

app.Run();
