using System.Linq;
using AutoMapper;
using ZwajApp.API.DTOS;
using ZwajApp.API.Models;

namespace ZwajApp.API.Helpers
{
    public class AutoMapperProfiles:Profile
    {
        public AutoMapperProfiles()
        {
            CreateMap<User,UserForListDto>()
            .ForMember(dest =>dest.PhotoUrl,opt=>{opt.MapFrom(src=>src.Photos.FirstOrDefault(x=>x.IsMain).Url);})
            .ForMember(dest=>dest.Age,opt=>{opt.ResolveUsing(src=>src.DateOfBirth.CalculateAge());});
            CreateMap<User,UserForDetailsDto>()
            .ForMember(dest =>dest.PhotoURL,opt=>{opt.MapFrom(src=>src.Photos.FirstOrDefault(x=>x.IsMain).Url);})
            .ForMember(dest=>dest.Age,opt=>{opt.ResolveUsing(src=>src.DateOfBirth.CalculateAge());});
            CreateMap<Photo,PhotoForDetailsDto>();
        }
    }
}