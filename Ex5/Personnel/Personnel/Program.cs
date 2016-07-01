/*Moran Ankori*/
/*Lab 10.1*/
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Personnel
{
    class Program
    {
        static void Main(string[] args)
        {
            List<string> myList = ReadData();
            
            //print list
            foreach (string str in myList)
            {
                Console.WriteLine(str);
            }

            Console.Read();
        } ///////////////////////


        public static List<string> ReadData()
        {
            List<string> myList = new List<string>();

            try
            {
                // Create an instance of StreamReader to read from a file.
                // The using statement also closes the StreamReader.
                using (StreamReader sr = new StreamReader("data.txt"))
                {
                    string line;
                    // Read and display lines from the file until the eof is reached.
                    while ((line = sr.ReadLine()) != null)
                    {
                        myList.Add(line);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine("The file could not be read:");
                Console.WriteLine(e.Message);
            }
            return myList;
        }



    } ///end class
}
