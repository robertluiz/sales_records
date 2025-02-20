using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale;

/// <summary>
/// Handler to process the create sale command
/// </summary>
public class CreateSaleHandler : IRequestHandler<CreateSaleCommand, CreateSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly CreateSaleCommandValidator _validator;
    private readonly IEventService _eventService;

    /// <summary>
    /// Initializes a new instance of the handler with required dependencies
    /// </summary>
    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IProductRepository productRepository,
        IMapper mapper,
        CreateSaleCommandValidator validator,
        IEventService eventService)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _validator = validator;
        _eventService = eventService;
    }

    /// <summary>
    /// Processes the create sale command
    /// </summary>
    public async Task<CreateSaleResult> Handle(CreateSaleCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        if (request.Items == null || !request.Items.Any())
            throw new ValidationException("At least one item is required");

        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var sale = new Sale
        {
            CreatedAt = DateTime.UtcNow,
            SaleDate = request.SaleDate,
            Number = $"SALE-{DateTime.UtcNow:yyyyMMddHHmmss}",
            BranchId = request.BranchId,
            CustomerId = request.CustomerId,
            Items = new List<SaleItem>()
        };

        // Agrupar itens por ProductId e somar suas quantidades
        var groupedItems = request.Items
            .GroupBy(item => item.ProductId)
            .Select(group => new
            {
                ProductId = group.Key,
                TotalQuantity = group.Sum(item => item.Quantity)
            });

        foreach (var groupedItem in groupedItems)
        {
            var product = await _productRepository.GetByIdAsync(groupedItem.ProductId, cancellationToken);
            if (product == null)
                throw new InvalidOperationException($"Product not found: {groupedItem.ProductId}");

            // Validar a quantidade total após o agrupamento
            if (groupedItem.TotalQuantity > 20)
                throw new ValidationException($"Total quantity for product {groupedItem.ProductId} exceeds maximum limit of 20");

            var saleItem = new SaleItem
            {
                ProductId = groupedItem.ProductId,
                Quantity = groupedItem.TotalQuantity,
                UnitPrice = product.Price,
                CreatedAt = DateTime.UtcNow
            };

            saleItem.CalculateDiscount();
            sale.Items.Add(saleItem);
        }

        sale.CalculateTotals();
        await _saleRepository.AddAsync(sale, cancellationToken);
        await _saleRepository.SaveChangesAsync(cancellationToken);

        var @event = new SaleCreatedEvent
        {
            Id = sale.Id,
            BranchId = sale.BranchId,
            CustomerId = sale.CustomerId,
            SaleDate = sale.SaleDate,
            TotalAmount = sale.TotalAmount,
            ItemCount = sale.Items.Count,
            CreatedAt = DateTime.UtcNow
        };

        await _eventService.PublishSaleCreatedEvent(@event);

        return _mapper.Map<CreateSaleResult>(sale);
    }
} 