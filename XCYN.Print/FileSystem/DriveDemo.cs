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
        /// DriveInfo驱动器类
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

        /// <summary>
        /// FileInfo常用方法
        /// </summary>
        public void Fun3()
        {
            //创建一个文件
            var file = new FileInfo(Path.Combine(@"D:\迅雷下载", "ReadMe.txt"));
            if (!file.IsReadOnly)
            {
                //File.WriteAllText(Path.Combine(@"D:\迅雷下载", "ReadMe.txt"), "Hello World2", Encoding.UTF8);
            }
            //复制
            //file.CopyTo(Path.Combine(@"D:\数据库文件","ReadMe.txt"));
            //删除
            //file.Delete();
            //创建
            //file.Create();
            Console.WriteLine($"文件名:{file.Name},特性(只读，临时...):{file.Attributes},创建日期:{file.CreationTime},目录名称:{file.Directory.FullName}");
            Console.WriteLine($"{file.FullName}该文件{(file.Exists == true ? "存在" : "不存在")},后缀名:{file.Extension},{(file.IsReadOnly ? "只读" :"读写")}");
            Console.WriteLine($"最近一次访问时间:{file.LastAccessTime},最近一次修改时间{file.LastWriteTime},大小:{file.Length / 1024}kb");
            //读取文件内容
            var stream = file.Open(FileMode.Open);
            byte[] b = new byte[stream.Length];
            stream.Read(b, 0, (int)stream.Length);
            Console.WriteLine(Encoding.UTF8.GetString(b));
        }

        /// <summary>
        /// File类 ReadAllLines(),ReadLines()读取文件
        /// 在C#4.0之前ReadAllLines()方法用于读取文件中所有的行，并以数组的形式返回，但是它有一个问题，就是这个方法要等所有的行写入内存之后才能读取第一行的内容，
        /// .NET4中增加了一个ReadLines方法，该返回返回IEnumerable<string>而不是string[]数组，这个新增的方法效率要高很多，它不是将所有行一次性加入内存中，而是每次只读取一行。
        /// 这种方法比之前的要好，因为如果打开一个较大的文件，之前的方法要等整个文本都加入内存后，才能访问第一行。而这个新方法并不是将所有行都加载到内存中，而是每次只读取一行。更加高效。
        /// </summary>
        public void Fun4()
        {
            var path = Path.Combine(@"D:\迅雷下载", "ReadMe.txt");
            //不推荐使用
            var files = File.ReadAllLines(path);
            foreach (var item in files)
            {
                Console.WriteLine(item);
            }

            //推荐使用
            var files2 = File.ReadLines(path);
            foreach (var item in files2)
            {
                Console.WriteLine(item);
            }
        }

        /// <summary>
        /// File类 WriteAllLines()，AppendAllLines()写入文件
        /// </summary>
        public void Fun5()
        {
            var path = Path.Combine(@"D:\迅雷下载", "ReadMe.txt");
            string[] str = {
                "你好~",
                "大熊"
            };
            File.WriteAllLines(path, str);
            
            File.AppendAllLines(path, str);
        }

        /// <summary>
        /// Path常用方法
        /// </summary>
        public void Fun6()
        {
            var fileName = @"D:\数据库文件\数据库.txt";
            var ext = Path.GetExtension(fileName);
            var dir = Path.GetDirectoryName(fileName);
            var file =  Path.GetFileName(fileName);
            var fileWithoutExt = Path.GetFileNameWithoutExtension(fileName);
            var rootName = Path.GetPathRoot(fileName);
            var randomFile = Path.GetRandomFileName();
            var fullName = Path.GetFullPath(randomFile);
            var tempName = Path.GetTempFileName();
            var tempPath = Path.GetTempPath();
            var tempNameAfter = Path.ChangeExtension(tempName, "txt");
            Console.WriteLine($"目录名称:{ dir }，拓展名:{ext}，文件名:{file}，没有拓展名的文件名:{fileWithoutExt}");
            Console.WriteLine($"根目录名称:{rootName}，随机文件名：{randomFile}，随机文件路径：{fullName}，临时文件:{tempName}");
            Console.WriteLine($"临时文件夹路径:{tempPath}，修改后的临时文件:{tempNameAfter}");
        }
    }
}
