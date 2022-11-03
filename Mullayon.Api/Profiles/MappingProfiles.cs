using AutoMapper;
using Mullayon.Api.Dtos;
using Mullayon.Core.Entities;

namespace Mullayon.Api.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        CreateMap<CreatePostDto, Post>();
        CreateMap<UpdatePostDto, Post>();
        CreateMap<Post, PostListDto>().ReverseMap();
        CreateMap<Post, PostDetailsDto>().ReverseMap();

        CreateMap<CreateCategoryDto, Category>();
        CreateMap<UpdateCategoryDto, Category>();
        CreateMap<Category, CategoryListDto>().ReverseMap();
        CreateMap<Category, CategoryDetailsDto>().ReverseMap();

        CreateMap<CreateTagDto, Tag>();
        CreateMap<UpdateTagDto, Tag>();
        CreateMap<Tag, TagListDto>().ReverseMap();
        CreateMap<Tag, TagDetailsDto>().ReverseMap();

        CreateMap<ApplicationUser, UserDto>().ReverseMap();
        CreateMap<ApplicationRole, RoleDto>().ReverseMap();
        CreateMap<RegisterDto, ApplicationUser>();
    }
}