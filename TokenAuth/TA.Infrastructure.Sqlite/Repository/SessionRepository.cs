using TA.Domain.Entities;
using TA.Domain.Interfaces;

namespace TA.Infrastructure.Sqlite.Repository
{
    public class SessionRepository : ISessionRepository
    {
        private readonly AuthContext _context;

        public SessionRepository(AuthContext context)
        {
            _context = context;
        }

        public Session GetSessionBy(int userId)
        {
            return _context.Sessions.Find(userId);
        }

        public void CreateSession(Session session)
        {
            _context.Update(session);
            _context.SaveChanges();
        }
    }
}
