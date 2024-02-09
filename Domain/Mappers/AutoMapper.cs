using AutoMapper;
using Data.Entities;
using Domain.Models;


namespace Domain.Mappers
{
    public class AutoMapper : Profile
    {
        public AutoMapper()
        {
            CreateMap<LimitedUser, UserEntity>().ReverseMap();
            CreateMap<RegisterUser, UserEntity>();
            CreateMap<CreateEvent, EventEntity>().ReverseMap();
            CreateMap<Event, EventEntity>().ReverseMap();
            CreateMap<Table, TableEntity>().ReverseMap();
        }
    }
}
