using System;

namespace Assets
{
    public class Program1
    {
        static Game g;

        
        static void Main(string[] args)
        {
            g = new Game();
            StartGame();
        }
        
        static void StartGame()
        {
            g.SetBoard();
            DisplayBoard();
        }

        static void DisplayBoard()
        {
            // Create the visual board
            char[][] board = new char[8][];
            for (int i = 0; i < 8; i++)
            {
                board[i] = new char[8];

                for (int j = 0; j < 8; j++)
                {
                    board[i][j] = 'x';
                }
            }

            foreach (Piece p in g.whitePieces)
            {
                int row = p.GetPosition() / 8,
                    pos = p.GetPosition() % 8;

                char name = GetPieceName(p);
                board[row][pos] = name;
            }

            for (int i = 0; i < 8; i++)
            {
                for (int j = 0; j < 8; j++)
                {
                    Console.Write($"{board[i][j]} ");
                }
                Console.WriteLine();
            }
        }

        static char GetPieceName(Piece p)
        {
            char name;

            switch (p.GetKind())
            {
                case 0:
                    name = 'p';
                    break;
                case 1:
                    name = 'r';
                    break;
                case 2:
                    name = 'n';
                    break;
                case 3:
                    name = 'b';
                    break;
                case 4:
                    name = 'q';
                    break;
                case 5:
                    name = 'k';
                    break;
                default:
                    name = 'x';
                    break;
            }
            
            return name;
        }
    }
}