using Data.Entities;
using Domain.Models;

namespace Domain.Services.Interfaces
{
    public interface ILoginTokenService
    {
        string LoginTokenGeneration(UserEntity user, int ttl);
        public UserEntity CheckToken(string token);
    }
}