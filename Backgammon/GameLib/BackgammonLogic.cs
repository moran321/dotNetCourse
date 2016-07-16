using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace GameLib
{
    public class BackgammonLogic
    {

        public Player PlayerOne { get; }
        public Player PlayerTwo { get; }
        public Board gameBoard;
        private DiceRoller roller;
        public bool IsGameOver { get; private set; }
        private int lengthOfCurrentTurn;

        //C'tor
        public BackgammonLogic(string player1name, string player2name)
        {
            IsGameOver = false;
            PlayerOne = new Player(player1name, 1);
            PlayerTwo = new Player(player2name, 2);
            roller = new DiceRoller();
            gameBoard = new Board();

           // gameBoard.InitializeBoardCustom();
             gameBoard.InitializeBoard();
        }
        /****************************************/

        public int GetLane(int lane)
        {
            return gameBoard.GetNumOfCheckersInLane(lane);

        }

        //return all the valid moves of the player
        public HashSet<Move> GetPlayerOptions(Player player)
        {
            HashSet<Move> validMoves = GetValidMoves(player, new int[] { roller.Dice1, roller.Dice2 });
            return validMoves;

        }
        /****************************************/

        public bool MakeMove(Player player, Move move)
        {
            if ((move = FindMove(player, move.From, move.To)) == null)
            {
                return false;
            }
            //eat move
            if (move.Type == Move.MoveType.Eat)
            {
                gameBoard.EatOtherPlayer(move);
            }
            //get back to game
            else if (move.Type == Move.MoveType.Eaten)
            {
                gameBoard.GetBackFromEaten(player, move.To);
            }
            //move out stone
            else if (move.Type == Move.MoveType.Out)
            {
                gameBoard.MoveStoneOut(move.From);
                player.AddStoneOut();
                //if the player finished to take out all his stones
                if (player.NumberOfCheckersOut == 15)
                {
                    IsGameOver = true;
                }
            }
            //regulaer move
            else
            {
                gameBoard.MakeRegularMove(move);
            }

            int moveLength = move.Length;

            //when moving out the player can move less than the dice number
            if (move.Type == Move.MoveType.Out)
            {
                int diff1 = roller.Dice1 - move.Length;
                int diff2 = roller.Dice2 - move.Length;

                if (diff1 == 0)
                {

                    moveLength = roller.Dice1;
                }
                else if (diff2 == 0)
                {

                    moveLength = roller.Dice2;
                }
                else if (diff1 < 0)
                {
                    //use the dice that is closer to the move length

                    moveLength = roller.Dice2;
                }
                else if (diff2 < 0)
                {

                    moveLength = roller.Dice1;
                }
                else
                {
                    moveLength = Math.Min(roller.Dice1, roller.Dice2);
                }

            }

            lengthOfCurrentTurn -= moveLength;
            roller.DiceUsed(lengthOfCurrentTurn);

            return true;

        }
        /****************************************/

        //check if move is valid
        public Move FindMove(Player player, int from, int to)
        {
            //get all the movements options of the current player
            //and check if his input was one of the options
            //return false if movement was not found.
            HashSet<Move> moves = GetPlayerOptions(player);
            foreach (Move move in moves)
            {
                if (to == move.To && from == move.From)
                {

                    return move;
                }
            }

            return null;
        }
        /****************************************/


        private HashSet<Move> GetValidMoves(Player player, int[] lengthes)
        {
            List<int> fromList;

            //the player can't play until he gets out of eaten zone
            if (gameBoard.EatenCheckers.Contains((CellContent)player.PlayerNumber))
            {
                if (player.PlayerNumber == 1)
                    fromList = new List<int> { 0 }; //start point of player 1
                else
                    fromList = new List<int> { 25 };//start point of player 2
            }
            else
            {
                //regular turn
                fromList = gameBoard.GetPlayerRowOccupation(player);
            }

            HashSet<Move> validMoves = new HashSet<Move>();
            Move move;
            foreach (var length in lengthes)
            {
                foreach (var from in fromList)
                {
                    //player 1 going up the board
                    if (player.PlayerNumber == 1)
                    {
                        if ((from + length) <= 24 && length > 0) //check if out of bounds
                        {
                            //check if destination is valid
                            if (fromList.Contains(from + length) || gameBoard.GetNumOfCheckersInLane(from + length) == 0)
                            {  //occupy by this player or none
                                move = new Move(from, from + length, Move.MoveType.Regular);
                                validMoves.Add(move);
                            }
                            else if (gameBoard.GetNumOfCheckersInLane(from + length) == 1)
                            {//other player has 1 stone to eat
                                move = new Move(from, from + length, Move.MoveType.Eat);
                                validMoves.Add(move);
                            }

                        }
                        else if (from + length > 24) //player 1 move out (from upper right board)
                        {
                            //check if all stones are at player's home (lanes 19-24)
                            if (fromList.Intersect(Enumerable.Range(1, 18)).Count() == 0)
                            {
                                bool isValid = true;
                                foreach (var validmove in validMoves)
                                {
                                    //check if there is a longer move
                                    //if exit, there is no move valid moves
                                    if (validmove.From < from && validmove.Type == Move.MoveType.Out)
                                    {
                                        isValid = false;
                                        break;
                                    }
                                }

                                if (isValid)
                                {
                                    move = new Move(from, 25, Move.MoveType.Out);
                                    validMoves.Add(move);
                                }

                            }
                        }
                    }

                    //player 2 move backwards
                    else
                    {
                        if ((from - length) > 0 && length > 0) //check if out of bounds
                        {
                            if (fromList.Contains(from - length) || //occupy by this player 
                                gameBoard.GetNumOfCheckersInLane(from - length) == 0) //none
                            {
                                move = new Move(from, from - length, Move.MoveType.Regular);
                                validMoves.Add(move);
                            }
                            else if (gameBoard.GetNumOfCheckersInLane(from - length) == 1)
                            {//other player has 1 stone to eat
                                move = new Move(from, from - length, Move.MoveType.Eat);
                                validMoves.Add(move);
                            }

                        }
                        else if (from - length < 1) //move out (from lower right board)
                        {
                            //check if all stones are at player's home (lanes 0-6)
                            if (fromList.Intersect(Enumerable.Range(7, 24)).Count() == 0)
                            {
                                bool isValid = true;
                                foreach (var validmove in validMoves)
                                {
                                    //check if there is a longer move
                                    //if exit, there is no move valid moves
                                    if (validmove.From > from && validmove.Type == Move.MoveType.Out)
                                    {
                                        isValid = false;
                                        break;
                                    }
                                }
                                if (isValid)
                                {
                                    move = new Move(from, 0, Move.MoveType.Out);
                                    validMoves.Add(move);
                                }

                            }
                        }
                    }
                }
            }
            return validMoves;
        }
        /****************************************/

        public int[] RollDice()
        {
            int[] rolled = roller.Roll();
            lengthOfCurrentTurn = rolled[0] + rolled[1];
            if (IsDouble())
            {
                lengthOfCurrentTurn *= 2;
            }
            return rolled;
        }
        /****************************************/

        public CellContent[,] GetGameBoard()
        {
            return gameBoard.BoardMatrix;
        }
        /****************************************/

        public CellContent[] GetEatenStones()
        {
            return gameBoard.EatenCheckers.ToArray();
        }
        /****************************************/

        public bool IsDouble()
        {
            return (roller.Dice1 == roller.Dice2);
        }
        /****************************************/

        public int GetLengthOfCurrentTurn()
        {
            return lengthOfCurrentTurn;
        }
        /****************************************/

        public int[] GetPlayerRowOccupation(Player player)
        {
            return gameBoard.GetPlayerRowOccupation(player).ToArray();
        }
        /****************************************/

        public int[] GetDiceResults()
        {
            return (new int[] { roller.Dice1, roller.Dice2 });
        }
        /****************************************/
    }
}
