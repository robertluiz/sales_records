using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Products.GetProduct;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Products.GetProductFeature;

/// <summary>
/// AutoMapper profile for get product feature
/// </summary>
public class GetProductProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the GetProductProfile class
    /// </summary>
    public GetProductProfile()
    {
        CreateMap<GetProductResult, GetProductResponse>()
            .ForMember(dest => dest.Rating, opt => opt.MapFrom(src => new ProductRating 
            { 
                Rate = src.Rating.Rate,
                Count = src.Rating.Count 
            }));
    }
} 