using TA.Application.DTO_s;
using TA.Domain.Entities;
using TA.Domain.Interfaces;

namespace TA.Application.Services
{
    public class SecretMessageService
    {
        private readonly ISecretMessageRepository _repository;

        public SecretMessageService(ISecretMessageRepository repository)
        {
            _repository = repository;
        }

        public void CreateNewMessage(SecretMessageRequest request)
        {
            var message = new SecretMessage(request.Secret);

            _repository.CreateSecretMessage(message);
        }

        public List<SecretMessage> GetSecretsAll()
        {
            var allmessages = _repository.GetSecretMessages();
            return allmessages;
        }

    }
}
