using FluentValidation;
using M02.BaselineAPIProjectMinimal.Requests;

namespace M02.BaselineAPIProjectMinimal.Validations;

public class UpdateTaskRequestValidator : AbstractValidator<UpdateTaskRequest>
{
    public UpdateTaskRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.Status).IsInEnum();
    }
}

