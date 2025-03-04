namespace TA.Domain.Interfaces
{
    public interface IJwtService
    {
        string GenerateToken(int userId, string sessionId);
    }
}
