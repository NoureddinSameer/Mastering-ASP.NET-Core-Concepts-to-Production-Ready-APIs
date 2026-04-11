using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace M03.SecureRESTAPIWithJWTAuthentication.Controllers;

[ApiController]
[Route("api/projects")]
public class ProjectsController : ControllerBase
{

    [HttpGet]
    public IActionResult GetProjects()
    {
        return Ok(new
        {
            Message = "Read List of project",
            UserInfo = GetUserInfo(),
            Permission = "project:read"
        });
    }


    [HttpGet("{id}")]
    public IActionResult GetProjectById(string id)
    {
        return Ok(new
        {
            Message = $"Read a single project with id = `{id}`",
            UserInfo = GetUserInfo(),
            Permission = "project:read"
        });
    }

    [HttpPost]
    public IActionResult CreateProject()
    {
        return CreatedAtAction(
            nameof(GetProjectById),
            new { id = Guid.NewGuid() },
            new
            {
                Message = "Project created successfully",
                UserInfo = GetUserInfo(),
                Permission = "project:create"
            });
    }

    [HttpPut("{id}")]
    public IActionResult UpdateProject(string id)
    {
        return Ok(new
        {
            Message = $"Project with Id = '{id}' was updated successfully",
            UserInfo = GetUserInfo(),
            Permission = "project:update"
        });
    }


    [HttpDelete("{id}")]
    public IActionResult DeleteProject(string id)
    {
        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;

        return Ok(new
        {
            Message = $"Project with Id = '{id}' was Deleted successfully",
            UserInfo = GetUserInfo(),
            Permission = "project:delete"
        });
    }

    [HttpPost("{id}/members")]
    public IActionResult AssignMember(string id)
    {
        return CreatedAtAction(
            nameof(GetProjectById),
            new { id },
            new
            {
                Message = $"A memeber has been assigned successfully to project '{id}'",
                UserInfo = GetUserInfo(),
                Permission = "project:assign_member"
            }
        );
    }

    [HttpGet("{id}/budget")]
    public IActionResult GetProjectBudget(string id)
    {
        return Ok(new
        {
            Message = $"Successfully accessed the budget for project '{id}'",
            UserInfo = GetUserInfo(),
            Permission = "project:manage_budget"
        });
    }

    [HttpGet("tasks/{taskId}")]
    public IActionResult GetTask(string taskId)
    {
        return Ok(new
        {
            Message = $"Task '{taskId}' details retrieved",
            UserInfo = GetUserInfo(),
            Permission = "task:read"
        });
    }

    [HttpPost("tasks")]
    public IActionResult CreateTask()
    {
        var taskId = Guid.NewGuid().ToString();

        return Created($"/api/projects/tasks/{taskId}", new
        {
            Message = $"Task '{taskId}' created successfully",
            UserInfo = GetUserInfo(),
            Permission = "task:create"
        });
    }

    [HttpPost("tasks/{taskId}/assign")]
    public IActionResult AssignUserToTask(string taskId)
    {
        return Ok(new
        {
            Message = $"User assigned to task '{taskId}' successfully",
            UserInfo = GetUserInfo(),
            Permission = "task:assign_user"
        });
    }

    [HttpPut("tasks/{taskId}")]
    public IActionResult UpdateTask(string taskId)
    {
        return Ok(new
        {
            Message = $"Task '{taskId}' updated successfully",
            UserInfo = GetUserInfo(),
            Permission = "task:update"
        });
    }

    [HttpDelete("tasks/{taskId}")]
    public IActionResult DeleteTask(string taskId)
    {
        return Ok(new
        {
            Message = $"Task '{taskId}' deleted successfully",
            UserInfo = GetUserInfo(),
            Permission = "task:delete"
        });
    }


    [HttpPut("tasks/{taskId}/status")]
    public IActionResult UpdateTaskStatus(string taskId)
    {
        return Ok(new
        {
            Message = $"Successfully updated status for task '{taskId}'",
            UserInfo = GetUserInfo(),
            Permission = "task:update_status"
        });
    }


    private string GetUserInfo()
    {
        if (User.Identity is { IsAuthenticated: false })
            return "Anonymous";

        var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        var firstName = User.FindFirstValue(ClaimTypes.GivenName);
        var lastName = User.FindFirstValue(ClaimTypes.Surname);

        return $"[{userId}] {firstName} {lastName}";
    }
}