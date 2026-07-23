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
            .Accepts<CreateProjectRequest>("application/json")
            .Produces<ProjectResponse>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Creates a new project for the current user.")
            .WithDescription("Creates a new project. Requires 'project:create' permission.")
            .WithName("CreateProjectV1")
            .WithTags("Projects")
            .MapToApiVersion(1);

        group.MapGet("", GetProjects)
            .RequireAuthorization(Permission.Project.Read)
            .Produces<List<ProjectResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Retrieves all projects.")
            .WithDescription("Returns all projects owned or accessible by the user.")
            .WithName("GetProjectsV1")
            .WithTags("Projects")
            .MapToApiVersion(1);

        group.MapGet("", GetProjectsV2)
            .RequireAuthorization(Permission.Project.Read)
            .Produces<List<ProjectResponse>>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Retrieves all projects with currency info.")
            .WithDescription("Returns all projects with the currency field set. Requires 'project:read' permission.")
            .WithName("GetProjectsV2")
            .WithTags("Projects")
            .MapToApiVersion(2);

        group.MapGet("{projectId:guid}", GetProject)
            .RequireAuthorization(Permission.Project.Read)
            .Produces<ProjectResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Gets a specific project.")
            .WithDescription("Fetches a single project by ID. Requires 'project:read' permission.")
            .WithName("GetProjectV1")
            .WithTags("Projects")
            .MapToApiVersion(1);

        group.MapGet("{projectId:guid}", GetProjectV2)
            .RequireAuthorization(Permission.Project.Read)
            .Produces<ProjectResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Gets a specific project with currency info.")
            .WithDescription("Fetches a single project by ID and adds currency info. Requires 'project:read' permission.")
            .WithName("GetProjectV2")
            .WithTags("Projects")
            .MapToApiVersion(2);

        group.MapPut("{projectId:guid}", UpdateProject)
            .RequireAuthorization(Permission.Project.Update)
            .AddEndpointFilter<ValidationFilter<UpdateProjectRequest>>()
            .Accepts<UpdateProjectRequest>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Updates an existing project.")
            .WithDescription("Updates project details. Requires 'project:update' permission.")
            .WithName("UpdateProjectV1")
            .WithTags("Projects")
            .MapToApiVersion(1);

        group.MapDelete("{projectId:guid}", DeleteProject)
            .RequireAuthorization(Permission.Project.Delete)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Deletes a project.")
            .WithDescription("Deletes a specific project. Requires 'project:delete' permission.")
            .WithName("DeleteProjectV1")
            .WithTags("Projects")
            .MapToApiVersion(1);

        group.MapPut("{projectId:guid}/budget", UpdateBudget)
            .RequireAuthorization(Permission.Project.ManageBudget)
            .AddEndpointFilter<ValidationFilter<UpdateBudgetRequest>>()
            .Accepts<UpdateBudgetRequest>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Updates project budget.")
            .WithDescription("Modifies the project budget. Requires 'project:managebudget' permission.")
            .WithName("UpdateBudgetV1")
            .WithTags("Projects")
            .MapToApiVersion(1);

        group.MapPut("{projectId:guid}/completion", EndProject)
            .RequireAuthorization(Permission.Project.Update)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Marks the project as completed.")
            .WithDescription("Sets the project status to completed. Requires 'project:update' permission.")
            .WithName("EndProjectV1")
            .WithTags("Projects")
            .MapToApiVersion(1);

        group.MapPost("{projectId:guid}/tasks", CreateTask)
            .RequireAuthorization(Permission.Task.Create)
            .AddEndpointFilter<ValidationFilter<CreateTaskRequest>>()
            .Accepts<CreateTaskRequest>("application/json")
            .Produces<ProjectTaskResponse>(StatusCodes.Status201Created)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Creates a task in the project.")
            .WithDescription("Adds a new task to the specified project. Requires 'task:create' permission.")
            .WithName("CreateTaskV1")
            .WithTags("Tasks")
            .MapToApiVersion(1);

        group.MapGet("{projectId:guid}/tasks/{taskId:guid}", GetTask)
            .RequireAuthorization(Permission.Task.Read)
            .Produces<ProjectTaskResponse>(StatusCodes.Status200OK)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Gets a task from a project.")
            .WithDescription("Fetches a single task from a specific project. Requires 'task:read' permission.")
            .WithName("GetTaskV1")
            .WithTags("Tasks")
            .MapToApiVersion(1);

        group.MapPut("{projectId:guid}/tasks/{taskId:guid}/status", UpdateTaskStatus)
            .RequireAuthorization(Permission.Task.UpdateStatus)
            .AddEndpointFilter<ValidationFilter<UpdateTaskStatusRequest>>()
            .Accepts<UpdateTaskStatusRequest>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Updates a task status.")
            .WithDescription("Modifies the status of a task. Requires 'task:updatestatus' permission.")
            .WithName("UpdateTaskStatusV1")
            .WithTags("Tasks")
            .MapToApiVersion(1);

        group.MapPut("{projectId:guid}/tasks/{taskId:guid}", UpdateTask)
            .RequireAuthorization(Permission.Task.Update)
            .AddEndpointFilter<ValidationFilter<UpdateTaskRequest>>()
            .Accepts<UpdateTaskRequest>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Updates a task.")
            .WithDescription("Updates task details. Requires 'task:update' permission.")
            .WithName("UpdateTaskV1")
            .WithTags("Tasks")
            .MapToApiVersion(1);

        group.MapPut("{projectId:guid}/tasks/{taskId:guid}/assignment", AssignUser)
            .RequireAuthorization(Permission.Task.AssignUser)
            .AddEndpointFilter<ValidationFilter<AssignUserToTaskRequest>>()
            .Accepts<AssignUserToTaskRequest>("application/json")
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status400BadRequest)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Assigns a user to a task.")
            .WithDescription("Assigns a user to a task. Requires 'task:assignuser' permission.")
            .WithName("AssignUserToTaskV1")
            .WithTags("Tasks")
            .MapToApiVersion(1);

        group.MapDelete("{projectId:guid}/tasks/{taskId:guid}", DeleteTask)
            .RequireAuthorization(Permission.Task.Delete)
            .Produces(StatusCodes.Status204NoContent)
            .Produces<ProblemDetails>(StatusCodes.Status403Forbidden)
            .Produces<ProblemDetails>(StatusCodes.Status404NotFound)
            .Produces<ProblemDetails>(StatusCodes.Status500InternalServerError)
            .WithSummary("Deletes a task.")
            .WithDescription("Deletes a task from the project. Requires 'task:delete' permission.")
            .WithName("DeleteTaskV1")
            .WithTags("Tasks")
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
