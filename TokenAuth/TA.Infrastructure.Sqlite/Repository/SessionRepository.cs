using Microsoft.EntityFrameworkCore;
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
        public async Task<Session> GetByUserId(int userId)
        {
            return await _context.Sessions.FirstOrDefaultAsync(s => s.UserId == userId);
        }

        public void UpdateSession(Session session)
        {
            _context.Update(session);
        }
        public async Task SaveChanges()
        {
            await _context.SaveChangesAsync();
        }

        public async Task CreateSession(Session session)
        {
            await _context.AddAsync(session);
        }
    }
}
