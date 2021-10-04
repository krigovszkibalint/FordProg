using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace FordProg_KDPEQ8
{
    class SourceHandler
    {
        string path = @"";

        string sourceCode;

        string content;

        //string rulePath;

        //string rules;

        /*     public string RulePath
             {
                 get
                 {
                     return this.rulePath;
                 }

                 set
                 {
                     if (value != null)
                     {
                         this.rulePath = value;
                     }
                     else
                     {
                         Console.WriteLine("A beolvasott path értéke ne legyen null");
                     }
                 }
             }
        */

        Dictionary<string, string> replaces = new Dictionary<string, string>
        {
        //RULES for text replacements

            {"  "," "},
            {"   "," "},
            {" )",")"},
            {") ",")"},
            {"= ","="},
            {" =","="},
            {"{ ","{"},
            {" {","{"},
            {"} ","}"},
            {" }","}"},
        };
        public string Path
        {
            get
            {
                return this.path;
            }

            set
            {
                if (value != null)
                {
                    this.path = value;
                }
                else
                {
                    Console.WriteLine("A beolvasott path értéke ne legyen null");
                }
            }
        }

        /*       public void openRules()
               {
                   try
                   {
                       StreamReader sr = new StreamReader(File.OpenRead(rulePath));
                       string newLine, lineOne, lineTwo;
                       newLine = sr.ReadLine();
                       while (newLine != null)
                       {
                           newLine.Split(",");
                           lineOne = sr.ReadLine();
                           lineOne.Replace("(", "");
                           lineOne.Replace(")", "");
                           lineTwo = sr.ReadLine();
                           lineTwo.Replace("(", "");
                           lineTwo.Replace(")", "");
                           replacingRules.Add(lineOne, lineTwo);
                       } 
                       sr.Close();
                   }

                   catch (IOException e)
                   {
                       Console.WriteLine(e.Message);
                   }

                   catch (Exception e)
                   {
                       Console.WriteLine(e.Message);
                   }
               }
        */
        public void openFile()
        {
            try
            {
                StreamReader sr = new StreamReader(File.OpenRead(path));
                sourceCode = sr.ReadToEnd();
                content = sourceCode;
                sr.Close();
            }

            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        public void replaceFirst()
        {
            //one line comment elemination
            content = Regex.Replace(content, @"//.*?\n", "");

            //block comment elemination
            content = Regex.Replace(content, @"/\*(.*?)\*/", "");

        }

        public void replaceContent()
        {
            //online comment
            string oneLineComment = @"//.*?\n";
            content = Regex.Replace(content, oneLineComment, " ");

            string blockComment = @"/[*] [\w\d\s]+ [*]/";
            content = Regex.Replace(content, blockComment, " ");


            foreach (var item in replaces)
            {
                while (content.Contains(item.Key))
                {
                    content = content.Replace(item.Key, item.Value);
                }
            }
        }

        public void openWrite(string path)
        {

            try
            {
                StreamWriter sr = new StreamWriter(File.OpenWrite(path));
                sr.WriteLine(content);
                sr.Flush();
                sr.Close();
            }

            catch (IOException e)
            {
                Console.WriteLine(e.Message);
            }

            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

        public SourceHandler(string text)
        {
            this.content = text;
            replaceContent();
            Console.WriteLine(content);
        }

        public SourceHandler() { }

        /*public sourceHandler(string path)
        {
            try
            {
                this.path = path;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }*/
    }
}
