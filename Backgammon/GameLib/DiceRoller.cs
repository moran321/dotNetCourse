using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class DiceRoller
    {
        public int Dice1 { get; private set; }
        public int Dice2 { get; private set; }
        public int[] Roll()
        {
            Random rand = new Random();
            Dice1 = rand.Next(1, 7);
            Dice2 = rand.Next(1, 7);
            return (new int[] { Dice1, Dice2 });
        }

        public void DiceUsed(int length)
        {
            if (length < Dice2 + Dice1)
            {
                if (Dice1 == length)
                {
                    Dice2 = 0;
                }
                else if (Dice2 == length)
                {
                    Dice1 = 0;
                }
            }
        }
    }
}
