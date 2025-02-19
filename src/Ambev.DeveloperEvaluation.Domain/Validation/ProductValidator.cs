using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class ProductValidator : AbstractValidator<Product>
{
    public ProductValidator()
    {
        RuleFor(product => product.Code)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Product code is required and cannot be longer than 50 characters.");

        RuleFor(product => product.Title)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Product title is required and cannot be longer than 100 characters.");

        RuleFor(product => product.Name)
            .NotEmpty()
            .MaximumLength(100)
            .WithMessage("Product name is required and cannot be longer than 100 characters.");

        RuleFor(product => product.Description)
            .MaximumLength(500)
            .When(product => !string.IsNullOrEmpty(product.Description))
            .WithMessage("Product description cannot be longer than 500 characters.");

        RuleFor(product => product.Category)
            .NotEmpty()
            .MaximumLength(50)
            .WithMessage("Product category is required and cannot be longer than 50 characters.");

        RuleFor(product => product.Image)
            .MaximumLength(500)
            .When(product => !string.IsNullOrEmpty(product.Image))
            .WithMessage("Product image URL cannot be longer than 500 characters.");

        RuleFor(product => product.Price)
            .GreaterThan(0)
            .WithMessage("Product price must be greater than zero.");

        When(product => product.Rating != null, () =>
        {
            RuleFor(product => product.Rating.Rate)
                .InclusiveBetween(0, 5)
                .WithMessage("Product rating must be between 0 and 5.");

            RuleFor(product => product.Rating.Count)
                .GreaterThanOrEqualTo(0)
                .WithMessage("Product rating count must be greater than or equal to zero.");
        });

        RuleFor(product => product.CreatedAt)
            .NotEmpty()
            .WithMessage("Creation date is required.");

        RuleFor(product => product.UpdatedAt)
            .Must((product, updatedAt) => !updatedAt.HasValue || (updatedAt.Value > product.CreatedAt))
            .When(product => product.UpdatedAt.HasValue)
            .WithMessage("Update date must be after creation date.");
    }
} 