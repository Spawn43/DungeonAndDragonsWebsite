using Data.Context;
using Data.Entities;
using Data.Repositories.Interface;

namespace Data.Repositories
{
    public class LoginTokenRepository : ILoginTokenRepository
    {

        private readonly ApplicationDbContext _db;
        public LoginTokenRepository(ApplicationDbContext db)
        {
            _db = db;
        }

        public void InsertLoginToken(LoginToken loginToken)
        {
            _db.LoginTokens.Add(loginToken);
            _db.SaveChanges();
        }
    }
}
