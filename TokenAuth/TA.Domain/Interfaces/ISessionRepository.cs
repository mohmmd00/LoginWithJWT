using TA.Domain.Entities;

namespace TA.Domain.Interfaces
{
    public interface ISessionRepository
    {
        Session GetSessionBy(int userId);
        Task CreateSession(Session session);
        void UpdateSession(Session session);
        Task SaveChanges();
        Task<Session> GetSessionByUserId(int userId);
    }
}
