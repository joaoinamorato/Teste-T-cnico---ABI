using Ambev.DeveloperEvaluation.Application.Sales.UpdateSale;
using AutoMapper;

namespace Ambev.DeveloperEvaluation.WebApi.Features.Sales.UpdateSale
{
    /// <summary>
    /// Profile for mapping between Application and API UpdateSale responses
    /// </summary>
    public class UpdateSaleProfile : Profile
    {
        /// <summary>
        /// Initializes the mappings for UpdateSale feature
        /// </summary>
        public UpdateSaleProfile()
        {
            CreateMap<UpdateSaleRequest, UpdateSaleCommand>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom((src, dest, destMember, ctx) => (int)ctx.Items["Id"]));

            CreateMap<UpdateSaleResult, UpdateSaleResponse>();
        }
    }
}
