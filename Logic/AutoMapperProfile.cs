using AutoMapper;
using Entities;
using Models.Request;
using Models.Response;

namespace Logic
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            /// *************************************************** 
            /// Configuracion de mapeos modelo y entidad Employee
            /// *************************************************** 
            CreateMap<Employee, EmployeeModel>()
                .ForMember(dest => dest.FullName, opt => opt.MapFrom(src => $"{src.FirstName} {src.LastName}"));
                

            /// *************************************************** 
            /// Configuracion de mapeos modelo y entidad Order
            /// *************************************************** 
            CreateMap<OrderModel, Order>()
                .ReverseMap();

            CreateMap<CreateOrderRequest, Order>()
                .ReverseMap();

            /// *************************************************** 
            /// Configuracion de mapeos modelo y entidad OrderDetail
            /// *************************************************** 
            CreateMap<CreateOrderDetailRequest, OrderDetail>()
                .ReverseMap();

            /// *************************************************** 
            /// Configuracion de mapeos modelo y entidad Shipper
            /// *************************************************** 
            CreateMap<ShipperModel, Shipper>()
                .ReverseMap();

            /// *************************************************** 
            /// Configuracion de mapeos modelo y entidad Product
            /// *************************************************** 
            CreateMap<ProductModel, Product>()
                .ReverseMap();
        }
    }
}
