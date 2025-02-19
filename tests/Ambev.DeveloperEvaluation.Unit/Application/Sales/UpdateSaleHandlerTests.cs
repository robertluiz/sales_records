using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.Repositories;
using Ambev.DeveloperEvaluation.Unit.Application.TestData;
using AutoMapper;
using FluentAssertions;
using FluentValidation;
using NSubstitute;
using Xunit;

namespace Ambev.DeveloperEvaluation.Unit.Application.Sales;

/// <summary>
/// Contains unit tests for the <see cref="UpdateSaleHandler"/> class.
/// </summary>
public class UpdateSaleHandlerTests
{
    private readonly ISaleRepository _saleRepository;
    private readonly IBranchRepository _branchRepository;
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;
    private readonly UpdateSaleCommandValidator _validator;
    private readonly UpdateSaleHandler _handler;

    /// <summary>
    /// Initializes a new instance of the <see cref="UpdateSaleHandlerTests"/> class.
    /// Sets up the test dependencies.
    /// </summary>
    public UpdateSaleHandlerTests()
    {
        _saleRepository = Substitute.For<ISaleRepository>();
        _branchRepository = Substitute.For<IBranchRepository>();
        _productRepository = Substitute.For<IProductRepository>();
        _mapper = Substitute.For<IMapper>();
        _validator = Substitute.For<UpdateSaleCommandValidator>();

        _handler = new UpdateSaleHandler(
            _saleRepository,
            _branchRepository,
            _productRepository,
            _mapper,
            _validator);
    }

    /// <summary>
    /// Tests that a valid sale update request is handled successfully.
    /// </summary>
    [Fact(DisplayName = "Given valid update data When updating sale Then returns success response")]
    public async Task Handle_ValidRequest_ReturnsSuccessResponse()
    {
        // Arrange
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = command.Id,
            Items = command.Items.Select(i => new SaleItem
            {
                Id = i.Id!.Value,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                IsCancelled = false
            }).ToList()
        };
        var branch = new Branch { Id = command.BranchId };
        var result = new UpdateSaleResult { Id = command.Id };

        _saleRepository.GetByIdWithDetailsAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(sale);
        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>())
            .Returns(branch);
        _productRepository.GetByIdAsync(Arg.Any<int>(), Arg.Any<CancellationToken>())
            .Returns(new Product { Id = 1, Price = 10.0m });
        _mapper.Map<UpdateSaleResult>(Arg.Any<Sale>()).Returns(result);

        // When
        var updateResult = await _handler.Handle(command, CancellationToken.None);

        // Then
        updateResult.Should().NotBeNull();
        updateResult.Id.Should().Be(command.Id);
        await _saleRepository.Received(1).SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that an invalid sale update request throws a validation exception.
    /// </summary>
    [Fact(DisplayName = "Given invalid update data When updating sale Then throws validation exception")]
    public async Task Handle_InvalidRequest_ThrowsValidationException()
    {
        // Arrange
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var validationException = new ValidationException("Validation failed");
        var sale = new Sale { Id = command.Id };
        var branch = new Branch { Id = command.BranchId };

        if (command.Items != null)
        {
            foreach (var item in command.Items)
            {
                sale.Items.Add(new SaleItem { Id = item.Id.GetValueOrDefault() });
                var product = new Product { Id = item.ProductId };
                _productRepository.GetByIdAsync(item.ProductId, Arg.Any<CancellationToken>())
                    .Returns(product);
            }
        }

        _saleRepository.GetByIdWithDetailsAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(sale);
            
        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>())
            .Returns(branch);

        _validator.WhenForAnyArgs(x => x.ValidateAndThrowAsync(default!, default))
            .Throw(validationException);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ValidationException>(() =>
            _handler.Handle(command, CancellationToken.None));

        Assert.Same(validationException, exception);
        await _saleRepository.DidNotReceive().SaveChangesAsync(Arg.Any<CancellationToken>());
    }

    /// <summary>
    /// Tests that when sale is not found, an InvalidOperationException is thrown.
    /// </summary>
    [Fact(DisplayName = "Given non-existent sale When updating sale Then throws InvalidOperationException")]
    public async Task Handle_SaleNotFound_ThrowsInvalidOperationException()
    {
        // Arrange
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();

        _saleRepository.GetByIdWithDetailsAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns((Sale?)null);

        // When/Then
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    /// <summary>
    /// Tests that when item is not found in sale, an InvalidOperationException is thrown.
    /// </summary>
    [Fact(DisplayName = "Given non-existent item When updating sale Then throws InvalidOperationException")]
    public async Task Handle_ItemNotFound_ThrowsInvalidOperationException()
    {
        // Arrange
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = command.Id,
            Items = new List<SaleItem>() // Empty items list
        };

        _saleRepository.GetByIdWithDetailsAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(sale);
        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>())
            .Returns(new Branch { Id = command.BranchId });

        // When/Then
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }

    /// <summary>
    /// Tests that when item is cancelled, an InvalidOperationException is thrown.
    /// </summary>
    [Fact(DisplayName = "Given cancelled item When updating sale Then throws InvalidOperationException")]
    public async Task Handle_CancelledItem_ThrowsInvalidOperationException()
    {
        // Arrange
        var command = UpdateSaleHandlerTestData.GenerateValidCommand();
        var sale = new Sale
        {
            Id = command.Id,
            Items = command.Items.Select(i => new SaleItem
            {
                Id = i.Id!.Value,
                ProductId = i.ProductId,
                Quantity = i.Quantity,
                UnitPrice = i.UnitPrice,
                IsCancelled = true
            }).ToList()
        };

        _saleRepository.GetByIdWithDetailsAsync(command.Id, Arg.Any<CancellationToken>())
            .Returns(sale);
        _branchRepository.GetByIdAsync(command.BranchId, Arg.Any<CancellationToken>())
            .Returns(new Branch { Id = command.BranchId });

        // When/Then
        await Assert.ThrowsAsync<InvalidOperationException>(() =>
            _handler.Handle(command, CancellationToken.None));
    }
} 