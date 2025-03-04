namespace TA.Domain.Interfaces
{
    public interface IPasswordService
    {
        string ToHashPassword(string password, string salt);
        bool ToVerifyPassword(string password, string salt, string hash);

    }
}
