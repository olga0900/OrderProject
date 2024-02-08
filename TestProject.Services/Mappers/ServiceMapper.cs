using AutoMapper;
using TestProject.Context.Contracts.Models;
using TestProject.Services.Contracts.Models;

namespace TestProject.Services.Mappers
{
    public class ServiceMapper : Profile
    {
        public ServiceMapper()
        {
            CreateMap<Order, OrderModel>(MemberList.Destination).ReverseMap();
        }
    }
}
