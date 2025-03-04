using TA.Domain.Entities;

namespace TA.Domain.Interfaces
{
    public interface IUserRepository
    {
        User GetUserby(string username);
        bool IsUserExistsBy(int id);
    }
}
