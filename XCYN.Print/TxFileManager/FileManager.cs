using ChinhDo.Transactions;
using XCYN.Common;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Transactions;

namespace XCYN.Print.TxFileManager
{
    public class FileManager
    {
        /// <summary>
        /// 正常流程.
        /// </summary>
        public static void Fun01()
        {
            IFileManager fm = new ChinhDo.Transactions.TxFileManager();
            using (TransactionScope scope = new TransactionScope())
            {
                // 删除1.txt文件后，添加两个txt文件
                fm.Delete(string.Format("{0}\\Log\\{1}", PathHelper.ApplicationPath, "1.txt"));

                var fileName = string.Format("{0}{1}{2}.txt", DateTime.Now.Hour.ToString().PadLeft(2, '0'), DateTime.Now.Minute.ToString().PadLeft(2, '0'), DateTime.Now.Second.ToString().PadLeft(2, '0'));
                fileName = string.Format("{0}\\Log\\{1}", PathHelper.ApplicationPath, fileName);
                fm.AppendAllText(fileName, "1");

                Thread.Sleep(1000);

                var fileName2 = string.Format("{0}{1}{2}.txt", DateTime.Now.Hour.ToString().PadLeft(2, '0'), DateTime.Now.Minute.ToString().PadLeft(2, '0'), DateTime.Now.Second.ToString().PadLeft(2, '0'));
                fileName2 = string.Format("{0}\\Log\\{1}", PathHelper.ApplicationPath, fileName2);
                fm.AppendAllText(fileName2, "2");
                scope.Complete();
            }
        }

        /// <summary>
        /// 流程执行到一半后，出现异常.
        /// </summary>
        public static void Fun02()
        {
            IFileManager fm = new ChinhDo.Transactions.TxFileManager();
            using (TransactionScope scope = new TransactionScope())
            {
                // 删除1.txt文件后，添加两个txt文件
                fm.Delete(string.Format("{0}\\Log\\{1}", PathHelper.ApplicationPath, "1.txt"));

                var fileName = string.Format("{0}{1}{2}.txt", DateTime.Now.Hour.ToString().PadLeft(2, '0'), DateTime.Now.Minute.ToString().PadLeft(2, '0'), DateTime.Now.Second.ToString().PadLeft(2, '0'));
                fileName = string.Format("{0}\\Log\\{1}", PathHelper.ApplicationPath, fileName);
                fm.AppendAllText(fileName, "1");

                Thread.Sleep(1000);

                // 抛出一个异常，方法立即回滚
                try
                {
                    throw new Exception();
                }
                catch (Exception ex)
                {
                    return;
                }

                var fileName2 = string.Format("{0}{1}{2}.txt", DateTime.Now.Hour.ToString().PadLeft(2, '0'), DateTime.Now.Minute.ToString().PadLeft(2, '0'), DateTime.Now.Second.ToString().PadLeft(2, '0'));
                fileName2 = string.Format("{0}\\Log\\{1}", PathHelper.ApplicationPath, fileName2);
                fm.AppendAllText(fileName2, "2");
                scope.Complete();
            }
        }
    }
}
