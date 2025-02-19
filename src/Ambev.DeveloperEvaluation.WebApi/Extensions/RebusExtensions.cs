using Rebus.Config;
using Rebus.Routing.TypeBased;
using Rebus.Transport.InMem;
using Ambev.DeveloperEvaluation.Domain.Events;
using Ambev.DeveloperEvaluation.Domain.Services;
using Ambev.DeveloperEvaluation.Services;

namespace Ambev.DeveloperEvaluation.WebApi.Extensions;

public static class RebusExtensions
{
    public static IServiceCollection AddRebusConfiguration(this IServiceCollection services)
    {
        services.AddRebus(configure => configure
            .Transport(t => t.UseInMemoryTransport(new InMemNetwork(), "sales"))
            .Routing(r => r.TypeBased()
                .Map<SaleCreatedEvent>("sales")
                .Map<SaleModifiedEvent>("sales")
                .Map<SaleCancelledEvent>("sales")
                .Map<SaleItemCancelledEvent>("sales")));

        services.AddTransient<IEventService, RebusEventService>();

        return services;
    }
} 