using Ambev.DeveloperEvaluation.Domain.Entities;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Domain.Validation;

public class BranchValidator : AbstractValidator<Branch>
{
    public BranchValidator()
    {
        RuleFor(branch => branch.Code)
            .NotEmpty()
            .MaxLength(50)
            .WithMessage("Branch code is required and cannot be longer than 50 characters.");

        RuleFor(branch => branch.Name)
            .NotEmpty()
            .MaxLength(100)
            .WithMessage("Branch name is required and cannot be longer than 100 characters.");

        RuleFor(branch => branch.Address)
            .MaxLength(200)
            .When(branch => !string.IsNullOrEmpty(branch.Address))
            .WithMessage("Branch address cannot be longer than 200 characters.");

        RuleFor(branch => branch.CreatedAt)
            .NotEmpty()
            .WithMessage("Creation date is required.");

        RuleFor(branch => branch.UpdatedAt)
            .Must((branch, updatedAt) => !updatedAt.HasValue || (updatedAt.Value > branch.CreatedAt))
            .When(branch => branch.UpdatedAt.HasValue)
            .WithMessage("Update date must be after creation date.");
    }
} 