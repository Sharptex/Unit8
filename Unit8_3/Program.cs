using System;
using System.IO;
using System.Linq;

namespace Unit8_3
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime boundary = DateTime.Now.Subtract(TimeSpan.FromMinutes(30));
            int filesCount = 0;

            Console.Write("Введите путь: ");
            string dirPath = Console.ReadLine();

            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

            if (!dirInfo.Exists)
            {
                Console.WriteLine("Папка по заданному адресу не существует");
                Console.ReadKey();
                return;
            }

            long startSize = GetDirectorySize(dirInfo);

            Console.WriteLine($"Исходный размер папки: {startSize} байт");
            Console.WriteLine("Удаление...");

            try
            {
                filesCount = DeleteByLastAccess(dirInfo, boundary);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            Console.WriteLine("Завершено успешно");

            long finalSize = GetDirectorySize(dirInfo);

            Console.WriteLine($"Файлов удалено: {filesCount}");
            Console.WriteLine("Освобождено: {0} байт", startSize - finalSize);
            Console.WriteLine("Текущий размер папки: {0} байт", finalSize);

            Console.ReadKey();
        }

        public static int DeleteByLastAccess(DirectoryInfo dirInfo, DateTime boundary, bool isRoot = true)
        {
            int filesCount = 0;

            if (dirInfo.LastAccessTime < boundary && !isRoot)
            {
                filesCount = dirInfo.GetFiles("*.*", SearchOption.AllDirectories).Count();
                dirInfo.Delete(true);
                return filesCount;
            }

            FileInfo[] files = dirInfo.GetFiles();

            foreach (FileInfo f in files)
            {
                if (f.LastAccessTime < boundary)
                {
                    f.Delete();
                    filesCount++;
                }
            }

            DirectoryInfo[] dirs = dirInfo.GetDirectories();

            foreach (DirectoryInfo d in dirs)
            {
                filesCount += DeleteByLastAccess(d, boundary, false);
            }

            return filesCount;
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
