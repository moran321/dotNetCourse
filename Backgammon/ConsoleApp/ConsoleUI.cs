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

        
        /****************************************/
        public static void Main(string[] args)
        {
            //create display object
            GameDisplay display = new GameDisplay(); // UI project
            display.Start();

            Console.Read();

        } /****************************************/


    }
}
