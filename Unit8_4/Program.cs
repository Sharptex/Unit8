using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace FinalTask
{
    [Serializable]
    public class Student
    {
        public string Name { get; set; }
        public string Group { get; set; }
        public DateTime DateOfBirth { get; set; }
    }

    class Program
    {
        static void Main(string[] args)
        {
            string sourceFile = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Students.dat");
            Student[] students;

            using (var fs = new FileStream(sourceFile, FileMode.Open))
            {
                BinaryFormatter formatter = new BinaryFormatter();
                students = (Student[])formatter.Deserialize(fs);
            }

            DirectoryInfo dirInfo = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Desktop), "Students"));
            if (dirInfo.Exists)
            {
                dirInfo.Delete(true);
            }
            dirInfo.Create();

            foreach (Student item in students)
            {
                string group = item.Group;
                using (StreamWriter sw = File.AppendText(Path.Combine(dirInfo.FullName, $"{group}.txt"))) 
                {
                    sw.WriteLine($"{item.Name}, {item.DateOfBirth}");
                }
            }

            Console.ReadLine();
        }
    }
}
