using System.Security.Cryptography;
using System.Text;

namespace WebApplication1.Services
{
    public class HashPasswordService
    {
        public static string Encrypt (string password) => ComputeSHA256Hash(password);

        public static bool ComparePasswords(string hashed, string toCompare) => hashed.Equals(ComputeSHA256Hash(toCompare));

        private static string ComputeSHA256Hash (string value)
        {
            byte[] bytes = SHA256.HashData(Encoding.UTF8.GetBytes(value));

            StringBuilder builder = new();
            for (int i = 0; i < bytes.Length; i++)
            {
                builder.Append(bytes[i].ToString("x2"));
            }
            return builder.ToString();
        }
    }
}
