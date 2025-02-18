using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Validator for the create sale command
/// </summary>
public class CreateSaleCommandValidator : AbstractValidator<CreateSaleCommand>
{
    /// <summary>
    /// Initializes a new instance of the validator with defined rules
    /// </summary>
    public CreateSaleCommandValidator()
    {
        RuleFor(x => x.BranchId)
            .NotEmpty()
            .WithMessage("Branch ID is required");

        RuleFor(x => x.SaleDate)
            .NotEmpty()
            .WithMessage("Sale date is required")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("Sale date cannot be in the future");

        RuleFor(x => x.Items)
            .NotEmpty()
            .WithMessage("Sale must have at least one item")
            .ForEach(item =>
            {
                item.SetValidator(new SaleItemCommandValidator());
            });
    }
}

/// <summary>
/// Validator for the create sale item command
/// </summary>
public class SaleItemCommandValidator : AbstractValidator<SaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the validator with defined rules
    /// </summary>
    public SaleItemCommandValidator()
    {
        RuleFor(x => x.ProductId)
            .NotEmpty()
            .WithMessage("Product ID is required");

        RuleFor(x => x.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero");

        RuleFor(x => x.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero");
    }
} 