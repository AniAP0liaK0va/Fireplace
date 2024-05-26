using System;
using System.Text;

namespace Fireplace.AppDataFolder.ClassFolder
{
    public class RandomeCodeClass
    {
        private static readonly Random Random = new Random();
        private static readonly char[] Characters = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789!@#$%^&*(){}[]".ToCharArray();

        public static string GenerateRandomCode(int length)
        {
            var stringBuilder = new StringBuilder(length);

            for (int i = 0; i < length; i++)
            {
                stringBuilder.Append(Characters[Random.Next(Characters.Length)]);
            }
            return stringBuilder.ToString();
        }
    }
}
