using System.Text;
using M04.RefreshingAccessToken.Permissions;
using M04.RefreshingAccessToken.Services;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();

builder.Services.AddScoped<JwtTokenProvider>();

builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(options =>
{
    var jwtSettings = builder.Configuration.GetSection("JwtSettings");

    options.TokenValidationParameters = new()
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        ValidateIssuerSigningKey = true,
        ValidIssuer = jwtSettings["Issuer"],
        ValidAudience = jwtSettings["Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(
            Encoding.UTF8.GetBytes(jwtSettings["SecretKey"]!)
        )
    };
});

builder.Services.AddAuthorization(options =>
{
    // Project Management Permissions
    options.AddPolicy(Permission.Project.Create, policy => policy.RequireClaim("permission", Permission.Project.Create));
    options.AddPolicy(Permission.Project.Read, policy => policy.RequireClaim("permission", Permission.Project.Read));
    options.AddPolicy(Permission.Project.Update, policy => policy.RequireClaim("permission", Permission.Project.Update));
    options.AddPolicy(Permission.Project.Delete, policy => policy.RequireClaim("permission", Permission.Project.Delete));
    options.AddPolicy(Permission.Project.AssignMember, policy => policy.RequireClaim("permission", Permission.Project.AssignMember));
    options.AddPolicy(Permission.Project.ManageBudget, policy => policy.RequireClaim("permission", Permission.Project.ManageBudget));

    // Task Management Permissions (demonstrating granularity)
    options.AddPolicy(Permission.Task.Create, policy => policy.RequireClaim("permission", Permission.Task.Create));
    options.AddPolicy(Permission.Task.Read, policy => policy.RequireClaim("permission", Permission.Task.Read));
    options.AddPolicy(Permission.Task.Update, policy => policy.RequireClaim("permission", Permission.Task.Update));
    options.AddPolicy(Permission.Task.Delete, policy => policy.RequireClaim("permission", Permission.Task.Delete));
    options.AddPolicy(Permission.Task.AssignUser, policy => policy.RequireClaim("permission", Permission.Task.AssignUser));
    options.AddPolicy(Permission.Task.UpdateStatus, policy => policy.RequireClaim("permission", Permission.Task.UpdateStatus));
    options.AddPolicy(Permission.Task.Comment, policy => policy.RequireClaim("permission", Permission.Task.Comment));
});

var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
