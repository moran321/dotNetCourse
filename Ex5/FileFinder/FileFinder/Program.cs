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
            string pattern = args[1];
            
            //get all files that match the pattern
            string[] allFiles =  Directory.GetFiles(path, pattern, SearchOption.AllDirectories);

           

            //display files and their length
            foreach (string file in allFiles)
            {
                FileInfo info = new FileInfo(file);
                long length = info.Length;   
                Console.WriteLine("File name: \"{0}\",\nLength: {1}\n",   Path.GetFileName(file), length);
            }

            Console.Read();

        } /****end main *****************/

    }/****end class *****************/
}
