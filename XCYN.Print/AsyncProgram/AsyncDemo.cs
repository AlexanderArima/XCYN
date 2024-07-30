namespace XCYN.Print.AsyncProgram
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    /// <summary>
    /// 存放异步编程的Demo.
    /// </summary>
    public class AsyncDemo
    {
        /// <summary>
        /// 执行异步操作.
        /// </summary>
        public static async Task Fun01()
        {
            int value = 13;

            // 异步等待5s
            await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
            value *= 2;

            // 异步等待5s
            await Task.Delay(TimeSpan.FromSeconds(5)).ConfigureAwait(false);
            Console.WriteLine(value);
        }
    }
}
