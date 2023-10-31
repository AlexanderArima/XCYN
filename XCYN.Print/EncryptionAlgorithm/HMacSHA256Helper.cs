namespace XCYN.Print.EncryptionAlgorithm
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Security.Cryptography;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// HMacSHA256加密算法帮助类.
    /// </summary>
    public class HMacSHA256Helper
    {
        /// <summary>
        /// 加密方法.
        /// </summary>
        /// <param name="secret">密文.</param>
        /// <param name="signKey">签名密钥.</param>
        /// <remarks>由于HMacSHA256的主要作用是验证数据的完整性和真实性，所以不用解密，只需要比对加密后的数据是否相同即可.</remarks>
        public static Tuple<bool, string> Encrypt(string secret, string signKey)
        {
            try
            {
                using (HMACSHA256 mac = new HMACSHA256(Encoding.UTF8.GetBytes(signKey)))
                {
                    var hash = mac.ComputeHash(Encoding.UTF8.GetBytes(secret));
                    var result = Convert.ToBase64String(hash);
                    return new Tuple<bool, string>(true, result);
                }
            }
            catch (Exception ex)
            {
                return new Tuple<bool, string>(false, ex.Message);
            }
        }

        /// <summary>
        /// 将byte数组转换为16进制.
        /// </summary>
        /// <param name="bytes">byte数组.</param>
        public static string ToHexString(byte[] bytes)
        {
            string hexString = string.Empty;
            if (bytes != null)
            {
                StringBuilder strB = new StringBuilder();
                foreach (byte b in bytes)
                {
                    // {0:x2}表示16进制输出
                    strB.AppendFormat("{0:x2}", b);
                }

                hexString = strB.ToString();
            }

            return hexString;
        }
    }
}
