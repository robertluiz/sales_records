using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides test data for <see cref="CancelSaleItemHandler"/> tests.
/// </summary>
public static class CancelSaleItemHandlerTestData
{
    private static readonly Faker<CancelSaleItemCommand> CommandFaker = new Faker<CancelSaleItemCommand>()
        .RuleFor(x => x.SaleId, (f, c) => f.Random.Guid())
        .RuleFor(x => x.ItemId, (f, c) => f.Random.Guid());

    /// <summary>
    /// Generates a valid <see cref="CancelSaleItemCommand"/> with random data.
    /// </summary>
    /// <returns>A valid command instance.</returns>
    public static CancelSaleItemCommand GenerateValidCommand()
    {
        return CommandFaker.Generate();
    }

    /// <summary>
    /// Generates a valid <see cref="CancelSaleItemCommand"/> with a specific sale ID.
    /// </summary>
    /// <param name="saleId">The sale ID to use.</param>
    /// <returns>A valid command instance with the specified sale ID.</returns>
    public static CancelSaleItemCommand GenerateValidCommandWithSaleId(Guid saleId)
    {
        return CommandFaker
            .RuleFor(x => x.SaleId, (f, c) => saleId)
            .Generate();
    }

    /// <summary>
    /// Generates a valid <see cref="CancelSaleItemCommand"/> with specific sale and item IDs.
    /// </summary>
    /// <param name="saleId">The sale ID to use.</param>
    /// <param name="itemId">The item ID to use.</param>
    /// <returns>A valid command instance with the specified IDs.</returns>
    public static CancelSaleItemCommand GenerateValidCommandWithIds(Guid saleId, Guid itemId)
    {
        return CommandFaker
            .RuleFor(x => x.SaleId, (f, c) => saleId)
            .RuleFor(x => x.ItemId, (f, c) => itemId)
            .Generate();
    }
} 