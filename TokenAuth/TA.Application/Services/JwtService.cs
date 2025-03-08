using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using TA.Domain.Interfaces;
namespace TA.Application.Services
{
    public class JwtService : IJwtService
    {
        private readonly IConfiguration _configure;

        public JwtService(IConfiguration configure)
        {
            _configure = configure;
        }

        public string GenerateToken(int userId, string sessionId)
        {
            if (string.IsNullOrWhiteSpace(sessionId))
                throw new Exception("session id cannot be empty");

            string secretKey = _configure["JwtSettings:SecretKey"]; // must impl in appsetting for configure to fetch seceret key i think its wrote by default DontTrust stackoverflow
            if (string.IsNullOrEmpty(secretKey))
                throw new InvalidOperationException("JWT secret key is not configured.");


            var key = Encoding.UTF8.GetBytes(secretKey); // we need to make every thing encode ... base64

            var newClaims = new Claim[]
            {
                new Claim(JwtRegisteredClaimNames.Sub, userId.ToString()),
                new Claim("sessionId", sessionId.ToString()), //added sission as another information that needs to be in jwt
                new Claim(JwtRegisteredClaimNames.Iat, DateTimeOffset.UtcNow.ToUnixTimeSeconds().ToString(), ClaimValueTypes.Integer64), //tarikh sodoor

                /*we can add exp here but security token will do it by default*/
            };

            var token = new JwtSecurityToken(
                issuer: null,
                audience: null,
                claims: newClaims, // add our new cliam (payload) that we made
                expires: DateTime.UtcNow.AddMinutes(10), // TOKEN ONLY WORKS FOR 10 MINUTES
                signingCredentials: new SigningCredentials( // creating a new signiture
                    new SymmetricSecurityKey(key), //using key in appsetting and hash it using hmacsha256
                    SecurityAlgorithms.HmacSha256));


            return new JwtSecurityTokenHandler().WriteToken(token); // create a anew instance of JwtSecurityTokenHandler to write our token that we made with our claim and key 
        }
    }
}
