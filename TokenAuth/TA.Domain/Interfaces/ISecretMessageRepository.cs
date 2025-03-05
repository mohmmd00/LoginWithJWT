using TA.Domain.Entities;

namespace TA.Domain.Interfaces
{
    public interface ISecretMessageRepository
    {
        void CreateSecretMessage(SecretMessage message);
        SecretMessage GetSecretMessageById(int id);
        List<SecretMessage> GetSecretMessages();
    }
}
