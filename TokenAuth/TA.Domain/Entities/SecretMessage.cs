namespace TA.Domain.Entities
{
    public class SecretMessage
    {
        public int PrimaryId { get; set; }
        public string Secret { get; set; }
        public SecretMessage( string secret)
        {
            Secret = secret;
        }
    }
}
