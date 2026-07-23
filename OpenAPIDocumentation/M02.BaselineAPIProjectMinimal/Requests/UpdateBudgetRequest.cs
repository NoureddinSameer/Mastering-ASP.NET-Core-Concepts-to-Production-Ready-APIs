namespace M02.BaselineAPIProjectMinimal.Requests;

/// <summary>
/// Represents the request to update a project's budget.
/// </summary>
public class UpdateBudgetRequest
{
    /// <summary>
    /// Gets or sets the new budget amount for the project.
    /// </summary>
    public decimal Budget { get; set; }
}