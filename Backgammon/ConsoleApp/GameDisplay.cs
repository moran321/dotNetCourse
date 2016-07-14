using System;
using GameLib;
using System.Collections.Generic;


namespace ConsoleUI
{
    //this class get input from user through console
    //and show the game moves
    class GameDisplay
    {

        private string playerOneSign = "O", playerTwoSign = "@", none = " ";
        private string playerOneName, playerTwoName ;
        private ConsoleColor playerOneColor = ConsoleColor.Red, playerTwoColor = ConsoleColor.Cyan;

      //  Game game;
        CellContent[,] board;
        GameManager manager;
        /****************************************/

        //C'tor
        public GameDisplay()
        {
           
            //game = manager.BackgammonGameInstance;
        }  /****************************************/


        //this function called by 'Main', keep looping until game is over
        public void Start()
        {
            WelcomeDisplay();

            Player player=manager.GetCurrentPlayer(); ;
            DisplayBoard();
            while (!manager.IsGameOver())
            {
                InstructionsDisplay();
                Roll(); //roll dice
                player = manager.GetCurrentPlayer();
               
                while (manager.GetLengthOfCurrentTurn()>0) //the same turn
                {
                  
                    if (DisplayMovesOptions()) //if there is options to move
                        GetInputAndMove();  //get 2 numbers
                    else
                    {
                        manager.ChangeTurn(); //skip turn if there is no moves
                        break;
                    }
                }
                Console.WriteLine("\n====================== Change Turn ================================\n");
                DisplayBoard();
            }
            Console.WriteLine("---- GAME OVER ----");
            Console.WriteLine("Congratulaions! {0} you won!", player.Name);

        }  /****************************************/

        private void InstructionsDisplay()
        {
            Console.WriteLine("\n======================================================\n");
            if (manager.currentPlayer.PlayerNumber == 1)
            {
                Console.ForegroundColor = playerOneColor;
            }
            else
            {
                Console.ForegroundColor = playerTwoColor;
            }
            System.Console.WriteLine("player {0}: {1} is your turn!", manager.currentPlayer.PlayerNumber, manager.currentPlayer.Name);

            Console.ResetColor();


        }  /****************************************/

        private void WelcomeDisplay()
        {
            Console.WriteLine("======================================================");
            System.Console.WriteLine("\t\tWelcome To Backgammon Game!");
            Console.WriteLine("======================================================\n");

            GetGameType();

            Console.Write("Player 1 you start from  1 and your stone are: ");
            Console.ForegroundColor = playerOneColor;
            Console.WriteLine(playerOneSign);
            Console.ResetColor();
            Console.Write("Player 2 you start from 24 and your stone are: ");
            Console.ForegroundColor = playerTwoColor;
            Console.WriteLine(playerTwoSign);
            Console.ResetColor();

        }  /****************************************/


        private void GetGameType()
        {
            Console.WriteLine("Choose a Game: ");
            Console.WriteLine("1) 1 player.");
            Console.WriteLine("2) 2 players.");
            string type;
            int itype=0;
            bool isValid = false;
            while (!isValid)
            {
                isValid = true;
                Console.Write("Enter a Number of game:");
                type = Console.ReadLine();

               if ( !int.TryParse(type, out itype))
                {
                    Console.WriteLine("Input Invalid!");
                    isValid = false;
                }else if( itype!=2 && itype != 1)
                {
                    Console.WriteLine("Invalid Option!");
                    isValid = false;
                }
            }

            if (itype==2 )
            {
                Console.Write("Player 1 enter your name: ");
                string name = Console.ReadLine();
                playerOneName = name;
                Console.Write("Player 2 enter your name: ");
                name = Console.ReadLine();
                playerTwoName = name;

                manager = new GameManager(GameManager.GameType.TwoPlayers, playerOneName, playerTwoName);
                return;
            }else  if (itype == 1)
            {
                Console.Write("Player 1 enter your name: ");
                string name = Console.ReadLine();
                playerOneName = name;
                manager = new GameManager(GameManager.GameType.TwoPlayers, playerOneName, "Computer");
                return;
            }
        }
        /****************************************/

        private void GetInputAndMove()
        {
            //first movement
            bool ismoved = false;
            while (!ismoved)
            {
                int[] input = Get2Inputs();
                ismoved = manager.Play(input[0], input[1]);
                if (!ismoved)
                {
                    Console.WriteLine("ERROR! try again.");
                }
            }

        }  
        /****************************************/


        private int[] Get2Inputs()
        {
            string from, to;
            int ifrom = 0, ito = 0;
            bool isValid = false;
            while (!isValid)
            {
                isValid = true;
                System.Console.Write("Enter a lane to take stone from:");
                from = Console.ReadLine();
                System.Console.Write("Enter a lane to pute stone on:");
                to = Console.ReadLine();
                if ( !int.TryParse(from, out ifrom))
                {
                    if (string.Equals(from, "out", StringComparison.OrdinalIgnoreCase))
                    {
                        if (manager.currentPlayer.PlayerNumber == 1)
                            ifrom = 0;
                        else
                            ifrom = 25;
                    }
                    else
                    {
                        Console.WriteLine("Input Error!");
                        isValid = false;
                    }

                }
                if ( !int.TryParse(to, out ito))
                { 
                     if (string.Equals(to, "out", StringComparison.OrdinalIgnoreCase))
                    {
                        if (manager.currentPlayer.PlayerNumber == 1)
                            ito = 25;
                        else
                            ito = 0;
                    }
                    else
                    {
                        Console.WriteLine("Input Error!");
                        isValid = false;
                    }

                }
                if (!manager.CheckInput(ifrom, ito))
                {
                    Console.WriteLine("Invalid move! pick from the list above.");
                    isValid = false;
                }
            }
            return (new int[] { ifrom, ito });
        }
        /****************************************/


