using System.Security.Cryptography;
using System.Text;

namespace Fireplace.AppDataFolder.ClassFolder
{
    public class HashClass // Для безопасности, мы берём пароль, что ввел пользователь, хешируем его и возвращаем уже хеш
    {
        /// <summary>
        /// GetHash получает строку, а после преобразует её в хеш вид.
        /// GetHash принимается только string.
        /// </summary>
        public static string GetHash(string inputeHash)
        {
            string hashSHA512;

            using (SHA512 sha512 = SHA512.Create())
            {
                byte[] hashByte = sha512.ComputeHash(Encoding.UTF8.GetBytes(inputeHash));

                StringBuilder stringBuilderSHA512 = new StringBuilder();
                for (int i = 0; i < hashByte.Length; i++)
                {
                    stringBuilderSHA512.Append(hashByte[i].ToString("x2"));
                }
                hashSHA512 = stringBuilderSHA512.ToString();
            }

            return hashSHA512;
        }
    }
}
