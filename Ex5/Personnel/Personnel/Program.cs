/*Moran Ankori*/
/*Lab 10.1*/
using System;
using System.Collections.Generic;
using System.IO;


namespace Personnel
{
    class Program
    {
      

        static void Main(string[] args)
        {
            List<string> myList = ReadData();
            
           
            foreach (string str in myList)
            {
                Console.WriteLine(str);
            }

            Console.Read();
        }  /***********************************/

        //this method open StreamReader to read from file and return the data in list of strings
        public static List<string> ReadData()
        {
            List<string> myList = new List<string>();
            string fileName = "data2.txt";
            try
            {
                using (StreamReader sr = new StreamReader(fileName))
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
        } /***********************************/



    } 
}
