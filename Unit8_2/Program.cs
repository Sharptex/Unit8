using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Unit8_2
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.Write("Введите путь: ");
            string dirPath = Console.ReadLine();

            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

            if (!dirInfo.Exists)
            {
                Console.WriteLine("Папка по заданному адресу не существует");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Начат подсчет размера папки...");
            long sum = 0;

            try
            {
                sum = GetDirectorySize(dirInfo);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine($"Размер папки {sum} байт");
            Console.ReadKey();
        }

        public static long GetDirectorySize(DirectoryInfo dirInfo)
        {
            long sum = 0;
            FileInfo[] files = dirInfo.GetFiles();

            foreach (FileInfo f in files)
            {
                sum += f.Length;
            }

            DirectoryInfo[] dirs = dirInfo.GetDirectories();

            foreach (DirectoryInfo d in dirs)
            {
                sum += GetDirectorySize(d);
            }

            return sum;
        }
    }
}
