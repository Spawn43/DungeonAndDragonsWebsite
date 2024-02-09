using Data.Context;
using Data.Entities;
using Data.Repositories.Interface;
using Microsoft.EntityFrameworkCore;

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

        public LoginToken GetLoginTokenByToken(string loginToken)
        {
            return _db.LoginTokens
                .Include(l => l.User)
                .FirstOrDefault(u => loginToken == u.Token);
                /*.Include(u => u.User)
                .FirstOrDefault(u => loginToken == u.Token);*/
        }
    }
}
