using System;

public class ConnectFour
{
    private char[,] board;
    private char currentPlayer;
    private int currentRow;
    private int currentCol;

    public ConnectFour()
    {
        board = new char[6, 7];
        currentPlayer = 'X';
        currentRow = -1;
        currentCol = -1;
    }

    public void Play()
    {
        bool gameOver = false;

        while (!gameOver)
        {
            DrawBoard();
            Console.WriteLine($"Player {currentPlayer}'s turn.");
            GetPlayerMove();

            if (CheckWinner())
            {
                DrawBoard();
                Console.WriteLine($"Player {currentPlayer} wins!");
                gameOver = true;
            }
            else if (IsBoardFull())
            {
                DrawBoard();
                Console.WriteLine("It's a draw!");
                gameOver = true;
            }
            else
            {
                SwitchPlayer();
            }
        }
    }

    private void DrawBoard()
    {
        Console.Clear();

        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                Console.Write(board[row, col] == '\0' ? "| " : $"|{board[row, col]}");
            }
            Console.WriteLine("|");
        }
        Console.WriteLine(" 1 2 3 4 5 6 7");
    }

    private void GetPlayerMove()
    {
        bool validMove = false;

        while (!validMove)
        {
            Console.Write("Enter the column number (1-7): ");
            string input = Console.ReadLine();

            if (int.TryParse(input, out int col) && col >= 1 && col <= 7)
            {
                int colIndex = col - 1;

                if (IsColumnFull(colIndex))
                {
                    Console.WriteLine("Column is full. Try again.");
                }
                else
                {
                    validMove = true;
                    PlaceMove(colIndex);
                }
            }
            else
            {
                Console.WriteLine("Invalid input. Try again.");
            }
        }
    }

    private void PlaceMove(int col)
    {
        for (int row = 5; row >= 0; row--)
        {
            if (board[row, col] == '\0')
            {
                board[row, col] = currentPlayer;
                currentRow = row;
                currentCol = col;
                break;
            }
        }
    }

    private bool IsColumnFull(int col)
    {
        return board[0, col] != '\0';
    }

    private bool CheckWinner()
    {
        return CheckHorizontal() || CheckVertical() || CheckDiagonal();
    }

    private bool CheckHorizontal()
    {
        int count = 0;

        for (int col = 0; col < 7; col++)
        {
            if (board[currentRow, col] == currentPlayer)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }

        return false;
    }

    private bool CheckVertical()
    {
        int count = 0;

        for (int row = 0; row < 6; row++)
        {
            if (board[row, currentCol] == currentPlayer)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
        }

        return false;
    }

    private bool CheckDiagonal()
    {
        // Check for diagonals that go from bottom-left to top-right
        int row = currentRow;
        int col = currentCol;
        while (row > 0 && col > 0)
        {
            row--;
            col--;
        }

        int count = 0;
        while (row < 6 && col < 7)
        {
            if (board[row, col] == currentPlayer)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
            row++;
            col++;
        }

        // Check for diagonals that go from top-left to bottom-right
        row = currentRow;
        col = currentCol;
        while (row < 5 && col > 0)
        {
            row++;
            col--;
        }

        count = 0;
        while (row >= 0 && col < 7)
        {
            if (board[row, col] == currentPlayer)
            {
                count++;
                if (count == 4)
                {
                    return true;
                }
            }
            else
            {
                count = 0;
            }
            row--;
            col++;
        }

        return false;
    }

    private bool IsBoardFull()
    {
        for (int row = 0; row < 6; row++)
        {
            for (int col = 0; col < 7; col++)
            {
                if (board[row, col] == '\0')
                {
                    return false;
                }
            }
        }

        return true;
    }

    private void SwitchPlayer()
    {
        currentPlayer = currentPlayer == 'X' ? 'O' : 'X';
    }
}

public class Program
{
    public static void Main()
    {
        ConnectFour game = new ConnectFour();
        game.Play();
    }
}