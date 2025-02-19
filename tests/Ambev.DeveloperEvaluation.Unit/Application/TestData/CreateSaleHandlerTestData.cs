using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Bogus;

namespace Ambev.DeveloperEvaluation.Unit.Application.TestData;

/// <summary>
/// Provides test data for <see cref="CreateSaleHandler"/> tests.
/// </summary>
public static class CreateSaleHandlerTestData
{
    private static readonly Faker<SaleItemCommand> ItemFaker = new Faker<SaleItemCommand>()
        .RuleFor(x => x.ProductId, (f, c) => f.Random.Int(1, 100))
        .RuleFor(x => x.Quantity, (f, c) => f.Random.Int(1, 10));

    private static readonly Faker<CreateSaleCommand> CommandFaker = new Faker<CreateSaleCommand>()
        .RuleFor(x => x.BranchId, (f, c) => f.Random.Guid())
        .RuleFor(x => x.CustomerId, (f, c) => f.Random.Guid())
        .RuleFor(x => x.SaleDate, (f, c) => f.Date.Recent())
        .RuleFor(x => x.Items, (f, c) => ItemFaker.Generate(f.Random.Int(1, 5)));

    /// <summary>
    /// Generates a valid <see cref="CreateSaleCommand"/> with random data.
    /// </summary>
    /// <returns>A valid command instance.</returns>
    public static CreateSaleCommand GenerateValidCommand()
    {
        return CommandFaker.Generate();
    }

    /// <summary>
    /// Generates a valid <see cref="CreateSaleCommand"/> with a specific number of items.
    /// </summary>
    /// <param name="itemCount">The number of items to generate.</param>
    /// <returns>A valid command instance with the specified number of items.</returns>
    public static CreateSaleCommand GenerateValidCommandWithItems(int itemCount)
    {
        return CommandFaker
            .RuleFor(x => x.Items, (f, c) => ItemFaker.Generate(itemCount))
            .Generate();
    }

    /// <summary>
    /// Generates a valid sale command with a specific branch ID.
    /// </summary>
    /// <param name="branchId">The branch ID to use.</param>
    /// <returns>A valid sale command with the specified branch ID.</returns>
    public static CreateSaleCommand GenerateValidCommandWithBranch(Guid branchId)
    {
        return CommandFaker
            .RuleFor(x => x.BranchId, (f, c) => branchId)
            .Generate();
    }
} 