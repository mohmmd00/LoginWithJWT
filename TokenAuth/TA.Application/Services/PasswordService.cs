﻿using System.Security.Cryptography;
using System.Text;
using TA.Domain.Interfaces;

namespace TA.Application.Services
{
    public class PasswordService : IPasswordService
    {
        public string HashPassword(string password, string salt)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password); // change password type string to bytes 
            byte[] saltBytes = Encoding.UTF8.GetBytes(salt); // change salt string to bytes

            using (var hmac = new HMACSHA256(saltBytes)) //give salt to bbytes as a key to sha256 alghoritm
            {
                byte[] hashBytes = hmac.ComputeHash(passwordBytes);// tell sha to hash password that changed to bytes 

                return Convert.ToBase64String(hashBytes);
            }
        }

        public bool VerifyPassword(string password, string salt, string alreadyHashedPassword)
        {
            string hashResult = HashPassword(password, salt);
            return hashResult == alreadyHashedPassword;
        }
    }
}