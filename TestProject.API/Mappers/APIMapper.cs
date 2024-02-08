using AutoMapper;
using TestProject.API.Models.CreateRequest;
using TestProject.API.Models.Request;
using TestProject.API.Models.Response;
using TestProject.Services.Contracts.Models;

namespace TestProject.API.Mappers
{
    public class APIMapper : Profile
    {
        public APIMapper()
        {
            CreateMap<CreateOrderRequest, OrderModel>(MemberList.Destination)
                .ForMember(x => x.Number, opt => opt.Ignore()).ReverseMap();

            CreateMap<OrderRequest, OrderModel>(MemberList.Destination).ReverseMap();
            CreateMap<OrderModel, OrderResponse>(MemberList.Destination)
                .ForMember(x => x.SenderData, opt => opt.MapFrom(src => $"{src.SenderCity}, {src.SenderAddress}"))
                .ForMember(x => x.RecipientData, opt => opt.MapFrom(src => $"{src.RecipientCity}, {src.RecipientAddress}"));
        }
    }
}
