using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Code)
            .NotEmpty()
            .MaxLength(50)
            .WithMessage("Product code is required and cannot be longer than 50 characters.");

        RuleFor(product => product.Name)
            .NotEmpty()
            .MaxLength(100)
            .WithMessage("Product name is required and cannot be longer than 100 characters.");

        RuleFor(product => product.Description)
            .MaxLength(500)
            .When(product => !string.IsNullOrEmpty(product.Description))
            .WithMessage("Product description cannot be longer than 500 characters.");

        RuleFor(product => product.Price)
            .GreaterThan(0)
            .WithMessage("Product price must be greater than zero.");

        RuleFor(product => product.CreatedAt)
            .NotEmpty()
            .WithMessage("Creation date is required.");

        RuleFor(product => product.UpdatedAt)
            .Must((product, updatedAt) => !updatedAt.HasValue || (updatedAt.Value > product.CreatedAt))
            .When(product => product.UpdatedAt.HasValue)
            .WithMessage("Update date must be after creation date.");
    }
} 