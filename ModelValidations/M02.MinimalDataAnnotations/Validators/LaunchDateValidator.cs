using System.ComponentModel.DataAnnotations;

namespace M02.MinimalDataAnnotations.Validators;

public static class LaunchDateValidator
{
    public static ValidationResult? MustBeTodayOrFuture(DateTime datetime, ValidationContext validationContext)
    {
        if (datetime.Date >= DateTime.UtcNow.Date)
            return ValidationResult.Success;

        return new ValidationResult("Launch date must be today or in the future",
        [validationContext.MemberName ?? "LaunchDate"]);
    }
}
