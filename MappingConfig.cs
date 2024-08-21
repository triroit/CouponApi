using AutoMapper;
using CouponApi.Models;
using CouponApi.Models.DTO;

namespace CouponApi
{
    public class MappingConfig : Profile
    {
        public MappingConfig() 
        { 
            CreateMap<Coupon, CouponCreateDto>().ReverseMap();
            CreateMap<Coupon, CouponUpdateDto>().ReverseMap();
        }
    }
}
