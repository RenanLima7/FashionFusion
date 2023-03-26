using System.Security.Cryptography;
using System.Text;

namespace WebLuto.Utils
{
    public class UtilityMethods
    {
        public static string EncryptPassword(string password)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(password);
            byte[] hashBytes;

            using (SHA512 shaM = new SHA512Managed())
            {
                hashBytes = shaM.ComputeHash(passwordBytes);
            }

            return Convert.ToBase64String(hashBytes);
        }


        public static bool VerifyPassword(string enteredPassword, string storedPassword)
        {
            byte[] enteredPasswordBytes = Encoding.UTF8.GetBytes(enteredPassword);
            byte[] hashBytes;

            using (SHA512 shaM = new SHA512Managed())
            {
                hashBytes = shaM.ComputeHash(enteredPasswordBytes);
            }

            string calculatedStoredPassword = Convert.ToBase64String(hashBytes);

            return storedPassword.Equals(calculatedStoredPassword);
        }
    }
}
