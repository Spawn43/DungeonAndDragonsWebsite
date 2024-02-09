using Data.Entities;

namespace Domain.Services.Interfaces
{
    public interface ILoginTokenService
    {
        string LoginTokenGeneration(UserEntity user, int ttl);
    }
}