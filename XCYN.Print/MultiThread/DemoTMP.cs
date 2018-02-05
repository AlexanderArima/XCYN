using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.MultiThread
{
    /// <summary>
    /// 基于Task的编程模型（TAP）
    /// 异步编程模型（APM）
    /// 基于事件的编程模型（EAP）
    /// 同步编程模式（SPM）
    /// </summary>
    public class DemoTMP
    {
        /// <summary>
        /// AMP编程的方式
        /// </summary>
        public void Fun1()
        {
            FileStream fs = new FileStream(Environment.CurrentDirectory + "\\1.txt", FileMode.Open);

            byte[] bytes = new byte[fs.Length];

            fs.BeginRead(bytes, 0, bytes.Length, (obj) => {
                var len = fs.EndRead(obj);
                Console.WriteLine(len);
            }, string.Empty);

            Console.Read();
        }

        /// <summary>
        /// 使用Task包装AMP编程的方式
        /// </summary>
        public void Fun2()
        {
            FileStream fs = new FileStream(Environment.CurrentDirectory + "\\1.txt", FileMode.Open);

            byte[] bytes = new byte[fs.Length];

            var task = Task.Factory.FromAsync(fs.BeginRead, fs.EndRead, bytes, 0, bytes.Length, string.Empty);

            var num = task.Result;

            Console.WriteLine(num);

            Console.Read();
        }

        /// <summary>
        /// async和await关键字
        /// </summary>
        /// <returns></returns>
        public async Task<string> Fun3()
        {
            Console.WriteLine("主线程1");
            var t = await Task.Run(() => {
                Console.WriteLine("工作线程1");
                return "hello world";
            });
            var t2 = await Task.Run(() => {
                Console.WriteLine("工作线程2");
                return "hello world";
            });
            Console.WriteLine("主线程2");
            return t.ToString() + t2.ToString();
        }
    }
}
