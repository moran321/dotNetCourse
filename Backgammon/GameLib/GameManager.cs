using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GameLib
{
    public class GameManager
    {
        public enum GameType { TwoPlayers, OnePlayer, ComputerOnly };
        public enum Victory { Regular, Mars, TurkishMars }
        public Game BackgammonGameInstance;
        public Player currentPlayer { get; private set; }
        /****************************************/

        //C'tor
        public GameManager(GameType gametype, string player1name, string player2name)
        {

            BackgammonGameInstance = new Game(player1name, player2name);
            currentPlayer = BackgammonGameInstance.GetPlayerOne();

        }
        /****************************************/


        public void ChangeTurn()
        {
            if (currentPlayer == BackgammonGameInstance.GetPlayerOne())
            {
                currentPlayer = BackgammonGameInstance.GetPlayerTwo();
            }
            else
            {
                currentPlayer = BackgammonGameInstance.GetPlayerOne();
            }
            if (currentPlayer.Name.Equals("Computer"))
                ComputerPlay();
        }
        /****************************************/

        public Player GetCurrentPlayer()
        {
            return currentPlayer;
        }
        /****************************************/


        public bool IsGameOver()
        {
            return BackgammonGameInstance.IsGameOver();
        }
        /****************************************/

        public bool Play(int fromLine, int toLine)
        {
            //check input
            bool isValid = CheckIfMoveExist(currentPlayer, fromLine, toLine);
            if (!isValid)
            {
                return false;
            }
            //check if thers is moves
            if (BackgammonGameInstance.GetPlayerOptions(currentPlayer).Length == 0)
            {
                ChangeTurn();
                return true;
            }

            //take a move
            BackgammonGameInstance.MakeMove(currentPlayer, fromLine, toLine);

            //change turn if player used both dice
            if (BackgammonGameInstance.GetLengthOfCurrentTurn() == 0)
            {
                ChangeTurn();
            }

            return true;
        } /****************************************/


        private void ComputerPlay()
        {
            RollDice();
            do
            {
                if (!BackgammonGameInstance.ComputerTurn(currentPlayer))
                {
                    ChangeTurn();
                    return;
                }
                    
            }while (BackgammonGameInstance.GetLengthOfCurrentTurn() > 0);

            ChangeTurn();
        }
        /****************************************/

        private int[] GetPlayerLanes(Player player)
        {
            return BackgammonGameInstance.GetPlayerRowOccupation(player);
        } 
        /****************************************/

        public int[] GetPlayerLanes(int player)
        {
            if (player == 1)
            {
                return GetPlayerLanes(BackgammonGameInstance.GetPlayerOne());
            }
            else
            {
                return GetPlayerLanes(BackgammonGameInstance.GetPlayerTwo());
            }
        }
        /****************************************/

        public bool CheckInput(int from, int to)
        {
            return CheckIfMoveExist(currentPlayer, from, to);
        } 
        /****************************************/



        private bool CheckIfMoveExist(Player player, int from, int to)
        {
            Move move = BackgammonGameInstance.FindMove(player, from, to);
            if (move != null)
            {
                return true;
            }
            return false;
        }
        /****************************************/


        public int[] RollDice()
        {
            return BackgammonGameInstance.RollDice();
        }
        /****************************************/

        public Move[] GetPlayerMoves()
        {
            return BackgammonGameInstance.GetPlayerOptions(currentPlayer);
        }
        /****************************************/

        public CellContent[,] GetBoard()
        {
            return BackgammonGameInstance.GetGameBoard();
        }  
        /****************************************/

        public CellContent[] GetEatenStones()
        {
            return BackgammonGameInstance.GetEatenStones();            
        }
        /****************************************/

        public int GetLengthOfCurrentTurn()
        {
            return BackgammonGameInstance.GetLengthOfCurrentTurn();
        }

    }
}
