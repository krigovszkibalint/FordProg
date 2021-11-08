using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.RegularExpressions;

//IF (a==2) {b==6}
//10 20 001 50 002 60 50 003 50 004 70...
//symbolTable["a","2","b","6"]

namespace SourceHandler
{
    class SourceHandler
    {

        string source, finalcode = "";
        string content = "";

        Dictionary<string, string> replaces = new Dictionary<string, string>
        {
            //keywords
            {"IF", "10"},
            {"(", "20"},
            {")", "30"},
            {"=", "40"},
            {"==", "50"},
            {"{", "60"},
            {"}", "70"},
            //whitespaces
            {"\n", " "},
            {"  ", " "},
            {"{ ", "{"},
            {" {", "{"},
            {"} ", "}"},
            {" }", "}"},
            {"( ", "("},
            {" (", "("},
            {") ", ")"},
            {" )", ")"}
        };

        List<String> symbolTable = new List<string>();
        int symbolTableIndex = 0;

        string changeVarConst(string nameVarConst)
        {
            symbolTable.Add(nameVarConst);
            symbolTableIndex += 1;
            string res = "00" + symbolTableIndex.ToString();
            //001, 0012 => 012
            return res.Substring(res.Length - 3);
        }

        public void replaceContent()
        {
            var blockComment = @"/[*][\w\d\s]+[*]/";
            var lineComment = @"//.*?\n";

            string patternNumber = @"([0-9]+)";
            string patternVar = @"([a-z-_]+)";
            //string replaceNumber = " CONST[$1]";
            //string replaceVar = " VAR[$1]";

            content = Regex.Replace(content, blockComment, " ");
            content = Regex.Replace(content, lineComment, " ");

            content = Regex.Replace(content, patternNumber, changeVarConst("$1"));
            content = Regex.Replace(content, patternVar, changeVarConst("$1"));

            foreach (var x in replaces)
            {
                while (content.Contains(x.Key))
                {
                    content = content.Replace(x.Key, x.Value);
                }
            }
        }

        public void openFileToRead()
        {
            try
            {
                StreamReader SR = new StreamReader(File.OpenRead(source));
                content = SR.ReadToEnd();
                SR.Close();
            }
            catch (IOException IOE)
            {
                Console.WriteLine(IOE.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void openFileToWrite()
        {
            try
            {
                StreamWriter SW = new StreamWriter(File.Open(finalcode, FileMode.Create));
                SW.WriteLine(content);
                SW.Flush();
                SW.Close();
            }
            catch (IOException IOE)
            {
                Console.WriteLine(IOE.Message);
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public SourceHandler(string source, string finalcode)
        {
            this.source = source;
            this.finalcode = finalcode;
        }

    }
    class Program
    {
        static void Main(string[] args)
        {
            string blockComments = "\n\n/* block  comment \n" +
                "almalma" +
                "*/\n" +
                "//line comment\n" +
                "//  line comment\n" +
                "IF (a==2) {b==6}";
            var blockComment = @"/[*][\w\d\s]+[*]/";
            var lineComment = @"//.*?\n";

            Console.WriteLine("Eredeti szöveg: {0}",blockComments);

            string result = Regex.Replace(blockComments, blockComment, String.Empty);
            result = Regex.Replace(result, lineComment, String.Empty);

            Console.WriteLine("Eredmény: {0}", result);

            string patternNumber = @"([0-9]+)";
            string patternVar = @"([a-z-_]+)";
            string replaceNumber = " CONST[$1]";
            string replaceVar = " VAR[$1]";

            result = Regex.Replace(result, patternNumber, replaceNumber);
            result = Regex.Replace(result, patternVar, replaceVar);

            result = result.Replace("IF", "10");
            result = result.Replace("{", "20");
            result = result.Replace("}", "30");

            Console.WriteLine("Eredmény 2: {0}", result);

            Console.ReadKey();
        }
    }
}