        private void Roll()
        {
            int[] dice = manager.RollDice(); //roll dice
            System.Console.Write("your roll result:");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" {0} ",dice[0]);
            Console.ResetColor();
            Console.Write(", ");
            Console.BackgroundColor = ConsoleColor.White;
            Console.ForegroundColor = ConsoleColor.Black;
            Console.Write(" {0} ", dice[1]);
            Console.ResetColor();
            if (dice[0] == dice[1])
            {
                Console.WriteLine("\nCongratulations! You got double, play twice!");
            }
        } 
        /****************************************/

        private bool DisplayMovesOptions()
        {
            Move[] moves = manager.GetPlayerMoves(); //get valid moves
            if (moves == null || moves.Length == 0)
            {
                System.Console.WriteLine("You don't have any moves!");
                return false;
            }
            System.Console.WriteLine("your options:");
            foreach (Move move in moves) //show player moves options
            {
                Console.WriteLine(move.ToString());
            }
            return true;
        }  
        /****************************************/

        private void DisplayBoard()
        {
            board = manager.GetBoard();

            Console.WriteLine("\n--13--14--15--16--17--18---Bl--19--20--21--22--23--24--");

            //upper side
            for (int i = 0; i < 6; i++)
            {
               
                Console.Write("|");
                for (int j = 0; j < 6; j++)
                {
                    if (board[i, j] == CellContent.PlayerTwo)
                    {
                      
                        Console.ForegroundColor = playerTwoColor;
                        Console.Write(" {0} ", playerTwoSign);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|", playerTwoSign);
                    }
                    else if (board[i, j] == CellContent.PlayerOne)
                    {
                  
                        Console.ForegroundColor = playerOneColor;
                        Console.Write(" {0} ", playerOneSign);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|", playerOneSign);

                    }
                    else
                    {
                        Console.Write(" {0} |", none);
                    }
                }
                Console.ResetColor();
                //eaten stones zone
                
                if (manager.GetEatenStones()?.Length > 0 && manager.GetEatenStones()?.Length > i)
                {
                    if ((int)manager.GetEatenStones()[i] == 1)
                    {
                        Console.Write("|/");
                        Console.ForegroundColor = playerOneColor;
                        Console.Write("{0}", playerOneSign);
                        Console.ForegroundColor = ConsoleColor.White; ;
                        Console.Write("/||");
                    }
                    else
                    {
                        Console.Write("|/");
                        Console.ForegroundColor = playerTwoColor;
                        Console.Write("{0}", playerTwoSign);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("/||");
                    }
                }
                else
                {
                    Console.Write("|///||"); //middle
                }

                for (int j = 6; j < 12; j++)
                {
                  
                    if (board[i, j] == CellContent.PlayerTwo)
                    {
                        Console.ForegroundColor = playerTwoColor;
                        Console.Write(" {0} ", playerTwoSign);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|", playerTwoSign);
                    }
                    else if (board[i, j] == CellContent.PlayerOne)
                    {
                        Console.ForegroundColor = playerOneColor;
                        Console.Write(" {0} ", playerOneSign);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|", playerOneSign);

                    }
                    else
                    {

                        Console.Write(" {0} |", none);
                    }
                }
                Console.ResetColor();
                Console.WriteLine();

            }
        
            Console.WriteLine("=======================================================");
          
            //down side
            for (int i = 6; i < 12; i++)
            {
                Console.ForegroundColor = ConsoleColor.White;
           
                Console.Write("|");
                for (int j = 0; j < 6; j++)
                {
                    if (board[i, j] == CellContent.PlayerTwo)
                    {
                        Console.ForegroundColor = playerTwoColor;
                        Console.Write(" {0} ", playerTwoSign);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|", playerTwoSign);
                    }
                    else if (board[i, j] == CellContent.PlayerOne)
                    {
                        Console.ForegroundColor = playerOneColor;
                        Console.Write(" {0} ", playerOneSign);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|", playerOneSign);

                    }
                    else
                    {
                        Console.Write(" {0} |", none);
                    }
                }
             
                //eaten stones zone
                if (manager.GetEatenStones()?.Length > 0 && manager.GetEatenStones()?.Length > i)
                {
                    if ((int)manager.GetEatenStones()[i] == 1)
                    {
                        Console.Write("|/");
                        Console.ForegroundColor = playerOneColor;
                        Console.Write("{0}", playerOneSign);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("/||");
                    }
                    else
                    {
                        Console.Write("|/");
                        Console.ForegroundColor = playerTwoColor;
                        Console.Write("{0}", playerTwoSign);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("/||");
                    }
                }
                else
                {
                    Console.Write("|///||"); //middle
                }

                for (int j = 6; j < 12; j++)
                {
                    if (board[i, j] == CellContent.PlayerTwo)
                    {
                        Console.ForegroundColor = playerTwoColor;
                        Console.Write(" {0} ", playerTwoSign);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|", playerTwoSign);
                    }
                    else if (board[i, j] == CellContent.PlayerOne)
                    {
                        Console.ForegroundColor = playerOneColor;
                        Console.Write(" {0} ", playerOneSign);
                        Console.ForegroundColor = ConsoleColor.White;
                        Console.Write("|", playerOneSign);

                    }
                    else
                    {
                        Console.Write(" {0} |", none);
                    }
                }
                Console.ResetColor();
                Console.WriteLine();

            }
         
            Console.WriteLine("--12--11--10--9---8---7----Wi---6---5---4---3---2---1--\n");

            Console.ResetColor();
        }  
        /****************************************/



    }
}
