using System;
using GameLib;
using System.Collections.Generic;


namespace ConsoleUI
{
    //this class gets input from user through console and shows the game moves
    class GameDisplay
    {
        private string playerOneSign = "O", playerTwoSign = "O", none = " ";
        private string playerOneName, playerTwoName;
        private ConsoleColor playerOneColor = ConsoleColor.Red, playerTwoColor = ConsoleColor.Cyan;
        private GameManager _manager;
        /****************************************/


        public void Start()
        {
            //create the instance of game and its players
            WelcomeDisplay();

            //subscribe to events of the game manager
            _manager.StartTurnEvent += DisplayBoardHandler;
            _manager.StartTurnEvent += InstructionsDisplayHandler;
        
            _manager.ChangedMovesEvent += DisplayMovesOptionsHandler;
            _manager.PlayerMovedEvent += PlayerMovedHandler;
            _manager.RollDiceEvent += RollDiceHandler;
            _manager.GameOverEvent += GameOverHandler;

            //start game 
            _manager.StartPlaying(Get2Inputs);
        }
        /****************************************/


        private void GameOverHandler(Object sender, GameOverEventArgs e)
        {
            Console.WriteLine("########## GAME OVER ##########");
            if (e.VictoryType == GameManager.Victory.Regular)
            {
                Console.WriteLine($"{e.Winner.Name} is the winner!");
            }
            else if (e.VictoryType== GameManager.Victory.Mars)
            {
                Console.WriteLine("{0} did a Mars!", e.Winner.Name);
            }
            else if (e.VictoryType == GameManager.Victory.TurkishMars)
            {
                Console.WriteLine("{0} did a Turkish Mars!", e.Winner.Name);
            }
        }
        /****************************************/

        private void PlayerMovedHandler(Object sender, PlayerMovedEventArgs e)
        {
            Console.WriteLine( "\n {0} moved from {1} to {2}", e.CurrentPlayer.Name, e.Moved.From, e.Moved.To);
        }
        /****************************************/

        private void InstructionsDisplayHandler(Object sender, TurnChangedEventArgs e)
        {

            Console.WriteLine("\n=================== New turn =======================\n");
            if (_manager._currentPlayer.PlayerNumber == 1)
            {
                Console.ForegroundColor = playerOneColor;
            }
            else
            {
                Console.ForegroundColor = playerTwoColor;
            }
            System.Console.WriteLine("player {0}: {1} is your turn!", _manager._currentPlayer.PlayerNumber, _manager._currentPlayer.Name);

            Console.ResetColor();

        } 
        /****************************************/

        private void WelcomeDisplay()
        {
            Console.WriteLine("======================================================");
            System.Console.WriteLine("\t\tWelcome To Backgammon Game!");
            Console.WriteLine("======================================================\n");

            GetGameType();
            Console.WriteLine("======================================================\n");
            Console.Write("Player 1 you start from  1 and your stone sign is: ");
            Console.ForegroundColor = playerOneColor;
            Console.WriteLine(playerOneSign);
            Console.ResetColor();
            Console.Write("Player 2 you start from 24 and your stone sign is: ");
            Console.ForegroundColor = playerTwoColor;
            Console.WriteLine(playerTwoSign);
            Console.ResetColor();

        }  
        /****************************************/

        private void GetGameType()
        {
            Console.WriteLine("Choose a Game: ");
            Console.WriteLine("1) 1 player");
            Console.WriteLine("2) 2 players");
            string type;
            int itype = 0;
            bool isValid = false;
            while (!isValid)
            {
                isValid = true;
                Console.Write("Enter a Number of game:");
                type = Console.ReadLine();

                if (!int.TryParse(type, out itype))
                {
                    Console.WriteLine("Invalid input!");
                    isValid = false;
                }
                else if (itype != 2 && itype != 1)
                {
                    Console.WriteLine("Invalid Option!");
                    isValid = false;
                }
            }

            if (itype == 2)
            {
                Console.Write("Player 1 enter your name: ");
                string name = Console.ReadLine();
                playerOneName = name;
                Console.Write("Player 2 enter your name: ");
                name = Console.ReadLine();
                playerTwoName = name;

                _manager = new GameManager(GameManager.GameType.TwoPlayers, playerOneName, playerTwoName);
                return;
            }
            else if (itype == 1)
            {
                Console.Write("Player 1 enter your name: ");
                string name = Console.ReadLine();
                playerOneName = name;
                _manager = new GameManager(GameManager.GameType.TwoPlayers, playerOneName, "Computer");
                return;
            }
        }
        /****************************************/
     
