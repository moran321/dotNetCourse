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
        public event EventHandler<TurnChangedEventArgs> StartTurnEvent;
        public event EventHandler<TurnEventArgs> ChangedMovesEvent;
        public event EventHandler<PlayerMovedEventArgs> PlayerMovedEvent;
        public event EventHandler<RollDiceEventArgs> RollDiceEvent;
        public event EventHandler<GameOverEventArgs> GameOverEvent;

        int[] dice;
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

        public Move Play(int fromLine, int toLine)
        {
            //take a move
            return _game.MakeMove(_currentPlayer, fromLine, toLine);
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

        public void StartPlaying(Func<TryMove> getMoveInput)
        {
            Player prevPlayer = null;
            
            while (!IsGameOver())
            {
                StartTurnEvent(this, new TurnChangedEventArgs());

                if (GetCurrentPlayer().Playertype == Player.PlayerType.Human)
                {
                    //wait for the user to roll
                    // RollDiceEvent(this, new RollDiceEventArgs());
                    dice = _game.GetDiceResults();

                    while (dice[0] == 0 || dice[1] == 0)
                    {
                        RollDiceEvent(this, new RollDiceEventArgs());
                        dice = _game.GetDiceResults();
                    }
                }
                else //computer
                {
                    dice = RollDice();
                }

                //save the player before changing, to remember who played last
                prevPlayer = _currentPlayer;

                PlayTurn(getMoveInput);

                ChangeTurn();
            }
            //----game over----

            GetWinner(prevPlayer);
        }
        /****************************************/


        //keep looping while the user didn't axploit all his turn moves
        private void PlayTurn(Func<TryMove> getMoveInput)
        {

            
            while (GetLengthOfCurrentTurn() > 0)
            {
                
                Move[] moves = GetPlayerMoves();

               
                ChangedMovesEvent(this, new TurnEventArgs(moves, dice));
                
             
                if (moves == null || moves.Length == 0)
                {
                    break; 
                }
               
                if (GetCurrentPlayer().Playertype == Player.PlayerType.Human)
                {
                    int[] input = ManageInput(getMoveInput);
                    Move moved = Play(input[0], input[1]);
                    PlayerMovedEvent(this, new PlayerMovedEventArgs(_currentPlayer, (new Move(input[0], input[1], Move.MoveType.Regular))));

                }
                else //computer
                {
                    Random rnd = new Random();
                    int r = rnd.Next(moves.Length);
                    if (moves[r] != null)
                    {
                        Move moved = Play(moves[r].From, moves[r].To);
                        PlayerMovedEvent(this, new PlayerMovedEventArgs(_currentPlayer, (new Move(moves[r].From, moves[r].To, Move.MoveType.Regular))));
                    }
                }
            }
        }
        /****************************************/

        //check validation
        private int[] ManageInput(Func<TryMove> getMoveInput)
        {
            int ifrom = 0, ito = 0;
            bool isValid = false;

            while (!isValid)
            {
                isValid = true;

                //get the current move from user (from UI\GUI...)
                TryMove input = getMoveInput();

                //check if player try to get back from eaten
                if (!int.TryParse(input.From, out ifrom))
                {
                    if (string.Equals(input.From, "out", StringComparison.OrdinalIgnoreCase))
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
                if (!int.TryParse(input.To, out ito))
                {
                    if (string.Equals(input.To, "out", StringComparison.OrdinalIgnoreCase))
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

        private void GetWinner(Player player)
        {

            //winnner
            if (_currentPlayer.NumberOfCheckersOut == 0)
            {

                GameOverEvent(this, new GameOverEventArgs(Victory.Mars, player));
            }
            else
            {
                GameOverEvent(this, new GameOverEventArgs(Victory.Regular, player));
            }
        }
        /****************************************/
    }
}
