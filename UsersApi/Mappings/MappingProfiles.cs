using AutoMapper;
using UsersApi.BusinessLayer;

namespace UsersApi
{
    public class MappingProfiles: Profile
    {
        public MappingProfiles()
        {
            CreateMap<ApiUserModel, User>()
                .ForMember(user =>user.Country,
                    opt => opt.MapFrom(src => src.Location!= null? src.Location.Country:""))
                .ForMember(user =>user.Username,
                    opt => opt.MapFrom(src => src.Login != null ? src.Login.Username : ""))
                .ForMember(user =>user.Name,
                    opt => opt.MapFrom(src => src.Name != null ? string.Join(" ", src.Name.First, src.Name.Last): ""))
                .ForMember(user => user.Id,
                    opt=> opt.MapFrom(src=> src.Email))
                .ReverseMap();
        }
    }
}
