using AutoMapper;
using ShopTI.Entities;
using ShopTI.Models;

namespace ShopTI
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Product, CreateProductModel>().ReverseMap(); //te same nazwy propertiesów powodują automatyczne zmapowanie
            CreateMap<NewOrderModel, Order>().ReverseMap();
            CreateMap<NewOrderModel, OrderDetail>().ReverseMap();
        }
    }
}
