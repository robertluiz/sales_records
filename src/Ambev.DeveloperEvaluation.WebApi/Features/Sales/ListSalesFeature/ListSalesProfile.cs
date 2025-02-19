using AutoMapper;
using Ambev.DeveloperEvaluation.Application.Sales.ListSales;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.ListSalesFeature;

/// <summary>
/// AutoMapper profile for list sales feature
/// </summary>
public class ListSalesProfile : Profile
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ListSalesProfile"/> class
    /// </summary>
    public ListSalesProfile()
    {
        CreateMap<ListSalesRequest, ListSalesQuery>();

        CreateMap<ListSalesResult, ListSalesResponse>()
            .ForMember(dest => dest.Page, opt => opt.MapFrom(src => src.CurrentPage));

        CreateMap<Application.Sales.ListSales.SaleListItem, ListSalesFeature.SaleListItem>();
    }
} 