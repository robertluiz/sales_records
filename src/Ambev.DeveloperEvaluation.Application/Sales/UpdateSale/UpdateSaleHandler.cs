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

    /// <summary>
    /// Initializes a new instance of the handler with required dependencies
    /// </summary>
    public UpdateSaleHandler(
        ISaleRepository saleRepository,
        IBranchRepository branchRepository,
        IProductRepository productRepository,
        IMapper mapper)
    {
        _saleRepository = saleRepository;
        _branchRepository = branchRepository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    /// <summary>
    /// Processes the update sale command
    /// </summary>
    public async Task<UpdateSaleResult> Handle(UpdateSaleCommand request, CancellationToken cancellationToken)
    {
        var validator = new UpdateSaleCommandValidator();
        await validator.ValidateAndThrowAsync(request, cancellationToken);

        var sale = await _saleRepository.GetByIdWithDetailsAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new InvalidOperationException("Sale not found");

        var branch = await _branchRepository.GetByIdAsync(request.BranchId, cancellationToken);
        if (branch == null)
            throw new InvalidOperationException("Branch not found");

        // Update basic sale data
        sale.BranchId = request.BranchId;
        sale.SaleDate = request.SaleDate;

        // Remove items that are no longer present
        var itemIdsToKeep = request.Items
            .Where(x => x.Id.HasValue)
            .Select(x => x.Id!.Value)
            .ToList();

        var itemsToRemove = sale.Items
            .Where(x => !itemIdsToKeep.Contains(x.Id))
            .ToList();

        foreach (var item in itemsToRemove)
        {
            sale.Items.Remove(item);
        }

        // Update existing items and add new ones
        foreach (var itemCommand in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(itemCommand.ProductId, cancellationToken);
            if (product == null)
                throw new InvalidOperationException($"Product not found: {itemCommand.ProductId}");

            if (itemCommand.Id.HasValue)
            {
                // Update existing item
                var existingItem = sale.Items.FirstOrDefault(x => x.Id == itemCommand.Id.Value);
                if (existingItem != null)
                {
                    existingItem.ProductId = itemCommand.ProductId;
                    existingItem.Quantity = itemCommand.Quantity;
                    existingItem.UnitPrice = itemCommand.UnitPrice;
                    existingItem.Subtotal = itemCommand.Quantity * itemCommand.UnitPrice;
                }
            }
            else
            {
                // Add new item
                var newItem = new SaleItem
                {
                    Id = Guid.NewGuid(),
                    SaleId = sale.Id,
                    ProductId = itemCommand.ProductId,
                    Quantity = itemCommand.Quantity,
                    UnitPrice = itemCommand.UnitPrice,
                    Subtotal = itemCommand.Quantity * itemCommand.UnitPrice
                };
                sale.Items.Add(newItem);
            }
        }

        // Recalculate sale total
        sale.TotalAmount = sale.Items.Sum(x => x.Subtotal);

        await _saleRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<UpdateSaleResult>(sale);
    }
} 