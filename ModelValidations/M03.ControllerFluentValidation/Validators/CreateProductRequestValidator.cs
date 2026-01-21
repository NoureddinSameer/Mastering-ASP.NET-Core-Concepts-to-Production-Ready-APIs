using FluentValidation;
using M03.ControllerFluentValidation.Requests;

namespace M03.ControllerFluentValidation.Validators;

public class CreateProductRequestValidator : AbstractValidator<CreateProductRequest>
{
    public CreateProductRequestValidator()
    {
        // Validation logic
        RuleFor(x => x.Name)
        .NotEmpty().WithMessage("Product name is required.")
        .Length(3, 255).WithMessage("Product name must be between 3 and 255 characters.");

        RuleFor(x => x.Description)
          .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

        RuleFor(x => x.SKU)
            .NotEmpty().WithMessage("SKU is required.")
            .Matches(@"^PRD-\d{5}$").WithMessage("SKU must be 'PRD-' followed by 5 digits 'PRD-XXXXX'.");

        RuleFor(x => x.Price)
            .GreaterThan(0).WithMessage("Price must be at least 0.01.");

        RuleFor(x => x.StockQuantity)
            .GreaterThanOrEqualTo(0).WithMessage("Stock quantity must be a non-negative integer.");

        RuleFor(x => x.LaunchDate)
        .Must(BeTodayOrFuture)
        .WithMessage("Launch date must be today or in the future.");

        RuleFor(x => x.Category)
            .IsInEnum().WithMessage("Invalid product category.");

        RuleFor(x => x.ImageUrl)
            .Must(BeValidUrl).When(x => !string.IsNullOrWhiteSpace(x.ImageUrl))
            .WithMessage("ImageUrl must be a valid URL.");

        RuleFor(x => x.Weight)
            .InclusiveBetween(0.01m, 1000m).WithMessage("Weight must be between 0.01 and 1000 kg.");

        RuleFor(x => x.WarrantyPeriodMonths)
            .Must(MustBe12_24_36).WithMessage("Warranty must be 0, 12, 24, or 36 months only.");

        When(x => x.IsReturnable, () =>
        {
            RuleFor(x => x.ReturnPolicyDescription)
                .NotEmpty().WithMessage("Return policy description is required if the product is returnable.");
        });

        RuleFor(x => x.Tags)
            .Must(tags => tags.Count <= 5).WithMessage("A maximum of 5 tags is allowed.");
    }

    private bool BeTodayOrFuture(DateTime datetime)
        => datetime.Date >= DateTime.UtcNow.Date;

    private bool MustBe12_24_36(int value)
      => value == 0 || value == 12 || value == 24 || value == 36;

    private bool BeValidUrl(string? url)
        => Uri.TryCreate(url, UriKind.Absolute, out _);
}