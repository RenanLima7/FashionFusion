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

            CreateMap<CreateUserRequest, User>()
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type));

            CreateMap<User, CreateUserResponse>()
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type));

            CreateMap<UpdateUserRequest, User>()
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type));

            CreateMap<User, UpdateUserResponse>()
                .ForMember(x => x.Type, options => options.MapFrom(y => y.Type));
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
