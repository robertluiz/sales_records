using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Validator for the cancel sale item command
/// </summary>
public class CancelSaleItemCommandValidator : AbstractValidator<CancelSaleItemCommand>
{
    /// <summary>
    /// Initializes a new instance of the validator with defined rules
    /// </summary>
    public CancelSaleItemCommandValidator(ISaleRepository saleRepository)
    {
        RuleFor(x => x.SaleId)
            .NotEmpty()
            .WithMessage("Sale ID is required");

        RuleFor(x => x.ItemId)
            .NotEmpty()
            .WithMessage("Item ID is required");
    }
} 