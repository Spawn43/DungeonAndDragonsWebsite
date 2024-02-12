using AutoMapper;
using Data.Entities;
using Data.Repositories.Interface;
using Domain.Models;
using Domain.Services.Interfaces;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using System.Text;

namespace Domain.Services
{
    public class EventService : IEventService
    {


        private static readonly Random RandomGenerator = new Random();
        private const string AlphanumericChars = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        private ILoginTokenService _loginTokenService;
        private IEventRepository _repository;
        private readonly IMapper _mapper;
        public EventService(ILoginTokenService loginTokenService, IEventRepository eventRepository, IMapper mapper)
        {
            _loginTokenService = loginTokenService;
            _repository = eventRepository;
            _mapper = mapper;
        }

        public Event GetEvent(string id)
        {
            EventEntity entity = _repository.GetById(id);
            if (entity == null)
            {
                return null;
            }
            Event ev = _mapper.Map<Event>(entity);
            return ev;

        }

        public string CreateEvent(CreateEvent ev)
        {
            UserEntity user = _loginTokenService.CheckToken(ev.LoginToken);
            if (user != null)
            {
                EventEntity newEvent = new EventEntity();
                newEvent.Id = GenerateRandomToken();
                newEvent.Planner = user;
                newEvent.Name = ev.Name;
                newEvent.Location = ev.Location;
                newEvent.Date = ev.Date;
                ICollection<TableEntity> tables = new List<TableEntity>();
                int i = 0;
                while (i < ev.NumberOfTables)
                {
                    TableEntity t = new TableEntity();
                    t.Id = GenerateRandomToken();
                    t.Event = newEvent;
                    tables.Add(t);
                    i++;
                }
                newEvent.Tables = tables;
                _repository.PostEvent(newEvent);
                return newEvent.Id;
            }
            return "error";
        }



        private string GenerateRandomToken()
        {
            StringBuilder stringBuilder = new StringBuilder();

            for (int i = 0; i < 20; i++)
            {
                int index = RandomGenerator.Next(AlphanumericChars.Length);
                char randomChar = AlphanumericChars[index];
                stringBuilder.Append(randomChar);
            }

            return stringBuilder.ToString();
        }
    }
}
