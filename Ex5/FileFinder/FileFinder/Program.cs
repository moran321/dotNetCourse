/* Moran Ankori */
/* Lab 10.2 */
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            string path = Path.GetFullPath(args[0]); //get directory
            Console.WriteLine(path);
            string subString = args[1];
            
            //get all files in path
            string[] allFiles =  Directory.GetFiles(path, "*", SearchOption.AllDirectories);

            //get files that contains the substring
            List<string> files = new List<string>();
            foreach (string str in allFiles)
            {
                if (str.Contains(subString))
                {
                    files.Add(str);
                }
            }
           

            //display files and their length
            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                long length = info.Length;   
                Console.WriteLine("File name: \"{0}\",\nLength: {1}\n",   Path.GetFileName(file), length);
            }

            Console.Read();

        } /****end main *****************/

    }/****end class *****************/
}
