using AutoMapper;
using ORM_Mini_Project.DTOs.UserDtos;
using ORM_Mini_Project.Models;

namespace ORM_Mini_Project.AutoMapper;

public class PostProfile : Profile
{
    public PostProfile()
    {
        CreateMap<UserPostDto,User>().ReverseMap();
    }
}
