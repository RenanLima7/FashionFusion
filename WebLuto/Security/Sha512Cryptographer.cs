using System.Security.Cryptography;
using System.Text;

namespace WebLuto.Security
{
    public static class Sha512Cryptographer
    {
        public static string Encrypt(string text, long salt)
        {
            var password = Encoding.UTF8.GetBytes(text);
            byte[] saltBytes = Encoding.ASCII.GetBytes(salt.ToString());
            var hmacSHA512 = new HMACSHA512(saltBytes);
            var saltedHash = hmacSHA512.ComputeHash(password);

            return Convert.ToBase64String(saltedHash);
        }

        public static bool Compare(string plainValue, long salt, string hashedValue)
        {
            return Encrypt(plainValue, salt).Equals(hashedValue);
        }
    }
}
