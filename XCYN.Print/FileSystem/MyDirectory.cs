using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.FileSystem
{
    public class MyDirectory
    {
        /// <summary>
        /// GetDirectories()遍历文件夹
        /// </summary>
        /// <param name="dirName"></param>
        /// <param name="search"></param>
        /// <param name="level"></param>
        public void Fun1(string dirName,string search,string level)
        {
            var dir = Directory.GetDirectories(dirName);
            for (int i = 0; i < dir.Length; i++)
            {
                if(dir.Contains(search))
                {
                    Console.WriteLine(level + dir[i]);
                }
                Fun1(dir[i], search, level + "--");
            }
            
        }

        /// <summary>
        /// Directory类的EnumerateDirectories()递归遍历文件夹
        /// </summary>
        public void Fun2()
        {
            var dir = Directory.EnumerateDirectories(@"D:\", "layer", SearchOption.AllDirectories);
            try
            {
                foreach (var item in dir)
                {
                    Console.WriteLine(item);
                }
            }
            catch(System.UnauthorizedAccessException ex)
            {
                Console.WriteLine(ex.Message);
            }
            
        }

        /// <summary>
        /// 读取流
        /// </summary>
        public void Fun3()
        {
            var path = Path.Combine(@"D:\迅雷下载", "ReadMe.txt");
            using (FileStream stream = new FileStream(path,FileMode.OpenOrCreate))
            {
                byte[] b = new byte[stream.Length];
                var r = stream.Read(b, 0, (int)stream.Length);
                var s = Encoding.UTF8.GetString(b);
                Console.WriteLine(s);
                
            }
        }

        /// <summary>
        /// 复制流
        /// </summary>
        public void Fun4(string inputFile,string outputFile)
        {
            const int BufferSize = 4096;
            //var path = Path.Combine(@"D:\迅雷下载", "ReadMe.txt");
            using (FileStream readStream = File.OpenRead(inputFile))
            {
                using (FileStream writeStream = File.OpenWrite(outputFile))
                {
                    byte[] buffer = new byte[BufferSize];
                    bool flag = false;
                    do
                    {
                        int read = readStream.Read(buffer, 0, BufferSize);
                        if (read == 0)
                        {
                            //read == 0表示已经没有可以读取的内容
                            flag = true;
                        }   
                        writeStream.Write(buffer, 0, read);
                    } while (!flag);
                }
            }
            
        }

        /// <summary>
        /// 复制流(功能等同于Fun4)
        /// </summary>
        public void Fun5(string inputFile, string outputFile)
        {
            using (FileStream readStream = File.OpenRead(inputFile))
            {
                using (FileStream writeStream = File.OpenWrite(outputFile))
                {
                    readStream.CopyTo(writeStream);
                }
            }

        }

        /// <summary>
        /// 创建一个大文件
        /// </summary>
        /// <param name="record"></param>
        public static async void CreateBigFile(int count)
        {
            string path = "./bigFile.txt";
            FileStream stream = File.Create(path);
            using (var writer = new StreamWriter(stream))
            {
                var r = new Random();
                var record = Enumerable.Range(0, count).Select(x => new {
                    Number = x,
                    Text = $"Sample text {r.Next(200)}",
                    Date = new DateTime(Math.Abs((long)((r.NextDouble() * 2 - 1) * DateTime.MaxValue.Ticks)))
                });
                foreach (var item in record)
                {
                    string s = $"#{item.Number,8};{item.Text,-20};{item.Date.ToString("yyyy-MM-dd")}#{Environment.NewLine}";
                    await writer.WriteAsync(s);
                }
            }
        }

        /// <summary>
        /// 使用读取器StreamReader
        /// </summary>
        public void Fun6()
        {
            string path = @"D:\迅雷下载\ReadMe.txt";
            goto s3;
            s1:
            //StreamReader可以使用EndOfStream检查是否在文件末尾，使用ReadLine()读取文本行
            using (var stream = new StreamReader(path))
            {
                while(!stream.EndOfStream)
                {
                    string line = stream.ReadLine();
                    Console.WriteLine(line);
                }
            }
            s2:
            //还可以使用File类的OpenText方法创建StreamReader,并且StreamReader还可以调用ReadToEnd()方法读取完整的文件。
            using (var stream = File.OpenText(path))
            {
                var content = stream.ReadToEnd();
                Console.WriteLine(content);
            }
            s3:
            //StreamReader还可以将内容写入一个字符数组中
            using (var stream = new StreamReader(path))
            {
                char[] c = new char[stream.BaseStream.Length];
                var lines = stream.Read(c, 0, (int)stream.BaseStream.Length);
                string str = new string(c);
                Console.WriteLine(str);
            }
        }

        /// <summary>
        /// 使用写入器StreamWriter
        /// </summary>
        public void Fun7(string[] lines)
        {
            string path = @"D:\迅雷下载\Copy.txt";
            var stream = File.OpenWrite(path);
            using (var writer = new StreamWriter(stream))
            {
                writer.WriteLine("中国第一{0}","哈哈");
                writer.WriteLine("美国第一{0}","哈哈");
            }
        }

        /// <summary>
        /// 写入二进制文件
        /// </summary>
        public void Fun8()
        {
            string path = @"D:\迅雷下载\Binary.txt";
            var stream = File.Create(path);
            using (var writer = new BinaryWriter(stream))
            {
                writer.Write(1);
                writer.Write("哈哈哈");
            }
        }

       

        
    }
}
