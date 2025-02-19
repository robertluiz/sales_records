using Microsoft.Extensions.Logging;
using Rebus.Bus;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Services;

namespace Ambev.DeveloperEvaluation.Services;

public class RebusEventService : IEventService
{
    private readonly IBus _bus;
    private readonly ILogger<RebusEventService> _logger;

    public RebusEventService(IBus bus, ILogger<RebusEventService> logger)
    {
        _bus = bus;
        _logger = logger;
    }

    public async Task PublishSaleCreatedEvent(SaleCreatedEvent @event)
    {
        _logger.LogInformation("Publicando SaleCreatedEvent: {SaleId}", @event.Id);
        await _bus.Publish(@event);
    }

    public async Task PublishSaleModifiedEvent(SaleModifiedEvent @event)
    {
        _logger.LogInformation("Publicando SaleModifiedEvent: {SaleId}", @event.Id);
        await _bus.Publish(@event);
    }

    public async Task PublishSaleCancelledEvent(SaleCancelledEvent @event)
    {
        _logger.LogInformation("Publicando SaleCancelledEvent: {SaleId}", @event.Id);
        await _bus.Publish(@event);
    }

    public async Task PublishSaleItemCancelledEvent(SaleItemCancelledEvent @event)
    {
        _logger.LogInformation("Publicando SaleItemCancelledEvent: {ItemId} da Venda {SaleId}", @event.Id, @event.SaleId);
        await _bus.Publish(@event);
    }
} 