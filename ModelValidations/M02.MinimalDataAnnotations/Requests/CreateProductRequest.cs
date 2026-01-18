using System.ComponentModel.DataAnnotations;
using M02.MinimalDataAnnotations.Enums;
using M02.MinimalDataAnnotations.Validators;

namespace M02.MinimalDataAnnotations.Requests;

public class CreateProductRequest
{
    [Required(ErrorMessage = "Product Name is required")]
    [StringLength(255, MinimumLength = 3, ErrorMessage = "Product name must be between 3 and 255 characters.")]
    public string? Name { get; set; }

    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
    public string? Description { get; set; }

    [Required(ErrorMessage = "SKU is required.")]
    [RegularExpression(@"^PRD-\d{5}$", ErrorMessage = "SKU must be 'PRD-' followed by 5 digits 'PRD-XXXXX'.")]
    public string? SKU { get; set; }

    [Range(0.01, double.MaxValue, ErrorMessage = "Price must be at least 0.01.")]
    public decimal Price { get; set; }

    [Range(0, int.MaxValue, ErrorMessage = "Integer Stock quantity value required..")]
    public int StockQuantity { get; set; }

    [Required(ErrorMessage = "Launch date is required.")]
    [CustomValidation(typeof(LaunchDateValidator), nameof(LaunchDateValidator.MustBeTodayOrFuture))]
    public DateTime LaunchDate { get; set; }

    [EnumDataType(typeof(ProductCategory), ErrorMessage = "Invalid product category.")]
    public ProductCategory Category { get; set; }

    [Url(ErrorMessage = "ImageUrl must be a valid URL.")]
    public string? ImageUrl { get; set; }

    [Range(0.01, 1000, ErrorMessage = "Weight must be between 0.01 and 1000 kg.")]
    public decimal Weight { get; set; }

    [Range(0, 36, ErrorMessage = "Warranty must be 0, 12, 24, or 36 months.")]
    [CustomValidation(typeof(WarrantyValidator), nameof(WarrantyValidator.MustBe12_24_36))]
    public int WarrantyPeriodMonths { get; set; }
    public bool IsReturnable { get; set; }

    [RequiredIf("IsReturnable", true, ErrorMessage = "Return policy description is required if the product is returnable.")]
    public string? ReturnPolicyDescription { get; set; }

    [MaxLength(5, ErrorMessage = "A maximum of 5 tags is allowed.")]
    public List<string> Tags { get; set; } = new();
}

