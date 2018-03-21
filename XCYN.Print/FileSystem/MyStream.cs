using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace XCYN.Print.FileSystem
{
    public class MyStream
    {

        /// <summary>
        /// 使用压缩流(压缩)
        /// </summary>
        /// <param name="fileName">被压缩文件</param>
        /// <param name="compressFileName">压缩文件</param>
        public void Fun1(string fileName, string compressFileName)
        {
            using (FileStream inputStream = File.OpenRead(fileName))
            {
                FileStream outputStream = File.OpenWrite(compressFileName);
                using (var compressStream = new DeflateStream(outputStream, CompressionMode.Compress))
                {
                    inputStream.CopyTo(compressStream);
                }
            }
        }

        /// <summary>
        /// 使用压缩流(解压)
        /// </summary>
        public void Fun2(string fileName)
        {
            FileStream inputStream = File.OpenRead(fileName);
            using (MemoryStream outputStream = new MemoryStream())
            {
                using (var compressStream = new DeflateStream(inputStream, CompressionMode.Decompress))
                {
                    compressStream.CopyTo(outputStream);
                    outputStream.Seek(0, SeekOrigin.Begin);
                    using (var reader = new StreamReader(outputStream, Encoding.UTF8, true, 4096))
                    {
                        string result = reader.ReadToEnd();
                        Console.WriteLine(result);
                    }
                }
            }
            Console.Read();
        }

        public static void CreateZipFile(string directory, string zipFile)
        {
            InitSampleFilesForZip(directory);
            string destDirectory = Path.GetDirectoryName(zipFile);
            if (!Directory.Exists(destDirectory))
            {
                Directory.CreateDirectory(destDirectory);
            }
            FileStream zipStream = File.Create(zipFile);
            using (var archive = new ZipArchive(zipStream, ZipArchiveMode.Create))
            {
                IEnumerable<string> files = Directory.EnumerateFiles(directory, "*", SearchOption.TopDirectoryOnly);
                foreach (var file in files)
                {
                    ZipArchiveEntry entry = archive.CreateEntry(Path.GetFileName(file));
                    using (FileStream inputStream = File.OpenRead(file))
                    using (Stream outputStream = entry.Open())
                    {
                        inputStream.CopyTo(outputStream);
                    }
                }
            }
        }

        private static void InitSampleFilesForZip(string directory)
        {
            if (!Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
                for (int i = 0; i < 10; i++)
                {
                    string destFileName = Path.Combine(directory, $"test{i}.txt");
                    File.Copy("Test.txt", destFileName);
                }
            } 
        }
    }
}
