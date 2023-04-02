using AutoMapper;
using WebLuto.DataContract.Responses;
using WebLuto.DataContract.Requests;
using WebLuto.Models;

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
        }
    }
}