        public TryMove Get2Inputs()
        {
           
            Console.WriteLine("Please pick from the list above");
            string[] input = new string[2];
            System.Console.Write("Enter a lane to take stone from:");
            input[0] = Console.ReadLine();
            System.Console.Write("Enter a lane to pute stone on:");
            input[1] = Console.ReadLine();

            return (new TryMove(input[0], input[1]));
        }
        /****************************************/


        private void RollDiceHandler(Object sender, RollDiceEventArgs e)
        {
            System.Console.Write("Press enter to roll dice:");
            Console.ReadLine();
            _manager.RollDice();
            if (_manager._game.IsDouble()) {
                Console.WriteLine("\nCongratulations! You've got a double, play 4 moves!");
            }
          //  _manager.isRolled = true;

        }
        /****************************************/

        private void RollResults(int[] dice)
        {
            System.Console.Write($"\n{ _manager.GetCurrentPlayer().Name}, your dice are:\n");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" {0} ", dice[0]);
            Console.ResetColor();
            Console.Write(", ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" {0} ", dice[1]);
            Console.ResetColor();
            Console.WriteLine();
            
        }
        /****************************************/

        private void DisplayMovesOptionsHandler(Object sender, TurnEventArgs e)
        {
            RollResults(_manager._game.GetDiceResults());
            if (e.Moves?.Length == 0)
            {
                Console.WriteLine("You don't have any moves!");
                return;
            }
            Console.WriteLine("your options:");
            foreach (Move move in e.Moves) //show player moves options
            {
                Console.WriteLine(move.ToString());
            }
        }
        /****************************************/

        private void PrintBoardCell(CellContent cell)
        {
            if (cell == CellContent.PlayerTwo)
            {

                Console.ForegroundColor = playerTwoColor;
                Console.Write(" {0} ", playerTwoSign);
                Console.ResetColor();
                Console.Write("|", playerTwoSign);
            }
            else if (cell == CellContent.PlayerOne)
            {

                Console.ForegroundColor = playerOneColor;
                Console.Write(" {0} ", playerOneSign);
                Console.ResetColor();
                Console.Write("|", playerOneSign);

            }
            else
            {
                Console.Write(" {0} |", none);
            }
        }
        /****************************************/

        private void PrintEatenzone(CellContent eatenChecker)
        {
            //eaten stones zone

            if ((int)eatenChecker == 1)
            {
                Console.Write("|/");
                Console.ForegroundColor = playerOneColor;
                Console.Write("{0}", playerOneSign);
                Console.ResetColor();
                Console.Write("/||");
            }
            else
            {
                Console.Write("|/");
                Console.ForegroundColor = playerTwoColor;
                Console.Write("{0}", playerTwoSign);
                Console.ResetColor();
                Console.Write("/||");
            }
        }
        /****************************************/

        private void DisplayBoardHandler(Object sender, TurnChangedEventArgs e)
        {
           
            CellContent[,] board = _manager.GetBoard();
            Console.WriteLine("\n                                   - {0}'s Home - ",playerOneColor);
            Console.WriteLine("--13--14--15--16--17--18--***--19--20--21--22--23--24--");

            //upper side
            for (int i = 0; i < 6; i++)
            {
                Console.Write("|");
                for (int j = 0; j < 6; j++)
                {
                    PrintBoardCell(board[i, j]);
                }
                Console.ResetColor();

                //eaten stones zone
                if (_manager.GetEatenCheckers()?.Length > 0 && _manager.GetEatenCheckers()?.Length > i)
                {
                    PrintEatenzone(_manager.GetEatenCheckers()[i]);
                }
                else
                {
                    Console.Write("|///||"); //middle
                }


                for (int j = 6; j < 12; j++)
                {

                    PrintBoardCell(board[i, j]);
                }
                Console.ResetColor();
                Console.WriteLine();

            }

            Console.WriteLine("=======================================================");

            //down side
            for (int i = 6; i < 12; i++)
            {
               
                Console.Write("|");
                for (int j = 0; j < 6; j++)
                {
                    PrintBoardCell(board[i, j]);
                }

                //eaten stones zone
                if (_manager.GetEatenCheckers()?.Length > 0 && _manager.GetEatenCheckers()?.Length > i)
                {
                    PrintEatenzone(_manager.GetEatenCheckers()[i]);
                }
                else
                {
                    Console.Write("|///||"); //middle
                }

                for (int j = 6; j < 12; j++)
                {
                    PrintBoardCell(board[i, j]);
                }
                Console.ResetColor();
                Console.WriteLine();

            }

            Console.WriteLine("--12--11--10--9---8---7---***---6---5---4---3---2---1--");
            Console.WriteLine("                                   - {0}'s Home - \n", playerTwoColor);
            Console.ResetColor();
        }
        /****************************************/



    }
}
