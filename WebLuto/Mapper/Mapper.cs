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
            ClientMap();
            AddressMap();
            SaleMap();
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
            CreateMap<CreateProductRequest, Product>();

            CreateMap<Product, CreateProductResponse>();
                        
            CreateMap<UpdateProductRequest, Product>();

            CreateMap<Product, UpdateProductResponse>();
        }

        public void ClientMap()
        {
            CreateMap<Client, LoginResponse>();

            CreateMap<Client, LoginClientResponse>();

            CreateMap<CreateClientRequest, Client>();

            CreateMap<Client, CreateClientResponse>();

            CreateMap<UpdateClientRequest, Client>();

            CreateMap<Client, UpdateClientResponse>();
        }

        private void AddressMap()
        {
            CreateMap<CreateAddressRequest, Address>();

            CreateMap<Address, CreateAddressResponse>();

            CreateMap<UpdateAddressRequest, Address>();

            CreateMap<Address, UpdateAddressResponse>();
        }

        private void SaleMap()
        {
            CreateMap<CreateSaleRequest, Sale>();

            CreateMap<CreateCardRequest, Card>();
        }
    }
}
