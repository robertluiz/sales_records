using Ambev.DeveloperEvaluation.Application.Sales.CreateSale;
using Ambev.DeveloperEvaluation.Application.Sales.GetSale;
using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSale;
using Ambev.DeveloperEvaluation.Application.Sales.CancelSaleItem;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using Ambev.DeveloperEvaluation.WebApi.Common;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CancelSaleFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.CreateSaleFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSaleFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.GetSaleFeature;
using Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSalesFeature;
using Microsoft.AspNetCore.Authorization;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales;

/// <summary>
/// Controller for managing sales operations
/// </summary>
[ApiController]
[Route("api/[controller]")]
[Authorize]
public class SalesController : BaseController
{
    private readonly IMediator _mediator;
    private readonly IMapper _mapper;

    /// <summary>
    /// Initializes a new instance of the SalesController
    /// </summary>
    public SalesController(IMediator mediator, IMapper mapper)
    {
        _mediator = mediator;
        _mapper = mapper;
    }

    /// <summary>
    /// Creates a new sale
    /// </summary>
    /// <param name="request">The sale creation request</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The created sale details</returns>
    [HttpPost]
    [ProducesResponseType(typeof(ApiResponseWithData<CreateSaleResponse>), StatusCodes.Status201Created)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Create([FromBody] CreateSaleRequest request, CancellationToken cancellationToken)
    {
        var validator = new CreateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<CreateSaleCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);

        return CreatedAtAction(nameof(GetById), new { id = result.Id }, 
            new ApiResponseWithData<CreateSaleResponse>
            {
                Success = true,
                Message = "Sale created successfully",
                Data = _mapper.Map<CreateSaleResponse>(result)
            });
    }

    /// <summary>
    /// Gets a sale by its ID
    /// </summary>
    /// <param name="id">The sale ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The sale details</returns>
    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<GetSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> GetById([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var query = new GetSaleQuery { Id = id };
        var result = await _mediator.Send(query, cancellationToken);
        return Ok(_mapper.Map<GetSaleResponse>(result), "Sale retrieved successfully");
    }

    /// <summary>
    /// Lists sales with pagination and optional filters
    /// </summary>
    /// <param name="request">The list request parameters</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>A paginated list of sales</returns>
    [HttpGet]
    [ProducesResponseType(typeof(PaginatedResponse<ListSalesFeature.SaleListItem>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> List([FromQuery] ListSalesRequest request, CancellationToken cancellationToken)
    {
        var query = _mapper.Map<ListSalesQuery>(request);
        var result = await _mediator.Send(query, cancellationToken);
        var items = _mapper.Map<List<ListSalesFeature.SaleListItem>>(result.Items);
        return OkPaginated(items, result.CurrentPage, result.PageSize, result.TotalRecords, "Sales listed successfully");
    }

    /// <summary>
    /// Partially updates an existing sale
    /// </summary>
    /// <param name="id">The sale ID</param>
    /// <param name="request">The sale update data</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The updated sale details</returns>
    [HttpPatch("{id:guid}")]
    [ProducesResponseType(typeof(ApiResponseWithData<UpdateSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> Update([FromRoute] Guid id, [FromBody] UpdateSaleRequest request, CancellationToken cancellationToken)
    {
        if (id != request.Id)
            return BadRequest("Route ID does not match request ID");

        var validator = new UpdateSaleRequestValidator();
        var validationResult = await validator.ValidateAsync(request, cancellationToken);

        if (!validationResult.IsValid)
            return BadRequest(validationResult.Errors);

        var command = _mapper.Map<UpdateSaleCommand>(request);
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(_mapper.Map<UpdateSaleResponse>(result), "Sale updated successfully");
    }

    /// <summary>
    /// Cancels an entire sale
    /// </summary>
    /// <param name="id">The sale ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cancelled sale details</returns>
    [HttpPost("{id:guid}/cancel")]
    [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CancelSale([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var command = new CancelSaleCommand { Id = id };
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(_mapper.Map<CancelSaleResponse>(result), "Sale cancelled successfully");
    }

    /// <summary>
    /// Cancels a specific item in a sale
    /// </summary>
    /// <param name="id">The sale ID</param>
    /// <param name="itemId">The sale item ID</param>
    /// <param name="cancellationToken">Cancellation token</param>
    /// <returns>The cancelled item details</returns>
    [HttpPost("{id:guid}/items/{itemId:guid}/cancel")]
    [ProducesResponseType(typeof(ApiResponseWithData<CancelSaleItemResponse>), StatusCodes.Status200OK)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status400BadRequest)]
    [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status401Unauthorized)]
    public async Task<IActionResult> CancelSaleItem([FromRoute] Guid id, [FromRoute] Guid itemId, CancellationToken cancellationToken)
    {
        var command = new CancelSaleItemCommand { SaleId = id, ItemId = itemId };
        var result = await _mediator.Send(command, cancellationToken);
        return Ok(_mapper.Map<CancelSaleItemResponse>(result), "Sale item cancelled successfully");
    }
} 