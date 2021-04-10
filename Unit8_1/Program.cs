using System;
using System.IO;

namespace Unit8_1
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime boundary = DateTime.Now.Subtract(TimeSpan.FromMinutes(30));

            Console.Write("Введите путь: ");
            string dirPath = Console.ReadLine();

            DirectoryInfo dirInfo = new DirectoryInfo(dirPath);

            if (!dirInfo.Exists)
            {
                Console.WriteLine("Папка по заданному адресу не существует");
                Console.ReadKey();
                return;
            }

            Console.WriteLine("Удаление...");

            try
            {
                DeleteByLastAccess(dirInfo, boundary);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            Console.WriteLine("Завершено успешно");
            Console.ReadKey();
        }

        public static void DeleteByLastAccess(DirectoryInfo dirInfo, DateTime boundary, bool isRoot = true)
        {
            if (dirInfo.LastAccessTime < boundary && !isRoot)
            {
                dirInfo.Delete(true);
                return;
            }

            FileInfo[] files = dirInfo.GetFiles();

            foreach (FileInfo f in files)
            {
                if (f.LastAccessTime < boundary)
                {
                    f.Delete();
                }
            }

            DirectoryInfo[] dirs = dirInfo.GetDirectories();

            foreach (DirectoryInfo d in dirs)
            {
                DeleteByLastAccess(d, boundary, false);
            }
        }
    }
}
