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
        private IUserRepository _userRepository;
        private readonly IMapper _mapper;
        public TableService(ILoginTokenService loginTokenService, ITableRepository tableRepository, IMapper mapper, IUserRepository userRepository)
        {
            _loginTokenService = loginTokenService;
            _repository = tableRepository;
            _mapper = mapper;
            _userRepository = userRepository;
        }

        public Table GetTable(string id)
        {
            TableEntity tableEntity = _repository.GetById(id);
            Table table = _mapper.Map<Table>(tableEntity);
            return table;
        }

        public KeyValuePair<int,string> ClaimTable(LoginToken token, string tableId)
        {
            UserEntity user = _loginTokenService.CheckToken(token.token);
            if (user != null)
            {
                TableEntity table = _repository.GetById(tableId);
                if (table == null)
                {
                    return new KeyValuePair<int, string>(409, "Table Does Not Exist");
                }
                else if (table.DungeonMaster != null)
                {
                    return new KeyValuePair<int, string>(409, "Already Existing Dungeon Master");
                }
                else
                {
                    table.DungeonMaster = user;
                    _repository.UpdateDataBase();
                    return new KeyValuePair<int, string>(200, "");
                }
            }
            return new KeyValuePair<int, string>(409, "Not Logged In");
        }

        public new KeyValuePair<int, string> SetPlayersAllowed(PlayersNoTable pnt, string tableId) 
        {
            UserEntity user = _loginTokenService.CheckToken(pnt.Token);
            if (user != null)
            {
                TableEntity table = _repository.GetById(tableId);
                if (table == null)
                {
                    return new KeyValuePair<int, string>(409, "Table Does Not Exist");
                }
                else if (table.DungeonMaster != user)
                {
                    return new KeyValuePair<int, string>(409, "User is not the Dungeon Master");
                }
                else
                {
                    table.PlayersAllowed = pnt.NumberOfPlayers;
                    _repository.UpdateDataBase();
                    return new KeyValuePair<int, string>(200, "");
                }             
            }
            return new KeyValuePair<int, string>(409, "Not Logged In");

        }

        public KeyValuePair<int, string> AddPlayer(LoginToken token, string tableId)
        {
            UserEntity user = _loginTokenService.CheckToken(token.token);
            if (user != null)
            {
                TableEntity table = _repository.GetById(tableId);
                if (table == null  )
                {
                    return new KeyValuePair<int, string>(409, "Table Does Not Exist");
                }
                else if(table.PlayersAllowed <= table.Players.Count)
                {
                    return new KeyValuePair<int, string>(409, "Table Full");
                }
                else if (table.Players.Contains(user))
                {
                    return new KeyValuePair<int, string>(409, "User Already Signed Up");
                }
                else
                {
                    table.Players.Add(user);
                    _repository.UpdateDataBase();
                    return new KeyValuePair<int, string>(200, "");
                }
            }
            return new KeyValuePair<int, string>(409, "Not Logged In");
        }

        public KeyValuePair<int, string> RemovePlayer(LoginToken lt, string tableId, string userId)
        {
            UserEntity user = _loginTokenService.CheckToken(lt.token);
            UserEntity removeUser = _userRepository.GetUserById(userId);

            if(user != null)
            {
                TableEntity table = _repository.GetById(tableId);
                if (table == null)
                {
                    return new KeyValuePair<int, string>(409, "Table Does Not Exist");
                }
                else if(removeUser == user || user == table.DungeonMaster || user == table.Event.Planner)
                {
                    table.Players.Remove(removeUser); 
                    _repository.UpdateDataBase();
                    return new KeyValuePair<int, string>(200, "");
                }
                else
                {
                    return new KeyValuePair<int, string>(409, "Incorrect Permissions");
                }
                
            }

            return new KeyValuePair<int, string>(409, "Not Logged In");
        }
    }
}
