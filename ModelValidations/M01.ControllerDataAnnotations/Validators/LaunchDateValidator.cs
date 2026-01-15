using System.ComponentModel.DataAnnotations;

namespace M01.ControllerDataAnnotations.Validators;

public static class LaunchDateValidator
{
    public static ValidationResult? MustBeTodayOrFuture(DateTime datetime)
    {
        if (datetime.Date >= DateTime.UtcNow.Date)
            return ValidationResult.Success;

        return new ValidationResult("Launch date must be today or in the future");
    }
}
