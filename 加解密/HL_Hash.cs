using System.Security.Cryptography;
using System.Text;

namespace HL.Hash
{

    /// <summary>
    /// 得到随机安全码（哈希加密）。
    /// </summary>
    public static class HL_Hash
    {

        /// <summary>
        /// 哈希加密一个字符串
        /// </summary>
        /// <param name="security">需要加密的字符串</param>
        /// <returns>加密后的数据</returns>
        public static string HashEncoding (this string security)
        {
            var code = new UnicodeEncoding();
            byte[] message = code.GetBytes(security);
            var arithmetic = new SHA512Managed();
            var value = arithmetic.ComputeHash(message);
            var sb = new StringBuilder();
            foreach (byte o in value)
            {
                sb.Append((int)o + "O");
            }

            return sb.ToString();
        }

    }
}
