using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides test data for <see cref="UpdateSaleHandler"/> tests.
/// </summary>
public static class UpdateSaleHandlerTestData
{
    private static readonly Faker<UpdateSaleItemCommand> ItemFaker = new Faker<UpdateSaleItemCommand>()
        .RuleFor(x => x.Id, (f, c) => f.Random.Guid())
        .RuleFor(x => x.ProductId, (f, c) => f.Random.Int(1, 100))
        .RuleFor(x => x.Quantity, (f, c) => f.Random.Int(1, 10))
        .RuleFor(x => x.UnitPrice, (f, c) => f.Random.Decimal(1, 1000));

    private static readonly Faker<UpdateSaleCommand> CommandFaker = new Faker<UpdateSaleCommand>()
        .RuleFor(x => x.Id, (f, c) => f.Random.Guid())
        .RuleFor(x => x.BranchId, (f, c) => f.Random.Guid())
        .RuleFor(x => x.SaleDate, (f, c) => f.Date.Recent())
        .RuleFor(x => x.Items, (f, c) => ItemFaker.Generate(f.Random.Int(1, 5)));

    /// <summary>
    /// Generates a valid <see cref="UpdateSaleCommand"/> with random data.
    /// </summary>
    /// <returns>A valid command instance.</returns>
    public static UpdateSaleCommand GenerateValidCommand()
    {
        return CommandFaker.Generate();
    }

    /// <summary>
    /// Generates a valid <see cref="UpdateSaleCommand"/> with a specific number of items.
    /// </summary>
    /// <param name="itemCount">The number of items to generate.</param>
    /// <returns>A valid command instance with the specified number of items.</returns>
    public static UpdateSaleCommand GenerateValidCommandWithItems(int itemCount)
    {
        return CommandFaker
            .RuleFor(x => x.Items, (f, c) => ItemFaker.Generate(itemCount))
            .Generate();
    }
} 