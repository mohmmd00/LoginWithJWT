using TA.Domain.Entities;
using TA.Domain.Interfaces;

namespace TA.Infrastructure.Sqlite.Repository
{
    public class UserRepository : IUserRepository
    {
        public User GetUserby(string username)
        {
            throw new NotImplementedException();
        }

        public bool IsUserExistsBy(int id)
        {
            throw new NotImplementedException();
        }

        public void SaveNewUser(User user)
        {
            throw new NotImplementedException();
        }
    }
}
