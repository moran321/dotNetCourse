/*Moran Ankori*/
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GameLib;
namespace ConsoleUI
{
    public class ConsoleGame
    {
        public static void Main(string[] args)
        {
            GameDisplay display = new GameDisplay(); 
            display.Start();
            Console.Read();
        } 
    }
}
