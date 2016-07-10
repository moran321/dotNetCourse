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
        public BackgammonGame BackgammonGameInstance { get; private set; }
        public Player currentPlayer { get; private set; }
      


        //C'tor
        /****************************************/
        public GameManager(GameType gametype,string player1name, string player2name )
        {
            if (gametype == GameType.TwoPlayers)
            {
                BackgammonGameInstance = new TwoPlayersGame(player1name, player2name);
            }
            else if (gametype == GameType.OnePlayer)
            {

            }

            currentPlayer = BackgammonGameInstance.getPlayerOne();

        } /****************************************/


        public void ChangeTurn()
        {
            if (currentPlayer == BackgammonGameInstance.getPlayerOne())
            {
                currentPlayer = BackgammonGameInstance.getPlayerTwo();
            }
            else
            {
                currentPlayer = BackgammonGameInstance.getPlayerOne();
            }
        }

        public Player getCurrentPlayer()
        {
            return currentPlayer;
        } /****************************************/


        public bool IsGameOver()
        {
            return BackgammonGameInstance.IsGameOver();
        }

        /****************************************/
        public bool Play(int fromLine, int toLine)
        {
            //check input
            bool isValid = BackgammonGameInstance.CheckValidation(currentPlayer, fromLine, toLine);
            if (!isValid)
            {
                return false;
            }
            //check if thers is moves
            if (BackgammonGameInstance.GetPlayerOptions(currentPlayer).Count == 0)
            {
                ChangeTurn();
                return true;
            }
            //take a move
            BackgammonGameInstance.MakeMove(currentPlayer, fromLine, toLine);

            //change turn if player used both dice
            if (BackgammonGameInstance.GetLengthOfCurrentTurn()==0)
                ChangeTurn();

            return true;
        } /****************************************/

        private List<int> GetPlayerLanes(Player player)
        {
            return BackgammonGameInstance.GetGameBoard().GetPlayerRowOccupation(player);
        } /****************************************/

        public List<int> GetPlayerLanes(int player)
        {
            if (player == 1)
            {
                return GetPlayerLanes(BackgammonGameInstance.getPlayerOne());
            }
            else
            {
                return GetPlayerLanes(BackgammonGameInstance.getPlayerTwo());
            }
        } /****************************************/

        public bool CheckInput(int from, int to)
        {
          return  BackgammonGameInstance.CheckValidation(currentPlayer, from, to);
        } /****************************************/


        public bool CheckInput(int from)
        {
            return BackgammonGameInstance.CheckValidation(currentPlayer, from, 0);
        } /****************************************/

    }
}
