using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;



namespace GameLib
{
    //this class manage the players turns and the whole progress of the game
    public class GameManager
    {
        public enum GameType { TwoPlayers, OnePlayer, ComputerOnly };
        public enum Victory { Regular, Mars, TurkishMars }
        public Game _game;
        public bool isRolled;
        public Player _currentPlayer { get; private set; }
        public event EventHandler<DisplayInstuctionsEventArgs> DisplayEvent;
        public event EventHandler<TurnEventArgs> ChangedMovesEvent;
        public event EventHandler<DisplayMessageEventArgs> MessageEvent;
        public event EventHandler<RollDiceEventArgs> RollDiceEvent;
        public event EventHandler<GameOverEventArgs> GameOverEvent;

        /****************************************/

        //C'tor
        public GameManager(GameType gametype, string player1name, string player2name)
        {
            _game = new Game(player1name, player2name);
            _currentPlayer = _game.GetPlayerOne();
            isRolled = false;
        }
        /****************************************/

        private void ChangeTurn()
        {
            if (_currentPlayer == _game.GetPlayerOne())
            {
                _currentPlayer = _game.GetPlayerTwo();
            }
            else
            {
                _currentPlayer = _game.GetPlayerOne();
            }
        }
        /****************************************/

        public Player GetCurrentPlayer()
        {
            return _currentPlayer;
        }
        /****************************************/

        public bool IsGameOver()
        {
            return _game.IsGameOver();
        }
        /****************************************/

        public bool Play(int fromLine, int toLine)
        {
            //check input
            bool isValid = CheckIfMoveExist(_currentPlayer, fromLine, toLine);
            if (!isValid)
            {
                return false;
            }
            //take a move
            _game.MakeMove(_currentPlayer, fromLine, toLine);
            return true;
        }
        /****************************************/

        private int[] GetPlayerLanes(Player player)
        {
            return _game.GetPlayerRowOccupation(player);
        }
        /****************************************/

        private int[] GetPlayerLanes(int player)
        {
            if (player == 1)
            {
                return GetPlayerLanes(_game.GetPlayerOne());
            }
            else
            {
                return GetPlayerLanes(_game.GetPlayerTwo());
            }
        }
        /****************************************/

        private bool CheckInput(int from, int to)
        {
            return CheckIfMoveExist(_currentPlayer, from, to);
        }
        /****************************************/

        private bool CheckIfMoveExist(Player player, int from, int to)
        {
            Move move = _game.FindMove(player, from, to);
            if (move != null)
            {
                return true;
            }
            return false;
        }
        /****************************************/

        public int[] RollDice()
        {
            return _game.RollDice();
        }
        /****************************************/

        public Move[] GetPlayerMoves()
        {
            return _game.GetPlayerOptions(_currentPlayer);
        }
        /****************************************/

        public CellContent[,] GetBoard()
        {
            return _game.GetGameBoard();
        }
        /****************************************/

        public CellContent[] GetEatenCheckers()
        {
            return _game.GetEatenCheckers();
        }
        /****************************************/

        private int GetLengthOfCurrentTurn()
        {
            return _game.GetLengthOfCurrentTurn();
        }
        /****************************************/

            

        public void StartPlaying(Func<string[]> get2Inputs)
        {
            Player prevPlayer = null;
            int[] dice;
            while (!IsGameOver())
            {
                DisplayEvent(this, new DisplayInstuctionsEventArgs());
                isRolled = false;
                if (GetCurrentPlayer().Playertype == Player.PlayerType.Human)
                {
                    while (isRolled == false)
                    {
                        RollDiceEvent(this, new RollDiceEventArgs());
                    }
                    dice = _game.GetDiceResults();  //##
                }
                else
                {
                    dice = RollDice();
                }

               
                //save the player before changing to know who played last
                prevPlayer = _currentPlayer;

                while (GetLengthOfCurrentTurn() > 0) //the same turn
                {

                    Move[] moves = GetPlayerMoves(); //get valid moves

                    //  Display Moves Options
                    ChangedMovesEvent(this, new TurnEventArgs(moves, dice));
                    if (moves == null || moves.Length == 0)
                    {
                        break;
                    }
                    //if human playing get input from UI
                    if (GetCurrentPlayer().Playertype == Player.PlayerType.Human)
                    {
                        int[] input = ManageInput(get2Inputs);
                        if (PlayTurn(input[0], input[1]))
                        {
                            MessageEvent(this, new DisplayMessageEventArgs(
                                string.Format("{0} moved from {1} to {2}", _currentPlayer.Name, input[0], input[1])));
                        }
                    }

                    else //generate random move
                    {
                        Random rnd = new Random();
                        int r = rnd.Next(moves.Length);
                        if (moves[r] != null)
                        {
                            if (PlayTurn(moves[r].From, moves[r].To))
                            {
                                MessageEvent(this, new DisplayMessageEventArgs(
                                    string.Format("{0} moved from {1} to {2}", _currentPlayer.Name, moves[r].From, moves[r].To)));
                            }
                        }

                    }

                }
                ChangeTurn();
            }
            //winnner


            if (_currentPlayer.NumberOfCheckersOut == 0)
            {
                GameOverEvent(this, new GameOverEventArgs(Victory.Mars, prevPlayer));
            }
            else
            {
                GameOverEvent(this, new GameOverEventArgs(Victory.Regular, prevPlayer));
            }


        }
        /****************************************/


        public int[] ManageInput(Func<string[]> get2Inputs)
        {
            int ifrom = 0, ito = 0;
            bool isValid = false;

            while (!isValid)
            {
                isValid = true;

                string[] input = get2Inputs(); //call the UI method


                //check if player try to get back from eaten
                if (!int.TryParse(input[0], out ifrom))
                {
                    if (string.Equals(input[0], "out", StringComparison.OrdinalIgnoreCase))
                    {
                        if (_currentPlayer.PlayerNumber == 1)
                            ifrom = 0;
                        else
                            ifrom = 25;
                    }
                    else
                    {
                        // Console.WriteLine("Input Error!");
                        isValid = false;
                    }

                }
                //check if player try move stone out 
                if (!int.TryParse(input[1], out ito))
                {
                    if (string.Equals(input[1], "out", StringComparison.OrdinalIgnoreCase))
                    {
                        if (_currentPlayer.PlayerNumber == 1)
                            ito = 25;
                        else
                            ito = 0;
                    }
                    else
                    {
                        //  Input Error
                        isValid = false;
                    }

                }
                if (!CheckInput(ifrom, ito))
                {
                    //  Invalid move
                    isValid = false;
                }
            }

            return (new int[] { ifrom, ito });
        }
        /****************************************/

        private bool PlayTurn(int from, int to)
        {
            Move[] moves = GetPlayerMoves();
            if (moves != null)
            {
                Play(from, to);
                return true;
            }
            return false;
        }
        /****************************************/


    }
}
