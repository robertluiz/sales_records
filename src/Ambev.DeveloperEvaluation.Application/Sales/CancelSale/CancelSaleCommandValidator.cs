using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Validator for the cancel sale command
/// </summary>
public class CancelSaleCommandValidator : AbstractValidator<CancelSaleCommand>
{
    private readonly ISaleRepository _saleRepository;

    /// <summary>
    /// Initializes a new instance of the validator with defined rules
    /// </summary>
    public CancelSaleCommandValidator(ISaleRepository saleRepository)
    {
        _saleRepository = saleRepository;

        RuleFor(x => x.Id)
            .NotEmpty()
            .WithMessage("Sale ID is required")
            .MustAsync(SaleExistsAsync)
            .WithMessage("Sale not found")
            .MustAsync(SaleNotCancelledAsync)
            .WithMessage("Sale is already cancelled");
    }

    private async Task<bool> SaleExistsAsync(Guid id, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(id, cancellationToken);
        return sale != null;
    }

    private async Task<bool> SaleNotCancelledAsync(Guid id, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(id, cancellationToken);
        return sale != null && !sale.IsCancelled;
    }
} 