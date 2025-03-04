using TA.Domain.Entities;
namespace TA.Domain.Interfaces
{
    public interface IUserRepository
    {
        User GetUserbyUsername(string username);
        List<User> GetAllUsers();
        bool IsUserExistsBy(int id);
        void SaveNewUser(User user);
    }
}
