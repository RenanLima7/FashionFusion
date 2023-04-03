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

            CreateMap<CreateUserRequest, User>()
                .ForMember(ur => ur.Type, options => options.MapFrom(u => u.Type));

            CreateMap<User, CreateUserResponse>()
                .ForMember(u => u.Type, options => options.MapFrom(cur => cur.Type));
            
            CreateMap<User, UpdateUserResponse>()
                .ForMember(u => u.Type, options => options.MapFrom(cur => cur.Type));
        }
    }
}
