using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace FileManager
{
    class SourceHandler
    {
        string path1, path2 = "";
        public string content = "";

        public SourceHandler(string path1, string path2)
        {
            this.path1 = path1;
            this.path2 = path2;
        }

        public void openFileToRead()
        {
            StreamReader SR = new StreamReader(File.OpenRead(this.path1));
            content = SR.ReadToEnd();
            SR.Close();
        }

        public void replaceContent(string s)
        {
            this.content += s;
        }

        public void openFileToWrite()
        {
            StreamWriter SW = new StreamWriter(File.Open(path2, FileMode.Create));
            SW.WriteLine(this.content);
            SW.Flush();
            SW.Close();
        }

        public void replaceFirst()
        {
            //single line
            content = Regex.Replace(content, @"//(.*?)\r\n", "");
            //block
            content = Regex.Replace(content, @"/\*(.?)\*/", "");

            replaceText("   "," ");
            replaceText("  ", " ");
            replaceText(" (", "(");

        }

        //"1,2,3,4;int i=10;while(i<10){i++;}"

        //replaceText(" ", "");
        public void replaceText(string from, string to)
        {
            while (content.Contains(from))
            {
                content = content.Replace(from, to);
            }
        }
    }
}
