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
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type));

            CreateMap<User, CreateUserResponse>()
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type));
            
            CreateMap<UpdateUserRequest, User>()
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type));

            CreateMap<User, UpdateUserResponse>()
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type));
        }
    }
}
