using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class SaleItemValidator : AbstractValidator<SaleItem>
{
    public SaleItemValidator()
    {
        RuleFor(item => item.ProductId)
            .NotEmpty()
            .WithMessage("Product is required.");

        RuleFor(item => item.Quantity)
            .GreaterThan(0)
            .WithMessage("Quantity must be greater than zero.")
            .LessThanOrEqualTo(20)
            .WithMessage("Quantity cannot exceed 20 items.");

        RuleFor(item => item.UnitPrice)
            .GreaterThan(0)
            .WithMessage("Unit price must be greater than zero.");

        RuleFor(item => item.DiscountPercentage)
            .InclusiveBetween(0, 20)
            .WithMessage("Discount percentage must be between 0 and 20.");

        RuleFor(item => item.TotalAmount)
            .GreaterThanOrEqualTo(0)
            .WithMessage("Total amount cannot be negative.");

        RuleFor(item => item.CreatedAt)
            .NotEmpty()
            .WithMessage("Creation date is required.");

        RuleFor(item => item.CancelledAt)
            .Must((item, cancelledAt) => !cancelledAt.HasValue || (cancelledAt.Value > item.CreatedAt))
            .When(item => item.IsCancelled)
            .WithMessage("Cancellation date must be after creation date.");

        // Regra de negócio: Desconto baseado na quantidade
        RuleFor(item => item.DiscountPercentage)
            .Must((item, discount) => ValidateDiscount(item.Quantity, discount))
            .WithMessage(item => GetDiscountValidationMessage(item.Quantity));
    }

    private bool ValidateDiscount(int quantity, decimal discount)
    {
        if (quantity < 4 && discount > 0)
            return false;

        if (quantity >= 4 && quantity < 10 && discount != 10)
            return false;

        if (quantity >= 10 && quantity <= 20 && discount != 20)
            return false;

        return true;
    }

    private string GetDiscountValidationMessage(int quantity)
    {
        if (quantity < 4)
            return "Items with quantity less than 4 cannot have discount.";

        if (quantity >= 4 && quantity < 10)
            return "Items with quantity between 4 and 9 must have 10% discount.";

        return "Items with quantity between 10 and 20 must have 20% discount.";
    }
} 