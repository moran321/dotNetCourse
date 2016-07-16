using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GameLib
{
    public class Game
    {
        private BackgammonLogic _backgammonLogic;
        /****************************************/

        //2 players game
        public Game(string player1name, string player2name)
        {
            _backgammonLogic = new BackgammonLogic(player1name, player2name);
        }
        /****************************************/

        //1 player game
        public Game(string player1name)
        {
            _backgammonLogic = new BackgammonLogic(player1name, "Computer");
        }
        /****************************************/

        public int GetCheckersInLane(int lane)
        {
          return  _backgammonLogic.GetLane(lane);
        }
        /****************************************/

        public int[] GetDiceResults()
        {
            return _backgammonLogic.GetDiceResults();
        }

        //generate random move from the list of valid moves
        //if there is no valid moves, return false
        public bool ComputerTurn(Player player, int indexOfMove)
        {
            Move move = GetPlayerOptions(player)[indexOfMove];
            if (move != null)
            {
                MakeMove(player, move.From, move.To);
                return true;
            }
            return false;
                
        }
        /****************************************/

        public Move[] GetPlayerOptions(Player player)
        {
            return _backgammonLogic.GetPlayerOptions(player).ToArray();
        }
        /****************************************/

        public bool MakeMove(Player player, int from, int to)
        {
            Move move = FindMove(player,from, to);
            return _backgammonLogic.MakeMove(player, move);
        }
        /****************************************/


        public Move FindMove(Player player, int from, int to)
        {
            return _backgammonLogic.FindMove(player, from, to);
        }
        /****************************************/

        public bool IsGameOver()
        {
            return _backgammonLogic.IsGameOver;
        }
        /****************************************/

        public int[] RollDice()
        {
            return _backgammonLogic.RollDice();
        }
        /****************************************/

        public CellContent[,] GetGameBoard()
        {
            return _backgammonLogic.GetGameBoard();
        }
        /****************************************/

        public bool IsDouble()
        {
            return _backgammonLogic.IsDouble();
        }
        /****************************************/


        public int GetLengthOfCurrentTurn()
        {
            return _backgammonLogic.GetLengthOfCurrentTurn();
        }
        /****************************************/


        public CellContent[] GetEatenCheckers()
        {
            return _backgammonLogic.GetEatenStones();
        }
        /****************************************/

        public int[] GetPlayerRowOccupation(Player player)
        {
            return _backgammonLogic.GetPlayerRowOccupation(player);
        }
        /****************************************/

        public Player GetPlayerOne()
        {
            return _backgammonLogic.PlayerOne;
        }
        /****************************************/

        public Player GetPlayerTwo()
        {
            return _backgammonLogic.PlayerTwo;
        }
        /****************************************/





    }


}