using M02.BaselineAPIProjectMinimal.Enums;

namespace M02.BaselineAPIProjectMinimal.Requests;

/// <summary>
/// Represents the request to update the status of a task.
/// </summary>
public class UpdateTaskStatusRequest
{
    /// <summary>
    /// Gets or sets the new status of the task.
    /// </summary>
    public ProjectTaskStatus Status { get; set; }
}