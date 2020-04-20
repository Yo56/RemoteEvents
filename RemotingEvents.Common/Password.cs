using System;
using System.Security.Cryptography;
using System.Text;


namespace TDIN_PROJ1.Common
{
    public class Password
    {
        //Password hash using sha256
        public static String HashSHA256(string clearPassword)
        {
            SHA256 sha256 = SHA256Managed.Create();
            byte[] bytes = Encoding.UTF8.GetBytes(clearPassword);
            byte[] hash = sha256.ComputeHash(bytes);
            return GetStringFromHash(hash);
        }

        // method used in HashPassword
        private static string GetStringFromHash(byte[] hash)
        {
            StringBuilder result = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                result.Append(hash[i].ToString("X2"));
            }
            return result.ToString();
        }
    }
}
