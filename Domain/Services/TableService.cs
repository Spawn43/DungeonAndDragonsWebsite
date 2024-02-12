using AutoMapper;
using Data.Entities;
using Data.Repositories.Interface;
using Domain.Models;
using Domain.Services.Interfaces;

namespace Domain.Services
{
    public class TableService : ITableService
    {
        private ILoginTokenService _loginTokenService;
        private ITableRepository _repository;
        private readonly IMapper _mapper;
        public TableService(ILoginTokenService loginTokenService, ITableRepository tableRepository, IMapper mapper)
        {
            _loginTokenService = loginTokenService;
            _repository = tableRepository;
            _mapper = mapper;
        }

        public Table GetTable(string id)
        {
            TableEntity tableEntity = _repository.GetById(id);
            Table table = _mapper.Map<Table>(tableEntity);
            return table;
        }

        public bool ClaimTable(LoginToken token, string tableId)
        {
            UserEntity user = _loginTokenService.CheckToken(token.token);
            if (user != null)
            {
                TableEntity table = _repository.GetById(tableId);
                if (table != null && table.DungeonMaster == null)
                {
                    table.DungeonMaster = user;
                    _repository.UpdateDataBase();
                    return true;
                }
            }
            return false;
        }

        public bool SetPlayersAllowed(PlayersNoTable pnt, string tableId) 
        {
            UserEntity user = _loginTokenService.CheckToken(pnt.Token);
            if (user != null)
            {
                TableEntity table = _repository.GetById(tableId);
                if (table != null && table.DungeonMaster == user)
                {
                    table.PlayersAllowed = pnt.NumberOfPlayers;
                    _repository.UpdateDataBase();
                    return true;
                }
            }
            return false;

        }

        public void AddPlayer(LoginToken token, string tableId)
        {
            UserEntity user = _loginTokenService.CheckToken(token.token);
            if (user != null)
            {
                TableEntity table = _repository.GetById(tableId);
                if (table != null && table.PlayersAllowed > table.Players.Count && !table.Players.Contains(user))
                {
                    table.Players.Add(user);
                    _repository.UpdateDataBase();
                }
            }
        }
    }
}
