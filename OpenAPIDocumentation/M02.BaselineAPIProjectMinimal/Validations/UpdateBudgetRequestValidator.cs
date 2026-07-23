using FluentValidation;
using M02.BaselineAPIProjectMinimal.Requests;

namespace M02.BaselineAPIProjectMinimal.Validations;

public class UpdateBudgetRequestValidator : AbstractValidator<UpdateBudgetRequest>
{
    public UpdateBudgetRequestValidator()
    {
        RuleFor(x => x.Budget).GreaterThanOrEqualTo(0);
    }
}

