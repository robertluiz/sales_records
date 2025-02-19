using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

namespace Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;

/// <summary>
/// Handler to process the update sale command
/// </summary>
public class UpdateSaleHandler : IRequestHandler<UpdateSaleCommand, UpdateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly UpdateSaleCommandValidator _validator;

    /// <summary>
    /// Initializes a new instance of the handler with required dependencies
    /// </summary>
    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        IBranchRepository branchRepository,
        IProductRepository productRepository,
        IMapper mapper,
        UpdateSaleCommandValidator validator)
    {
        _saleRepository = saleRepository;
        _branchRepository = branchRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _validator = validator;
    }

    /// <summary>
    /// Processes the update sale command
    /// </summary>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var sale = await _saleRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new InvalidOperationException("Sale not found");

        if (sale.IsCancelled)
            throw new InvalidOperationException("Cannot update cancelled sale");

        var branch = await _branchRepository.GetByIdAsync(request.BranchId, cancellationToken);
        if (branch == null)
            throw new InvalidOperationException($"Branch not found: {request.BranchId}");

        sale.BranchId = request.BranchId;
        sale.SaleDate = request.SaleDate;

        if (request.Items != null)
        {
            foreach (var requestItem in request.Items)
            {
                var existingItem = sale.Items.FirstOrDefault(i => i.Id == requestItem.Id);
                if (existingItem == null)
                    throw new InvalidOperationException($"Item not found in sale: {requestItem.Id}");

                if (existingItem.IsCancelled)
                    throw new InvalidOperationException($"Cannot update cancelled item: {requestItem.Id}");

                var product = await _productRepository.GetByIdAsync(requestItem.ProductId, cancellationToken);
                if (product == null)
                    throw new InvalidOperationException($"Product not found: {requestItem.ProductId}");

                existingItem.ProductId = requestItem.ProductId;
                existingItem.Quantity = requestItem.Quantity;
                existingItem.UnitPrice = product.Price;
                existingItem.CalculateDiscount();
            }
        }

        sale.CalculateTotals();
        await _saleRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UpdateSaleResult>(sale);
    }
} 