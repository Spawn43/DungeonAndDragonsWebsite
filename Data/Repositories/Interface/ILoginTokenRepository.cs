using Data.Entities;

namespace Data.Repositories.Interface
{
    public interface ILoginTokenRepository
    {
        void InsertLoginToken(LoginToken loginToken);
    }
}