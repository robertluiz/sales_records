using AutoMapper;
using Ambev.DeveloperEvaluation.Domain.Entities;
using DomainModels = Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.Application.Products.ListProducts;

/// <summary>
/// Profile for mapping between product entities and DTOs in list products feature
/// </summary>
public class ListProductsProfile : Profile
{
    /// <summary>
    /// Initializes the mappings for the list products operation
    /// </summary>
    public ListProductsProfile()
    {
        CreateMap<Product, ProductResult>();
        
        CreateMap<DomainModels.ProductRating, ProductRating>()
            .ForMember(dest => dest.Rate, opt => opt.MapFrom(src => src.Rate))
            .ForMember(dest => dest.Count, opt => opt.MapFrom(src => src.Count));
    }
} 