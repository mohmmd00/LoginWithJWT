using TA.Domain.Entities;
using TA.Domain.Interfaces;

namespace TA.Infrastructure.Sqlite.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AuthContext _context;

        public UserRepository(AuthContext context)
        {
            _context = context;
        }

        public User GetUserbyUsername(string username)
        {
            var selecteduser = _context.Users.FirstOrDefault(x=>x.Username == username);
            return selecteduser;
        }
        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public bool IsUserExistsBy(int id)
        {
            bool status = false;
            var chodenuser = _context.Users.Find(id);
            if (chodenuser != null)
            {
                status = true;
                return status;
            }

            return status;
        }

        public void CreateUser(User user)
        {
            _context.Add(user);
            
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }
    }
}
