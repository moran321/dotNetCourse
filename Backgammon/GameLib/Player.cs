using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class Player
    {
        public string Name { get; }
        public int PlayerNumber { get; }
        public int NumberOfStonesOut{ get; private set;}

        public Player(string playerName, int playerNumber)
        {
            Name = playerName;
            PlayerNumber = playerNumber;
            NumberOfStonesOut = 0;
        }


        public override string ToString()
        {
            return Name;
        }

        public void AddStoneOut()
        {
            if (NumberOfStonesOut<15)
                NumberOfStonesOut++;
        }

    }
}
