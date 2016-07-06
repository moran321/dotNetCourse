/* Moran Ankori */
/* Lab 10.2 */
using System;
using System.Collections.Generic;
using System.IO;


namespace FileFinder
{
    class Program
    {
        static void Main(string[] args)
        {
            //get input from command line
            string directoryPath = args[0];
            string subString = args[1];

            string path = Path.GetFullPath(directoryPath);


            List<string> files = new List<string>();
            FileFinderRecursion recursion = new FileFinderRecursion();
            recursion.FindFiles(path, subString, files);

            //display files and their length
            foreach (string file in files)
            {
                FileInfo info = new FileInfo(file);
                long length = info.Length;   
                Console.WriteLine("File name: \"{0}\",\nLength: {1}\n",   Path.GetFileName(file), length);
            }

            Console.Read();

        }/**************/






    }
}
