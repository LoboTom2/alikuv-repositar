using System;

class Battleships
{
    static void Main()
    {
        char[,] playerBoard = new char[10, 10];
        char[,] enemyBoard = new char[10, 10];
        char[,] playerAttackBoard = new char[10, 10];

        InitializeBoard(playerBoard);
        InitializeBoard(enemyBoard);
        InitializeBoard(playerAttackBoard);

        Console.WriteLine("Nepřátelské území:");
        PrintBoard(enemyBoard, hideShips: true);

        Console.WriteLine("\nTvoje území:");
        PrintBoard(playerBoard, hideShips: false);

        PlaceShipsManually(playerBoard);
        PlaceShipsRandomly(enemyBoard);
        
        bool gameActive = true;
        Random rand = new Random();
        while (gameActive)
        {
            Console.Clear();
            Console.WriteLine("Nepřátelské území:");
            PrintBoard(enemyBoard, hideShips: true);

            Console.WriteLine("\nTvoje území:");
            PrintBoard(playerBoard, hideShips: false);

            // Hraje uživatel
            Console.Write("\nZadej souřadnice útoku (x y): ");
            string input = Console.ReadLine();
            if (ParseCoordinates(input, out int playerRow, out int playerCol))
            {
                if (enemyBoard[playerRow, playerCol] == 'S')
                {
                    Console.WriteLine("Trefa!");
                    enemyBoard[playerRow, playerCol] = 'X';
                }
                else if (enemyBoard[playerRow, playerCol] == '-')
                {
                    Console.WriteLine("Vedle!");
                    enemyBoard[playerRow, playerCol] = 'O';
                }
                else
                {
                    Console.WriteLine("Na tyhle souřadnice už si střílel");
                }
            }
            else
            {
                Console.WriteLine("Neplatné souřadnice, zkus to znovu");
            }

            if (!CheckIfShipsRemain(enemyBoard))
            {
                Console.WriteLine("Gratuluji, zničil si všechny nepřítelské lodě!");
                break;
            }

            // Hraje počítač. Stejný princip jako u hráče ale náhodně.
            Console.WriteLine("Hraje nepřítel...");
            int compRow, compCol;
            do
            {
                compRow = rand.Next(0, 10);
                compCol = rand.Next(0, 10);
            } while (playerBoard[compRow, compCol] == 'X' || playerBoard[compRow, compCol] == 'O');

            if (playerBoard[compRow, compCol] == 'S')
            {
                Console.WriteLine($"Nepřítel trefil tvou loď ({compRow}, {compCol})!");
                playerBoard[compRow, compCol] = 'X';
            }
            else
            {
                Console.WriteLine($"Nepřítel minul ({compRow}, {compCol}).");
                playerBoard[compRow, compCol] = 'O';
            }

            if (!CheckIfShipsRemain(playerBoard))
            {
                Console.WriteLine("Nepřítel zničil všechny tvé lodě, prohrál jsi!");
                break;
            }

            Console.WriteLine("Stiskni libovolnou klávesu...");
            Console.ReadKey();
        }
    }

    static void InitializeBoard(char[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                board[i, j] = '-';
            }
        }
    }

    static void PrintBoard(char[,] board, bool hideShips)
    {
        Console.Write("   ");
        for (int col = 0; col < board.GetLength(1); col++)
        {
            Console.Write(col + " ");
        }
        Console.WriteLine();

        for (int row = 0; row < board.GetLength(0); row++)
        {
            Console.Write(row + " ");
            if (row < 10) Console.Write(" ");

            for (int col = 0; col < board.GetLength(1); col++)
            {
                if (hideShips && board[row, col] == 'S')
                {
                    Console.Write("- ");
                }
                else
                {
                    Console.Write(board[row, col] + " ");
                }
            }
            Console.WriteLine();
        }
    }

    static void PlaceShipsManually(char[,] board)
    {
        int[] shipSizes = { 5, 4, 3, 3, 2 };
        for (int i = 0; i < shipSizes.Length; i++)
        {
            bool placed = false;
            while (!placed)
            {
                Console.WriteLine($"Polož loď o velikosti {shipSizes[i]} (vertikálně směrem dolů).");
                Console.Write("Zadej počáteční souřadnice (x y): ");
                string startInput = Console.ReadLine();

                if (ParseCoordinates(startInput, out int startRow, out int startCol) &&
                    CanPlaceShip(board, startRow, startCol, 'V', shipSizes[i]))
                {
                    PlaceShip(board, startRow, startCol, 'V', shipSizes[i]);
                    placed = true;
                }
                else
                {
                    Console.WriteLine("Neplatné souřadnice, zkus to znovu");
                }
            }
        }
    }

    static bool CanPlaceShip(char[,] board, int startRow, int startCol, char direction, int size)
    {
        if (direction == 'V')
        {
            if (startRow + size > board.GetLength(0)) return false;
            for (int i = 0; i < size; i++)
            {
                if (board[startRow + i, startCol] != '-') return false;
            }
        }
        else
        {
            return false;
        }

        return true;
    }

    static void PlaceShip(char[,] board, int startRow, int startCol, char direction, int size)
    {
        if (direction == 'V')
        {
            for (int i = 0; i < size; i++)
            {
                board[startRow + i, startCol] = 'S';
            }
        }
    }

    static void PlaceShipsRandomly(char[,] board)
    {
        Random rand = new Random();
        int[] shipSizes = { 5, 4, 3, 3, 2 };

        foreach (int size in shipSizes)
        {
            bool placed = false;
            while (!placed)
            {
                int startRow = rand.Next(0, 10);
                int startCol = rand.Next(0, 10);

                if (CanPlaceShip(board, startRow, startCol, 'V', size))
                {
                    PlaceShip(board, startRow, startCol, 'V', size);
                    placed = true;
                }
            }
        }
    }

    static bool ParseCoordinates(string input, out int row, out int col)
    {
        row = -1;
        col = -1;

        string[] parts = input.Split(' ');
        if (parts.Length == 2 && int.TryParse(parts[0], out row) && int.TryParse(parts[1], out col))
        {
            if (row >= 0 && row < 10 && col >= 0 && col < 10)
            {
                return true;
            }
        }

        return false;
    }

    static bool CheckIfShipsRemain(char[,] board)
    {
        for (int i = 0; i < board.GetLength(0); i++)
        {
            for (int j = 0; j < board.GetLength(1); j++)
            {
                if (board[i, j] == 'S')
                {
                    return true;
                }
            }
        }

        return false;
    }
}