using System.Text.Json.Serialization;

namespace M02.BaselineAPIProjectMinimal.Enums;

/// <summary>Defines all possible states of a task in the project workflow.</summary>
public enum ProjectTaskStatus
{
    /// <summary>Task has not started yet.</summary>
    NotStarted = 0,

    /// <summary>Task is currently in progress.</summary>
    InProgress = 1,

    /// <summary>Task has been completed.</summary>
    Completed = 2,

    /// <summary>Task is blocked or on hold.</summary>
    Blocked = 3,

    /// <summary>Task has been cancelled.</summary>
    Cancelled = 4
}