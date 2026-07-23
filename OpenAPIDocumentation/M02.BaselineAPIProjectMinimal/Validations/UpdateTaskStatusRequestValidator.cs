using FluentValidation;
using M02.BaselineAPIProjectMinimal.Requests;

namespace M02.BaselineAPIProjectMinimal.Validations;

public class UpdateTaskStatusRequestValidator : AbstractValidator<UpdateTaskStatusRequest>
{
    public UpdateTaskStatusRequestValidator()
    {
        RuleFor(x => x.Status).IsInEnum();
    }
}

