using AutoMapper;
using proje.Entities;
using proje.Models;

namespace proje
{
    public class AutoMapperConfig: Profile
    {
        public AutoMapperConfig() {
            
            //program.cs de builder kur
            CreateMap<Ad,AdViewModel>().ReverseMap();
            CreateMap<AdViewModel, AdViewModel>().ReverseMap();
            CreateMap<User,RegisterViewModel>().ReverseMap();


            CreateMap<SalesHouse,AdViewModel>().ReverseMap();
            CreateMap<Address,AdViewModel>().ReverseMap();
            CreateMap<RentHouse, AdViewModel>().ReverseMap();
            CreateMap<Garden, AdViewModel>().ReverseMap();

            //landsize bozuyor
            CreateMap<Land, AdViewModel>().ReverseMap();
        }
    }
}
