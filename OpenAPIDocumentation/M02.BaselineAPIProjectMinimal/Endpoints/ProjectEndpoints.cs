using System.Security.Claims;
using M02.BaselineAPIProjectMinimal.Filters;
using M02.BaselineAPIProjectMinimal.Permissions;
using M02.BaselineAPIProjectMinimal.Requests;
using M02.BaselineAPIProjectMinimal.Responses;
using M02.BaselineAPIProjectMinimal.Services;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace M02.BaselineAPIProjectMinimal.Endpoints;

public static class ProjectEndpoints
{
    public static RouteGroupBuilder MapProjectEndpoints(this IEndpointRouteBuilder app)
    {
        var group = app.MapGroup("projects")
          .WithOpenApi();

        group.MapPost("", CreateProject)
            .RequireAuthorization(Permission.Project.Create)
            .AddEndpointFilter<ValidationFilter<CreateProjectRequest>>()
            .MapToApiVersion(1);

        group.MapGet("", GetProjects)
            .RequireAuthorization(Permission.Project.Read)
            .MapToApiVersion(1);

        group.MapGet("", GetProjectsV2)
            .RequireAuthorization(Permission.Project.Read)
            .MapToApiVersion(2);

        group.MapGet("{projectId:guid}", GetProject)
            .RequireAuthorization(Permission.Project.Read)
            .MapToApiVersion(1);

        group.MapGet("{projectId:guid}", GetProjectV2)
            .RequireAuthorization(Permission.Project.Read)
            .MapToApiVersion(2);

        group.MapPut("{projectId:guid}", UpdateProject)
            .RequireAuthorization(Permission.Project.Update)
            .AddEndpointFilter<ValidationFilter<UpdateProjectRequest>>()
            .MapToApiVersion(1);

        group.MapDelete("{projectId:guid}", DeleteProject)
            .RequireAuthorization(Permission.Project.Delete)
            .MapToApiVersion(1);

        group.MapPut("{projectId:guid}/budget", UpdateBudget)
            .RequireAuthorization(Permission.Project.ManageBudget)
            .AddEndpointFilter<ValidationFilter<UpdateBudgetRequest>>()
            .MapToApiVersion(1);

        group.MapPut("{projectId:guid}/completion", EndProject)
            .RequireAuthorization(Permission.Project.Update)
            .MapToApiVersion(1);

        group.MapPost("{projectId:guid}/tasks", CreateTask)
            .RequireAuthorization(Permission.Task.Create)
            .AddEndpointFilter<ValidationFilter<CreateTaskRequest>>()
            .MapToApiVersion(1);

        group.MapGet("{projectId:guid}/tasks/{taskId:guid}", GetTask)
            .RequireAuthorization(Permission.Task.Read)
            .MapToApiVersion(1);

        group.MapPut("{projectId:guid}/tasks/{taskId:guid}/status", UpdateTaskStatus)
            .RequireAuthorization(Permission.Task.UpdateStatus)
            .AddEndpointFilter<ValidationFilter<UpdateTaskStatusRequest>>()
            .MapToApiVersion(1);

        group.MapPut("{projectId:guid}/tasks/{taskId:guid}", UpdateTask)
            .RequireAuthorization(Permission.Task.Update)
            .AddEndpointFilter<ValidationFilter<UpdateTaskRequest>>()
            .MapToApiVersion(1);

        group.MapPut("{projectId:guid}/tasks/{taskId:guid}/assignment", AssignUser)
            .RequireAuthorization(Permission.Task.AssignUser)
            .AddEndpointFilter<ValidationFilter<AssignUserToTaskRequest>>()
            .MapToApiVersion(1);

        group.MapDelete("{projectId:guid}/tasks/{taskId:guid}", DeleteTask)
            .RequireAuthorization(Permission.Task.Delete)
            .MapToApiVersion(1);

        return group;
    }

    private static Guid GetUserId(HttpContext context)
        => Guid.Parse(context.User.FindFirst(ClaimTypes.NameIdentifier)?.Value!);

