using System;
using System.Collections.Generic;
using System.IO;
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
    }
}
