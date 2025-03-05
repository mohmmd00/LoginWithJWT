namespace TA.Domain.Interfaces
{
    public interface IPasswordService
    {
        string HashPassword(string password, string salt);
        bool VerifyPassword(string password, string salt, string hash);

    }
}