    private static async Task<Created<ProjectResponse>> CreateProject(CreateProjectRequest req, IProjectService service, HttpContext ctx)
    {
        var id = GetUserId(ctx);

        var result = await service.CreateProjectAsync(req, id);

        return TypedResults.Created($"/api/v1/projects/{result.Id}", result);
    }

    private static async Task<Ok<List<ProjectResponse>>> GetProjects(IProjectService service)
        => TypedResults.Ok(await service.GetProjectsAsync());

    private static async Task<Ok<ProjectResponse>> GetProject(Guid projectId, IProjectService service)
        => TypedResults.Ok(await service.GetProjectAsync(projectId));

    private static async Task<Ok<List<ProjectResponse>>> GetProjectsV2(IProjectService service)
    {
        var projects = await service.GetProjectsAsync();
        foreach (var p in projects) p.Currency = "USD";
        return TypedResults.Ok(projects);
    }

    private static async Task<Results<Ok<ProjectResponse>, NotFound<string>>> GetProjectV2(
        Guid projectId,
        IProjectService service)
    {
        var response = await service.GetProjectAsync(projectId);

        if (response is null)
            return TypedResults.NotFound("Project was not found");

        response.Currency = "USD";

        return TypedResults.Ok(response);
    }

    private static async Task<NoContent> UpdateProject(Guid projectId, UpdateProjectRequest request, IProjectService service, HttpContext ctx)
    {
        await service.UpdateProjectAsync(projectId, request, GetUserId(ctx));

        return TypedResults.NoContent();
    }

    private static async Task<NoContent> DeleteProject(Guid projectId, IProjectService service, HttpContext ctx)
    {
        await service.DeleteProjectAsync(projectId, GetUserId(ctx));

        return TypedResults.NoContent();
    }

    private static async Task<NoContent> UpdateBudget(Guid projectId, UpdateBudgetRequest request, IProjectService service, HttpContext ctx)
    {
        await service.ManageBudgetAsync(projectId, request, GetUserId(ctx));

        return TypedResults.NoContent();
    }

    private static async Task<NoContent> EndProject(Guid projectId, IProjectService service, HttpContext ctx)
    {
        await service.EndProjectAsync(projectId, GetUserId(ctx));

        return TypedResults.NoContent();
    }

    private static async Task<Created<ProjectTaskResponse>> CreateTask(Guid projectId, CreateTaskRequest request, IProjectService service, HttpContext ctx)
    {
        var task = await service.CreateTaskAsync(projectId, request, GetUserId(ctx));

        return TypedResults.Created($"/api/v1/projects/{projectId}/tasks/{task.Id}", task);
    }

    private static async Task<Ok<ProjectTaskResponse>> GetTask(Guid projectId, Guid taskId, IProjectService service)
        => TypedResults.Ok(await service.GetTaskAsync(projectId, taskId));

    private static async Task<NoContent> UpdateTaskStatus(Guid projectId, Guid taskId, UpdateTaskStatusRequest request, IProjectService service, HttpContext ctx)
    {
        await service.UpdateTaskStatusAsync(projectId, taskId, request, GetUserId(ctx));
        return TypedResults.NoContent();
    }

    private static async Task<NoContent> UpdateTask(Guid projectId, Guid taskId, UpdateTaskRequest request, IProjectService service, HttpContext ctx)
    {
        await service.UpdateTaskAsync(projectId, taskId, request, GetUserId(ctx));
        return TypedResults.NoContent();
    }

    private static async Task<NoContent> AssignUser(Guid projectId, Guid taskId, AssignUserToTaskRequest request, IProjectService service, HttpContext ctx)
    {
        await service.AssignUserToTaskAsync(projectId, taskId, request, GetUserId(ctx));
        return TypedResults.NoContent();
    }

    private static async Task<NoContent> DeleteTask(Guid projectId, Guid taskId, IProjectService service, HttpContext ctx)
    {
        await service.DeleteTaskAsync(projectId, taskId, GetUserId(ctx));
        return TypedResults.NoContent();
    }
}
