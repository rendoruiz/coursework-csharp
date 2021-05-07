using System;

namespace TicTacToeGame
{
    class Program
    {
        static void Main(string[] args)
        {
            char[,] boardTokens;
            int currentTurn = 0;
            char currentPlayer = ' ';

            char input = '\0';
            string gameLoopInput = "";
            
            while (gameLoopInput != "n")
            {
                currentTurn = 0;
                gameLoopInput = "";
                Console.WriteLine("********************");
                Console.WriteLine("* Tic-Tac-Toe Game *");
                Console.WriteLine("********************");
                Console.WriteLine("The cell numbers for the game is shown below.");
                boardTokens = new char[,] { { '7', '8', '9' }, { '4', '5', '6' }, { '1', '2', '3' } };
                ConstructBoard(boardTokens);
                currentTurn++;
                boardTokens = new char[3, 3];

                while (currentTurn < 10 && !IsGameOver(boardTokens, currentPlayer))
                {
                    currentPlayer = (currentTurn % 2 == 0) ? 'O' : 'X';

                    Console.Write($"Turn {currentTurn}) Enter cell number (1-9) for player ");
                    ColorizeToken(currentPlayer);
                    Console.Write(": ");
                    input = Console.ReadKey().KeyChar;
                    Console.WriteLine();

                    try
                    {
                        int inputToInteger = int.Parse(input.ToString());
                        if (inputToInteger >= 1 && inputToInteger <= 9)
                        {
                            InsertToken(boardTokens, currentPlayer, inputToInteger, ref currentTurn);
                        }
                        else
                            Console.WriteLine("\tInvalid input. Enter a number from 1 to 9.");
                    }
                    catch
                    {
                        Console.WriteLine("\tInvalid input. Enter a number from 1 to 9.");
                    }

                    ConstructBoard(boardTokens);
                    if (currentTurn > 9 && !IsGameOver(boardTokens, currentPlayer))
                    {
                        Console.WriteLine("Game over. It's a draw!");
                    }
                    else if (IsGameOver(boardTokens, currentPlayer))
                    {
                        Console.Write("Game over. Player ");
                        ColorizeToken(currentPlayer);
                        Console.Write(" wins!\n");
                    }
                }

                while (gameLoopInput != "y" && gameLoopInput != "n")
                {
                    Console.Write("\nWould you like to play again? (y/n): ");
                    gameLoopInput = Console.ReadLine().ToLower();
                    if (gameLoopInput == "n")
                        Console.WriteLine("Goodbye and thanks for playing!");
                    else if (gameLoopInput != "y")
                        Console.WriteLine("Invalid input. Try again.");
                    else
                        Console.Clear();
                }
            }

        }

        static void ConstructBoard(char[,] boardTokens)
        {
            char token;
            
            Console.WriteLine("-------------");
            for (int x = 0; x < 3; x++)
            {
                Console.Write("|");
                for (int y = 0; y < 3; y++)
                {
                    token = (boardTokens[x, y] == '\0') ? ' ' : boardTokens[x, y];
                    Console.Write(" ");
                    ColorizeToken(token);
                    Console.Write(" |");
                }
                Console.WriteLine("\n-------------");
            }
        }
        
        static void InsertToken(char[,] boardTokens, char currentPlayer, int inputCellPosition, ref int currentGameTurn)
        {
            int cellX = 0, cellY = 0;
            switch (inputCellPosition)
            {
                case 1:
                    cellX = 2; cellY = 0;
                    break;
                case 2:
                    cellX = 2; cellY = 1;
                    break;
                case 3:
                    cellX = 2; cellY = 2;
                    break;
                case 4:
                    cellX = 1; cellY = 0;
                    break;
                case 5:
                    cellX = 1; cellY = 1;
                    break;
                case 6:
                    cellX = 1; cellY = 2;
                    break;
                case 7:
                    cellX = 0; cellY = 0;
                    break;
                case 8:
                    cellX = 0; cellY = 1;
                    break;
                case 9:
                    cellX = 0; cellY = 2;
                    break;
            }
            if (boardTokens[cellX, cellY] == '\0')
            {
                boardTokens[cellX, cellY] = currentPlayer;
                currentGameTurn++;
            }
            else
            {
                Console.WriteLine("\tInvalid input. Cell is already occupied.");
            }
        }
        
        static bool IsGameOver(char[,] boardTokens, char currentPlayer)
        {
            int consecutiveCells = 0;
            // horizontal check
            for (int x = 0; x < 3; x++)
            {
                for (int y = 0; y < 3; y++)
                {
                    if (boardTokens[x, y] == currentPlayer)
                        consecutiveCells++;
                }
                if (consecutiveCells == 3)
                    return true;
                else
                    consecutiveCells = 0;
            }

            // vertical check
            for (int y = 0; y < 3; y++)
            {
                for (int x = 0; x < 3; x++)
                {
                    if (boardTokens[x, y] == currentPlayer)
                        consecutiveCells++;
                }
                if (consecutiveCells == 3)
                    return true;
                else
                    consecutiveCells = 0;
            }

            // diagonal check
            if (boardTokens[1, 1] == currentPlayer)
            {
                if (boardTokens[0, 0] == currentPlayer && boardTokens[2, 2] == currentPlayer)
                    return true;
                else if (boardTokens[0, 2] == currentPlayer && boardTokens[2, 0] == currentPlayer)
                    return true;
            }

            return false;
        }

        static void ColorizeToken(char token)
        {
            if (token == 'O')
                Console.ForegroundColor = ConsoleColor.Red;
            else if (token == 'X')
                Console.ForegroundColor = ConsoleColor.Cyan;
            Console.Write(token);
            Console.ResetColor();
        }
    }
}
