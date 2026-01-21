using System.ComponentModel.DataAnnotations;

namespace M03.ControllerFluentValidation.Validators;

public class RequiredIfAttribute : ValidationAttribute
{
    // As an example:
    // _dependentProperty represents the name of property:IsReturnable
    // _targetValue means: if IsReturnable is true then ReturnPolicyDescription is required
    private readonly string _dependentProperty;
    private readonly object? _targetValue;

    // constructor
    public RequiredIfAttribute(string dependentProperty, object? targetValue)
    {
        _dependentProperty = dependentProperty;
        _targetValue = targetValue;
    }
    // value represents the value of the property: ReturnPolicyDescription
    protected override ValidationResult? IsValid(object? value, ValidationContext validationContext)
    {
        // Get the type of the whole current object so the containerType will be CreateProductRequest
        var containerType = validationContext.ObjectInstance.GetType();

        // check if the filed(property(IsReturnable)) in the containerType(CreateProductRequest) exists
        var field = containerType.GetProperty(_dependentProperty);

        // if IsReturnable doesn't exist then return
        if (field == null)
            return new ValidationResult($"Unknown property: {_dependentProperty}");

        // get the value of IsReturnable field
        var dependentValue = field.GetValue(validationContext.ObjectInstance, null);

        // check if the value of IsReturnable field is equal to _targetValue in our case is true
        if (Equals(dependentValue, _targetValue))
        {
            // check if the value of ReturnPolicyDescription is null or if it is whitespace after convert it to string
            if (value == null || (value is string str && string.IsNullOrWhiteSpace(str)))
                return new ValidationResult(ErrorMessage ?? $"{validationContext.DisplayName} is required.");

        }

        return ValidationResult.Success;
    }
}