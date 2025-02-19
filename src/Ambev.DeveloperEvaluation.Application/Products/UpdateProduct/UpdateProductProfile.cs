using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.UpdateProduct;

/// <summary>
/// Profile for mapping between product entities and DTOs
/// </summary>
public class UpdateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the update product operation
    /// </summary>
    public UpdateProductProfile()
    {
        CreateMap<Product, UpdateProductResult>();
    }
} 