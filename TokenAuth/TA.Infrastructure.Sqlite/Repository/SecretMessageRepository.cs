using TA.Domain.Entities;
using TA.Domain.Interfaces;

namespace TA.Infrastructure.Sqlite.Repository
{
    public class SecretMessageRepository : ISecretMessageRepository
    {

        private readonly AuthContext _context;

        public SecretMessageRepository(AuthContext context)
        {
            _context = context;
        }

        public void CreateSecretMessage(SecretMessage message)
        {
            _context.SecretMessages.Add(message);
        }

        public SecretMessage GetSecretMessageById(int id)
        {
            var selectedMessage = _context.SecretMessages.Find(id);
            return selectedMessage;
        }

        public List<SecretMessage> GetSecretMessages()
        {
            var allmessages = _context.SecretMessages.ToList();
            return allmessages;
        }
    }
}
