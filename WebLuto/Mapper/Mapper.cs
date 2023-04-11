using AutoMapper;
using WebLuto.DataContract.Requests;
using WebLuto.DataContract.Responses;
using WebLuto.Models;

namespace WebLuto.Mapper
{
    public class Mapper : Profile
    {
        public Mapper()
        {
            UserMap();
            ProductMap();
        }

        public void UserMap()
        {
            CreateMap<User, LoginResponse>();

            CreateMap<CreateUserRequest, User>();

            CreateMap<User, CreateUserResponse>();

            CreateMap<UpdateUserRequest, User>();

            CreateMap<User, UpdateUserResponse>();
        }

        public void ProductMap()
        {
            CreateMap<CreateProductRequest, Product>()
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type));

            CreateMap<Product, CreateProductResponse>()
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type));

            CreateMap<UpdateProductRequest, Product>()
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type));

            CreateMap<Product, UpdateProductResponse>()
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type));
        }
    }
}
