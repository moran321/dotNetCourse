using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameLib
{
    public interface BackgammonGame
    {

        void EndGame();

        Player getPlayerOne();

        Player getPlayerTwo();

        bool IsGameOver();

        Board GetGameBoard();

        HashSet<Move> GetPlayerOptions(Player currentPlayer);

        int[] rollDice();

        bool IsDouble();

        bool MakeMove(Player player, int from, int to);

        int GetLengthOfCurrentTurn();

        bool CheckValidation(Player player, int from, int to);

    }
}
