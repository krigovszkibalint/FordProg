using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FordProg_KDPEQ8
{
    class Program
    {
        static void Main(string[] args)
        {
            string inpath = @"..\\input.txt";
            string outpath = @"..\\output.txt";
            SourceHandler sh = new SourceHandler();
            sh.Path = inpath;
            sh.openFile();
            sh.replaceContent();
            sh.openWrite(outpath);
            Console.WriteLine("A fájl beolvasás és fájlba kiírás sikeres");
            Console.ReadKey();
        }
    }
}
