using AutoMapper;
using MediatR;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using FluentValidation;

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

    /// <summary>
    /// Initializes a new instance of the handler with required dependencies
    /// </summary>
    public CreateSaleHandler(
        ISaleRepository saleRepository,
        IProductRepository productRepository,
        IMapper mapper,
        CreateSaleCommandValidator validator)
    {
        _saleRepository = saleRepository;
        _productRepository = productRepository;
        _mapper = mapper;
        _validator = validator;
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
            Number = $"SALE-{DateTime.UtcNow:yyyyMMddHHmmss}",
            BranchId = request.BranchId,
            Items = new List<SaleItem>()
        };

        foreach (var requestItem in request.Items)
        {
            var product = await _productRepository.GetByIdAsync(requestItem.ProductId, cancellationToken);
            if (product == null)
                throw new InvalidOperationException($"Product not found: {requestItem.ProductId}");

            var saleItem = new SaleItem
            {
                ProductId = requestItem.ProductId,
                Quantity = requestItem.Quantity,
                UnitPrice = product.Price,
                CreatedAt = DateTime.UtcNow
            };

            saleItem.CalculateDiscount();
            sale.Items.Add(saleItem);
        }

        sale.CalculateTotals();
        await _saleRepository.AddAsync(sale, cancellationToken);
        await _saleRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CreateSaleResult>(sale);
    }
} 