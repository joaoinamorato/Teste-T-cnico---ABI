using Ambev.DeveloperEvaluation.Domain.Entities;
using Ambev.DeveloperEvaluation.Domain.ValueObjects;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.Application.Sales.CreateSale
{
    public class CreateSaleProfile : Profile
    {
        public CreateSaleProfile()
        {
            CreateMap<CreateSaleCommand, Sale>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.TotalAmount, opt => opt.Ignore())
                .ForMember(dest => dest.Customer, opt => opt.MapFrom(src => new CustomerInfo { Id = src.CustomerId }))
                .ForMember(dest => dest.Branch, opt => opt.MapFrom(src => new BranchInfo { Id = src.BranchId }))
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(_ => false))
                .ForMember(dest => dest.Items, opt => opt.MapFrom(src => src.Items));

            CreateMap<CreateSaleItemDto, SaleItem>()
                .ForMember(dest => dest.Id, opt => opt.Ignore())
                .ForMember(dest => dest.SaleId, opt => opt.Ignore())
                .ForMember(dest => dest.Product, opt => opt.MapFrom(src => new ProductInfo { Id = src.ProductId }))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity))
                .ForMember(dest => dest.DiscountPerecentage, opt => opt.Ignore())
                .ForMember(dest => dest.IsCancelled, opt => opt.MapFrom(_ => false));


            CreateMap<Sale, CreateSaleResult>();
        }
    }
}
