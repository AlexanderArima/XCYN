using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.FileSystem
{
    public class DriveDemo
    {

        /// <summary>
        /// 检查驱动器信息
        /// </summary>
        public void Fun1()
        {
            var drives = DriveInfo.GetDrives();
            foreach (var item in drives)
            {
                if(item.IsReady)
                {
                    Console.WriteLine($"驱动器名称:{item.Name}");
                    Console.WriteLine($"驱动器根目录:{item.RootDirectory}");
                    Console.WriteLine($"文件系统名称:{item.DriveFormat}");
                    Console.WriteLine($"驱动器类型:{item.DriveType}");
                    Console.WriteLine($"总大小:{Math.Round(1.0d * item.TotalSize / (1024 * 1024 * 1024),2)}GB");
                    Console.WriteLine($"可用空间:{Math.Round(1.0d * item.TotalFreeSpace / (1024 * 1024 * 1024),2)}GB");
                    Console.WriteLine($"卷标名称:{item.VolumeLabel}");
                    Console.WriteLine();
                }
            }
              
        }

        /// <summary>
        /// 使用字符串连接操作符合并多个文件夹和文件时，很容易遗漏单个分隔符或使用太多字符。
        /// 为此，Path类可以提供帮助，因为这个类会添加缺少的分隔符，它还在基于Windows和Linux系统上，处理不同平台的需求
        /// </summary>
        public void Fun2()
        {

            string drive = Environment.GetEnvironmentVariable("HOMEDRIVE");
            string path = Environment.GetEnvironmentVariable("HOMEPATH");
            var p = Path.Combine(drive, path, "documents");
            Console.WriteLine(p);

            Console.WriteLine(Path.Combine(@"D:\eBook", "07311 多媒体技术", "2008年4.doc"));
        }

        public void Fun3()
        {
            //创建一个文件，并写入字符串。如果已存在文件，则覆盖该文件
            File.WriteAllText(Path.Combine(@"D:\迅雷下载","ReadMe.txt"),"Hello World2",Encoding.UTF8);

            var file = new FileInfo(Path.Combine(@"D:\迅雷下载", "ReadMe.txt"));
            file.CopyTo(Path.Combine(@"D:\数据库文件","ReadMe.txt"));
        }
    }
}
