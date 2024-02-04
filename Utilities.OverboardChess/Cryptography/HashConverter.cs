using System.Security.Cryptography;
using System.Text;

namespace Utilities.OverboardChess.Cryptography
{
    public class HashConverter
    {
        public static string ToSHA256(string input)
        {
            var data = SHA256.HashData(Encoding.UTF8.GetBytes(input));
            return ToString(data);
        }

        private static string ToString(byte[] data)
        {
            var sBuilder = new StringBuilder();
            for (int i = 0; i < data.Length; i++)
            {
                sBuilder.Append(data[i].ToString("x2"));
            }
            return sBuilder.ToString();
        }
    }
}
