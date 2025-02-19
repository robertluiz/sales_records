using MediatR;
using AutoMapper;
using FluentValidation;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Application.Sales.CancelSale;

/// <summary>
/// Handler to process the cancel sale command
/// </summary>
public class CancelSaleHandler : IRequestHandler<CancelSaleCommand, CancelSaleResult>
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly IEventService _eventService;
    private readonly CancelSaleCommandValidator _validator;

    /// <summary>
    /// Initializes a new instance of the handler with required dependencies
    /// </summary>
    public CancelSaleHandler(
        ISaleRepository saleRepository,
        IMapper mapper,
        IEventService eventService,
        CancelSaleCommandValidator validator)
    {
        _saleRepository = saleRepository;
        _mapper = mapper;
        _eventService = eventService;
        _validator = validator;
    }

    /// <summary>
    /// Processes the cancel sale command
    /// </summary>
    public async Task<CancelSaleResult> Handle(CancelSaleCommand request, CancellationToken cancellationToken)
    {
        var sale = await _saleRepository.GetByIdAsync(request.Id, cancellationToken);
        if (sale == null)
            throw new InvalidOperationException("Sale not found");

        if (sale.IsCancelled)
            throw new InvalidOperationException("Sale is already cancelled");

        await _validator.ValidateAndThrowAsync(request, cancellationToken);

        sale.Cancel();

        var @event = new SaleCancelledEvent
        {
            Id = sale.Id,
            BranchId = sale.BranchId,
            RefundAmount = sale.TotalAmount,
            CancelledAt = DateTime.UtcNow
        };

        await _eventService.PublishSaleCancelledEvent(@event);
        await _saleRepository.SaveChangesAsync(cancellationToken);

        return _mapper.Map<CancelSaleResult>(sale);
    }
} 