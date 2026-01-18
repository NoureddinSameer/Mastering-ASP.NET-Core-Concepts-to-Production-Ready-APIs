using System.ComponentModel.DataAnnotations;

namespace M02.MinimalDataAnnotations.Validators;

public static class WarrantyValidator
{
    public static ValidationResult? MustBe12_24_36(int value, ValidationContext context)
    {
        return value == 0 || value == 12 || value == 24 || value == 36
            ? ValidationResult.Success
            : new ValidationResult("Warranty must be 0, 12, 24, or 36 months only.");
    }
}