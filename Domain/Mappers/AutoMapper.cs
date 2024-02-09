using AutoMapper;
using Data.Entities;
using Domain.Models;


namespace Domain.Mappers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<LimitedUser, UserEntity>();
            CreateMap<RegisterUser, UserEntity>();
        }
    }
}
