using FluentValidation;
using M02.BaselineAPIProjectMinimal.Requests;

namespace M02.BaselineAPIProjectMinimal.Validations;

public class CreateTaskRequestValidator : AbstractValidator<CreateTaskRequest>
{
    public CreateTaskRequestValidator()
    {
        RuleFor(x => x.Title).NotEmpty().MaximumLength(100);
        RuleFor(x => x.Description).MaximumLength(500);
        RuleFor(x => x.AssignedUserId).NotEmpty();
    }
}

