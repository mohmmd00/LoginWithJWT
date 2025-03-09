namespace TA.Domain.Entities
{
    public class User
    {
        public int PrimaryId { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Salt { get; set; }
        public Session Session { get; set; }

        public User(string username, string password, string salt)
        {
            Username = username;
            Password = password;
            Salt = salt;
        }
    }
}
