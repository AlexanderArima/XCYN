using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.AsyncProgram
{
    public class JiaMiYuanLi
    {
        /// <summary>
        /// 对数字进行异或操作，实现数字的加密/解密
        /// </summary>
        public static void Fun1()
        {
            int content = 1;    //明文
            int key = 90;   //密钥
            int content_after = content ^ key;  //加密后的值
            int content_before = content_after ^ key;   //解密后的值
            Console.WriteLine(string.Format("content_after：{0}，content_before：{1}", content_after, content_before));
        }

        /// <summary>
        /// 对字符数组进行异或操作，实现字符串的加密/解密
        /// </summary>
        public  static void Fun2()
        {
            string content = "GER";
            string key = "BIO";
            var content_char = System.Text.Encoding.Unicode.GetBytes(content);
            var key_char = System.Text.Encoding.Unicode.GetBytes(key);
            //加密
            byte[] content_after_char = new byte[content_char.Length];  //加密后的字符数组
            for (int i = 0; i < content_char.Length; i++)
            {
                content_after_char[i] = (byte)(content_char[i] ^ key_char[i]);
            }
            var content_after_string = System.Text.Encoding.Unicode.GetString(content_after_char);

            //解密
            byte[] content_before_char = new byte[content_char.Length];  //解密后的字符数组
            for (int i = 0; i < content_char.Length; i++)
            {
                content_before_char[i] = (byte)(content_after_char[i] ^ key_char[i]);
            }
            var content_before_string = System.Text.Encoding.Unicode.GetString(content_before_char);
            Console.WriteLine(string.Format("原文：{0}", content));
            Console.WriteLine(string.Format("加密后的内容：{0}", content_after_string));
            Console.WriteLine(string.Format("解密后的内容：{0}", content_before_string));
        }

    }
}
