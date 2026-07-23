using FluentValidation;
using M02.BaselineAPIProjectMinimal.Requests;

namespace M02.BaselineAPIProjectMinimal.Validations;

public class AssignUserToTaskRequestValidator : AbstractValidator<AssignUserToTaskRequest>
{
    public AssignUserToTaskRequestValidator()
    {
        RuleFor(x => x.UserId).NotEmpty();
    }
}

