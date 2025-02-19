using System.Threading.Tasks;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Services;

public interface IEventService
{
    Task PublishSaleCreatedEvent(SaleCreatedEvent @event);
    Task PublishSaleModifiedEvent(SaleModifiedEvent @event);
    Task PublishSaleCancelledEvent(SaleCancelledEvent @event);
    Task PublishSaleItemCancelledEvent(SaleItemCancelledEvent @event);
} 