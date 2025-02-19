using MediatR;

namespace Ambev.DeveloperEvaluation.Application.Products.ListCategories;

/// <summary>
/// Query to list all product categories
/// </summary>
public class ListCategoriesQuery : IRequest<List<string>>
{
} 