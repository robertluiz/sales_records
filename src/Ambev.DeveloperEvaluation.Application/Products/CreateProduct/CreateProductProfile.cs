using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;

namespace Ambev.DeveloperEvaluation.Application.Products.CreateProduct;

/// <summary>
/// Profile for mapping between product entities and DTOs
/// </summary>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the create product operation
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<Product, CreateProductResult>();
    }
} 