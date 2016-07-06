using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FileFinder
{
    class FileFinderRecursion
    {
        //return file if contains the substring
        public void FindFiles(string file, string subString, List<string> files)
        {
            FileAttributes attr = File.GetAttributes(file);

            if (attr.HasFlag(FileAttributes.Directory))  //folder
            {

                string[] allFiles = Directory.GetFileSystemEntries(file, "*", SearchOption.TopDirectoryOnly);
               
                //  DirectoryInfo[] dirInfo = new DirectoryInfo(file).GetDirectories();

                foreach (string str in allFiles)
                {
                    FindFiles(str, subString, files); //recursion call
                }
            }
            else  //regular file
            {
                if (file.Contains(subString))
                {
                    files.Add(file);
                    return;
                }
            }
        }/**************/
    }
}
