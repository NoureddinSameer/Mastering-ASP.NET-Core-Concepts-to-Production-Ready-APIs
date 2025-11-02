
using M02.QueryString.Models;
using Microsoft.AspNetCore.Mvc;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();



var app = builder.Build();


app.MapGet("/product-minimal", (int page, int pageSize) =>
{
    return Results.Ok($"Showing {pageSize} items of page # {page}");
});

app.MapGet("/product-minimal-1", ([FromQuery(Name = "page")]int p,[FromQuery(Name = "pageSize")]int ps) =>
{
    return Results.Ok($"Showing {ps} items of page # {p}");
});


app.MapGet("/product-minimal-asparameters", ([AsParameters] SearchRequest request) =>
{
    return Results.Ok(request);
});


app.MapGet("/product-minimal-array", (Guid[] ids) =>
{
    return Results.Ok(ids);
});


app.MapGet("/date-range-minimal", (DateRangeQuery dateRange) =>
{
    return Results.Ok(dateRange);
});

app.MapGet("/date-range-minimal-1", (string dateRange) =>
{
    var parsed = DateRangeQuery.Parse(dateRange, null);
    return Results.Ok(parsed);
});

app.MapControllers();

app.Run();

public class SearchRequest
{
    public string Query { get; set; }
    public int Page{ get; set; }
    public int PageSize{ get; set; }
}