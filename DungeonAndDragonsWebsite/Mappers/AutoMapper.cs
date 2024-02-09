using AutoMapper;
using DungeonAndDragonsWebsite.Models;
using DungeonAndDragonsWebsite.Models.DTOs;

namespace DungeonAndDragonsWebsite.Mappers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<LimtedUser, UserEntity>();
        }
    }
}
