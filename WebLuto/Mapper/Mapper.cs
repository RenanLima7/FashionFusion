using AutoMapper;
using WebLuto.DataContract.Requests;
using WebLuto.DataContract.Responses;
using WebLuto.Models;
using WebLuto.Models.Enums.UserEnum;

namespace WebLuto.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            ClientMap();
        }

        public void ClientMap()
        {
            CreateMap<User, LoginResponse>();

            //CreateMap<List<User>, List<UserResponse>>();

            CreateMap<UserRequest, User>()
                .ForMember(ur => ur.Type, options => options.MapFrom(u => u.Type));

            CreateMap<User, UserResponse>()
                .ForMember(u => u.Type, options => options.MapFrom(ur => ur.Type));
        }
    }
}
