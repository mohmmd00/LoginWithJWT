using System.Security.Cryptography; 
using System.Text;
using TA.Domain.Interfaces;

namespace TA.Application.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashedPassword(string password, string salt)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt))
                throw new ArgumentNullException("Password or salt cannot be null or empty.");
            

            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt);

            using (var hmac = new HMACSHA256(saltBytes))
            {
                byte[] hashBytes = hmac.ComputeHash(passwordBytes);

                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifiedPassword(string password, string salt, string hash)
        {
            if (string.IsNullOrEmpty(password) || string.IsNullOrEmpty(salt) || string.IsNullOrEmpty(hash))
                throw new ArgumentNullException("Password, salt, or hash cannot be null or empty.");


            string computedHash = HashedPassword(password, salt);

            return computedHash == hash;
        }
    }
}