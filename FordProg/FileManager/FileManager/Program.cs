using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace FileManager
{
    class Program
    {
        static void Main(string[] args)
        {

            SourceHandler SH = new SourceHandler(@"filename1.txt", "filename2.txt");
            SH.openFileToRead();
            Console.WriteLine(SH.content);
            SH.replaceContent("new text");
            Console.WriteLine(SH.content);
            SH.openFileToWrite();
            Console.ReadKey();
            /*
            StreamReader SR = new StreamReader(File.OpenRead("filepath1"));
            string s = SR.ReadToEnd();

            while (SR.Peek() > -1 )
            {
                s = SR.ReadLine();
            }

            SR.Close();

            StreamWriter SW = new StreamWriter(File.Open("filepath2", FileMode.Create));
            SW.WriteLine("text")
            SW.Flush();
            SW.Close();
            */
        }
    }
}
