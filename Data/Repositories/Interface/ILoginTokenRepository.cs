using Data.Entities;

namespace Data.Repositories.Interface
{
    public interface ILoginTokenRepository
    {
        void InsertLoginToken(LoginTokenEntity loginToken);
        public LoginTokenEntity GetLoginTokenByToken(string loginToken);
    }
}