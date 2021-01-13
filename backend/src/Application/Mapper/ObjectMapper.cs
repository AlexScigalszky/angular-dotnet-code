using Application.Models.Product;
using Application.Models.Role;
using Application.Models.User;
using Application.Models.UserRole;
using AutoMapper;
using Core.Models;
using Domain.Entities;
using System;

namespace Application.Mapper
{
    public static class ObjectMapper
    {
        private static readonly Lazy<IMapper> Lazy = new Lazy<IMapper>(() =>
        {
            var config = new MapperConfiguration(cfg =>
            {
                // This line ensures that internal properties are also mapped over.
                cfg.ShouldMapProperty = p => p.GetMethod.IsPublic || p.GetMethod.IsAssembly;
                cfg.AddProfile<SeapolDtoMapper>();
            });
            var mapper = config.CreateMapper();
            return mapper;
        });
        public static IMapper Mapper => Lazy.Value;
    }

    public class SeapolDtoMapper : Profile
    {
        public SeapolDtoMapper()
        {
            /**
             * Trim all strings
             * */
            CreateMap<string, string>()
                .ConvertUsing(str => (str ?? "").Trim());

            /**
             * Models
             * */
            CreateMap<User, UserModel>()
                .ReverseMap();
            CreateMap<UserRole, UserRoleModel>()
                .ReverseMap();
            CreateMap<Role, RoleModel>()
                .ReverseMap();
            CreateMap(typeof(PageableList<>), typeof(BaseListModel<>))
                .ReverseMap();

            /**
             * DTOs
             * */
            CreateMap<User, UserDto>()
                .ReverseMap();

            // TODO For Example
            CreateMap<Product, ProductModel>()
                .ForMember(dest => dest.ProductName, opt => opt.MapFrom(src => src.ProductName))
                .ReverseMap();

            CreateMap<Product, ProductDTO>().ReverseMap();
        }
    }
}
