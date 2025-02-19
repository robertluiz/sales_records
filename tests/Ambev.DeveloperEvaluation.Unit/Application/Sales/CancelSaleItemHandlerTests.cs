using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="CancelSaleItemHandler"/> class.
/// </summary>
public class CancelSaleItemHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IMapper _mapper;
    private readonly CancelSaleItemCommandValidator _validator;
    private readonly CancelSaleItemHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="CancelSaleItemHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public CancelSaleItemHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _mapper = Substitute.For<IMapper>();
        _validator = new CancelSaleItemCommandValidator(_saleRepository);

        _handler = new CancelSaleItemHandler(
            _saleRepository,
            _mapper,
            _validator);
    }

    /// <summary>
    /// Tests that a valid sale item cancellation request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid cancellation data When canceling sale item Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = new CancelSaleItemCommand
        {
            SaleId = Guid.NewGuid(),
            ItemId = Guid.NewGuid()
        };

        var sale = new Sale
        {
            Id = command.SaleId,
            Items = new List<SaleItem>
            {
                new()
                {
                    Id = command.ItemId,
                    IsCancelled = false
                }
            }
        };

        var result = new CancelSaleItemResult
        {
            Id = command.ItemId,
            IsCancelled = true
        };

        _saleRepository.GetByIdWithDetailsAsync(command.SaleId, Arg.Any<CancellationToken>())
            .Returns(sale);
        _mapper.Map<CancelSaleItemResult>(Arg.Any<SaleItem>())
            .Returns(result);

        // When
        var cancelResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        cancelResult.Should().NotBeNull();
        cancelResult.Id.Should().Be(command.ItemId);
        cancelResult.IsCancelled.Should().BeTrue();
        await _saleRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale item cancellation request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid cancellation data When canceling sale item Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = new CancelSaleItemCommand();

        _saleRepository.GetByIdWithDetailsAsync(command.SaleId, Arg.Any<CancellationToken>())
            .Returns((Sale?)null);

        // When/Then
        await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    /// <summary>
    /// Tests that when sale is not found, an InvalidOperationException is thrown.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale When canceling sale item Then throws InvalidOperationException")]
    public async Task Handle_SaleNotFound_ThrowsInvalidOperationException()
    {
        // Arrange
        var command = new CancelSaleItemCommand
        {
            SaleId = Guid.NewGuid(),
            ItemId = Guid.NewGuid()
        };

        _saleRepository.GetByIdWithDetailsAsync(command.SaleId, Arg.Any<CancellationToken>())
            .Returns((Sale?)null);

        // When/Then
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    /// <summary>
    /// Tests that when item is not found in sale, an InvalidOperationException is thrown.
    /// </summary>
    [Fact(DisplayName = "Given non-existent item When canceling sale item Then throws InvalidOperationException")]
    public async Task Handle_ItemNotFound_ThrowsInvalidOperationException()
    {
        // Arrange
        var command = new CancelSaleItemCommand
        {
            SaleId = Guid.NewGuid(),
            ItemId = Guid.NewGuid()
        };

        var sale = new Sale
        {
            Id = command.SaleId,
            Items = new List<SaleItem>() // Empty items list
        };

        _saleRepository.GetByIdWithDetailsAsync(command.SaleId, Arg.Any<CancellationToken>())
            .Returns(sale);

        // When/Then
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    /// <summary>
    /// Tests that when item is already cancelled, an InvalidOperationException is thrown.
    /// </summary>
    [Fact(DisplayName = "Given already cancelled item When canceling sale item Then throws InvalidOperationException")]
    public async Task Handle_ItemAlreadyCancelled_ThrowsInvalidOperationException()
    {
        // Arrange
        var command = new CancelSaleItemCommand
        {
            SaleId = Guid.NewGuid(),
            ItemId = Guid.NewGuid()
        };

        var sale = new Sale
        {
            Id = command.SaleId,
            Items = new List<SaleItem>
            {
                new()
                {
                    Id = command.ItemId,
                    IsCancelled = true
                }
            }
        };

        _saleRepository.GetByIdWithDetailsAsync(command.SaleId, Arg.Any<CancellationToken>())
            .Returns(sale);

        // When/Then
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
} 