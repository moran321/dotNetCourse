using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class Player
    {
        public enum PlayerType { Human, Computer };
        public PlayerType Playertype { get; }
        public string Name { get; }
        public int PlayerNumber { get; }
        public int NumberOfCheckersOut{ get; private set;}
        /****************************************/

        public Player(string playerName, int playerNumber)
        {
            if (playerName == "Computer")
            {
                Playertype = PlayerType.Computer;
            }
            else
            {
                Playertype = PlayerType.Human;
            }
            Name = playerName;
            PlayerNumber = playerNumber;
            NumberOfCheckersOut = 0;
        }
        /****************************************/

        public override string ToString()
        {
            return Name;
        }
        /****************************************/

        public void AddStoneOut()
        {
            if (NumberOfCheckersOut<15)
                NumberOfCheckersOut++;
        }
        /****************************************/
    }
}
