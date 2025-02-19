using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.CreateProduct;
using Ambev.DeveloperEvaluation.WebApi.Features.Products.Models;
using DomainModels = Ambev.DeveloperEvaluation.Domain.Models;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.CreateProductFeature;

/// <summary>
/// AutoMapper profile for create product feature
/// </summary>
public class CreateProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the CreateProductProfile class
    /// </summary>
    public CreateProductProfile()
    {
        CreateMap<ProductRating, DomainModels.ProductRating>();

        CreateMap<CreateProductRequest, CreateProductCommand>();

        CreateMap<CreateProductResult, CreateProductResponse>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRating 
            { 
                Rate = src.Rating.Rate,
                Count = src.Rating.Count 
            }));
    }
} 