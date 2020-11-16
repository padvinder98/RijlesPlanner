using System;
using System.Security.Cryptography;

namespace RijlesPlanner.ApplicationCore.Services
{
    public class HashingService
    {
        public string GetSalt()
        {
            // generate a 128-bit salt using a secure PRNG
            byte[] salt = new byte[128 / 8];
            using (var rng = RandomNumberGenerator.Create())
            {
                rng.GetBytes(salt);
            }

            return Convert.ToBase64String(salt);
        }
    }
}
