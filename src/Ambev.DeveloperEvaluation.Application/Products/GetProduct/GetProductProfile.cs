using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.GetProduct;

/// <summary>
/// Profile for mapping between product entities and DTOs
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the get product operation
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<Product, GetProductResult>();
    }
} 