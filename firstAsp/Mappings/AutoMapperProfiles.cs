using AutoMapper;
using firstAsp.Models.Domain;
using firstAsp.Models.DTO;

namespace firstAsp.Mappings
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            /*CreateMap<UserDto,UserDomain>()
                .ForMember(x=>x.Name,opt=>opt.MapFrom(x=>x.FullName))
                .ReverseMap();*/
            CreateMap<Region, RegionDto>().ReverseMap();
            CreateMap<AddRegionRequestDto,Region>().ReverseMap();
            CreateMap<UpdateRegionRequestDto, Region>().ReverseMap();
            
        }
    }
   /* public class UserDto
    {
       // public String FullName { get; set; }
    }*/
   /* public class UserDomain
    {
        //public String Name { get; set; }
    }*/
}
