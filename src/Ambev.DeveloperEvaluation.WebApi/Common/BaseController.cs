using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Ambev.DeveloperEvaluation.WebApi.Common;

[Route("api/[controller]")]
[ApiController]
public class BaseController : ControllerBase
{
    protected int GetCurrentUserId() =>
            int.Parse(User.FindFirst(ClaimTypes.NameIdentifier)?.Value ?? throw new NullReferenceException());

    protected string GetCurrentUserEmail() =>
        User.FindFirst(ClaimTypes.Email)?.Value ?? throw new NullReferenceException();

    protected IActionResult Ok<T>(T data, string message = "Operation completed successfully")
    {
        if (data is ApiResponse apiResponse)
            return base.Ok(apiResponse);

        if (data is PaginatedResponse<T> paginatedResponse)
            return base.Ok(paginatedResponse);

        return base.Ok(new ApiResponseWithData<T>
        {
            Success = true,
            Message = message,
            Data = data
        });
    }

    protected IActionResult Created<T>(string routeName, object routeValues, T data) =>
        base.CreatedAtRoute(routeName, routeValues, new ApiResponseWithData<T> { Data = data, Success = true });

    protected IActionResult BadRequest(string message) =>
        base.BadRequest(new ApiResponse { Message = message, Success = false });

    protected IActionResult NotFound(string message = "Resource not found") =>
        base.NotFound(new ApiResponse { Message = message, Success = false });

    protected IActionResult OkPaginated<T>(IEnumerable<T> items, int currentPage, int pageSize, int totalRecords, string message = "Operation completed successfully")
    {
        var totalPages = (int)Math.Ceiling(totalRecords / (double)pageSize);
        
        return Ok(new PaginatedResponse<T>
        {
            Data = items,
            CurrentPage = currentPage,
            TotalPages = totalPages,
            TotalCount = totalRecords,
            Success = true,
            Message = message
        });
    }
}
