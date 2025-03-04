namespace TA.Domain.Interfaces
{
    public interface IPasswordService
    {
        string HashedPassword(string password, string salt);
        bool VerifyPassword(string password, string salt, string hash);

    }
}
