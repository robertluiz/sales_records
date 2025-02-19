using MediatR;
using AutoMapper;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;

/// <summary>
/// Handler to process the cancel sale item command
/// </summary>
public class CancelSaleItemHandler : IRequestHandler<CancelSaleItemCommand, CancelSaleItemResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IEventService _eventService;
    private readonly CancelSaleItemCommandValidator _validator;

    /// <summary>
    /// Initializes a new instance of the handler with required dependencies
    /// </summary>
    public CancelSaleItemHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        IEventService eventService,
        CancelSaleItemCommandValidator validator)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _eventService = eventService;
        _validator = validator;
    }

    /// <summary>
    /// Processes the cancel sale item command
    /// </summary>
    public async Task<CancelSaleItemResult> Handle(CancelSaleItemCommand request, CancellationToken cancellationToken)
    {
        if (request == null)
            throw new ArgumentNullException(nameof(request));

        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        var sale = await _saleRepository.GetByIdWithDetailsAsync(request.SaleId, cancellationToken);
        if (sale == null)
            throw new InvalidOperationException("Sale not found");

        var item = sale.Items.FirstOrDefault(i => i.Id == request.ItemId);
        if (item == null)
            throw new InvalidOperationException($"Item not found in sale: {request.ItemId}");

        if (item.IsCancelled)
            throw new InvalidOperationException("Item is already cancelled");

        item.Cancel();
        sale.CalculateTotals();

        var @event = new SaleItemCancelledEvent
        {
            Id = item.Id,
            SaleId = sale.Id,
            ProductId = item.ProductId,
            Quantity = item.Quantity,
            RefundAmount = item.Total,
            CancelledAt = DateTime.UtcNow
        };

        await _eventService.PublishSaleItemCancelledEvent(@event);
        await _saleRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CancelSaleItemResult>(item);
    }
} 