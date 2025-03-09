namespace TA.Domain.Entities
{
    public class Session
    {
        public int UserId { get; set; }
        public string SessionId { get; set; }
        public User User { get; set; }

        public Session(int userId, string sessionId)
        {
            UserId = userId;
            SessionId = sessionId;
        }
    }
}
