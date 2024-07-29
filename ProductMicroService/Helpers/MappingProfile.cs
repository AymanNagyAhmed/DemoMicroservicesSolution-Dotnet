using AutoMapper;
using ProductMicroService.Dtos;
using ProductMicroService.Models;

namespace ProductMicroService.Helpers
{
    public class MappingProfile:Profile
    {
        public MappingProfile()
        {
            CreateMap<Product, ProductDetailsDto>();
        }
    }
}
