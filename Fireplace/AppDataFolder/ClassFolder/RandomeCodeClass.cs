using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
